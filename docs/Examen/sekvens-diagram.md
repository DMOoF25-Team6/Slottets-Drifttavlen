# Sequence Diagram – Shift Change and Citizen Status Update

## Description

Dette sekvensdiagram beskriver dataflowet under et vagtskifte, hvor en medarbejder ser aktuelle opgaver og opdaterer en borger/beboerstatus i systemet.

Diagrammet viser, hvordan frontend, API, tjenester, repositories og database interagerer under processen.

Flowet følger den systemarkitektur, der blev anvendt i projektet:
- Blazor WebUI frontend
- ASP.NET Core API-controllere
- Servicelag
- Repository/Datalag
- MySQL-database med Entity Framework Core

---

## Mermaid Sequence Diagram


## Presentation → Application


```mermaid
sequenceDiagram
    actor StaffMember as Staff Member

    participant StaffAssignmentsPage as Presentation Layer
    participant StaffAssignmentService as Application Layer

    StaffMember->>+StaffAssignmentsPage: OpenShiftAssignments()

    StaffAssignmentsPage->>+StaffAssignmentService: GetAssignments(shiftDto)

    alt Success
        StaffAssignmentService-->>StaffAssignmentsPage: AssignmentOverviewDto[]

    else NoAssignmentsFound
        StaffAssignmentService-->>StaffAssignmentsPage: EmptyResult()

    else DataAccessError
        StaffAssignmentService-->>StaffAssignmentsPage: Error(message)
    end

    StaffMember->>+StaffAssignmentsPage: CreateAssignment()

    StaffAssignmentsPage->>+StaffAssignmentService: CreateAssignment(staffAssignmentDto)

    alt Success
        StaffAssignmentService-->>StaffAssignmentsPage: AssignmentOverviewDto
        StaffAssignmentsPage-->>StaffAssignmentsPage: RefreshAssignmentOverview()

    else ValidationError
        StaffAssignmentService-->>StaffAssignmentsPage: Error(message)
    end
```


### WebApi Layer → Infrastructure Layer (Data Access)
```mermaid
sequenceDiagram
    participant StaffAssignmentService as Application Layer
    participant StaffAssignmentManager as Infrastructure Layer
    participant WebApi as WebApi

    StaffAssignmentService->>StaffAssignmentManager: GetAssignments(shiftDto)

    StaffAssignmentManager->>WebApi: GET /staff-assignments/list

    alt Success
        WebApi-->>StaffAssignmentManager: 200 OK (AssignmentOverviewDto[])
        StaffAssignmentManager-->>StaffAssignmentService: AssignmentOverviewDto[]

    else Error
        WebApi-->>StaffAssignmentManager: 4xx/5xx (Error)
        StaffAssignmentManager-->>StaffAssignmentService: Error(message)
    end

    StaffAssignmentService->>StaffAssignmentManager: CreateAssignment(staffAssignmentDto)

    StaffAssignmentManager->>WebApi: POST /staff-assignments

    alt Success
        WebApi-->>StaffAssignmentManager: 200 OK (AssignmentOverviewDto)
        StaffAssignmentManager-->>StaffAssignmentService: AssignmentOverviewDto

    else ValidationError
        WebApi-->>StaffAssignmentManager: 400 BadRequest
        StaffAssignmentManager-->>StaffAssignmentService: Error(message)
    end
```
---

## Notes
- Scope: indlæsning og opdatering af staff assignments under vagtskifte.
- Presentation Layer anmoder om assignment-data og viser aktuelle ansvarsområder.
- Staff assignments opdateres gennem WebApi-laget.
- Infrastrukturkommunikation abstraheres gennem manager-klasser.
- DTO’er anvendes på tværs af arkitektoniske lag.
- Beskyttede endpoints kræver JWT authentication og authorization.
- Arkitekturen følger Clean Architecture dependency direction.

## Compliance
- Følger Clean Architecture-principper.
- Benytter Mermaid sequence diagrams.
- Følger projektets dokumentationsstruktur.
- Versionslog vedligeholdes.