---
description: 'Entity Relationship Diagram (ERD) quality requirements and template for project documentation.'
applyTo: 'docs/erd.*.md' and 'docs/use-cases/**/uc-*.erd.*.md'
---

# Entity Relationship Diagram (ERD) Instructions
This instruction file provides a template and quality criteria for documenting Entity Relationship Diagrams (ERD) in markdown format.
Use this as a starting point for any project requiring an ERD. Replace all placeholders in the diagram with project-specific content.

## General Instructions
- Use this template for all ERD documentation in markdown format.
- Replace all bracketed placeholders in the Mermaid diagram with project-specific information.
- Store ERD files in the centralized repository.
- Review and approve ERDs with relevant stakeholders before acceptance.
- Keep solution ERDs up to date with changes in the overall system design.

## Best Practices
- Clearly define all entities, attributes, and relationships.
- Use clear, concise, and domain-oriented language.
- Document all assumptions and dependencies.
- Ensure visuals and layout are consistent and easy to understand.

## Code Standards
- Each ERD must have a unique version identifier and a documented change log.
- Use the provided Mermaid diagram layout for consistency.

### File Naming
- Name files in lowercase
  - following the pattern in ERD model for use case: `erd.md` (e.g., `erd.md`).
    - for use case ERD models, include the use case identifier in the file name as a prefix.
      - save files for use case ERD models in a subfolder named after the use case (e.g., `docs/use-cases/uc-001/uc-001.erd.md`).
    - for solution ERD models, do not include a use case identifier in the file name.
      - save files for solution ERD models in the main `docs` folder (e.g., `docs/erd.md`).
- Increment version numbers for significant changes.
- Include the todays date and author in the version log.

## Common Patterns
### Good Example
```markdown
# Entity Relationship Diagram (ERD) for [Insert Project Name]
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | ERD                               |
| crossReference    | DCD-xxx                           |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [yyyy-mm-dd] | Initial                  | Team 6     |
```

```mermaid
%% Entity Relationship Diagram Template: Replace all [Insert ...] placeholders with project-specific content.
erDiagram
    [Entity1] {
        string [Attribute1]
        int [Attribute2]
        string [Attribute3]
    }
    [Entity2] {
        string [Attribute1]
        int [Attribute2]
    }
    [Entity3] {
        string [Attribute1]
        string [Attribute2]
    }
    [Entity1] ||--o{ [Entity2] : [Relationship1]
    [Entity2] ||--|{ [Entity3] : [Relationship2]
    [Entity1] }o--|| [Entity3] : [Relationship3]
```

### Bad Example
```
## Entity Relationship Diagram Components
| Entity1 | Entity2 | Entity3 |
|---------|---------|---------|
| [Attribute1], [Attribute2] | [Attribute1], [Attribute2] | [Attribute1], [Attribute2] |
| [Relationship1] | [Relationship2] | [Relationship3] |
```

## Validation
- Review ERDs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review ERDs for accuracy and relevance.

## Language 
- Professional
- English

## Class object
if class object chenges name form artifacts before then make / update glosery `/docs/glosery.md` with class name in artifacts we transform from and class name in this artifacts.
