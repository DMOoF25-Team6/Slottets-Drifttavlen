---
description: 'FURPS is a software quality model that categorizes software attributes into five main categories: Functionality, Usability, Reliability, Performance, and Supportability. It is used to evaluate and improve the quality of software products.'
applyTo: 'docs/furps.md'
references:
  - docs/quality-criteria/OOA/qc-furps.md
---

# Instructions (Summary)
- Use the provided markdown template / examples.
- Replace all placeholders with project-specific content.
- Store files in `docs/` as `furps.md`.
- FURPS artifacts must follow the structure and content guidelines outlined in this document.
- All requirements must use the `REQ-<category>-<number>` format for IDs (e.g., REQ-F-001, REQ-U-001, etc.).
- All '+' (plus) requirements must be categorized into relevant subcategories (e.g., Design Constraints, Implementation, Interface, Physical, Security, etc.) and not grouped as a single category.
- FURPS artifacts must be versioned and include a change log.
- FURPS artifacts must be authored in professional English, with translated versions maintained if the product owner's domain language is not English. if product owner domain language differs, create a separate file with language code suffix (e.g., `furps.da.md`) and include a translation section in the English file.

## Template (Minimal):

```markdown
# FURPS Quality Model for [Insert Project Name]

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | FURPS                             |
| crossReference    | BC                                |

## Change log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | <today date in format yyyy-mm-dd> | Initial                  | <Insert Author Name> |

---

## FURPS Categories
### Functionality
- REQ-F-001: [List functional requirements and attributes related to functionality]

### Usability
- REQ-U-001: [List usability requirements and attributes related to usability]

### Reliability
- REQ-R-001: [List reliability requirements and attributes related to reliability]

### Performance
- REQ-P-001: [List performance requirements and attributes related to performance]

### Supportability
- REQ-SUP-001: [List supportability requirements and attributes related to supportability]

```

Optional sections for + in furps:

```markdown
## Additional Quality Attributes
### [List any additional quality attributes that are relevant to the project but do not fit into the FURPS categories]
- [ID]: [Description of the attribute and its importance to the project]

### [List any additional quality attributes that are relevant to the project but do not fit into the FURPS categories]
- [ID]: [Description of the attribute and its importance to the project]
