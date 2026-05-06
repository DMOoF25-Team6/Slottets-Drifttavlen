# Entity Relationship Diagram: UC-014 Citizen Administration

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | ERD-UC-014                        |
| crossReference    | DCD-UC-014, UC-014                |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-05-03 | Initial                  | Team 6     |

---

```mermaid
erDiagram
    Resident {
        Guid Id PK
        string Initials
        string FirstName
        string LastName
        string TrafficLightStatus
    }
    ResidentNote {
        Guid Id PK
        Guid ResidentId FK
        string Note
        DateTime EditedAt
    }
    MedicineRecord {
        Guid Id PK
        Guid ResidentId FK
        string MedicineName
        DateTime Timestamp
        bool Given
    }
    PainkillerRecord {
        Guid Id PK
        Guid ResidentId FK
        string Type
        DateTime GivenAt
        DateTime NextAllowedTime
    }
    Resident ||--o{ ResidentNote : has
    Resident ||--o{ MedicineRecord : has
    Resident ||--o{ PainkillerRecord : has
```

---

- All entities, attributes, and relationships are derived from the UC-014 DCD.
- TrafficLightStatus is represented as a string attribute for ERD simplicity.
- Foreign keys (ResidentId) are added to related records for relational clarity.
