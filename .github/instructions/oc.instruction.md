---
description: 'Object Constract (OC) quality requirements and template for project documentation.'
applyTo: 'docs/use-cases/**/uc*.oc.*.md'
---

# Object Constract (OC) Instructions
This instruction file provides a template and quality criteria for documenting Object Constracts (OC) in markdown format. Use this as a starting point for any project requiring an OC. Replace all placeholders in the diagram with project-specific content.

## General Instructions
- Use this template for all OC documentation in markdown format.
- Replace all bracketed placeholders in the Markdown with project-specific information.
- Store OC files in the centralized repository.
- Review and approve OCs with relevant stakeholders before acceptance.

## Best Practices
- Transformed from a `uc-xxx.ssd.*.md`. All interaction to system has it own oc chapter.
- Ensure visuals and layout are consistent and easy to understand.
- Use markdown format.

## Code Standards
- Each OC must have a unique version identifier and a documented change log.
- Ensure that all OC documentation follows the provided markdown layout for consistency.

### File Naming
- Name files in lowercase, using digits for version,
  - following the file name pattern: `uc-yyy.oc.xxxx.md` (e.g., `uc-001.oc.0001.md`).
    - for use case operational contracts, include the use case identifier in the file name as a prefix.
      - save files for use case operational contracts in a subfolder named after the use case (e.g., `docs/use-cases/uc-001/uc-001.oc.0001.md`).
- Increment version numbers for significant changes.
- Include today's date and author in the version log.
- Only keep the latest version in the main branch; delete older versions.

## Common Patterns
### Good Example
```markdown
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | OC                                |
| crossReference    | [Insert SSD Reference]            |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [Insert Today Date yyyy-mm-dd] | Initial                  | Project owner | 
```

```markdown
## Object Constract
<!-- Object Constract Template: Replace all [Insert ...] placeholders with project-specific content. -->

### [Insert Interaction Name]
- **Preconditions**: [Insert any preconditions that must be met before this interaction can  occur.]
- **Postconditions**: [Insert any postconditions that will be true after this interaction occurs.]
```

### Additional Guidelines
- Ensure that each interaction is clearly defined and follows a logical sequence.
- Use clear and concise language to describe each step of the interaction.
- Include any relevant details that may help stakeholders understand the interaction better, such as expected outcomes or potential edge cases.
- Fields should be filled out from a use case ssd.*.md, and all interactions to system should have its own oc chapter.

## Validation
- Review OCs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update OCs as necessary when changes occur in the system or interactions.
- Keep a change log to track updates and revisions to the OCs.

## Language 
- Professional
- English
