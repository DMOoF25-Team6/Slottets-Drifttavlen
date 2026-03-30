---
description: 'Domain Class Diagram (DCD) quality requirements and template for project documentation.'
applyTo: '**/docs/dcd.*.md' and '**/use-cases/**/uc*.dcd.*.mdx'
---


# Domain Class Diagram (DCD) Instructions

This instruction file provides the authoritative template, syntax, naming, and quality requirements for documenting Domain Class Diagrams (DCD) in markdown format. All DCD automation agents (such as `.github/agents/dcd-artifact.agent.md`) must strictly follow these instructions for DCD content, structure, and compliance. Any new or changed commands, conventions, or templates introduced by the agent must be reflected here, and vice versa. 

**Note:** All automation/agent workflow and process steps are maintained in `.github/agents/dcd-artifact.agent.md`. This file is focused on standards, templates, and requirements only. Replace all placeholders in the diagram with project-specific content.

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
- Ensure we have DCD for Domain Layer, Application Layer, and Infrastructure Layer in solution DCD. If not, create them with the appropriate namespaces and structure.
- Layers can only reference inward layers, never outward layers. For example, Domain Layer cannot reference Application Layer or Infrastructure Layer, but Application Layer can reference Domain Layer, and Infrastructure Layer can reference both Application and Domain Layers.
- Use valid Mermaid class diagram syntax.
- Use appropriate class diagram notations to represent different types of relationships (e.g., inheritance, association, aggregation).
- Include notes or comments in the diagram to clarify complex relationships or design decisions.

## Code Standards
- Each DCD must have a unique version identifier and a documented change log.
- Use the provided Mermaid diagram layout for consistency.

### File Naming
- Name files in lowercase
  - following the pattern in DCD model for use case: `dcd.md` (e.g., `dcd.md`).
    - for use case DCD models, include the use case identifier in the file name as a prefix.
      - save files for use case DCD models in a subfolder named after the use case (e.g., `docs/use-cases/uc-001/uc-001.dcd.md`).
    - for solution DCD models, do not include a use case identifier in the file name.
      - save files for solution DCD models in the main `docs` folder (e.g., `docs/dcd.md`).
- Increment version numbers for significant changes.
- Include the todays date and author in the version log.

## Namespaces and Folder Structure
All Mermaid class diagrams **must** use namespaces that match the Clean Architecture folder structure (excluding `src`).

**Folder to namespace mapping:**
- `Domain/Entities/` → `Domain.Entities`
- `Domain/Enums/` → `Domain.Enums`
- `Domain/Interfaces/` → `Domain.Interfaces`
- `Domain/Attributes/` → `Domain.Attributes`
- `Application/DTOs/` → `Application.DTOs`
- `Application/Handlers/` → `Application.Handlers`
- `Application/Helpers/` → `Application.Helpers`
- `Application/Interfaces/` → `Application.Interfaces`
- `Application/Managers/` → `Application.Managers`
- `Application/Mappers/` → `Application.Mappers`
- `Application/Services/` → `Application.Services`
- `Infrastructure/Persistents/` → `Infrastructure.Persistents`
- `Infrastructure/Persistents/Configurations/` → `Infrastructure.Persistents.Configurations`
- `Infrastructure/Repositories/` → `Infrastructure.Repositories`
- `WebUI/` → `WebUI`

Each class in the diagram should be placed in the appropriate namespace according to its folder. If solution use Core instead of Application then use Core. Do **not** include `src` in the namespace. This ensures diagrams are consistent with the codebase structure and Clean Architecture principles.

## DM Template
### Minimal Template:
```markdown
# Domain Class Diagram (DCD) for [Insert Project Name]
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | [Use case identifier].DCD            |
| crossReference    |                                   |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | [yyyy-mm-dd] | Initial                  | Team 6     |
```

```mermaid
%% Design Class Diagram Template: Use namespaces matching Clean Architecture folders (excluding src)
classDiagram
  namespace Domain.Entities {
    class [EntityClass] {
      +[Attribute1]: [Type]
      +[Method1]([params]): [ReturnType]
    }
  }
  namespace Core.DTOs {
    class [DtoClass] {
      +[Attribute1]: [Type]
    }
  }
  namespace Infrastructure.Repositories {
    class [RepositoryClass] {
      +[Method1]([params]): [ReturnType]
    }
  }
  [EntityClass] o-- [DtoClass] : [Association]
  [RepositoryClass] *-- [EntityClass] : [Composition]
```

### Example Diagram (Minimal) for a simple DCD with an entity, a DTO, and a repository:
```mermaid
classDiagram
  namespace Domain.Interfaces {
    class IEntity {
      <<interface>>
      +Id: Guid
    }
  }
  namespace Domain.Entities {
    class User {
      +Username: string
      +Email: string
    }
    class UserRole {
      <<enumeration>>
      Admin
      User
      Guest
    }
  }

  namespace Application.DTOs {
    class UserDto {
      +Id: Guid
      +Username: string
      +Email: string
    }
  }
  namespace Infrastructure.Repositories {
    class UserRepository
  }
  %% Associations

  %% Associations DTOs
  User o-- UserDto : maps to
  %% Repository Associations
  UserRepository *-- User : persists
  %% Interface Implementations
  User <|-- IEntity : implements
  %% Interface Inheritance
  %% Inheritance and Implementation
  %% Service Dependencies

```

### Example Diagram (Complex) for a DCD with multiple entities, relationships, and namespaces:
```mermaid
classDiagram
  %% Associations
  %% Associations DTOs
  %% Repository Associations
  %% Interface Implementations
  %% Interface Inheritance
  %% Inheritance and Implementation
  %% Service Dependencies
```

## accepted parts of the DCD syntax:
<|--	Inheritance
*--	Composition
o--	Aggregation
-->	Association
--	Link (Solid)
..>	Dependency
..|>	Realization
..	Link (Dashed)

if link is between entities then we need multiplicities ala
"0..1" -- "*"
"1" -- "1..*"

## Language 
- Professional
- English

## Class object
if class object changes name form artifacts before then make / update glossary `/docs/glossary.md` with class name in artifacts we transform from and class name in this artifacts.
