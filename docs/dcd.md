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

## Domain Entities Diagram

```mermaid
%% Domain Entities Class Diagram
classDiagram
  direction TB

  class Resident {
    +Initials: string
    +TrafficLightStatus: TrafficLightStatus
    #Notes: ICollection~ResidentNote~
  }
  class ResidentNote {
    +Content: string
    +CreatedAt: DateTime
  }
  class TrafficLightStatus {
    <<enumeration>>
    Green
    Yellow
    Red
  }
  class IEntity {
    <<interface>>
    +Id: Guid
  }

  Resident "1" --o "0..*" ResidentNote : has
  Resident "0..*" --o "1" TrafficLightStatus : has
  Resident --|> IEntity : implements
  ResidentNote --|> IEntity : implements
```

### Note:

- The `Resident` entity represents an individual resident in the system, with properties for their initials and traffic light status. It has a collection of `ResidentNote` entities representing notes associated with the resident.
- The `ResidentNote` entity represents a note associated with a resident, containing content and a timestamp for when the note was created.
- The `TrafficLightStatus` enumeration defines the possible traffic light statuses for a resident.

---

## Repositories and Interfaces Diagram

```mermaid
%% Repositories and Interfaces Class Diagram
classDiagram
  direction TB

  class ICrud~TEntity~ {
    <<interface>>
    +AddAsync(entity, cancellationToken): Task~TEntity~
    +AddRangeAsync(entities, cancellationToken): Task~IEnumerable~TEntity~~
    +GetAsync(id, cancellationToken): Task~TEntity?~
    +GetAsync(cancellationToken): Task~IEnumerable~TEntity~~
    +UpdateAsync(entity, cancellationToken): Task
    +UpdateRangeAsync(entities, cancellationToken): Task
    +DeleteAsync(entity, cancellationToken): Task
    +DeleteRangeAsync(entities, cancellationToken): Task
  }
  class IRepository~TEntity~ {
    <<interface>>
    +Entities: IEnumerable~TEntity~
  }
  class IResidentRepository {
    <<interface>>
  }
  class IResidentNoteRepository {
    <<interface>>
  }
  class RepositoryBase~TEntity~ {
    +Entities: IEnumerable~TEntity~
    +AddAsync(entity, cancellationToken): Task~TEntity~
    +AddRangeAsync(entities, cancellationToken): Task~IEnumerable~TEntity~~
    +GetAsync(id, cancellationToken): Task~TEntity?~
    +GetAsync(cancellationToken): Task~IEnumerable~TEntity~~
    +UpdateAsync(entity, cancellationToken): Task
    +UpdateRangeAsync(entities, cancellationToken): Task
    +DeleteAsync(entity, cancellationToken): Task
    +DeleteRangeAsync(entities, cancellationToken): Task
  }
  class ResidentRepository {
  }
  class ResidentNoteRepository {
  }

  ICrud~TEntity~ <|.. IRepository~TEntity~ : inherits
  IRepository~TEntity~ <|.. RepositoryBase~TEntity~ : implements
  IRepository~TEntity~ <|.. IResidentRepository : specializes
  IRepository~TEntity~ <|.. IResidentNoteRepository : specializes
  IResidentRepository <|.. ResidentRepository : implements
  IResidentNoteRepository <|.. ResidentNoteRepository : implements
  RepositoryBase~TEntity~ <|-- ResidentRepository : extends
  RepositoryBase~TEntity~ <|-- ResidentNoteRepository : extends
```

---

This DCD documents the core repository and interface abstractions used in the solution, following Clean Architecture principles. All repositories implement the generic `IRepository<TEntity>` interface, which enforces CRUD operations for domain entities implementing `IEntity`. The `Repository<TEntity>` class provides a base implementation for infrastructure repositories.

> All placeholders have been replaced with project-specific content. See `/docs/quality-criteria/artifact/lld/qc-dcd.0001.md` for quality criteria and `/docs/glosery.md` for glossary updates if class names change from previous artifacts.

---

## Service Interfaces Diagram (Core Layer)

```mermaid
%% Core Service Interfaces Class Diagram
classDiagram
  direction TB

  class IResidentService {
    <<interface>>
    +AddAsync(entity, cancellationToken): Task~Resident~
    +AddRangeAsync(entities, cancellationToken): Task~IEnumerable~Resident~~
    +GetAsync(id, cancellationToken): Task~Resident?~
    +GetAsync(cancellationToken): Task~IEnumerable~Resident~~
    +UpdateAsync(entity, cancellationToken): Task
    +UpdateRangeAsync(entities, cancellationToken): Task
    +DeleteAsync(entity, cancellationToken): Task
    +DeleteRangeAsync(entities, cancellationToken): Task
  }
  class IResidentNoteService {
    <<interface>>
    +GetAllByResidentIdAsync(residentId, cancellationToken): Task~IEnumerable~ResidentNote~~
    +AddAsync(residentId, noteText, cancellationToken): Task~ResidentNote~
    +UpdateAsync(residentId, noteId, newText, cancellationToken): Task
    +DeleteAsync(residentId, noteId, cancellationToken): Task
  }
  IResidentService <|.. ICrud~Resident~ : inherits
```

### Note:

- The `IResidentService` interface provides CRUD operations for the `Resident` entity, inheriting from the generic `ICrud<Resident>` interface.
- The `IResidentNoteService` interface provides note-specific operations for residents, such as adding, updating, and deleting notes, as well as retrieving all notes for a resident.
- The service interfaces are part of the Core layer and define the contracts for business logic related to residents and their notes.
