---
name: 'Sequence Diagram (SD) Instructions'
description: 'Sequence Diagram (SD) quality requirements and template for project documentation.'
applyTo: '**/uc*.sd.*.md'
---

# SD Instructions (Summary)
- Use the provided SD markdown template or examples.
- Replace all placeholders with project-specific content.
- Store SD files in `docs/use-cases/uc-<Insert Use Case Identifier>*/` as `uc-<Insert Use Case Identifier>.sd.<Insert Version>.md`.
- Increment version numbers for significant changes; keep only the latest version in main, archive older versions.
- Include metadata, version log (with date, author), and use Mermaid sequence diagram.
- Create files in English; if product owner domain language differs, create a separate file with language code suffix.
- Update glossary files for new terms.
- Validate SDs for completeness, clarity, and template compliance.


## SD Template (Minimal):
```markdown
# [Insert Sequence Diagram Title]


## Metadata
| Key            | Value           |
|----------------|-----------------|
| Id             | [Use case].SD   |
| crossReference | [Use case].SSD [Use case].OC   |

## Version Log
| Version | Date       | Description | Author |
|---------|------------|-------------|--------|
| 0001    | [date]     | Initial     | [name] |


## Sequence Diagram
### Presentation Layer → Application Layer
```

```mermaid
sequenceDiagram
    actor [Insert Actor Name] as Actor
    participant A
    participant B
    participant C

    Actor->>+A: [Insert Message 1]
    A->>+B: [Insert Message 2]
    B->>+C: [Insert Message 3]
    C-->>-B: [Insert Message 4]
    B-->>-A: [Insert Message 5]
    A-->>-Actor: [Insert Message 6]
    %% Add more interactions as needed
```

```markdown
### Application Layer → Infrastructure Layer (External Interfaces)
```

```mermaid
sequenceDiagram
    %% Application Layer
    participant A
    participant B

    A->>+B: [Insert Message 1]
    B-->>-A: [Insert Message 2]
```

if there are an WebApi for data access, we can add another sequence diagram for the interactions between Application Layer and Infrastructure Layer (Data Access):

```markdown
### Application Layer → Infrastructure Layer (Data Access)
```

```mermaid
sequenceDiagram
    %% Application Layer
    participant A
    participant B
    A->>+B: [Insert Message 1]
    B-->>-A: [Insert Message 2]
```

```
**Note:** While Strict UML purists argue that actor is not part of sequence diagram, we can use actor in sequence diagram if it helps to clarify the interactions and roles of different participants in the system. The key is to ensure that the diagram remains clear and easy to understand for all stakeholders even it breaks strict UML rules.

---

**Note on DTOs and Data Transformation:**
[Insert any notes regarding the need for Data Transfer Objects (DTOs) or data transformation between layers, if applicable. Provide examples of how data should be transformed if necessary.]

[Show class example if needed, e.g., for a DTO or data transformation]
```



## SD Example: UC-002 Dashboard ResidentNote

```markdown
# UC-002 Dashboard ResidentNote Sequence Diagram

## Metadata
| Key            | Value           |
|----------------|-----------------|
| Id             | UC-002.SD  |
| crossReference | UC-002.SSD UC-002.OC   |

## Version Log
| Version | Date       | Description | Author |
|---------|------------|-------------|--------|
| 0007    | 2026-06-09 | Change to WebApi→Infrastructure Data Access diagram | Team 6 |

## Sequence Diagram
```

```markdown
### WebApi Layer → Infrastructure Layer (Data Access)
```

```mermaid
sequenceDiagram
    participant ResidentNoteService as ResidentNoteService
    participant ResidentNoteManager as ResidentNoteManager
    participant WebApi as WebApi

    ResidentNoteService->>+ResidentNoteManager: GetResidentNotes(residentId)
    ResidentNoteManager->>+WebApi: GET /api/residents/{residentId}/notes
    WebApi-->>-ResidentNoteManager: JSON response with ResidentNotes
    ResidentNoteManager-->>-ResidentNoteService: ResidentNotesDto[]

    ResidentNoteService->>+ResidentNoteManager: AddResidentNote(AddResidentNoteDto)
    ResidentNoteManager->>+WebApi: POST /api/residents/{residentId}/notes
    alt addResidentNote success
        WebApi-->>ResidentNoteManager: 201 Created with ResidentNote details
        ResidentNoteManager-->>ResidentNoteService: ResidentNoteSaved
    else addResidentNote error
        WebApi-->>ResidentNoteManager: 400 Bad Request or 500 Internal Server Error
        ResidentNoteManager-->>ResidentNoteService: error
    end

    ResidentNoteService->>+ResidentNoteManager: EditResidentNote(EditResidentNoteDto)
    ResidentNoteManager->>+WebApi: PUT /api/residents/{residentId}/notes/{residentNoteId}
    alt editResidentNote success
        WebApi-->>ResidentNoteManager: 200 OK with ResidentNote details
        ResidentNoteManager-->>ResidentNoteService: ResidentNoteUpdated
    else editResidentNote error
        WebApi-->>ResidentNoteManager: 400 Bad Request or 500 Internal Server Error
        ResidentNoteManager-->>ResidentNoteService: error
    end

    ResidentNoteService->>+ResidentNoteManager: DeleteResidentNote(residentId, residentNoteId)
    ResidentNoteManager->>+WebApi: DELETE /api/residents/{residentId}/notes/{residentNoteId}
    alt deleteResidentNote success
        WebApi-->>ResidentNoteManager: 200 OK with ResidentNote details
        ResidentNoteManager-->>ResidentNoteService: ResidentNoteDeleted
    else deleteResidentNote error
        WebApi-->>ResidentNoteManager: 400 Bad Request or 500 Internal Server Error
        ResidentNoteManager-->>ResidentNoteService: error
    end
```


