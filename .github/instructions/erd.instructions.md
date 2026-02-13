
---
description: 'Entity Relationship Diagram (ERD) quality requirements and template for project documentation.'
applyTo: '**/erd.*.md'
---

# Entity Relationship Diagram (ERD) Instructions

This instruction file provides a template and quality criteria for documenting Entity Relationship Diagrams (ERD) in markdown format.
Use this as a starting point for any project requiring an ERD. Replace all placeholders in the diagram with project-specific content.

## General Instructions

- Use this template for all ERD documentation in markdown format.
- Replace all bracketed placeholders in the Mermaid diagram with project-specific information.
- Store ERD files in the centralized repository.
- Review and approve ERDs with relevant stakeholders before acceptance.

## Best Practices

- Clearly define all entities, attributes, and relationships.
- Use clear, concise, and domain-oriented language.
- Document all assumptions and dependencies.
- Ensure visuals and layout are consistent and easy to understand.

## Code Standards

- Each ERD must have a unique version identifier and a documented change log.
- Use the provided Mermaid diagram layout for consistency.

### File Naming
- Name files in lowercase, using digits for version, following the pattern: `erd.xxxx.md` (e.g., `erd.0001.md`).
- Increment version numbers for significant changes.
- Include the date and author in the version log.
- we only keep the latest version in the main branch; archive older versions in a designated folder.

## Common Patterns
### Good Example
```markdown
# Entity Relationship Diagram (ERD) for [Insert Project Name]
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | ERD                               |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: 2026-01-20

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-01-20 | Initial                  | project owner |
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
