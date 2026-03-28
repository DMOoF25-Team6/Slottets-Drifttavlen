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


## DCD for Domain Layer

```mermaid
%% Design Class Diagram for Solution Repositories and Interfaces
classDiagram
  direction TB

  name

  namespace Domain.Entities {
    class Resident {
      +Initials: string
      +TrafficLightStatus: TrafficLightStatus
      ~Notes: ICollection~ResidentNote~
    }
    class ResidentNote {
      +Note: string
      +EditedAt: DateTime
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

  %% Associations
  Resident "1" --o "0..*" ResidentNote : has

  Resident --* TrafficLightStatus : 

  %% Associations for Interfaces

  %% Interface Implementations
 
  %% Inheritance

  %% Implementation
  Resident --|> IEntity : implements
  ResidentNote --|> IEntity : implements

  %% Service Dependencies

```

---

## DCD for Core Layer

```mermaid
%% Design Class Diagram for Core Layer (Use Cases, Services, DTOs)
classDiagram
  direction TB

  namespace Core.Services {
    class ResidentService {
      -IResidentRepository _residentRepository
      -IResidentApiClient _residentApiClient
    }
    class ResidentNoteService {
      -IResidentNoteRepository _residentNoteRepository
    }
  }

  namespace Core.Interfaces.Services {
    class IResidentNoteService {
      <<interface>>
      +AddAsync(residentId, noteText, cancellationToken): Task~ResidentNote~
      +DeleteAsync(residentId, noteId, cancellationToken): Task
      +GetAllByResidentIdAsync(residentId, cancellationToken): Task~IEnumerable~ResidentNote~~
      +UpdateAsync(residentId, noteId, newText, cancellationToken): Task
    }
  }

  namespace Core.Interfaces.ApiClients {
    class IResidentApiClient {
      <<interface>>
      +GetByIdAsync(id, cancellationToken): Task~Resident?~
      +GetAllAsync(cancellationToken): Task~IEnumerable~Resident~~
    }
  }

  namespace Core.Interfaces.Repositories {
    class IResidentRepository {
      <<interface>>
    }
    class IResidentNoteRepository {
      <<interface>>
    }
  }

  namespace Domain.Entities {
    class Resident {
    }
    class ResidentNote {
    }
  }

  %% Associations
  ResidentService --> IResidentRepository : uses
  ResidentService --> IResidentApiClient : uses
  ResidentNoteService --> IResidentNoteRepository : uses
  ResidentNoteService ..|> IResidentNoteService : implements
  IResidentNoteRepository <|.. IResidentNoteService : dependency
  ResidentNoteService --> ResidentNote : manages
  ResidentService --> Resident : manages
```

---

## DCD for Infrastructure Layer

```mermaid
%% Design Class Diagram for Infrastructure Layer (Repositories, Services)
classDiagram
  direction TB

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

  namespace Infrastructure.Services {
    class ResidentApiClient {
      -HttpClient _httpClient
      +GetByIdAsync(id, cancellationToken): Task~Resident?~
      +GetAllAsync(cancellationToken): Task~IEnumerable~Resident~~
    }
  }

  namespace Domain.Entities {
    class Resident {
    }
    class ResidentNote {
    }
  }

  %% Associations
  %% Repository Associations
  ResidentRepository <|-- Repository~Resident~ : extends
  ResidentNoteRepository <|-- Repository~ResidentNote~ : extends

  ResidentApiClient --> Resident : fetches

  Repository~TEntity~ --> Resident : manages
  Repository~TEntity~ --> ResidentNote : manages
```

---

This DCD documents the core repository and interface abstractions used in the solution, following Clean Architecture principles.
All repositories implement the generic `IRepository<TEntity>` interface, which enforces CRUD operations for domain entities implementing `IEntity`.
The `Repository<TEntity>` class provides a base implementation for infrastructure repositories.


