---
description: 'Domain Model (DM) quality requirements and template for project documentation.'
applyTo: '**/dm.*.md'
---

# Domain Model (DM) Instructions
This instruction file provides a template and quality criteria for documenting domain models (DM) in markdown format. 
Use this as a starting point for any project requiring a domain model. Replace all placeholders in the diagram with project-specific content.

## General Instructions
- Use this template for all DM documentation in markdown format.
- Replace all bracketed placeholders in the Mermaid diagram with project-specific information.
- Store DM files in the centralized repository.
- Review and approve DMs with relevant stakeholders before acceptance.

## Best Practices
- Clearly define all entities, attributes, and relationships.
- Use clear, concise, and domain-oriented language.
- Document all assumptions and dependencies.
- Ensure visuals and layout are consistent and easy to understand.

## Code Standards
- Each DM must have a unique version identifier and a documented change log.
- Use the provided Mermaid diagram layout for consistency.

### File Naming
- Name files in lowercase, using digits for version, following the pattern: `dm.xxxx.md` (e.g., `dm.0001.md`).
- Increment version numbers for significant changes.
- Include the date and author in the version log.
- we only keep the latest version in the main branch; archive older versions in a designated folder.

## Common Patterns
### Good Example
```markdown
# Domain Model (DM) for [Insert Project Name]
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | DM                               |
| crossReference    |                                   |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [yyyy-mm-dd] | Initial                  | project owner |
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
    [Entity1] "0..1" --> "*" [Entity2] : [RelationshipName]
```

### Bad Example
```
## Domain Model (DM) Components
| Entity1 | Entity2 | Entity3 |
|---------|---------|---------|
| [Attribute1], [Attribute2] | [Attribute1], [Attribute2] | [Attribute1], [Attribute2] |
| [Relationship1] | [Relationship2] | [Relationship3] |
```

## Validation

- Review DMs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review DMs for accuracy and relevance.
