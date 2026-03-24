---
description: 'Glossary template for project documentation.'
applyTo: 'docs/glossary.md' or 'docs/glossary.<language code>.md'
---

# Glossary Instructions
This instruction file provides a template and quality criteria for documenting glossary entries in markdown format.
Use this as a starting point for any project requiring glossary documentation. Replace all placeholders in the template with project-specific content.

## Artifact Term Change Table
For each artifact (e.g., domain model, use case, UI component), include a table listing all terms that have changed names from high-level design to low-level design artifacts. The table should have columns:

| High-Level Term | Low-Level Term | Reason/Context |
|-----------------|---------------|---------------|
| ...             | ...           | ...           |

## Language Translation Mapping
If product owner domain language differs from English, add a section mapping terms to the product owner's domain language. Use a table format:

| English Term | Product Owner Term | Notes |
|--------------|-------------------|-------|
| ...          | ...               | ...   |

Include this section in `docs/glossary.<language code>.md` for each supported language.

