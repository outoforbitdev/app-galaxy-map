# Requirements: Entity Data Model

- Status: Draft
- Author: @jaymirecki
- Issue: https://github.com/outoforbitdev/app-galaxy-map/issues/430
- Last updated: 2026-11-04

<!--
A requirements doc says WHAT must be true and WHY, never HOW.
Keep solutions out. If you catch yourself naming a table, library, or algorithm, move it to the design doc.
Delete these comments and any section that does not apply.
-->

## 1. Problem

<!-- Two to five sentences. What is wrong or missing today, and who feels it? -->

The current data model is fragmented. Some common properties (like parent relationships) are defined multiple times, and relationships between different entity types require new definitions and storage. This makes it more difficult for contributors by requiring additional boilerplate and relationship storage when adding new entity types, and a more complex definition of seed data.

## 2. Goals

<!-- Outcomes, not features. Each goal should be checkable. -->

- Properties shared by multiple entity types should be defined once.
- Many-to-many or time-constrained relationships between different entity types should be represented the same way.
- Adding a new entity type or relationship type should not require adding new relationship storage.

## 3. Non-goals

<!-- Things a reader might reasonably expect that this work will NOT do. -->

- Complex cases, such as binary star systems or multi-government planet control.
- Changes to the user-facing UI or API.
- Migrating existing data. Seed data will be recreated externally in the new format.

## 4. Users and scenarios

<!-- Who uses this, and what are they trying to do? One line each is enough. -->

| User | Scenario |
| --- | --- |
| Contributor | Adds a new entity type, such as a moon or an asteroid, and relates it to existing entities without new relationship storage. |
| Contributor | Records that a government governs a planet from one date to another, then that a different government governs it afterward. |

## 5. Requirements

<!--
Give each requirement an ID so the design doc and tickets can cite it.
Use P0 for required and P1 for desirable, and word the requirement with "must" or "should" to match. Each requirement should be testable.
-->

### Functional

| ID | Requirement | Priority |
| --- | --- | --- |
| F1 | The model must be able to represent each of these entity types: star systems, stars, planets, moons, asteroids, spacelanes, spacelane segments, and governments. | P0 |
| F2 | Relationships must support an optional start and end date, using the same date and time representation as entities. | P0 |
| F3 | Every entity must have a name and an optional start and end date. | P0 |
| F4 | Every relationship must have a type that describes the relationship, such as governs or orbits. | P0 |
| F5 | The system should reject a relationship whose type is not valid for the types of the two entities it relates, taking the direction of the relationship into account. | P1 |
| F6 | The system should reject a time-constrained relationship whose end is before its start. | P1 |
| F7 | A rejected relationship should identify which rule it violated. | P1 |
| F8 | Relationships of an entity must be retrievable from either side. | P1 |
| F9 | The system should reject a relationship whose start or end date falls outside the date range of either entity it relates. | P1 |
| F10 | The system should reject an entity with an end date before the start date. | P1 |
| F11 | Entities and relationships should support an optional start reason and an optional end reason. A start or end date does not require a reason. | P1 |


### Non-functional

<!-- Scale, performance, security, accessibility, compatibility, operability. Give numbers where you can. -->

| ID | Requirement | Priority |
| --- | --- | --- |
| N1 | Existing API responses must be unchanged by the new model. | P0 |
