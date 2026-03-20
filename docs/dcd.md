# Domain Class Diagram (DCD) for Solution Repositories and Interfaces

## Metadata
| Key            | Value                         |
|----------------|-------------------------------|
| Id             | DCD                           |
| crossReference | DM                            |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-03-06 | Initial                  | Team 6     |

---

## Diagram for repositories and interfaces

```mermaid
%% Design Class Diagram for Solution Repositories and Interfaces
classDiagram
  direction TB

  namespace Domain.Entities {
    class Resident {
      +Initials: string
      +TrafficLightStatus: TrafficLightStatus
      #Notes: ICollection~ResidentNote~
    }
    class TrafficLightStatus {
      <<enumeration>>
      Green
      Yellow
      Red
    }
    class ResidentNote {
      +Content: string
      +CreatedAt: DateTime
    }
  }

  namespace Domain.Enums {
    class TrafficLightStatus {
      <<enumeration>>
      Green
      Yellow
      Red
    }
  }

  namespace Domain.Interfaces {
    class IEntity {
      <<interface>>
      +Id: Guid
    }
  }

  namespace Core.Interfaces.Repositories {
    class IRepository~TEntity~ {
      <<interface>>
      +Entities: IEnumerable~TEntity~
      +AddAsync(entity, cancellationToken): Task~TEntity~
      +AddRangeAsync(entities, cancellationToken): Task~IEnumerable~TEntity~~
      +GetByIdAsync(id, cancellationToken): Task~TEntity?~
      +GetAllAsync(cancellationToken): Task~IEnumerable~TEntity~~
      +UpdateAsync(entity, cancellationToken): Task
      +UpdateRangeAsync(entities, cancellationToken): Task
      +DeleteAsync(entity, cancellationToken): Task
      +DeleteRangeAsync(entities, cancellationToken): Task
    }
    class IResidentRepository {
      <<interface>>
    }
    class IResidentNoteRepository {
      <<interface>>
    }
  }

  namespace Infrastructure.Repositories {
    class Repository~TEntity~ {
      +Entities: IEnumerable~TEntity~
      +AddAsync(entity, cancellationToken): Task~TEntity~
      +AddRangeAsync(entities, cancellationToken): Task~IEnumerable~TEntity~~
      +GetByIdAsync(id, cancellationToken): Task~TEntity?~
      +GetAllAsync(cancellationToken): Task~IEnumerable~TEntity~~
      +UpdateAsync(entity, cancellationToken): Task
      +UpdateRangeAsync(entities, cancellationToken): Task
      +DeleteAsync(entity, cancellationToken): Task
      +DeleteRangeAsync(entities, cancellationToken): Task
    }
    class ResidentRepository {
    }
    class ResidentNoteRepository {
    }
  }

  %% Associations
  Resident "0..*" --o "1" TrafficLightStatus : has
  Resident "1" --o "0..*" ResidentNote : has

  %% Repository Associations
  Repository~TEntity~ <|-- ResidentRepository : extends
  Repository~TEntity~ <|-- ResidentNoteRepository : extends
  
  %% Interface Implementations
  IRepository~TEntity~ <|.. Repository~TEntity~ : implements
  IResidentRepository <|.. ResidentRepository : implements
  IResidentNoteRepository <|.. ResidentNoteRepository : implements
  
  %% Interface Inheritance
  IRepository~TEntity~ <|.. IResidentRepository : Inheritance
  IRepository~TEntity~ <|.. IResidentNoteRepository : Inheritance

  %% Inheritance and Implementation
  Resident --|> IEntity : implements
  ResidentNote --|> IEntity : implements

  %% Service Dependencies

```

---

This DCD documents the core repository and interface abstractions used in the solution, following Clean Architecture principles. All repositories implement the generic `IRepository<TEntity>` interface, which enforces CRUD operations for domain entities implementing `IEntity`. The `Repository<TEntity>` class provides a base implementation for infrastructure repositories.

> All placeholders have been replaced with project-specific content. See `/docs/quality-criteria/artifact/lld/qc-dcd.0001.md` for quality criteria and `/docs/glosery.md` for glossary updates if class names change from previous artifacts.
