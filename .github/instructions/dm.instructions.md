---
Description: 'Domain Model (DM) template for project documentation.'
---

# DM Instructions (Summary)
- Use the provided DM markdown template.
- Replace all placeholders with project-specific content.
- Store DM files in `docs/use-cases/uc-<Insert Use Case Identifier>*/` as `uc-<Insert Use Case Identifier>.dm.<Insert Version>.md` or in `docs/` as `dm.md` for solution domain models.
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
