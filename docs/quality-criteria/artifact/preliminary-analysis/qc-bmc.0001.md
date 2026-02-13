# Quality Criteria: Business Model Canvas (BMC)
The Business Model Canvas (BMC) is a strategic management tool that provides a visual framework for developing, describing, and analyzing business models. It consists of nine key building blocks that represent the essential components of a business. The BMC helps organizations to understand their value proposition, customer segments, revenue streams, and other critical aspects of their business.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-BMC                            |
| crossReference    |                                   |


## Version
- **Version**: 0001
- **Date**: 2026-02-07

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-07 | 0001    | Initial creation of the document |               |

## Quality Criteria for Business Model Canvas
When evaluating a Business Model Canvas, consider the following quality criteria:
1. **Clarity and Simplicity**: The BMC should be easy to understand, with clear and concise descriptions for each of the nine building blocks. Avoid jargon and complex language.
1. **Completeness**: Ensure that all nine components of the BMC are addressed:
   - Customer Segments
   - Value Propositions
   - Channels
   - Customer Relationships
   - Revenue Streams
   - Key Resources
   - Key Activities
   - Key Partnerships
   - Cost Structure

1. **Relevance**: The information provided in each section should be relevant to the specific business being analyzed. Avoid generic statements that do not add value.
1. **Consistency**: The components of the BMC should be logically consistent with each other. For example, the value propositions should align with the customer segments and revenue streams.
1. **Visual Appeal**: The BMC should be visually organized and easy to navigate. Use colors, icons, and layout techniques to enhance readability and engagement.

## Common Patterns for BMC Markdown Files

### Filename Convention
- Name files in lowercase, using digits for version, following the pattern: `bmc.xxxx.md` (e.g., `bmc.0001.md`).
- Increment version numbers for significant changes.
- Include the date and author in the version log.
- we only keep the latest version in the main branch; archive older versions in a designated folder.

### Good Example
```markdown
# Business Model Canvas Components (BMC) for [Insert Project Name]

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | BMC                               |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: [insert todays date]

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [insert todays date] | Initial                  | project owner |
```

### Table Layout Template
```markdown
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

## Validation
- Review BMCs for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.

## Maintenance
- Update the version and change log for major changes.
- Regularly review BMCs for accuracy and relevance.
