# Domain Class Diagram (DCD) for Solution Repositories and Interfaces

## Metadata
| Key            | Value                         |
|----------------|-------------------------------|
| Id             | DCD                           |
| crossReference | DM                            |

## Version Log
| Version | Date       | Description                        | Author     |
|---------|------------|------------------------------------|------------|
| 0001    | 2026-03-06 | Initial                            | Team 6     |
| 0002    | 2026-03-30 | Add login/auth classes from UC-004 | Team 6     |

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
      -IResidentManager _ResidentManager
    }
    class ResidentNoteService {
      -IResidentNoteRepository _residentNoteRepository
    }
    class LoginService {
      +LoginAsync(username: string, password: string): LoginResult
    }
    class NotificationHandler {
      +Info(message: string)
      +Error(message: string)
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
    class IResidentManager {
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

  namespace Core.DTOs {
    class LoginResult {
      +Success: bool
      +JwtToken: string
    }
    class AuthResult {
      +Success: bool
      +User: object
    }
    class CredentialsDto {
      +Username: string
      +Password: string
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
  ResidentService --> IResidentManager : uses
  ResidentNoteService --> IResidentNoteRepository : uses

  ResidentNoteService ..|> IResidentNoteService : implements
  IResidentNoteRepository <|.. IResidentNoteService : dependency
  ResidentNoteService --> ResidentNote : manages
  ResidentService --> Resident : manages
  LoginService --> NotificationHandler : notifies
  LoginService --> LoginResult : returns
  LoginService --> CredentialsDto : receives
  NotificationHandler --> LoginService : notifies
```

---

## DCD for Infrastructure Layer

```mermaid
%% Design Class Diagram for Infrastructure Layer (Repositories, Services, Identity)
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

  namespace Infrastructure.Managers {
    class ResidentManager {
      -HttpClient _httpClient
      +GetByIdAsync(id, cancellationToken): Task~Resident?~
      +GetAllAsync(cancellationToken): Task~IEnumerable~Resident~~
    }

    class AuthManager {
      +AuthenticateAsync(username: string, password: string): AuthResult
      +GenerateJwtToken(user: object): string
    }
  }

  namespace WebApi.Controllers {
    class LoginApiController {
      +Login(CredentialsDto): LoginResult
    }
  }

  %% External dependencies (Identity framework abstractions)
  class Microsoft.AspNetCore.Identity.SignInManager {
    <<external>>
  }
  class Microsoft.AspNetCore.Identity.UserManager {
    <<external>>
  }
  class System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler {
    <<external>>
  }

  namespace Domain.Entities {
    class Resident {
    }
    class ResidentNote {
    }
  }

%% Associations
ResidentRepository <|-- Repository~Resident~ : extends
ResidentNoteRepository <|-- Repository~ResidentNote~ : extends
ResidentManager --> Resident : fetches
Repository~TEntity~ --> Resident : manages
Repository~TEntity~ --> ResidentNote : manages
LoginApiController --> AuthManager : uses
LoginApiController --> LoginService : uses

%% Associations for Interfaces

%% Interface Implementations

%% Inheritance

%% Implementation
AuthManager --> AuthResult : returns
AuthManager --> LoginResult : issues

%% Service Dependencies
AuthManager ..> Microsoft.AspNetCore.Identity.SignInManager : depends
AuthManager ..> Microsoft.AspNetCore.Identity.UserManager : depends
AuthManager ..> System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler : depends
```

---

This DCD documents the core repository and interface abstractions used in the solution, following Clean Architecture principles.

All repositories implement the generic `IRepository<TEntity>` interface, which enforces CRUD operations for domain entities implementing `IEntity`.
The `Repository<TEntity>` class provides a base implementation for infrastructure repositories.

---

## DCD for WebApi Layer

```mermaid
%% Design Class Diagram for WebApi Layer (Controllers, Endpoints)
classDiagram
  direction TB

  namespace WebApi.Controllers {
    class LoginApiController {
      +Login(CredentialsDto): LoginResult
    }
    class ResidentApiController {
      +GetResident(id: Guid): Resident
      +GetAllResidents(): List~Resident~
    }
  }

  %% Associations
  LoginApiController --> Infrastructure.Identity.AuthManager : uses
  ResidentApiController --> Core.Services.ResidentService : uses
```

---

## DCD for WebUI Layer

```mermaid
%% Design Class Diagram for WebUI Layer (Pages, Components)
classDiagram
  direction TB

  namespace WebUI.Pages {
    class LoginPage {
      +EnterCredentials(username: string, password: string)
      +ShowLoginError()
      +RedirectToDashboard(jwtToken: string)
    }
    class DashboardPage {
      +DisplayResidents(residents: List~Resident~)
    }
  }

  namespace WebUI.Components {
    class NotificationComponent {
      +ShowInfo(message: string)
      +ShowError(message: string)
    }
  }

  %% Associations
  LoginPage --> NotificationComponent : uses
  DashboardPage --> NotificationComponent : uses
  NotificationComponent --> Core.Services.NotificationHandler : subscribes
```


