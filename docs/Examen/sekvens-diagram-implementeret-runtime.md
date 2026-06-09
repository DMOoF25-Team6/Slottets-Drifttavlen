# Sequence Diagrams - Implementerede Runtime-flows

## Formål

Disse sekvensdiagrammer viser de faktiske metodekald i projektet. Navnene kan
derfor findes direkte i koden og bruges som breakpoints under en demonstration.

Projektet har ikke ét ensartet flow for alle funktioner:

- Opdatering af en beboer går gennem et Core-service og en HTTP-manager.
- Hentning af vagtoversigten kalder API'et direkte fra Blazor-komponenten.
- Flere API-controllere kalder repositories direkte, mens andre kalder services.

---

## Opdater Beboer - Faktisk Implementeret Flow

Udgangspunktet er administrationssiden for beboere. Brugeren redigerer en
beboer og indsender formularen.

```mermaid
sequenceDiagram
    actor User as Bruger
    participant Page as Residents.razor.cs
    participant Service as ResidentService
    participant Manager as ResidentManager
    participant JwtHandler as JwtAuthorizationMessageHandler
    participant Auth as ASP.NET Authentication/Authorization
    participant Controller as ResidentController
    participant Repository as ResidentRepository / Repository of Resident
    participant Context as AppDbContext / EF Core
    participant Database as MySQL

    User->>Page: HandleFormSubmitAsync()
    activate Page
    Page->>Service: UpdateAsync(_editingId, ResidentUpdateRequestDto)
    activate Service
    Service->>Manager: UpdateAsync(id, resident, ct)
    activate Manager
    Manager->>JwtHandler: HttpClient.PutAsJsonAsync("residents/{id}", entity, ct)
    activate JwtHandler
    JwtHandler->>JwtHandler: SendAsync(request, cancellationToken)<br/>GetTokenAsync()<br/>Tilføj Authorization: Bearer token
    JwtHandler->>Auth: HTTP PUT /residents/{id}
    deactivate JwtHandler
    Auth->>Auth: UseAuthentication() og UseAuthorization()<br/>Kontroller [Authorize(Policy = "ManageResidents")]

    alt Token eller policy afvises
        Auth-->>JwtHandler: 401 Unauthorized eller 403 Forbidden
        JwtHandler-->>Manager: HttpResponseMessage
        Manager-->>Service: throw InvalidOperationException
        Service-->>Page: Exception
        Page-->>User: Vis fejlbesked
    else Godkendt
        Auth->>Controller: Update(id, dto, cancellationToken)
        activate Controller
        Controller->>Repository: GetByIdAsync(id, cancellationToken)
        activate Repository
        Repository->>Context: DbSet.FindAsync(id)
        Context->>Database: SELECT Resident
        Database-->>Context: Resident eller null
        Context-->>Repository: Resident eller null
        Repository-->>Controller: Resident eller null
        deactivate Repository

        alt Beboer findes ikke
            Controller-->>Auth: NotFound()
            Auth-->>JwtHandler: HTTP 404 Not Found
            JwtHandler-->>Manager: HttpResponseMessage
            Manager-->>Service: throw InvalidOperationException
            Service-->>Page: Exception
            Page-->>User: Vis fejlbesked
        else Beboer findes og bruger må redigere
            Controller->>Controller: UserCanManageDepartment(department)<br/>Opdater felter på existing
            Controller->>Repository: UpdateAsync(existing, cancellationToken)
            activate Repository
            Repository->>Context: DbSet.Update(existing)<br/>SaveChangesAsync(cancellationToken)
            Context->>Database: SQL UPDATE Residents
            Database-->>Context: Opdatering gennemført
            Context-->>Repository: SaveChangesAsync returnerer
            Repository-->>Controller: Task completed
            deactivate Repository
            Controller-->>Auth: NoContent()
            deactivate Controller
            Auth-->>JwtHandler: HTTP 204 No Content
            JwtHandler-->>Manager: HttpResponseMessage
            Manager-->>Service: UpdateAsync completed
            deactivate Manager
            Service-->>Page: UpdateAsync completed
            deactivate Service
            Page->>Page: LoadResidentsAsync()
            Page-->>User: Vis opdateret beboerliste
        end
    end
    deactivate Page
```

### Metoder at finde og sætte breakpoints i

1. `Residents.HandleFormSubmitAsync()`
2. `ResidentService.UpdateAsync(Guid, ResidentUpdateRequestDto, CancellationToken)`
3. `ResidentManager.UpdateAsync(Guid, ResidentUpdateRequestDto, CancellationToken)`
4. `JwtAuthorizationMessageHandler.SendAsync(HttpRequestMessage, CancellationToken)`
5. `ResidentController.Update(Guid, ResidentUpdateRequestDto, CancellationToken)`
6. `Repository<Resident>.GetByIdAsync(Guid, CancellationToken)`
7. `Repository<Resident>.UpdateAsync(Resident, CancellationToken)`
8. `AppDbContext.SaveChangesAsync()` kaldes af repositoryet via EF Core

