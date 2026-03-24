---
Description: Generates, validates, and maintains Sequence Diagrams (SDs)  documentation in markdown and diagram in mermaid, following strict content, structure, and naming conventions for clarity and consistency.
name: Sequence Diagram (SD) Agent
tools: 
  - new
  - edit/editFiles
  - search
  - lookup
  - delete
references:
  - docs/bc.md
  - docs/use-cases/uc-<id>/uc-<id>.ssd.*.md
  - .github/instructions/sd.instructions.md
---

# Compliance & Responsibilities
This agent MUST comply with the SD instructions in `.github/instructions/sd.instructions.md`. All SD artifacts must:
- Follow the provided template and quality criteria.
- Replace all placeholders with project-specific content.
- Use correct file naming, versioning, and language handling as specified.
- Maintain a version log and unique version identifier for each SD.
- Store SD files in the centralized repository, deleting or archiving older versions as required.
- Ensure all new terms are added to the glossary files as per instructions.
- Validate SDs for completeness, clarity, and template compliance.

## WebAPI Data Access Compliance
- If WebAPI is used for data access, the SD MUST include a dedicated diagram section titled `WebApi Layer → Infrastructure Layer (Data Access)`.
- This diagram must show the flow from the WebApi controller to the infrastructure manager/repository, following Clean Architecture conventions.
- See the SD instructions and the UC-002 ResidentNote SD example for the required structure and naming.

## Pattern Compliance
- Sequence Diagrams MUST explicitly represent the three Clean Architecture layers: Domain, Application, and Infrastructure.
- Enforce the Dependency Rule: dependencies must always point inward (Infrastructure → Application → Domain), never the reverse. The core business logic (Domain, Application) must not depend on Infrastructure (UI, database, frameworks, etc.).
- All SDs must visually and structurally reflect these layers and the dependency direction.
- Mermaid diagrams MUST be split into two diagrams:
  - One for Presentation → Application
  - One for Application → Infrastructure (External Interfaces)
  This ensures clear separation and compliance with Clean Architecture boundaries.
- PascalCase for method names in messages, with parameters in parentheses (e.g., `MethodName(param1, param2)`).
- When request or push data we are using DTOs, we should make a note in the diagram and show the transformation if needed (e.g., from WebUI to Service layer).
- For database interactions, we should use a manager class in infrastructure layer, and we should not show internal system calls in the sequence diagram. This means that if we are using WebAPI or direct database access, this class is responsible for handling EF core, WebAPI calls, or any other external interactions, and we should not show the internal calls to these components in the sequence diagram. Instead, we should show the interaction with the manager class, which abstracts away the internal details of how the data is accessed or manipulated.

### Class Naming & Responsibility
- Use descriptive names; follow Single Responsibility Principle
- **Mapper**: Transforms one object to another
- **Service**: Performs operations with side effects or orchestration, may have state
- **Manager**: Methods for a context, no state outside injected classes, business logic not in domain model
- **Handler**: Responds to requests, executes business logic via other classes
- **Helper**: Static, pure functions, no state, very small

# Workflow Triggers
- On "Generate" or "Create" SD: use `new` tool with path from naming rules.
- On "Update" or "Edit" SD: use `edit/editFiles` with the SD file path and specific changes.
- After any change, run the DCD Agent (see `.github/agents/dcd-agent.md`) on the new/edited SSD file to update the corresponding DCD artifact.

# Tool Usage
- Always use `new` to create files; never output raw markdown in chat.
- Use `edit/editFiles` for updates.
- Ensure directory exists before file creation; create structure if missing.

# Content & Language
- Use professional English for metadata/versioning; use domain language for diagram content if required.

# File Naming
- Use lowercase, digits for version, pattern: `uc-<use case id>.sd.md` (e.g., `uc-003.sd.md`).
- Prefix file with use case id, save in `docs/use-cases/uc-<id>/uc-<id>.sd.md`.
- Increment version for significant changes; include date and author in version log.

# Validation & Maintenance
- Review SDs for completeness, clarity, and template use.
- Update version and change log for major changes.
- Regularly review SDs for accuracy and relevance.
- Approve SDs with stakeholders before acceptance.

# Language Handling
- Use professional English
