# Requirements: Datetime and Calendar

- Status: Draft
- Author: @jaymirecki
- Issue: https://github.com/outoforbitdev/app-galaxy-map/issues/430
- Last updated: 2026-10-29

<!--
A requirements doc says WHAT must be true and WHY, never HOW.
Keep solutions out. If you catch yourself naming a table, library, or algorithm, move it to the design doc.
Delete these comments and any section that does not apply.
-->

## 1. Problem

The current implementation of `Date`, `DateTime`, and `Calendar` hardcodes a relationship between days and minutes, which prevents representing a non-24 hour day. Users cannot select a calendar that has a different length of day. The current representation also does not support precision. Precision is de facto implemented by selecting the middle day of a 368 day year.

<!-- Two to five sentences. What is wrong or missing today, and who feels it? -->

## 2. Non-goals

<!-- Things a reader might reasonably expect that this work will NOT do. -->

- We do not need to represent months or weeks.
- We do not need to represent sub-second precision.
- We do not need leap rules or other irregular years. A year has a fixed length within a calendar.

## 3. Users and scenarios

<!-- Who uses this, and what are they trying to do? One line each is enough. -->

| User | Scenario |
| --- | --- |
| Data author | Enters dates for entities and relationships at whatever precision the source material gives. |
| Map viewer | Chooses a calendar and views the map as of a date shown in that calendar. |
| Developer | Writes as-of and range queries over dated entities and relationships. |

## 4. Requirements

<!--
Give each requirement an ID so the design doc and tickets can cite it.
Use "must" for required, "should" for desirable. Each requirement should be testable.
-->

### Functional

| ID | Requirement | Priority |
| --- | --- | --- |
| F1 | A single type must represent dates and times on every dated entity and relationship. | P0 |
| F2 | An instant must be interpretable by a calendar, including its year and day of year. | P0 |
| F3 | A calendar must be able to define its own epoch, length of day, and length of year. | P0 |
| F4 | An instant must be able to be imprecise at a precision of year, day, hour, minute, or second. | P0 |
| F5 | Instants, including imprecise ones, must be comparable and orderable. | P0 |
| F6 | An instant must convert to calendar components and back, and pass through the API, with no change in value. | P0 |

### Non-functional

<!-- Scale, performance, security, accessibility, compatibility, operability. Give numbers where you can. -->

| ID | Requirement | Priority |
| --- | --- | --- |
| N1 | The system must represent a total span of 600,000 earth years (365 days, 24 hours in a day, etc.). | P0 |
| N2 | The system must represent time at a resolution of one minute. | P0 |
| N3 | The system should represent 13,000,000,000 earth years at a resolution of one minute. | P1 |
| N4 | The system should represent time at a resolution of one second across the span in N1. | P1 |
| N5 | Point-in-time and range queries over dated entities and relationships must be efficient. | P0 |

## 5. Constraints and assumptions

<!-- Fixed decisions (a required database, a deadline) and things you are assuming but have not confirmed. -->

- The design must not block adding months and weeks later.
- The 365 day, 24 hour year in N1 is a sizing basis only, not a default calendar.

## 6. Open questions

<!-- Number them so reviewers can answer by ID. Say who can answer each one. -->

- **Q1.** What query time is acceptable for N5 (for example, a map render for one date)? Deferred: this is a known gap, so N5 has no measurable target yet. The design should not treat N5 as verified. (owner: @jaymirecki)
