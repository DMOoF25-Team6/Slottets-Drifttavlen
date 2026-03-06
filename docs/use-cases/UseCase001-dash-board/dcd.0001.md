```markdown
# Domain Class Diagram (DCD) for Slottets-Drifttavlen Resident Dashboard
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | DCD                               |
| crossReference    |                                   |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-02-25 | Initial                  | Team 6     |

## Diagram

```mermaid
%% Domain Class Diagram for Resident Dashboard
classDiagram
    namespace ResidentDashboard {
        class Resident {
            +Initials: string
            +TrafficLightStatus: TrafficLight
            +GetNotes(): Note[]
        }
        class Note {
            +Text: string
            +Author: string
            +GetNoteInfo(): string
        }
        enum TrafficLight {
            Green
            Yellow
            Red
        }
    }
    ResidentDashboard.Resident "1" --> "0..*" ResidentDashboard.Note : has
```

## Notes
- Resident has a traffic light status, which can be green, yellow, or red (see TrafficLight enum).
- Resident can have multiple notes, each containing text, date/time, and author information.
- Initials are only a single letter from the first name, as management wants to preserve resident anonymity. (GDPR-compliant)
