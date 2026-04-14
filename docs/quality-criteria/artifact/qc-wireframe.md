# Quality Criteria: Wireframe
A Wireframe illustrates the layout and structure of a UI screen or section for a specific use case.
This document defines quality criteria and a template for documenting wireframes in markdown format using ASCII layout.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-WIREFRAME                      |
| crossReference    |                                   |

## Version and Change Log
| Date       | Version | Description                      | Author  |
|------------|---------|----------------------------------|---------|
| 2026-04-13 | 0001    | Initial creation of the document | Team 6  |

## Quality Criteria for Wireframes
When evaluating a Wireframe, consider the following quality criteria:
1. **Clarity and Simplicity**: The wireframe should be easy to interpret, with clear section labels, input fields, and layout zones. Avoid unnecessary complexity.
2. **Completeness**: All relevant UI sections, fields, and labels for the use case must be included.
3. **Correctness**: The layout must accurately reflect the use case scope and domain model. It must not introduce functionality not covered by the use case.
4. **Consistency**: Layout conventions and field notation should be consistent. Use [ ] for editable input fields and fixed text for read-only labels.
5. **Traceability**: Each UI element must be traceable to a requirement, use case, or domain model entity.
6. **Context**: The wireframe must show the UI section in context of the full page where relevant.
7. **No Implementation Details**: Wireframes are UI layout only. Do not include CSS, HTML, backend calls, or technical implementation details.
8. **Visual Appeal**: The ASCII layout should be well-aligned and easy to read.
9. **Versioning and Change Log**: Every change must be logged with a version, date, and author.
10. **Language/Translation Compliance**: If the product owner language is not English, create a separate translated file with the correct language code suffix.

## Authoring Patterns and Templates
For filename conventions, templates, and authoring examples, see `.github/instructions/wireframe.instructions.md`.

## Validation
- Review wireframes for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.
- Verify layout matches the use case scope and domain model.

## Maintenance
- Update the version and change log for major changes.
- Regularly review wireframes for accuracy and relevance.

## Language
- Professional
- English
- If product owner domain language is different, create a translated file with a language code suffix (e.g., uc-xxx.wireframe.da.md for Danish).
- Both files must be kept in the use case subfolder.
