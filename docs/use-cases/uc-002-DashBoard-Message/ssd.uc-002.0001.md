## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | SSD-UC-002                        |
| crossReference    | UC-002                            |


## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-03-06 | Initial                  | Team 6     |

## System Sequence Diagram
```mermaid
sequenceDiagram
    actor Employee
    participant System
    Employee ->> System: Select resident
    System -->> Employee: Show current messages
    Employee ->> System: Add message
    alt Add message flow
        System -->> Employee: Confirm message added
    else Add message error
        System -->> Employee: Error adding message cannot be saved
    end
    Employee ->> System: Edit message
    alt Edit message flow
        System -->> Employee: Confirm message edited
    else Edit message error
        System -->> Employee: Error editing message cannot be saved
    end
    Employee ->> System: Delete message
    System -->> Employee: Confirm message deleted
```
