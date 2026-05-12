---
description: 'User Flow Diagram template for project documentation.'
applyTo: 'docs/use-cases/**/uc*.userflow.md'
references:
  - docs/quality-criteria/artifact/qc-userflow.md
---

# User Flow Diagram Instructions (Summary)
- Use the provided User Flow Diagram (UFD) markdown template and examples.
- A UFD shows the user's navigation journey across screens, decision points, and end states — from the user's perspective, NOT system internals.
- Use Mermaid `flowchart TD` (top-down) or `flowchart LR` (left-right) syntax — never omit the direction modifier, and never use `sequenceDiagram` (that is reserved for SSD/SD).
- Before generating a UFD, verify that the corresponding `uc-<id>.usecase.md` file exists. If not, abort and request the use case content rather than generating a fictional flow.
- Replace all placeholders with project-specific content.
- Store UFD files in `docs/use-cases/uc-<Insert Use Case Identifier>/` as `uc-<Insert Use Case Identifier>.userflow.md`.
- Increment version numbers for significant changes; keep only the latest version in main, archive older versions.
- Include metadata, version log (with date, author), Mermaid diagram in markdown code block, and notes section.
- Notes section MUST mention: dependencies on other use cases, assumptions about prior authentication/authorization, edge cases not visualized in the diagram, and references to other artifacts (e.g., wireframe, SSD).
- Create files in English; if product owner domain language differs, create a separate file with language code suffix (e.g., uc-xxx.userflow.da.md).
- If the UFD introduces domain terms not already in `docs/glossary.md`, add them to that file. For Danish UFDs, also add the translation to `docs/glossary.da.md`.
- Validate UFDs for completeness, clarity, and template compliance.
- UFDs are user navigation only — do NOT include system sequence details (use SSD), object interactions (use SD), or UI layout (use wireframe).
- Keep diagrams under 25 nodes for readability. If the flow is more complex, split into multiple diagrams (one per main journey) within the same file.

## Template (Minimal)

The following shows the required structure. The Mermaid code block is shown using `~~~mermaid` fences instead of triple-backticks to keep this template renderable inside this instructions file. When creating the actual UFD file, use standard triple-backtick fences (` ```mermaid `).

## Metadata
| Key            | Value                              |
|----------------|------------------------------------|
| Id             | [Use case identifier].UserFlow     |
| crossReference | [Use case identifier]<br/>[Use case identifier].UC<br/>[Use case identifier].Wireframe |

## Version Log
| Version | Date       | Description | Author |
|---------|------------|-------------|--------|
| 0001    | [date]     | Initial     | [name] |

## User Flow Diagram

[Brief description of the user journey covered by this diagram]

~~~mermaid
flowchart TD
    Start([User starts]) --> Auth{Authorized?}
    Auth -->|No| Deny[Access denied]
    Auth -->|Yes| Page1[Main page]
    Page1 --> Decision{User choice?}
    Decision -->|Option A| ActionA[Perform action A]
    Decision -->|Option B| ActionB[Perform action B]
    ActionA --> Success([Success])
    ActionB --> Success
    Deny --> End([End])
    Success --> End
~~~

## Node Conventions
- `([Rounded])` — Start/End states (entry and exit points)
- `[Rectangle]` — Screen, page, or user action
- `{Diamond}` — Decision point (branch based on condition or user choice)
- Arrows labeled with `|condition|` show branching logic
- Use clear, user-facing labels (e.g., "Click Save", "Open Settings") — not technical terms

## Notes
[Add any clarifications about the flow, dependencies on other use cases, authentication assumptions, edge cases not visualized, and references to related artifacts]

## Example Reference
For a complete reference example, see `docs/use-cases/uc-010-ensure-data-security-and-gdpr-compliance/uc-010.userflow.md` once available.

