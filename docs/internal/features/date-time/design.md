# Design: Datetime and Calendar

- Status: Draft
- Author: @jaymirecki
- Issue: https://github.com/outoforbitdev/app-galaxy-map/issues/430
- Requirements: [requirements.md](./requirements.md)
- Last updated: 2026-11-04

<!--
A design doc says HOW the requirements will be met and why this approach beats the alternatives.
Aim for 5000 words or less, not counting tables, code sketches, and diagrams.
State a recommendation for every choice. Do not just survey options.
Delete these comments and any section that does not apply.
-->

## 1. Summary

<!-- One paragraph: the problem, the proposal, and the main trade-off. A reader should be able to stop here. -->

We need a date and calendar system that does not hardcode relative measurements of time (e.g. a day as a certain number of minutes) and supports imprecise dates (e.g. a date that specifies the year, but not the day). To address this we will consolidate down to a single `DateTime` class that represents time as a `long` integer representing seconds and precision as an enum. These values will be stored as two separate database columns (the instant and the precision). The main tradeoff is the representation of an instant as non-negative 64-bit integer means that an instant cannot be sent to Javascript as numbers, so only the backend can handle them.

## 2. Context

<!--
What exists today? Verify against the code, do not copy from memory.
Link to the requirements this design satisfies by ID (F1, N2, ...).
-->

We currently have both a `Date` class (which uses integers to represent days) and a `DateTime` class (which uses longs to represent minutes) [F1]. Converting between these hardcodes a certain length of day, which impedes the ability of `Calendar` to fully define the measurement of time [F3]. The current `Date` and `DateTime` classes also lack a representation of precision, so there is no way to know if an instant is fully precise down to the second, or if it is only precise at the timescale of a year or a day [F4].

## 3. Proposal

<!--
The design itself. Use subsections per component or concept.
Include a diagram if structure or flow matters. Include a short code or schema sketch if it removes ambiguity.
-->

### 3.1 `DateTime` [F1, F4, F5, N1, N2, N3, N4]

`DateTime` will be the only way to represent an instant in time. It will use `long Seconds` to represent an instant. `Seconds` will be strictly non-negative. This definition allows representation of a timerange of over 292 billion Earth-years.

`DateTime` will use an enum `DateTimePrecision Precision` to denote how precise the instant is. `DateTime` can be precise to the `Year`, `Day`, `Hour`, `Minute`, or `Second`. It will be up to queries to decide how to use this information. Precision will default to `Second`, but constructors can accept a precision argument.

`DateTime` will be saved in Postgres as two columns: `DateTime` and `DateTimePrecision`.

### 3.2 `Calendar` [F2, F3, F6]

`Calendar` will continue to be the representation of a system of time measurement. It is defined by:

- `Instance`: The instance the calendar belongs to.
- `Id`: The id for the calendar.
- `Name`: The name of the calendar.
- `Epoch`: A full precision `DateTime` that marks the start of the first year of the calendar's epoch.
- `SecondsPerMinute`: The number of seconds per minute in the calendar.
- `MinutesPerHour`: The number of minutes per hour in the calendar. (We do not support hours equivalent to partial minutes.)
- `HoursPerDay`: The number of hours per day in the calendar. (We do not support days equivalent to partial hours.)
- `DaysPerYear`: The number of days per year in the calendar. (We do not support years equivalent to partial days.)
- `ZeroYearAfterEpoch`: Whether the first year after the epoch is year 0 or year 1. Years before epochs always start with 1 immediately preceding the epoch.
- `AfterEpochSuffix`: The suffix for years after the epoch.
- `BeforeEpochSuffix`: The suffix for years before the epoch.

A `Calendar`, given a `DateTime`, should be able to return the year, day, hour, minute, and second of the instant, or a date, time, or datetime string representing the instant. A `Calendar`, given a year, day, hour, minute, and second (or a subset thereof) should be to construct a `DateTime`. If no second is provided, `Calendar` should default to 0 seconds and set a precision of `Minute`. If no minutes are provided, `Calendar` should default to 0 minutes and set a precision of `Hour`. If no hour is provided, `Calendar` should default to `HoursPerDay / 2` hours and set a precision of `Day`. If no day is provided, `Calendar` should default to `DaysPerYear / 2` and set a precision of `Year`. A year must be provided. If only a year is provided, the stored instant defaults to midday on the middle day of the year. Precision is strictly increasing. If a second is provided, so must a minute. If a minute is provided, so must an hour. If an hour is provided, so must a day. 

We will store calendars in a Calendar table in the database.

Each `Instance` will have a `DefaultCalendar`. It is a suggested calendar for constructing and interpreting dates. It is not applied automatically: the app decides which calendar to use, and can fall back to the `DefaultCalendar` when no other calendar is provided or preferable.

### 3.3 Frontend [F6]

The frontend does not need a representation of dates or calendars. It would be a maintenance headache to implement `Calendar` on the frontend and the backend. The only need the frontend has for dates is the date selection dropdown and the start and end dates in infoboxes. These needs can be met by storing `DateString` on the objects to be rendered by the infoboxes, and using IDs (and likely `DateString` again) to represent the dates available in the dropdown. This aligns with https://github.com/outoforbitdev/app-galaxy-map/issues/258, which aims to migrate the actual date views to the database instead of hardcoding in the app.

If the frontend allows the user to select a preferred calendar, the API should be expanded to accept a calendar param.

### Queries [F5, N5]

