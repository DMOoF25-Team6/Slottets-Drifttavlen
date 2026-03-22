---
description: 'Operation Constract (OC) quality requirements and template for project documentation.'
applyTo: 'docs/use-cases/**/uc*.oc.*.md'
---

# OC Instructions (Summary)
- Use the provided OC markdown template.
- Replace all placeholders with project-specific content.
- Store OC files in `docs/use-cases/uc-<Insert Use Case Identifier>*/` as `uc-<Insert Use Case Identifier>.oc.<Insert Version>.md`.
- Increment version numbers for significant changes; keep only the latest version in main, archive older versions.
- Include metadata, version log (with date, author), and use Mermaid diagram.
- Create files in English.

## OC Template (Minimal):
```markdown
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | [Use case identifier].OC          |
| crossReference    | [Insert SSD Reference]            |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [Insert Today Date yyyy-mm-dd] | Initial                  | Project owner | 
```

```markdown
## Operation Constract
<!-- Operation Constract Template: Replace all [Insert ...] placeholders with project-specific content. -->

### [Insert Interaction Name]
- **Preconditions**: [Insert any preconditions that must be met before this interaction can  occur.]
- **Postconditions**: [Insert any postconditions that will be true after this interaction occurs.]
```

## OC example (Medicine Status for a Resident):
```markdown
# Operation Contract: Medicine Status for a Resident
## Metadata
| Key            | Value           |
|----------------|-----------------|
| Id             | UC-003.OC       |
| crossReference | UC-003.SSD      |

## Version Log
| Version | Date       | Description | Author |
|---------|------------|-------------|--------|
| 0001    | [date]     | Initial     | Team 6 |

## Operation Contract
### Request Medicine Status
- **Preconditions**: Caregiver is logged into the system.
- **Postconditions**: System displays medicine administration status for all residents.

### Request Painkiller Status
- **Preconditions**: Caregiver is logged into the system.
- **Postconditions**: System displays painkiller administration status and next allowed timespan for a resident.
```
