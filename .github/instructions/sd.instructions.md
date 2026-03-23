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
```

```mermaid
sequenceDiagram
    actor [Insert Actor Name] as Actor
    participant A as [Insert Participant A]
    participant B as [Insert Participant B]
    participant C as [Insert Participant C]

    A->>+B: [Insert Message 1]
    B->>+C: [Insert Message 2]
    C-->>-B: [Insert Message 3]
    B-->>-A: [Insert Message 4]
    %% Add more interactions as needed
```

```
**Note:** While Strict UML purists argue that actor is not part of sequence diagram, we can use actor in sequence diagram if it helps to clarify the interactions and roles of different participants in the system. The key is to ensure that the diagram remains clear and easy to understand for all stakeholders even it breaks strict UML rules.

---

**Note on DTOs and Data Transformation:**
[Insert any notes regarding the need for Data Transfer Objects (DTOs) or data transformation between layers, if applicable. Provide examples of how data should be transformed if necessary.]

[Show class example if needed, e.g., for a DTO or data transformation]
```

## SD Example

```markdown
# Use case 003 - Request Medical Record and Painkiller Record Sequence Diagram

## Metadata
| Key            | Value           |
|----------------|-----------------|
| Id             | UC-003.SD  |
| crossReference | UC-003.SSD UC-003.OC   |

## Version Log
| Version | Date       | Description | Author |
|---------|------------|-------------|--------|
| 0001    | 23.03.2026 | Initial     | Team 6 |

## Sequence Diagram
### Presentation → Application
```

```mermaid
sequenceDiagram
    actor Timer as Timer
    participant WebUI as WebUI
    participant Application.Service as MedicalService

    Timer->>+WebUI: Trigger Request
    WebUI->>+MedicalService: GetMedicalRecord(ResidentId, TimeSpan)
    MedicalService-->>-WebUI: Return MedicalRecord
    WebUI->>+MedicalService: GetPainkillerRecord(ResidentId, TimeSpan)
    MedicalService-->>-WebUI: Return PainkillerRecord
```

```markdown
### Application → Infrastructure - External Interfaces
```

```mermaid
sequenceDiagram
    %% Application Layer
    participant Application.Service as MedicalService

    %% Infrastructure - External Interfaces
    participant Infrastructure.Managers as MedicalManager
    participant WebAPI.Controllers as MedicalController

    %% Infrastructure - Data Access
    participant Infrastructure.Repositories as Repositories
    participant Infrastructure.Persistance as Persistance
    participant Database as Database

    %% Flow: MedicalRecord
    MedicalService->>+MedicalManager: GetMedicalRecord(ResidentId, TimeSpan)
    MedicalManager->>+MedicalController: GetMedicalRecord(ResidentId, TimeSpan)
    MedicalController->>+Repositories: GetAsync(ResidentId, TimeSpan)
    Repositories->>+Persistance: DbSet<MedicalRecord>.Find(ResidentId, TimeSpan)
    Persistance-->>+Database: Query MedicalRecord
    Database-->>-Persistance: Return MedicalRecord
    Persistance-->>-Repositories: Return MedicalRecord
    Repositories-->>-MedicalController: Return MedicalRecord
    MedicalController-->>-MedicalManager: Return MedicalRecord
    MedicalManager-->>-MedicalService: Return MedicalRecord

    %% Flow: PainkillerRecord
    MedicalService->>+MedicalManager: GetPainkillerRecord(ResidentId, TimeSpan)
    MedicalManager->>+MedicalController: GetPainkillerRecord(ResidentId, TimeSpan)
    MedicalController->>+Repositories: GetAsync(ResidentId, TimeSpan)
    Repositories->>+Persistance: DbSet<PainkillerRecord>.Find(ResidentId, TimeSpan)
    Persistance-->>+Database: Query PainkillerRecord
    Database-->>-Persistance: Return PainkillerRecord
    Persistance-->>-Repositories: Return PainkillerRecord
    Repositories-->>-MedicalController: Return PainkillerRecord
    MedicalController-->>-MedicalManager: Return PainkillerRecord
    MedicalManager-->>-MedicalService: Return PainkillerRecord
```

```markdown
**Note:** This sequence diagram illustrates the interactions between various components of the system when a timer triggers a request for medical records and painkiller records. The diagram shows how the WebUI interacts with the MedicalService, which in turn interacts with the MedicalManager, MedicalController, Repositories, Persistance layer, and Database to retrieve the necessary information.

**Note on DTOs and Data Transformation:**
In this example, if there is a need for Data Transfer Objects (DTOs) to facilitate data transfer between layers, we can define DTOs for MedicalRecord and PainkillerRecord. For instance, we might have a `MedicalRecordDTO` that contains only the necessary fields required by the WebUI, and a `PainkillerRecordDTO` for the painkiller information. The MedicalController can be responsible for transforming the data from the database entities to the DTOs before returning them to the MedicalManager and ultimately to the WebUI.
```

