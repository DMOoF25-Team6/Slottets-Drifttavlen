# Quality Criteria: Use Case Documentation

This document provides quality criteria for reviewing and accepting Use Case documentation in markdown format. Each use case must be saved under the `use-cases` folder, and a dedicated folder named `uc-XXX` (where XXX is the unique use case number, no spaces) must be created for each use case.

## Use Case Types
Use cases can be documented in three forms:
- **Brief**: A short paragraph summarizing the use case goal and outcome.
- **Casual**: A short description with main actors and main flow and 1-2 alternative flows.
- **Fully Dressed**: Complete details including all sections listed below.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-UC                             |
| crossReference    |                                   |

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-07 | 0001    | Initial creation of the document | Team 6       |
| 2026-03-07 | 0002    | Updated quality criteria and template | Team 6  |

## Quality Criteria for Use Case Documentation
When evaluating a Use Case, consider the following quality criteria:
1. **Clarity and Simplicity**: Each use case must be easy to understand, with clear and concise descriptions. Avoid jargon and complex language.
2. **Completeness**: Ensure that all required sections of the use case are addressed, depending on the type:
   - **Brief**: Use Case Number, Title, success flow only as one paragraph.
   - **Casual**: Use Case Number, Title, Actors, Main Flow, main exceptions, summary. Each flow step is numbered. Each exception references the flow step number (e.g., 1a, 3a) and includes a paragraph description.
   - **Fully Dressed**: Use Case Number, Title, Actors, Preconditions, Main Flow, Alternate Flows, Postconditions, Exceptions, Related Requirements. Each flow step is numbered. Each exception references the flow step number and includes a paragraph description.
3. **Relevance**: The information provided in each section should be relevant to the specific use case. Avoid generic statements that do not add value.
4. **Consistency**: The sections of the use case should be logically consistent with each other. For example, the main flow should align with preconditions and postconditions.
5. **Visual Appeal**: The use case description should be visually organized and easy to navigate. Use tables, lists, and layout techniques to enhance readability and engagement.
6. **Traceability**: Each use case should be traceable to business requirements, features, or user stories.
7. **Stakeholder Validation**: Use cases must be reviewed and validated by relevant stakeholders (e.g., product owner, business analyst).
8. **Maintainability**: Use cases should be easy to update as requirements evolve (modular, not overly coupled).
9. **Tool Compatibility**: Use case documentation should be compatible with your chosen tools (e.g., markdown, diagramming tools).
10. **Documentation Quality**: All tables, diagrams, and flows must be accompanied by concise explanations if not self-explanatory.
11. **Versioning and Change Log**: Every change to a use case must be logged with a version, date, and author.
12. **Language/Translation Compliance**: If the product owner’s language is not English, ensure translation and dual-file compliance.

a: At any time, [Insert Extension Description]  
b: At any time, [Insert Extension Description]  
&nbsp;&nbsp;&nbsp;&nbsp;1: [Insert Extension Description]  

1a: At any time, [Insert Extension Description]  
&nbsp;&nbsp;&nbsp;&nbsp;1: [Insert Extension Description]  
&nbsp;&nbsp;&nbsp;&nbsp;2: [Insert Extension Description]  
2a: [Insert Extension Description 1]  
3a: [Insert Extension Description 2]  

**Postconditions**:

## Authoring Patterns and Templates
For filename conventions, templates, and authoring examples, see `.github/instructions/usecase.instructions.md`.

## Validation
- Review use cases for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review use cases for accuracy and relevance.

## Language
- Active form
- Professional
- English
- If product owner domain language is different, use that language for the diagram content while maintaining English for metadata and versioning. And save the file with a language code suffix (e.g., `uc-xxx.0001.da.md` for Danish). So now we have two files: `uc-xxx.0001.md` (English) and `uc-xxx.0001.da.md` (Danish).
