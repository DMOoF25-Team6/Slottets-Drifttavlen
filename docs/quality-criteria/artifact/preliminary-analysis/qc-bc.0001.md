# Quality Criteria: Business Case (BC)
The Business Case (BC) is a structured document that justifies the initiation of a project or task. It outlines the rationale, expected benefits, costs, risks, and alternatives, providing a basis for decision-making and prioritization.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-BC                             |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: 2026-02-07

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-07 | 0001    | Initial creation of the document |               |

## Quality Criteria for Business Case
When evaluating a Business Case, consider the following quality criteria:
1. **Clarity and Structure**: The BC should be well-organized, with clear sections and concise language. Avoid jargon and ensure the rationale is easy to follow.
1. **Completeness**: Ensure that all essential components are addressed:
   - Executive Summary
   - Problem Statement
   - Objectives
   - Options and Alternatives
   - Benefits
   - Costs
   - Risks and Mitigations
   - Timeline
   - Success Criteria

1. **Relevance**: The information provided should be specific to the project or task. Avoid generic statements that do not add value.
1. **Consistency**: The components of the BC should logically support each other. For example, the objectives should align with the benefits and success criteria.
1. **Evidence-Based**: Claims and projections should be supported by data, references, or reasonable assumptions.

## Common Patterns for BC Markdown Files

### Filename Convention
- Name files in lowercase, using digits for version, following the pattern: `bc.xxxx.md` (e.g., `bc.0001.md`).
- Increment version numbers for significant changes.
- Include the date and author in the version log.
- Only keep the latest version in the main branch; archive older versions in a designated folder.

### Good Example
```markdown
# Business Case for [Insert Project Name]

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | BC                                |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: [insert todays date]

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [insert todays date] | Initial                  | project owner |
```

### Section Layout Template
```markdown
<!-- Business Case Template: Replace all [Insert ...] placeholders with project-specific content. -->
# Business Case for [Insert Project Name]

## Executive Summary
[Insert summary of the business case]

## Problem Statement
[Describe the problem or opportunity]

## Objectives
[State the objectives of the project or task]

## Options and Alternatives
[List and briefly describe considered options]

## Benefits
[List expected benefits]

## Costs
[Summarize estimated costs]

## Risks and Mitigations
[List key risks and how they will be managed]

## Timeline
[Provide a high-level timeline]

## Success Criteria
[Define how success will be measured]
```

## Validation
- Review BCs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review BCs for accuracy and relevance.

