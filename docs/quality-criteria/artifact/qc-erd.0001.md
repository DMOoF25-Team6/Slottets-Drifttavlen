# Quality Criteria: Entity Relationship Diagram (ERD)
Entity Relationship Diagrams (ERD) are essential tools for modeling the data structure of a system.
ERDs visually represent entities, their attributes, and relationships, providing a clear overview of the database schema and its logical structure.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-ERD                            |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: 2026-02-11

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-11 | 0001    | Initial creation of the document | [Insert Project Owner] |

## Quality Criteria for Entity Relationship Diagrams
When evaluating an ERD, consider the following quality criteria:
1. **Clarity and Simplicity**: The ERD should be easy to interpret, with clear entity names, attribute labels, and relationship lines. Avoid unnecessary complexity and ambiguous notation.
1. **Completeness**: All relevant entities, attributes, and relationships must be included. Ensure that primary keys, foreign keys, and cardinality are properly represented.
1. **Correctness**: The diagram must accurately reflect the intended data model and business rules. Entity and relationship definitions should be precise.
1. **Consistency**: Naming conventions, symbols, and layout should be consistent throughout the diagram. Relationships should logically connect entities as per requirements.
1. **Visual Appeal**: The ERD should be visually organized and easy to navigate. Use layout techniques, colors, and grouping to enhance readability and engagement.

## Common Patterns for ERD Markdown Files

### Filename Convention
- Name files in lowercase, using digits for version, following the pattern: `erd.xxxx.md` (e.g., `erd.0001.md`).
- Use end of line lf.
- Use charset utf-8.
- Increment version numbers for significant changes.
- Include the date and author in the version log.
- we only keep the latest version in the main branch; archive older versions in a designated folder.

### Good Example
```markdown
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
| 0001    | 2026-01-20 | Initial                  | [Insert Project Owner] |
```

### Table Layout Template
```mermaid
%% Entity Relationship Diagram Template: Replace all [Insert ...] placeholders with project-specific content.
erDiagram
    [Entity1] {
        Guid Id
        nvchar [Attribute1]
        int [Attribute2]
        nvchar [Attribute3]
    }
    [Entity2] {
        Guid Id
        nvchar [Attribute1]
        int [Attribute2]
    }
    [Entity3] {
        Guid Id
        nvchar [Attribute1]
        nvchar [Attribute2]
    }
    [Entity1] ||--o{ [Entity2] : [Relationship1]
    [Entity2] ||--|{ [Entity3] : [Relationship2]
    [Entity1] }o--|| [Entity3] : [Relationship3]
```

## Validation
- Review ERDs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review ERDs for accuracy and relevance.

### Language
- Translate to english.
- Use professional terminology appropriate for database modeling.
