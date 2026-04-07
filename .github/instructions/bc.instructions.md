---
description: ' Business Case (BC) quality requirements and template for project documentation.'
applyTo: '/docs/bc.md' and '/docs/bc.*.md'
reference: '/docs/quality-criteria/ooa/qc-bc.md'
---

# DM Instructions (Summary)
- Use the provided BC markdown template / examples.
- Replace all placeholders with project-specific content.
- Store BC files in `docs/` as `bc.md`.
- Increment version numbers for significant changes and add it to the change log.
- Include metadata, version log (with date, author).
- Create files in English; if product owner domain language differs, create a separate file with language
- Use the provided BC markdown template / examples.

## BC Template (Minimal):
```markdown
# Business Case (BC) for [Insert Project Name]
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | BC                                |
| crossReference    | [Insert Reference to DM or other relevant document] |

## Change log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | <today date in format yyyy-mm-dd> | Initial                  | project owner |

## Executive Summary

## Problem Statement

## Objectives

- **Technical Requirements:**

- **Business Requirements:**

**Phases:**

## Options and Alternatives (0-scenario, 1-scenario, 2-scenario)

## Cost Benefit Analysis
| Option | Cost | Benefit |
|--------|------|---------|

## Risks and Mitigations

## Timeline

## Success Criteria

```
