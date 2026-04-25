# Quality Criteria: Operational Contracts (OC)
Operational Contracts define the core operations, commands, and queries supported by the system, forming the interface between the domain model and external actors or system components.
A well-designed Operational Contract ensures clarity, traceability, and supports business requirements.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-OC                             |
| crossReference    | Applying UML and patterns by Craig Larman |

### Change Log
| Date       | Version | Description                        | Author        |
|------------|---------|------------------------------------|---------------|
| 2026-03-07 | 0001    | Initial creation of the document   | Tirsvad        |

## Quality Criteria for Operational Contracts
When evaluating an Operational Contract, consider the following quality criteria:
1. **Clarity and Unambiguity**: The contract should be easy to interpret, with clear operation names, parameters, and results. Avoid ambiguity and unnecessary complexity.
2. **Coverage**: All relevant business operations, inputs, outputs, and exceptions must be included. Ensure that key business rules and constraints are properly represented.
3. **Correctness**: The contract must accurately reflect the intended business requirements. Definitions of operations and parameters should be precise and relevant to the domain.
4. **Consistency**: Naming conventions, notation, and structure should be consistent across all contracts. Parameters and results should follow the same conventions.
5. **Traceability**: Each operational contract should be traceable to specific business requirements or use cases.

## Common Patterns for Operational Contract Markdown Files

### Filename Convention
- Name files in lowercase, using digits for version,
  - following the file name pattern: `uc-yyy.oc.xxxx.md` (e.g., `uc-001.oc.0001.md`).
    - for use case operational contracts, include the use case identifier in the file name as a prefix.
      - save files for use case operational contracts in a subfolder named after the use case (e.g., `docs/use-cases/uc-001/uc-001.oc.0001.md`).
- Increment version numbers for significant changes.
- Include today's date and author in the version log.
- Only keep the latest version in the main branch; delete older versions.

### Good Example
```markdown
# Operational Contract: [Insert Project or UseCase]

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | [UseCase].OC                      |
| crossReference    | [UseCase].SSD                     |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [insert today's date] | Initial                  | [insert author name] |

## Object Constract
<!-- Object Constract Template: Replace all [Insert ...] placeholders with project-specific content. -->

### [Insert Interaction Name]
- **Preconditions**: [Insert any preconditions that must be met before this interaction can  occur.]
- **Postconditions**: [Insert any postconditions that will be true after this interaction occurs.]
```

## Validation
- Review operational contracts for coverage, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review operational contracts for accuracy and relevance.

## Language
- Professional
- English
