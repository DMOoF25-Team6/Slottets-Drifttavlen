# Quality Criteria: User Flow Diagram (UFD)
A User Flow Diagram illustrates the user's navigation journey across screens, decision points, and end states for a specific use case.
This document defines quality criteria and a template for documenting UFDs in markdown format using Mermaid flowchart syntax.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-USERFLOW                       |
| crossReference    |                                   |

## Version and Change Log
| Date       | Version | Description                                          | Author  |
|------------|---------|------------------------------------------------------|---------|
| 2026-05-11 | 0001    | Initial creation of the document                     | Team 6  |
| 2026-05-11 | 0002    | Tightened Mermaid syntax, completeness, and size     | Team 6  |

## Quality Criteria for User Flow Diagrams
When evaluating a UFD, consider the following quality criteria:
1. **Clarity and Simplicity**: The flow should be easy to follow, with clear node labels, decision points, and arrows. Avoid unnecessary complexity.
2. **Completeness**: Every step in the use case's main flow must appear as a node. Authorization failures, validation errors, and user-facing error states must be represented. Internal/technical extensions (e.g., 'database connection lost') need NOT appear since they belong in SSD.
3. **Correctness**: The flow must accurately reflect the use case scope and the user's actual navigation experience. It must not introduce navigation paths not covered by the use case.
4. **Consistency**: Node conventions must be applied consistently: `([rounded])` for start/end, `[rectangle]` for screens/actions, `{diamond}` for decisions.
5. **User Perspective**: The diagram MUST describe what the user sees, clicks, and decides — NOT what the system does internally. System internals belong in SSD/SD artifacts.
6. **Traceability**: Each node must be traceable to a step, decision, or outcome in the use case.
7. **Mermaid Syntax**: The diagram MUST use Mermaid `flowchart TD` or `flowchart LR` syntax. Use of `sequenceDiagram` or `flowchart` without a direction modifier is incorrect.
8. **No Implementation Details**: UFDs are navigation only. Do not include API endpoints, database operations, service names, or technical implementation details.
9. **Branching Logic**: Decision diamonds must have clearly labeled outgoing arrows showing the condition for each branch (e.g., `|Approved|`, `|Rejected|`).
10. **End States**: Every path must terminate in an explicit end state (success, failure, or cancellation). No dangling nodes.
11. **Readability**: Diagrams should contain no more than 25 nodes. Larger flows must be split into multiple diagrams within the same file, one per main user journey.
12. **Versioning and Change Log**: Every change must be logged with a version, date, and author.
13. **Language/Translation Compliance**: If the product owner language is not English, create a separate translated file with the correct language code suffix.

## Common Anti-Patterns (Do NOT do this)
- ❌ Including technical method names like `SaveAsync()`, `Repository.Add()`, or `POST /api/policy` as node labels.
- ❌ Showing system-internal messages (e.g., "DbContext returns success") — those belong in SSD/SD.
- ❌ Creating an unauthenticated entry point when the use case requires authorization.
- ❌ Omitting error/denied paths so the diagram looks "cleaner" — these paths are required for completeness.
- ❌ Using `flowchart` without `TD` or `LR` direction modifier — produces invalid Mermaid.
- ❌ Mixing user actions and system processes in the same node (e.g., "User saves and system validates" should be two nodes).

## Relationship to Other Artifacts
- **UC (Use Case)**: UFD visualizes the navigation derived from the use case's main flow and extensions.
- **Wireframe**: UFD shows the journey BETWEEN screens; Wireframe shows the layout WITHIN a screen.
- **SSD (System Sequence Diagram)**: SSD shows system-level message exchanges; UFD shows user-level navigation. They are complementary, not redundant.
- **SD (Sequence Diagram)**: SD shows object collaborations inside the system; UFD shows the user's path outside the system.
- **OC (Operation Contracts)**: OC defines preconditions/postconditions per system operation; UFD does not enforce these — they are validated at the SSD/SD/OC level.
- **DCD (Domain Class Diagram)**: DCD describes the static structure of domain classes; UFD describes the user's runtime journey through the UI.

## Authoring Patterns and Templates
For filename conventions, templates, and authoring examples, see `.github/instructions/userflow.instructions.md`.

## Validation
- Review UFDs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.
- Verify the diagram covers the main flow and key extensions from the linked use case.
- Verify that the linked use case file (`uc-<id>.usecase.md`) exists and matches the UFD's crossReference. A UFD without a matching use case is invalid.
- Verify Mermaid syntax renders correctly — test at https://mermaid.live/ if in doubt.
- Verify total node count is ≤ 25; if not, split into multiple diagrams within the file.

## Maintenance
- Update the version and change log for major changes.
- Regularly review UFDs for accuracy and relevance.

## Language
- Professional
- English
- If product owner domain language is different, create a translated file with a language code suffix (e.g., `uc-xxx.userflow.da.md` for Danish).
- Both files must be kept in the use case subfolder.
