## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | OC-UC-002                         |
| crossReference    | SSD-UC-002                        |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-03-06 | Initial                  | Team 6     |

## Object Constract

### Select Resident
- **Preconditions**: Employee is authenticated and has access to the dashboard.
- **Postconditions**: Current messages for the selected resident are displayed.


### Add Message
- **Preconditions**: Resident is selected; Employee has permission to add messages.
- **Postconditions**: New message is added and confirmation is shown, or error is displayed if saving fails.


### Edit Message
- **Preconditions**: Resident is selected; Employee has permission to edit messages.
- **Postconditions**: Message is updated and confirmation is shown, or error is displayed if saving fails.


### Delete Message
- **Preconditions**: Resident is selected; Employee has permission to delete messages.
- **Postconditions**: Message is deleted and confirmation is shown.
