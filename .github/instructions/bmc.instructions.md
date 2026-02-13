---
description: 'Business Model Canvas (BMC) quality requirements and template for project documentation.'
applyTo: '**/docs/bmc.*.md'
---

# Business Model Canvas (BMC) Instructions

This instruction file provides a template and quality criteria for documenting Business Model Canvas (BMC) diagrams in markdown format. 
Use this as a starting point for any project requiring a BMC. Replace all placeholders in the table with project-specific content.

## General Instructions

- Use this template for all BMC documentation in markdown format.
- Replace all bracketed placeholders in the table with project-specific information.
- Store BMC files in the centralized repository.
- Review and approve BMCs with relevant stakeholders before acceptance.

## Best Practices

- Clearly define all nine BMC building blocks.
- Use clear, concise, and business-oriented language.
- Document all assumptions and dependencies.
- Ensure visuals and layout are consistent and easy to understand.

## Code Standards

- Each BMC must have a unique version identifier and a documented change log.
- Use the provided HTML table layout for consistency.

### File Naming
- Name files in lowercase, using digits for version, following the pattern: `qc-bmc.xxxx.md` (e.g., `qc-bmc.0001.md`).

## Common Patterns
### Good Example
```markdown
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-BMC                            |
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
## Business Model Canvas Components
<!-- Business Model Canvas Template: Replace all [Insert ...] placeholders with project-specific content. -->
<table border="1" width="100%" height="600px" style="border-collapse: collapse; vertical-align: top;">
  <!-- Upper Section -->
  <tr>
    <th colspan="2" width="20%">Key Partners</th>
    <th colspan="2" width="20%">Key Activities</th>
    <th colspan="2" width="20%">Value Propositions</th>
    <th colspan="2" width="20%">Customer Relationships</th>
    <th colspan="2" width="20%">Customer Segments</th>
  </tr>
  <tr>
    <td rowspan="3" colspan="2">
      <!--- Key Partners List -->
      <ul>
        <li>[Insert Key Partner 1]</li>
        <li>[Insert Key Partner 2]</li>
        <li>[Insert Key Partner 3]</li>
        <li>[Insert Key Partner 4]</li>
      </ul>
    </td>
    <td colspan="2">
      <!--- Key Activities List -->
      <ul>
        <li>[Insert Key Activity 1]</li>
        <li>[Insert Key Activity 2]</li>
        <li>[Insert Key Activity 3]</li>
        <li>[Insert Key Activity 4]</li>
      </ul>
    </td>
    <td rowspan="3" colspan="2">
      <!--- Value Propositions List -->
      <ul>
        <li>[Insert Value Proposition 1]</li>
        <li>[Insert Value Proposition 2]</li>
        <li>[Insert Value Proposition 3]</li>
        <li>[Insert Value Proposition 4]</li>
      </ul>
    </td>
    <td colspan="2">
      <!--- Customer Relationships List -->
      <ul>
        <li>[Insert Customer Relationship 1]</li>
        <li>[Insert Customer Relationship 2]</li>
        <li>[Insert Customer Relationship 3]</li>
      </ul>
    </td>
    <td rowspan="3" colspan="2">
      <!--- Customer Segments List -->
      <ul>
        <li>[Insert Customer Segment 1]</li>
        <li>[Insert Customer Segment 2]</li>
        <li>[Insert Customer Segment 3]</li>
        <li>[Insert Customer Segment 4]</li>
      </ul>
    </td>
  </tr>
  <tr>
    <th colspan="2">Key Resources</th>
    <th colspan="2">Channels</th>
  </tr>
  <tr>
    <td colspan="2">
      <!--- Key Resources List -->
      <ul>
        <li>[Insert Key Resource 1]</li>
        <li>[Insert Key Resource 2]</li>
        <li>[Insert Key Resource 3]</li>
        <li>[Insert Key Resource 4]</li>
        <li>[Insert Key Resource 5]</li>
      </ul>
    </td>
    <td colspan="2">
      <!--- Channels List -->
      <ul>
        <li>[Insert Channel 1]</li>
        <li>[Insert Channel 2]</li>
        <li>[Insert Channel 3]</li>
        <li>[Insert Channel 4]</li>
      </ul>
    </td>
  </tr>
  
  <!-- Lower Section -->
  <tr>
    <th colspan="5">Cost Structure</th>
    <th colspan="5">Revenue Streams</th>
  </tr>
  <tr>
    <td colspan="5">
      <!--- Cost Structure List -->
      <ul>
        <li>[Insert Cost Structure 1]</li>
        <li>[Insert Cost Structure 2]</li>
        <li>[Insert Cost Structure 3]</li>
        <li>[Insert Cost Structure 4]</li>
      </ul>
    </td>
    <td colspan="5">
      <!--- Revenue Streams List -->
      <ul>
        <li>[Insert Revenue Stream 1]</li>
        <li>[Insert Revenue Stream 2]</li>
        <li>[Insert Revenue Stream 3]</li>
      </ul>
    </td>
  </tr>
</table>
```

### Bad Example
  
```
## Business Model Canvas Components
| Key Partners         | Key Activities         | Key Resources         |
|----------------------|-----------------------|----------------------|
| [Partner 1]<br>[Partner 2]<br>[Partner 3]<br>[Partner 4] | [Activity 1]<br>[Activity 2]<br>[Activity 3]<br>[Activity 4] | [Resource 1]<br>[Resource 2]<br>[Resource 3]<br>[Resource 4] |

| Value Propositions   | Customer Relationships | Channels             |
|----------------------|-----------------------|----------------------|
| [Value Proposition 1]<br>[Value Proposition 2]<br>[Value Proposition 3]<br>[Value Proposition 4] | [Relationship 1]<br>[Relationship 2]<br>[Relationship 3] | [Channel 1]<br>[Channel 2]<br>[Channel 3]<br>[Channel 4] |

| Customer Segments    | Cost Structure         | Revenue Streams      |
|----------------------|-----------------------|----------------------|
| [Segment 1]<br>[Segment 2]<br>[Segment 3]<br>[Segment 4] | [Cost 1]<br>[Cost 2]<br>[Cost 3]<br>[Cost 4] | [Revenue 1]<br>[Revenue 2]<br>[Revenue 3] |
```


## Validation

- Review BMCs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance

- Update the version and change log for major changes.
- Regularly review BMCs for accuracy and relevance.