Database queries can order and filter `DateTime` by the stored `DateTime`. A query that attempts to find all events in a range requires a `Calendar` to help define the range. The app chooses the calendar, and can fall back to the `Instance`'s `DefaultCalendar` when none is provided or preferable. For example, "all events in a year" will depend on a `Calendar` to define the start and end instants of the year. When finding all events in a range, queries can use `DateTimePrecision` to exclude events that are not precisely defined to be in the range. For example, "all events in a day" can require a precision of at least `Day`, which would exclude events that have an instant within the range but are precise only to the `Year`. The same rule applies to arbitrary ranges: the query should require a precision at least as fine as its range endpoints. For example, "all events between day 100 and day 200" should require a precision of at least `Day`, because an event precise only to the `Year` cannot be said to fall within that range.

## 4. Alternatives considered

<!-- For each real choice: the options, the recommendation, and why. Keep rejected options to a few lines. -->

| Choice | Options | Recommendation | Why |
| --- | --- | --- | --- |
| Type for backend `Seconds` | A: `long` / B: `ulong` / C: `int` | A | Postgres does not support unsigned integers, so `ulong` does not actually improve the range of time that can be represented, while it does introduce an inconsistency between the backend and the database (the same would be true of `uint`). `int` would potentially provide storage savings, but at too small a scale to be worth trading a shorter timespan. Using `long` to represent seconds allows us to represent ~292 billion Earth-years. |
| How to define units of time | A: Each unit of measure defined by the unit below it (e.g. `DaysPerYear`) / B: Each unit of measure defined by the base unit (e.g. `SecondsPerYear`) | A | While approach B allows for some interesting and more realistic scenarios (e.g. an Earth-year is 365.25 days), it leads to hard questions about how to represent a date. If a year is 365.25 days, what year is it when we hit the 366th day? These complexities are probably better represented by expanding the `Calendar` definition with, for example, leap years. This also makes the representation easier to understand. It's more obvious how long a year is when it's defined by 365 days rather than 31536000 seconds. |
| Non-negative instants | A: Represent instants as strictly non-negative / B: Represent instants as positive or negative | A | While using negative integers would double the representable time range, that increase is unnecessary to meet the requirements. It could also cause confusion because it would be tempting to view 0 as the global epoch instant (due to both positive and negative instants), when different calendars may set different epoch instants. It would also require special handling for negative arithmetic.

## 5. Edge cases

<!-- Hard scenarios and the decision for each. Say whether it is handled generically or special-cased. -->

| Edge case | Decision | Handling (generic or special) | Rationale |
| --- | --- | --- | --- |
| Precision across calendars. An instant that is only precise to the year is placed at midday in the middle of the year _for that `Calendar`_. If another `Calendar` with a different definition of year is used to find all events in a year, it may or may not include the imprecise instants depending on the exact boundaries. | This is an acceptable failure. | Generic | Precision will still work for `Calendar`s with epochs offset by a number of years. This would suffice, for example, for a simplistic implementation of the Hebrew and Islamic calendars. |

## 6. Validation and failure modes

<!-- What invalid states are possible, which are prevented (and where), and what happens on failure. -->
1. Negative `Seconds`: Add validation on `DateTime` constructors and a database `CHECK` on each `DateTime` column.

2. Null datetimes: This should continue to be allowed, to represent ongoing or unbounded.

3. Calendar creation: Every period should be `>=1`, Epoch should be full precision, and the total seconds per year (`SecondsPerMinute * MinutesPerHour * HoursPerDay * DaysPerYear`) must fit in a `long`. Validate in the constructors and with database `CHECK` conditions.

4. Calendar date construction: Reject input that would be beyond the limit of a period (e.g. an hour that is greater than `HoursPerDay`). Reject input that would create an invalid `DateTime` (or catch and re-throw/handle).

## 7. Testing

<!-- How will we know it works? Unit, integration, and any comparison against current behavior. -->

Unit tests should be added for both `DateTime` and `Calendar`. Use a series of simplified realworld calendar representations, such as the Julian/Gregorian without leap years, to validate.

## 8. Rollout and migration

<!-- Steps from today's state to this design, how to verify each, and how to roll back. -->

This should be included as part of the complete data model migration.

## 9. Impact

<!-- API, client, data, operations, and docs. Who must change something because of this? -->

- **API:** Instants are not sent to the frontend as numbers. Endpoints return `DateString` values for infoboxes and the date dropdown, and may accept an optional calendar param if users can select a preferred calendar.
- **Client:** The frontend gains no date or calendar logic. It renders the `DateString` values it receives and uses IDs to represent the dates in the dropdown.
- **Data:** The existing `Date` and `DateTime` columns are replaced by pairs of `DateTime` and `DateTimePrecision` columns, with `CHECK` constraints for non-negative values. A new Calendar table is added, and `Instance` gains a `DefaultCalendar` column. Existing values must be converted as part of the data model migration.
- **Operations:** No new infrastructure. The change ships with the complete data model migration (see section 8).
- **Docs:** Developer docs should describe `DateTime`, `DateTimePrecision`, and `Calendar`, including how queries should choose a precision.

## 10. Follow-up work

<!-- Proposed tickets, in order. -->

1. https://github.com/outoforbitdev/app-galaxy-map/issues/258: store event dates in the database, instead of hardcoded in the frontend.
2. Add support for months, weeks, holidays, etc. in `Calendar`.

## 11. Open questions

<!-- Number them. Say who can answer each one. -->

- **Q1.** <question> (owner: <name>)
