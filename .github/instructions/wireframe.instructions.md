---
description: 'Wireframe template for project documentation.'
applyTo: 'docs/use-cases/**/uc*.wireframe.md'
references:
  - docs/quality-criteria/artifact/qc-wireframe.md
---

# Wireframe Instructions (Summary)
- Use the provided wireframe markdown template and examples.
- Replace all placeholders with project-specific content.
- Store wireframe files in `docs/use-cases/uc-<Insert Use Case Identifier>/` as `uc-<Insert Use Case Identifier>.wireframe.md`.
- Increment version numbers for significant changes; keep only the latest version in main, archive older versions.
- Include metadata, version log (with date, author), ASCII layout in markdown code block, and notes section.
- Create files in English; if product owner domain language differs, create a separate file with language code suffix (e.g., uc-xxx.wireframe.da.md).
- Update glossary files for new terms.
- Validate wireframes for completeness, clarity, and template compliance.
- Wireframes are UI layout only — do not include implementation details.

## Template (Minimal):

## Metadata
| Key            | Value                        |
|----------------|------------------------------|
| Id             | [Use case identifier].Wireframe |
| crossReference | [Use case identifier] [Use case identifier].DM [Use case identifier].UC |

## Version Log
| Version | Date       | Description | Author |
|---------|------------|-------------|--------|
| 0001    | [date]     | Initial     | [name] |

## Wireframe

[Brief description of the UI section and its position on the page]

Overview layout:
+-------------------------------------------------------+
|  [Page Title]                        [Shift type]     |
+-------------------------------------------------------+
|  [Main content area]                                  |
+-------------------+-------------------+---------------+
|  [Section A]      |  [Section B]      |  [Section C]  |
|  [Label]: [     ] |  [Label]: [     ] |  [text area]  |
+-------------------+-------------------+---------------+

Detail layout:
+------------------+-----------+
|  [Column A]      |  [Column B]|
+------------------+-----------+
|  [Value 1]       | [        ] |
|  [Value 2]       | [        ] |
+------------------+-----------+

- Use [ ] for editable input fields.
- Use fixed text for read-only labels.

## Notes
- [Design decision or assumption]
- [Reference to physical layout if applicable]
- [Field behaviour e.g. resets per shift, read-only]

## Example (Resident Card on Dashboard):

## Metadata
| Key            | Value                        |
|----------------|------------------------------|
| Id             | UC-001.Wireframe             |
| crossReference | UC-001 UC-001.DM UC-001.UC   |

## Version Log
| Version | Date       | Description | Author |
|---------|------------|-------------|--------|
| 0001    | 2026-04-13 | Initial     | Team 6 |

## Wireframe

Dashboard displays 12 resident cards in a grid layout.
Each card shows resident initials, traffic light status, and notes.

Overview layout:
+------------------------------------------------------------------+
|  SLOTTETS DRIFTTAVLEN                          [Shift type]      |
+------------------------------------------------------------------+
|  +----------+  +----------+  +----------+  +----------+         |
|  | [Init.]  |  | [Init.]  |  | [Init.]  |  | [Init.]  |         |
|  | [status] |  | [status] |  | [status] |  | [status] |         |
|  +----------+  +----------+  +----------+  +----------+         |
+------------------------------------------------------------------+

Detail layout — Resident Card:
+-----------------------------+
|  Initials:  AB              |
|  Status:    [ G / Y / R ]   |
|  Notes:     [             ] |
+-----------------------------+

- Initials are read-only (GDPR — no full names).
- Traffic light status is selectable per shift.
- Notes field is editable.

## Notes
- Layout follows the physical Excel overlap sheet used at Slottet.
- No full resident names displayed (GDPR compliance).
