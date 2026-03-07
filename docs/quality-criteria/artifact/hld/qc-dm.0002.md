# Quality Criteria: Domain Model (DM)
Domain Models are fundamental for representing the core business concepts, rules, and relationships within a system.
A well-designed Domain Model provides clarity, supports maintainability, and ensures alignment with business requirements.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-DM                             |
| crossReference    |                                   |
 
### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-13 | 0001    | Initial creation of the document | Team 6        |
| 2026-03-07 | 0002    | Updated quality criteria and template | Tirsvad       |

## Quality Criteria for Domain Models
When evaluating a Domain Model, consider the following quality criteria:
1. **Clarity and Simplicity**: The Domain Model should be easy to interpret, with clear class/entity names, attribute labels, and relationship definitions. Avoid unnecessary complexity and ambiguous notation.
2. **Completeness**: All relevant business entities, attributes, multiplicity and relationships must be included. Ensure that key business rules and constraints are properly represented.
3. **Correctness**: The model must accurately reflect the intended business domain and requirements. Entity and relationship definitions should be precise and unambiguous. We do not have type for attributes in the domain model, but we should ensure that the attributes are meaningful and relevant to the domain.
4. **Consistency**: Naming conventions, symbols, and layout should be consistent throughout the model. Relationships should logically connect entities as per requirements.
5. **Visual Appeal**: The Domain Model should be visually organized and easy to navigate. Use layout techniques, grouping, and clear diagrams to enhance readability and engagement.

## Common Patterns for Domain Model Markdown Files

### Filename Convention
- Name files in lowercase, using digits for version,
  - following the file name pattern: `dm.xxxx.md` (e.g., `dm.0001.md`).
    - for use case domain models, include the use case identifier in the file name as a prefix.
      - save files for use case domain models in a subfolder named after the use case (e.g., `docs/use-cases/uc001/dm.0001.md`).
    - for solution domain models, do not include a use case identifier in the file name.
      - save files for solution domain models in the main `docs` folder (e.g., `docs/dm.0001.md`).
- Increment version numbers for significant changes.
- Include the todays date and author in the version log.
- we only keep the latest version in the main branch; archive older versions in a designated folder `archive`.

### Good Example
```markdown
# Domain Model: [Insert Project or UseCase]

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | [UseCase].DM                      |
| crossReference    | BC                                |

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
        Attribute1
        Attribute2
        Attribute3
    }
    class [Entity2] {
        Attribute1
        Attribute2
        Attribute3
    }
    class [Entity3] {
        Attribute1
        Attribute2
    }
    [Entity1] "0..1" -- "*" [Entity2] : [Relationship1]
    [Entity2] "0..1" -- "*" [Entity3] : [Relationship2]
    [Entity1] "*" -- "*" [Entity3] : [Relationship3]
```

## Validation
- Review Domain Models for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review Domain Models for accuracy and relevance.

## Language
- Professional
- English
- If product owner domain language is different, use that language for the diagram content while maintaining English for metadata and versioning. And save the file with a language code suffix (e.g., `uc-xxx.dm.0001.da.md` for Danish). So now we have two files: `uc-xxx.dm.0001.md` (English) and e.g., `uc-xxx.dm.0001.da.md` (Danish).
