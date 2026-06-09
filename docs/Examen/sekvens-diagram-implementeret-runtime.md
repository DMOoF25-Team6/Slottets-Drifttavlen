# Sequence Diagrams - Implementerede Runtime-flows

## Formål

Diagrammerne viser projektets faktiske metodekald. De er opdelt ved HTTP-grænsen,
så teksten kan læses i VS Codes Markdown Preview uden kraftig zoom.

- **Client-side:** Koden kører i Blazor WebAssembly i browseren.
- **Server-side:** Koden kører i ASP.NET Core API'et.
- **Grænsen:** Et HTTP-request sendes fra clienten til API'et.

Det oprindelige, planlagte sekvensdiagram er bevaret separat i
`docs/Examen/sekvens-diagram.md`.

---

## Opdater Beboer - Samlet Oversigt

```mermaid
%%{init: {"themeVariables": {"fontSize": "18px"}, "sequence": {"useMaxWidth": false}}}%%
sequenceDiagram
    actor User as Bruger
    participant Client as Blazor Client
    participant API as ASP.NET Core API
    participant DB as MySQL

    User->>Client: HandleFormSubmitAsync()
    Client->>API: HTTP PUT /residents/{id}<br/>Authorization: Bearer token
    API->>DB: SELECT og UPDATE Resident
    DB-->>API: Opdatering gennemført
    API-->>Client: HTTP 204 No Content
    Client-->>User: Vis opdateret beboerliste
```

## Opdater Beboer - Client-side

Alt i dette diagram kører i browseren. Det sidste kald sender requestet over
client/server-grænsen.

```mermaid
%%{init: {"themeVariables": {"fontSize": "18px"}, "sequence": {"useMaxWidth": false}}}%%
sequenceDiagram
    actor User as Bruger
    participant Page as Residents.razor.cs
    participant Service as ResidentService
    participant Manager as ResidentManager
    participant JWT as JwtAuthorizationMessageHandler
    participant API as API-grænse

    User->>Page: HandleFormSubmitAsync()
    Page->>Service: UpdateAsync(_editingId, ResidentUpdateRequestDto)
    Service->>Manager: UpdateAsync(id, resident, ct)
    Manager->>JWT: HttpClient.PutAsJsonAsync("residents/{id}", entity, ct)
    JWT->>JWT: SendAsync(request, cancellationToken)
    JWT->>JWT: tokenStorageService.GetTokenAsync()
    JWT->>JWT: Tilføj Authorization: Bearer token
    JWT->>API: HTTP PUT /residents/{id}

    alt API svarer 204 No Content
        API-->>JWT: HttpResponseMessage
        JWT-->>Manager: HttpResponseMessage
        Manager-->>Service: UpdateAsync completed
        Service-->>Page: UpdateAsync completed
        Page->>Page: LoadResidentsAsync()
        Page-->>User: Vis opdateret beboerliste
    else API svarer 4xx
        API-->>JWT: HttpResponseMessage
        JWT-->>Manager: HttpResponseMessage
        Manager-->>Service: throw InvalidOperationException
        Service-->>Page: Exception
        Page-->>User: Vis fejlbesked
    end
```

### Client-metoder at finde

1. `Residents.HandleFormSubmitAsync()`
2. `ResidentService.UpdateAsync(Guid, ResidentUpdateRequestDto, CancellationToken)`
3. `ResidentManager.UpdateAsync(Guid, ResidentUpdateRequestDto, CancellationToken)`
4. `JwtAuthorizationMessageHandler.SendAsync(HttpRequestMessage, CancellationToken)`

## Opdater Beboer - Server-side

Dette diagram begynder, når API'et modtager `PUT /residents/{id}`.

```mermaid
%%{init: {"themeVariables": {"fontSize": "18px"}, "sequence": {"useMaxWidth": false}}}%%
sequenceDiagram
    participant Client as Blazor Client
    participant Auth as Authentication / Authorization
    participant Controller as ResidentController
    participant Repo as Repository of Resident
    participant EF as AppDbContext / EF Core
    participant DB as MySQL

    Client->>Auth: HTTP PUT /residents/{id}<br/>Bearer token + ResidentUpdateRequestDto
    Auth->>Auth: UseAuthentication()
    Auth->>Auth: UseAuthorization()<br/>ManageResidents-policy

    alt Token eller policy afvises
        Auth-->>Client: HTTP 401 eller 403
    else Godkendt
        Auth->>Controller: Update(id, dto, cancellationToken)
        Controller->>Repo: GetByIdAsync(id, cancellationToken)
        Repo->>EF: DbSet.FindAsync(id)
        EF->>DB: SQL SELECT Resident
        DB-->>EF: Resident eller null
        EF-->>Repo: Resident eller null
        Repo-->>Controller: Resident eller null

        alt Beboer findes ikke
            Controller-->>Client: NotFound() / HTTP 404
        else Beboer findes
            Controller->>Controller: UserCanManageDepartment(department)
            Controller->>Controller: Opdater felter på existing
            Controller->>Repo: UpdateAsync(existing, cancellationToken)
            Repo->>EF: DbSet.Update(existing)
            Repo->>EF: SaveChangesAsync(cancellationToken)
            EF->>DB: SQL UPDATE Residents
            DB-->>EF: Opdatering gennemført
            EF-->>Repo: SaveChangesAsync returnerer
            Repo-->>Controller: Task completed
            Controller-->>Client: NoContent() / HTTP 204
        end
    end
```

