# Quality Criteria: Use Case Documentation

This document provides quality criteria and a template for documenting Use Cases in markdown format. Each use case must be saved under the `use-cases` folder, and a dedicated folder named `uc-XXX` (where XXX is the unique use case number, no spaces) must be created for each use case.

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
| 2026-03-07 | 0002    | Updated quality criteria and template | Tirsvad |

## Quality Criteria for Use Case Documentation
When evaluating a Use Case, consider the following quality criteria:
1. **Clarity and Simplicity**: Each use case must be easy to understand, with clear and concise descriptions. Avoid jargon and complex language.
1. **Completeness**: Ensure that all required sections of the use case are addressed, depending on the type:
   - **Brief**: Use Case Number, Title, success flow only as one paragraph.
   - **Casual**: Use Case Number, Title, Actors, Main Flow, main exceptions, summary. Each flow step is numbered. Each exception references the flow step number (e.g., 1a, 3a) and includes a paragraph description.
   - **Fully Dressed**: Use Case Number, Title, Actors, Preconditions, Main Flow, Alternate Flows, Postconditions, Exceptions, Related Requirements. Each flow step is numbered. Each exception references the flow step number and includes a paragraph description.

1. **Relevance**: The information provided in each section should be relevant to the specific use case. Avoid generic statements that do not add value.
1. **Consistency**: The sections of the use case should be logically consistent with each other. For example, the main flow should align with preconditions and postconditions.
1. **Visual Appeal**: The use case description should be visually organized and easy to navigate. Use tables, lists, and layout techniques to enhance readability and engagement.

## Common Patterns for Use Case Markdown Files

### Filename and Folder Convention
- Name files and folders without spaces, using digits for use case numbers, following the pattern: `uc-XXX` (e.g., `uc-001`).
- Store use case markdown files in the corresponding folder.

### Good Example
```markdown
# Use Case Description: Use Case [insert use case number example 001] - [Insert Title]

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | UC-[insert use case number example 001] |
| crossReference    | BC                                |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [insert todays date] | Initial                  | [insert author name] |

```

### Table Layout Template
```markdown
## Use Case Description
<!-- Use Case Template: Replace all [Insert ...] placeholders with project-specific content. -->

### Brief Use Case
**Title**: [Insert Title]
**Success Flow**:
[insert flow as one paragraph]

### Casual Use Case
**Title**: [Insert Title]
**Scope**: [Insert Scope]
**Level**: [Insert Level]
**Actors**:
- [Insert Actor 1]
- [Insert Actor 2]
**Main Flow**:
1: [Insert Main Flow Step 1]
2: [Insert Main Flow Step 2]
3: [Insert Main Flow Step 3]
**Main Extensions**:
1a: [Insert Extensions Description 1]
3a: [Insert Extensions Description 2]
**Summary**: [Insert casual summary]

### Fully Dressed Use Case
**Title**: [Insert Title]
**Scope**: [Insert Scope]
**Level**: [Insert Level]
**Actors**:
- [Insert Actor 1]
- [Insert Actor 2]
**Related Requirements**:
- [Insert Requirement 1]
- [Insert Requirement 2]
**Preconditions**:
- [Insert Precondition 1]
- [Insert Precondition 2]
**Main Flow**:
1. [Insert Main Flow Step 1]
2. [Insert Main Flow Step 2]
3. [Insert Main Flow Step 3]
**Extensions**:

a: At any time, [Insert Extension Description]  
b: At any time, [Insert Extension Description]  
&nbsp;&nbsp;&nbsp;&nbsp;1: [Insert Extension Description]  

1a: At any time, [Insert Extension Description]  
&nbsp;&nbsp;&nbsp;&nbsp;1: [Insert Extension Description]  
&nbsp;&nbsp;&nbsp;&nbsp;2: [Insert Extension Description]  
2a: [Insert Extension Description 1]  
3a: [Insert Extension Description 2]  

**Postconditions**:
- [Insert Postcondition 1]
- [Insert Postcondition 2]

```

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
