# Requirements: <title>

- Status: Draft | In review | Approved
- Author: <name>
- Issue: <link>
- Last updated: <date>

<!--
A requirements doc says WHAT must be true and WHY, never HOW.
Keep solutions out. If you catch yourself naming a table, library, or algorithm, move it to the design doc.
Delete these comments and any section that does not apply.
-->

## 1. Problem

<!-- Two to five sentences. What is wrong or missing today, and who feels it? -->

## 2. Goals

<!-- Outcomes, not features. Each goal should be checkable. -->

- <goal>

## 3. Non-goals

<!-- Things a reader might reasonably expect that this work will NOT do. -->

- <non-goal>

## 4. Users and scenarios

<!-- Who uses this, and what are they trying to do? One line each is enough. -->

| User | Scenario |
| --- | --- |
| <role> | <what they want to accomplish> |

## 5. Requirements

<!--
Give each requirement an ID so the design doc and tickets can cite it.
Use "must" for required, "should" for desirable. Each requirement should be testable.
-->

### Functional

| ID | Requirement | Priority |
| --- | --- | --- |
| F1 | The system must <behavior>. | Must |

### Non-functional

<!-- Scale, performance, security, accessibility, compatibility, operability. Give numbers where you can. -->

| ID | Requirement | Priority |
| --- | --- | --- |
| N1 | <measurable constraint> | Must |

## 6. Constraints and assumptions

<!-- Fixed decisions (a required database, a deadline) and things you are assuming but have not confirmed. -->

- <constraint or assumption>

## 7. Success criteria

<!-- How will we know this worked? These feed the Definition of Done on the issue. -->

- <criterion>

## 8. Open questions

<!-- Number them so reviewers can answer by ID. Say who can answer each one. -->

- **Q1.** <question> (owner: <name>)