### Server-metoder at finde

1. `ResidentController.Update(Guid, ResidentUpdateRequestDto, CancellationToken)`
2. `ResidentController.UserCanManageDepartment(Department)`
3. `Repository<Resident>.GetByIdAsync(Guid, CancellationToken)`
4. `Repository<Resident>.UpdateAsync(Resident, CancellationToken)`
5. EF Core-metoderne `FindAsync()` og `SaveChangesAsync()`

---

## Hent Vagtoversigt - Client til API

Vagtoversigten kalder API'et direkte fra Blazor-komponenten. Den bruger ikke et
client-side Core-service til dette request.

```mermaid
%%{init: {"themeVariables": {"fontSize": "18px"}, "sequence": {"useMaxWidth": false}}}%%
sequenceDiagram
    actor User as Bruger
    participant Page as StaffAssignments.razor.cs
    participant JWT as JwtAuthorizationMessageHandler
    participant API as StaffAssignmentController

    User->>Page: Åbn vagtoversigt eller vælg dato/vagt
    Page->>Page: LoadAssignmentsAsync()
    Page->>JWT: GetFromJsonAsync("staff-assignments/list?...")
    JWT->>JWT: SendAsync(request, cancellationToken)
    JWT->>JWT: Tilføj Bearer token hvis det findes
    JWT->>API: HTTP GET /staff-assignments/list
    API-->>JWT: HTTP 200 + AssignmentOverviewDto[]
    JWT-->>Page: AssignmentOverviewDto[]
    Page->>Page: _assignments = assignments.ToList()
    Page-->>User: Vis vagtoversigt
```

## Hent Vagtoversigt - API til Database

```mermaid
%%{init: {"themeVariables": {"fontSize": "18px"}, "sequence": {"useMaxWidth": false}}}%%
sequenceDiagram
    participant API as StaffAssignmentController
    participant Service as StaffAssignmentManager
    participant Repo as StaffAssignmentRepository
    participant EF as AppDbContext / EF Core
    participant DB as MySQL

    API->>Service: GetAssignmentsByShiftAsync((ShiftType)shiftType,<br/>DateTime.Parse(assignmentDate))
    Service->>Repo: GetByShiftAsync(shiftType, assignmentDate, cancellationToken)
    Repo->>EF: DbSet.Include(Resident).Include(Employee)
    Repo->>EF: Where(...).ToListAsync(cancellationToken)
    EF->>DB: SQL SELECT StaffAssignments med relationer
    DB-->>EF: StaffAssignment[]
    EF-->>Repo: StaffAssignment[]
    Repo-->>Service: IEnumerable of StaffAssignment
    Service->>Service: assignments.Select(MapToOverviewDto)
    Service-->>API: IEnumerable of AssignmentOverviewDto
    API-->>API: Ok(result)
```

### Vagtoversigt-metoder at finde

1. `StaffAssignments.LoadAssignmentsAsync()`
2. `JwtAuthorizationMessageHandler.SendAsync(HttpRequestMessage, CancellationToken)`
3. `StaffAssignmentController.GetAssignments(int, string)`
4. `StaffAssignmentManager.GetAssignmentsByShiftAsync(ShiftType, DateTime, CancellationToken)`
5. `StaffAssignmentRepository.GetByShiftAsync(ShiftType, DateTime, CancellationToken)`
6. EF Core-metoden `ToListAsync(CancellationToken)`

---

## Sådan Følges Et Nyt Flow Uden Debugging

1. Start ved brugerhandlingen i en `.razor.cs`-fil.
2. Find event-handleren, eksempelvis `HandleFormSubmitAsync()`.
3. Brug **Peek Call Hierarchy**, **Find All References** og `Cmd + klik`.
4. Hvis kaldet rammer et interface, brug **Go to Implementations**.
5. Når du finder `GetFromJsonAsync`, `PostAsJsonAsync`, `PutAsJsonAsync` eller
   `DeleteAsync`, har du fundet client/server-grænsen.
6. Match URL'en med API-controllerens `[Route]` og HTTP-attribut.
7. Følg controllerens injected service eller repository.
8. `SaveChangesAsync`, `ToListAsync`, `FindAsync` og `FirstOrDefaultAsync`
   markerer normalt kommunikationen gennem EF Core til MySQL.

## Vigtig Arkitektur-Bemærkning

Ved HTTP-kald fra WebUI:

`Blazor Client -> Core-service eller direkte HttpClient -> JWT-handler -> WebApi`

Ved API-kald mod databasen:

`WebApi -> service/repository -> Infrastructure.Data -> AppDbContext/EF Core -> MySQL`