---

## Hent Vagtoversigt - Faktisk Implementeret Flow

Dette flow går direkte fra Blazor-komponenten til API'et. Der er ikke et
klient-side Core-service mellem siden og API'et.

```mermaid
sequenceDiagram
    actor User as Bruger
    participant Page as StaffAssignments.razor.cs
    participant JwtHandler as JwtAuthorizationMessageHandler
    participant Controller as StaffAssignmentController
    participant Service as StaffAssignmentManager<br/>(IStaffAssignmentService)
    participant Repository as StaffAssignmentRepository
    participant Context as AppDbContext / EF Core
    participant Database as MySQL

    User->>Page: Åbn vagtoversigt eller vælg dato/vagt
    activate Page
    Page->>Page: LoadAssignmentsAsync()
    Page->>JwtHandler: GetFromJsonAsync("staff-assignments/list?shiftType=...&assignmentDate=...")
    activate JwtHandler
    JwtHandler->>JwtHandler: SendAsync(request, cancellationToken)<br/>Tilføj Bearer token hvis det findes
    JwtHandler->>Controller: HTTP GET /staff-assignments/list
    deactivate JwtHandler
    activate Controller
    Controller->>Service: GetAssignmentsByShiftAsync((ShiftType)shiftType, DateTime.Parse(assignmentDate))
    activate Service
    Service->>Repository: GetByShiftAsync(shiftType, assignmentDate, cancellationToken)
    activate Repository
    Repository->>Context: DbSet.Include(Resident).Include(Employee)<br/>Where(...).ToListAsync(cancellationToken)
    Context->>Database: SQL SELECT StaffAssignments med relationer
    Database-->>Context: StaffAssignment[]
    Context-->>Repository: StaffAssignment[]
    Repository-->>Service: IEnumerable of StaffAssignment
    deactivate Repository
    Service->>Service: assignments.Select(MapToOverviewDto)
    Service-->>Controller: IEnumerable of AssignmentOverviewDto
    deactivate Service
    Controller-->>JwtHandler: Ok(result) / HTTP 200
    deactivate Controller
    JwtHandler-->>Page: AssignmentOverviewDto[]
    Page->>Page: _assignments = assignments.ToList()
    Page-->>User: Vis vagtoversigt
    deactivate Page
```

### Metoder at finde og sætte breakpoints i

1. `StaffAssignments.LoadAssignmentsAsync()`
2. `JwtAuthorizationMessageHandler.SendAsync(HttpRequestMessage, CancellationToken)`
3. `StaffAssignmentController.GetAssignments(int, string)`
4. `StaffAssignmentManager.GetAssignmentsByShiftAsync(ShiftType, DateTime, CancellationToken)`
5. `StaffAssignmentRepository.GetByShiftAsync(ShiftType, DateTime, CancellationToken)`
6. EF Core-metoden `ToListAsync(CancellationToken)`

---

## Sådan Følges Et Nyt Flow

1. Start ved brugerhandlingen i en `.razor.cs`-fil.
2. Find event-handleren, eksempelvis `HandleFormSubmitAsync()`.
3. Følg hvert metodekald med `Cmd + klik` eller `F12`.
4. Hvis kaldet rammer et interface, find implementeringen i en
   `DependencyInjection.cs`-fil.
5. Når du finder `GetFromJsonAsync`, `PostAsJsonAsync`, `PutAsJsonAsync` eller
   `DeleteAsync`, stopper det lokale call stack. Kaldet fortsætter som HTTP.
6. Match URL'en med API-controllerens `[Route]` og `[HttpGet]`, `[HttpPost]`,
   `[HttpPut]` eller `[HttpDelete]`.
7. Følg controllerens injected service eller repository.
8. Når du finder `SaveChangesAsync`, `ToListAsync`, `FindAsync` eller
   `FirstOrDefaultAsync`, kommunikerer EF Core med MySQL.
9. Følg returværdien baglæns til UI'et.

## Vigtig Arkitektur-Bemærkning

Det tidligere diagram viste `Infrastructure.Data -> WebApi`. Det er ikke det
implementerede runtime-flow. Ved API-kald er retningen:

`WebApi -> service/repository -> Infrastructure.Data -> AppDbContext/EF Core -> MySQL`

Ved HTTP-kald fra WebUI er retningen:

`Blazor WebUI -> Core-service eller direkte HttpClient -> HTTP-manager/JWT-handler -> WebApi`
