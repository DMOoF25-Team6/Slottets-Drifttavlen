## Metadata
| Key           | Value                        |
|---------------|-----------------------------|
| Id            | UC-001                      |
| crossreference| BC-001                      |

## Version Log
| Version | Date       | Description          | Author        |
|---------|------------|----------------------|---------------|
| 0001    | 2026-02-25 | Initial use case     | Team 6        |
| 0002    | 2026-02-26 | Updated actor to System Time Event | Team 6        |

## Use Case Description

## User Story
A System Time Event for resident dashboard screen  
wants to update resident dashboard
in order to get the status of a resident and notes about the resident.

### Brief Use Case
**Use Case Number**: UC-001  
**Title**: Update resident dashboard  
**Summary**: The caregiver updates a dashboard to see the current status and notes for a resident.  
**Preconditions**:
- The application is started and the dashboard is available on screen 2
- The resident exists in the system
**Success Flow**:
1. Time event is triggered and request is sent to update the resident dashboard
2. The system displays the resident's status and notes
**Postconditions**:
- The caregiver has updated information about the resident

#### Note:
- The time event (part of system) is triggered when 60 seconds have passed since the last update.
