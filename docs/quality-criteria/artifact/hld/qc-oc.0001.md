# Quality Criteria: Operation Contract (OC)
Operation Contracts (OC) specify the precise obligations and guarantees for system operations, focusing on state changes in the domain model. Each operation in a System Sequence Diagram (SSD) must have a corresponding OC.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-OC                             |
| crossReference    | QC-SSD                            |


### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-13 | 0001    | Initial creation of the document |               |

## Quality Criteria for Operation Contracts
When evaluating an Operation Contract, consider the following quality criteria:
1. **Declarative Style**: Contracts must describe what state changes occur, not how they are achieved.
2. **Past Tense**: Postconditions are written in the past tense (e.g., "A sale was created") to emphasize completed state changes.
3. **Atomicity**: The operation is a single unit of work; either all postconditions are met, or the system state remains unchanged.
4. **Black-Box Specification**: Contracts reference only domain model elements, not internal variables or UI elements.
5. **Completeness**: All relevant state changes are specified, including instance creation/deletion, attribute modification, and association formation/breaking.
6. **Preconditions (Obligations)**: Preconditions define what must be true before the operation. If violated, the system is not obligated to fulfill the postcondition and may throw an error.
7. **Postconditions (Guarantees)**: Postconditions guarantee what will be true after the operation, provided preconditions were met. They must cover:
   - Instance creation/deletion
   - Attribute modification
   - Association formation/breaking
8. **Verifiability**: All conditions must be objectively checkable against the domain model.
9. **Independence from Implementation**: No reference to algorithms, data structures, or UI.

## Common Patterns for Operation Contract Markdown Files

### Filename Convention
- Name files in lowercase, using digits for version, following the pattern: `oc.uc-yyy.xxxx.md` (e.g., `oc.uc-001.0001.md`) where `yyy` is the use case number.

### Good Example
```markdown
# Operation Contract for Use Case [Insert Use Case Name]

<!-- TOC should kink to all operation contracts in this file -->
## TOC

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | OC                                |
| crossReference    | ssd-uc-001.0001                   |

## Version
- **Version**: 0001
- **Date**: [insert todays date]

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [insert todays date] | Initial                  | [insert author name] |


<!-- Each operation contract should follow the template below, replacing all [insert ...] placeholders with project-specific content. Repeat the section for each operation in this file. -->
## Operation Contract: [insert operation name]

### Preconditions (Obligations)
- [insert precondition 1]
- [insert precondition 2]

### Postconditions (Guarantees)
- [insert postcondition 1]
- [insert attribute modification]
- [insert association]

---

## Example Operation Contract: Register Payment

### Preconditions (Obligations)
- A Sale instance exists.
- The Customer is authenticated.

### Postconditions (Guarantees)
- A Payment was created.
- Sale.isComplete became true.
- The Payment was associated with the Sale.

```

## Validation
- Review OCs for completeness, clarity, and correct use of the template.
- Ensure all state changes are specified and verifiable.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review OCs for accuracy and relevance.

## Language
- Professional
- English
