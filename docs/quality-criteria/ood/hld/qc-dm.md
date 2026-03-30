# Quality Criteria: Domain Model (DM)
Domain Models are fundamental for representing the core business concepts, rules, and relationships within a system.
A well-designed Domain Model provides clarity, supports maintainability, and ensures alignment with business requirements.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-DM                             |
| crossReference    | Applying UML and patterns by Craig Larman |
 
### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-13 | 0001    | Initial creation of the document | Team 6       |
| 2026-03-07 | 0002    | Updated quality criteria and template | Team 6  |
| 2026-27-03 | 0003    | Added more QC                   | Team 6        |

## Quality Criteria for Domain Models
When evaluating a Domain Model, consider the following quality criteria:
1. **Clarity and Simplicity**: The Domain Model should be easy to interpret, with clear class/entity names, attribute labels, and relationship definitions. Avoid unnecessary complexity and ambiguous notation.
2. **Completeness**: All relevant business entities, attributes, multiplicity and relationships must be included. Ensure that key business rules and constraints are properly represented.
3. **Correctness**: The model must accurately reflect the intended business domain and requirements. Entity and relationship definitions should be precise and unambiguous. We do not have type for attributes in the domain model, but we should ensure that the attributes are meaningful and relevant to the domain.
4. **Consistency**: Naming conventions, symbols, and layout should be consistent throughout the model. Relationships should logically connect entities as per requirements.
5. **Visual Appeal**: The Domain Model should be visually organized and easy to navigate. Use layout techniques, grouping, and clear diagrams to enhance readability and engagement.
6. **Traceability**: Each element in the domain model should be traceable to a business requirement or use case.
7. **Stakeholder Validation**: The model must be reviewed and validated by relevant business/domain stakeholders.
8. **Maintainability**: The model should be easy to update as requirements evolve (e.g., modular, not overly coupled).
9. **Tool Compatibility**: The model should be compatible with your chosen modeling/documentation tools (e.g., Mermaid, PlantUML).
10. **Documentation Quality**: All diagrams and tables must be accompanied by concise explanations/legends if not self-explanatory.
11. **Versioning and Change Log**: Every change to the model must be logged with a version, date, and author.
12. **Language/Translation Compliance**: If the product owner’s language is not English, ensure translation and dual-file compliance.

## Authoring Patterns and Templates
For filename conventions, templates, and authoring examples, see `.github/instructions/dm.instructions.md`.

## Validation
- Review Domain Models for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review Domain Models for accuracy and relevance.

## Language
- Professional
- English
- If product owner domain language is different, use that language for the diagram content while maintaining English for metadata and versioning. And save the file with a language code suffix (e.g., `uc-xxx.dm.da.md` for Danish). So now we have two files: `uc-xxx.dm.md` (English) and e.g., `uc-xxx.dm.da.md` (Danish).
