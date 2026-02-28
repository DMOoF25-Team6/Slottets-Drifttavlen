---
description: 'Object Constract (OC) quality requirements and template for project documentation.'
applyTo: '**/oc.*.md'
---

# Object Constract (OC) Instructions
This instruction file provides a template and quality criteria for documenting Object Constracts (OC) in markdown format. Use this as a starting point for any project requiring an OC. Replace all placeholders in the diagram with project-specific content.

## General Instructions
- Use this template for all OC documentation in markdown format.
- Replace all bracketed placeholders in the Markdown with project-specific information.
- Store OC files in the centralized repository.
- Review and approve OCs with relevant stakeholders before acceptance.

## Best Practices
- Transformed from a `ssd.*.md`. All interaction to system has it own oc chapter.
- Ensure visuals and layout are consistent and easy to understand.
- Use markdown format.

## Code Standards
- Each OC must have a unique version identifier and a documented change log.
- Ensure that all OC documentation follows the provided markdown layout for consistency.

### File Naming
- Name files in lowercase, using digits for version, following the pattern: `oc.xxxx.md` (e.g., `oc.0001.md`).

## Common Patterns
### Good Example
```markdown
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | OC                                |
| crossReference    | [Insert SSD Reference]            |

## Version
- **Version**: 0001
- **Date**: [Insert Date yyyy-mm-dd]
## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [Insert Date yyyy-mm-dd] | Initial                  | Project owner | 
```

```markdown
## Object Constract
<!-- Object Constract Template: Replace all [Insert ...] placeholders with project-specific content. -->

### [Insert Interaction Name]
- **Description**: [Insert a brief description of the interaction and its purpose.]
- **Actors Involved**: [Insert a list of actors involved in this interaction.]
- **Preconditions**: [Insert any preconditions that must be met before this interaction can  occur.]
- **Postconditions**: [Insert any postconditions that will be true after this interaction occurs.]
- **Steps**:
1. [Insert Step 1: Describe the first step of the interaction.]
2. [Insert Step 2: Describe the second step of the interaction.]
3. [Insert Step 3: Describe the third step of the interaction.]
```

### Additional Guidelines
- Ensure that each interaction is clearly defined and follows a logical sequence.
- Use clear and concise language to describe each step of the interaction.
- Include any relevant details that may help stakeholders understand the interaction better, such as expected outcomes or potential edge cases.

## Validation
- Review OCs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update OCs as necessary when changes occur in the system or interactions.
- Keep a change log to track updates and revisions to the OCs.

## Language 
- Professional
- English

