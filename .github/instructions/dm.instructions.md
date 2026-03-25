---
Description: 'Domain Model (DM) template for project documentation.'
applyTo: 'docs/dm.md' and 'docs/use-cases/**/uc-*.dm.md'
---

# DM Instructions (Summary)
- Use the provided DM markdown template.
- Replace all placeholders with project-specific content.
- Store DM files in `docs/use-cases/uc-<Insert Use Case Identifier>*/` as `uc-<Insert Use Case Identifier>.dm.md` or in `docs/` as `dm.md` for solution domain models.
- Increment version numbers for significant changes; keep only the latest version in main, archive older versions
- Include metadata, version log (with date, author), and use Mermaid diagram.
- Create files in English; if product owner domain language differs, create a separate file with language

## DM Template (Minimal):
```markdown
# Domain Model (DM) for [Insert Project Name]
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | <Use case identifier>.DM          |
| crossReference    | <Insert Business Case Identifier> |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | <today date in format yyyy-mm-dd> | Initial                  | project owner |
```
```mermaid
%% Domain Model Diagram Template: Replace all [Insert ...] placeholders with project-specific content.

classDiagram
    class [Entity1] {
        [Attribute1]
        [Attribute2]
        [Attribute3]
    }
    class [Entity2] {
        [AttributeA]
        [AttributeB]
    }
    [Entity1] "0..1" -- "*" [Entity2] : [RelationshipName]
```

optional sections for use case:
```markdown
## Assumptions and Dependencies
- [Assumption 1]
- [Assumption 2]
```

optional sections for product owner domain language:
```markdown
## Terms Translation

| Original Term           | [Language] Translation         |
|------------------------|---------------------------|
| [Original Term 1]      | [Translation 1]            |
| [Original Term 2]      | [Translation 2]            |
```

## DM example (Medicine Status for a Resident):
```markdown
# Domain Model (DM) for Slottets Drifttavlen
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | UC-003.DM                         |
| crossReference    | BC                                |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-03-20 | Initial                  | Team 6     |

## Diagram
```mermaid
%% Domain Model Diagram for UC-003 Medicine Status
classDiagram
    class Resident {
        Initials
    }
    class MedicineAdministration {
        Timestamp
        WasGiven
    }
    class PainkillerAdministration {
        PainkillerType
        WasGivenAtTimestamp
        NextAllowedTime
    }
    Resident "1" -- "*" MedicineAdministration : receives
    Resident "1" -- "*" PainkillerAdministration : receives
```

```markdown
## Assumptions and Dependencies
- MedicineAdministration records are filtered for the last 24 hours.
- PainkillerAdministration is a specialized record linked to MedicineAdministration.
- Dashboard displays status for all residents.

## Terms Translation
| Original Term           | Danish Translation         |
|------------------------|---------------------------|
| Resident               | Beboer                    |
| MedicineAdministration | Medicinadministration      |
| PainkillerAdministration| Smertestillendeadministration |
| Timestamp              | Tidsstempel                |
| WasGiven               | Givet                     |
| PainkillerType         | Smertestillende type       |
| NextAllowedTime        | Næste tilladte            |
```
