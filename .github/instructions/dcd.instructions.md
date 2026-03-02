---
description: 'Domain Class Diagram (DCD) quality requirements and template for project documentation.'
applyTo: '**/docs/dcd.*.md' and '**/use-cases/**/dcd.*.mdx'
---

# Domain Class Diagram (DCD) Instructions
This instruction file provides a template and quality criteria for documenting Domain Class Diagrams (DCD) in markdown format. Use this as a starting point for any project requiring a DCD. Replace all placeholders in the diagram with project-specific content.

## General Instructions
- Use this template for all DCD documentation in markdown format.
- Replace all bracketed placeholders in the Mermaid diagram and Markdown with project-specific information.
- Store DCD files in the centralized repository.
- Review and approve DCDs with relevant stakeholders before acceptance.

## Best Practices
- Clearly define all relevant classes, attributes, and relationships.
- Use clear, concise, and domain-oriented language.
- Document all assumptions and dependencies.
- Ensure visuals and layout are consistent and easy to understand.
- Use valid Mermaid class diagram syntax.
- Use appropriate class diagram notations to represent different types of relationships (e.g., inheritance, association, aggregation).
- Include notes or comments in the diagram to clarify complex relationships or design decisions.

## Code Standards
- Each DCD must have a unique version identifier and a documented change log.
- Use the provided Mermaid diagram layout for consistency.

### File Naming
- Name files in lowercase, using digits for version, following the pattern: `dcd.xxxx.md` (e.g., `dcd.0001.md`).
- Increment version numbers for significant changes.
- Include the date and author in the version log.
- We only keep the latest version in the main branch; archive older versions in a designated folder.

## Common Patterns
### Good Example
```markdown
# Domain Class Diagram (DCD) for [Insert Project Name]
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | DCD                               |
| crossReference    |                                   |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [yyyy-mm-dd] | Initial                  | Team 6     |
```

```mermaid
%% Design Class Diagram Template: Replace all [Insert ...] placeholders with project-specific content.
classDiagram
  namespace [NamespaceName] {
    class [Class1] {
      +[Attribute1]: [Type]
      +[Attribute2]: [Type]
      +[Method1]([params]): [ReturnType]
      +[Method2]([params]): [ReturnType]
    }
  }
  namespace [NamespaceName] {
    class [Class2] {
      +[Attribute1]: [Type]
      +[Method1]([params]): [ReturnType]
    }
  }
  [Class1] <|-- [Class2] : [Inheritance]
  [Class1] o-- [Class3] : [Association]
  [Class2] *-- [Class4] : [Composition]
```

## accepted parts of the DCD syntax:
<|--	Inheritance
*--	Composition
o--	Aggregation
-->	Association
--	Link (Solid)
..>	Dependency
..|>	Realization
..	Link (Dashed

if link is between entities then we need multiplicities ala
"0..1" -- "*"

## Language 
- Professional
- English

## Class object
if class object chenges name form artifacts before then make / update glosery `/docs/glosery.md` with class name in artifacts we transform from and class name in this artifacts.
