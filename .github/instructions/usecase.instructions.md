---
description: 'Use Case quality requirements and template for project documentation.'
applyTo: '**/use-cases/usecase*.md'
---

# Use Case Instructions

This instruction file provides a template and quality criteria for documenting Use Cases in markdown format.
Use this as a starting point for any project requiring use case documentation.
Replace all placeholders in the template with project-specific content.

## General Instructions

- Use this template for all use case documentation in markdown format.
- Replace all bracketed placeholders in the template with project-specific information.
- Store use case files in the centralized repository under the `use-cases` folder and `UseCaseXXX-descriptions` subfolders (no spaces in names).
- Review and approve use cases with relevant stakeholders before acceptance.

## Best Practices

- Clearly define all required use case sections.
- Use clear, concise, and business-oriented language.
- Document all assumptions and dependencies.
- Ensure layout is consistent and easy to understand.
- Number each flow step (1, 2, 3, ...).
- Reference flow step numbers in exceptions (e.g., 1.a, 3.a) and provide a paragraph description for each exception.

## Code Standards

- Each use case must have a unique version identifier and a documented change log.
- Use the provided markdown template for consistency.

### File Naming
- Name files and folders without spaces, using digits for use case numbers, following the pattern: `UseCaseXXX-descriptions` (e.g., `UseCase001-descriptions`).

## Common Patterns
### Good Example
```markdown
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | UC-001                            |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: 2026-01-20

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-01-20 | Initial                  | project owner |
```

```markdown
## Use Case Description
<!-- Use Case Template: Replace all [Insert ...] placeholders with project-specific content. -->

### Brief Use Case
**Use Case Number**: [Insert Use Case Number]
**Title**: [Insert Title]
**Summary**: [Insert brief summary]
**Success Flow**:
1. [Insert Main Flow Step 1]
2. [Insert Main Flow Step 2]
3. [Insert Main Flow Step 3]

### Casual Use Case
**Use Case Number**: [Insert Use Case Number]
**Title**: [Insert Title]
**Actors**:
- [Insert Actor 1]
- [Insert Actor 2]
**Main Flow**:
1. [Insert Main Flow Step 1]
2. [Insert Main Flow Step 2]
3. [Insert Main Flow Step 3]
**Main Exceptions**:
1.a [Insert Exception 1 paragraph description]
3.a [Insert Exception 2 paragraph description]
**Summary**: [Insert casual summary]

### Fully Dressed Use Case
**Use Case Number**: [Insert Use Case Number]
**Title**: [Insert Title]
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
**Alternate Flows**:
2.1 [Insert Alternate Flow 1]
2.2 [Insert Alternate Flow 2]
**Exceptions**:
1.a [Insert Exception 1 paragraph description]
2.2.a [Insert Exception 2 paragraph description]
3.a [Insert Exception 3 paragraph description]
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
