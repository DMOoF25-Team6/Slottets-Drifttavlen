# Quality Criteria: Domain Model (DM)
Domain Models are fundamental for representing the core business concepts, rules, and relationships within a system.
A well-designed Domain Model provides clarity, supports maintainability, and ensures alignment with business requirements.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-DM                             |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: 2026-02-11

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-11 | 0001    | Initial creation of the document |               |

## Quality Criteria for Domain Models
When evaluating a Domain Model, consider the following quality criteria:
1. **Clarity and Simplicity**: The Domain Model should be easy to interpret, with clear class/entity names, attribute labels, and relationship definitions. Avoid unnecessary complexity and ambiguous notation.
2. **Completeness**: All relevant business entities, attributes, and relationships must be included. Ensure that key business rules and constraints are properly represented.
3. **Correctness**: The model must accurately reflect the intended business domain and requirements. Entity and relationship definitions should be precise and unambiguous.
4. **Consistency**: Naming conventions, symbols, and layout should be consistent throughout the model. Relationships should logically connect entities as per requirements.
5. **Visual Appeal**: The Domain Model should be visually organized and easy to navigate. Use layout techniques, grouping, and clear diagrams to enhance readability and engagement.

## Common Patterns for Domain Model Markdown Files

### Filename Convention
- Name files in lowercase, using digits for version, following the pattern: `domain.xxxx.md` (e.g., `domain.0001.md`).
  - If under a use case folder, use the pattern: `domain.uc-yyy.xxxx.md` where `yyy` is the use case number.

### Good Example
```markdown
# Domain Model: [Insert Project or UseCase]

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | DM                                |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: [insert todays date]

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [insert todays date] | Initial                  | [insert author name] |
```

### Table Layout Template
```mermaid
%% Domain Model Diagram Template: Replace all [Insert ...] placeholders with project-specific content.
classDiagram
    class [Entity1] {
        +Guid Id
        +Guid RelatedEntityId
        +string Attribute1
        +int Attribute2
        +string Attribute3
    }
    class [Entity2] {
        +Guid Id
        +Guid Entity1Id
        +string Attribute1
        +int Attribute2
    }
    class [Entity3] {
        +Guid Id
        +string Attribute1
        +string Attribute2
    }
    [Entity1] <|-- [Entity2] : [Relationship1]
    [Entity2] o-- [Entity3] : [Relationship2]
    [Entity1] *-- [Entity3] : [Relationship3]
```

## Validation
- Review Domain Models for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review Domain Models for accuracy and relevance.

