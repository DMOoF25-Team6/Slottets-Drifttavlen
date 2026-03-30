# Quality Criteria: Use Case (UC)
Use Cases are essential for capturing functional requirements and user interactions with a system. A well-crafted Use Case provides clarity, supports development, and ensures alignment with user needs and business goals.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-UC                             |
| crossReference    | Applying UML and patterns by Craig Larman |

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-28 | 0001    | Initial creation of the document | Team 6        |

---

## Quality Criteria for use cases
Always include a user story, use case brief and a use case casual. Optionally you can also include a use case fully dressed. When evaluating a Use Case, consider the following quality criteria:

### General criteria for all use case formats
- Clearly identifies primary actor(s) and their goals
- Describes value delivered to the actor (user-centric)
- Focuses on external, observable behavior (not implementation)
- Is written in simple, unambiguous language
- Is testable and verifiable
- Avoids design or UI details
- Each step is a clear interaction between actor and system
- Includes main success scenario and extensions (alternatives, exceptions)
- Is complete, consistent, and traceable to requirements
- A use case always has a reference to BC, and possibly also to Furps, KPI and risk analysis artifacts.

### Quality Criteria for User Story
- Clearly identifies the user role (type of user)
- States a specific goal or need of the user
- Explains the reason or value behind the goal
- As a [type of user], I want [some goal] so that [some reason].

### Quality Criteria for Use Case Brief
- Summarizes the main success scenario in a few sentences
- Identifies primary actor and goal
- Describes system’s response to the actor’s trigger
- Avoids design or UI details

### Quality Criteria for Use Case Casual
- Lists main success scenario as a sequence of numbered steps
- Includes extensions (alternatives, exceptions) as needed
- Each step is a clear actor-system interaction
- Written in unambiguous, testable language

### Quality Criteria for Use Case Fully Dressed
- Contains all elements: scope, level, stakeholders, pre/postconditions, main/alternate flows
- Each step is explicit and observable
- Includes non-functional requirements and business rules if relevant
- Traceable to requirements and testable

## Authoring Patterns and Templates
For filename conventions, templates, and authoring examples, see `.github/instructions/usecase.instructions.md`.

## Validation
- Review Use Cases for completeness, clarity, and correct use of the template.
- Verify that all placeholders are replaced with project-specific content.
- Ensure that the Use Case is traceable to a business requirement.

## Maintenance

## Language
- Professional
- English
- If product owner domain language is different, use that language for the diagram content while maintaining English for metadata and versioning. And save the file with a language code suffix (e.g., `uc-xxx.da.md` for Danish). So now we have two files: `uc-xxx.md` (English) and e.g., `uc-xxx.da.md` (Danish).