```markdown
### Application Layer → Infrastructure Layer (WebAPI)
```

```mermaid
sequenceDiagram
    participant ResidentNoteService as ResidentNoteService
    participant WebApi as WebApi
    participant ResidentNoteRepository as ResidentNoteRepository

    ResidentNoteService->>+WebApi: GetResidentNotes(residentId)
    WebApi->>+ResidentNoteRepository: FetchResidentNotes(residentId)
    ResidentNoteRepository-->>-WebApi: ResidentNotes
    WebApi-->>-ResidentNoteService: ResidentNotesDto[]

    ResidentNoteService->>+WebApi: AddResidentNote(AddResidentNoteDto)
    WebApi->>+ResidentNoteRepository: SaveResidentNote(residentId, noteText)
    alt addResidentNote success
        ResidentNoteRepository-->>WebApi: ResidentNoteSaved
        WebApi-->>ResidentNoteService: ResidentNoteSaved
    else addResidentNote error
        ResidentNoteRepository-->>WebApi: error
        WebApi-->>ResidentNoteService: error
    end

    ResidentNoteService->>+WebApi: EditResidentNote(EditResidentNoteDto)
    WebApi->>+ResidentNoteRepository: UpdateResidentNote(residentId, residentNoteId, newText)
    alt editResidentNote success
        ResidentNoteRepository-->>WebApi: ResidentNoteUpdated
        WebApi-->>ResidentNoteService: ResidentNoteUpdated
    else editResidentNote error
        ResidentNoteRepository-->>WebApi: error
        WebApi-->>ResidentNoteService: error
    end

    ResidentNoteService->>+WebApi: DeleteResidentNote(residentId, residentNoteId)
    WebApi->>+ResidentNoteRepository: DeleteResidentNote(residentId, residentNoteId)
    alt deleteResidentNote success
        ResidentNoteRepository-->>WebApi: ResidentNoteDeleted
        WebApi-->>ResidentNoteService: ResidentNoteDeleted
    else deleteResidentNote error
        ResidentNoteRepository-->>WebApi: error
        WebApi-->>ResidentNoteService: error
    end
```

```markdown
### Presentation Layer → Application Layer
```

```mermaid
sequenceDiagram
    actor Employee as Actor
    participant WebUI as WebUI
    participant ResidentNoteService as ResidentNoteService

    Employee->>+WebUI: selectResident(residentId)
    WebUI->>+ResidentNoteService: GetResidentNotes(residentId)
    ResidentNoteService-->>-WebUI: ResidentNotesDto[]
    WebUI-->>-Employee: showResidentNotes(ResidentNotesDto[])

    Employee->>+WebUI: addResidentNote(residentId, noteText)
    WebUI->>+ResidentNoteService: AddResidentNote(residentId, noteText)
    ResidentNoteService-->>-WebUI: confirmResidentNoteAdded()
    WebUI-->>-Employee: confirmResidentNoteAdded()

    Employee->>+WebUI: editResidentNote(residentId, residentNoteId, newText)
    WebUI->>+ResidentNoteService: EditResidentNote(residentId, residentNoteId, newText)
    ResidentNoteService-->>-WebUI: confirmResidentNoteEdited()
    WebUI-->>-Employee: confirmResidentNoteEdited()

    Employee->>+WebUI: deleteResidentNote(residentId, residentNoteId)
    WebUI->>+ResidentNoteService: DeleteResidentNote(residentId, residentNoteId)
    ResidentNoteService-->>-WebUI: confirmResidentNoteDeleted()
    WebUI-->>-Employee: confirmResidentNoteDeleted()
```

---

**Notes:**
- The WebUI never calls the controller or data access directly; it always calls the Application layer (Service/Handler), which orchestrates all business logic and data access.
- Data Transfer Objects (DTOs) are used between layers to decouple UI and domain models.
- Example: `AddResidentNote(residentId, noteText)` in WebUI is transformed into an `AddResidentNoteDto` when sent to the Application layer, which then passes it to the WebApi.
- Data returned from the database is mapped to DTOs before being sent to the WebUI.
- All data transformations are explicit and documented in the implementation.

**DTO Example:**
```csharp
public class AddResidentNoteDto
{
    public Guid Id { get; set; }
    public string ResidentNote { get; set; }
}

