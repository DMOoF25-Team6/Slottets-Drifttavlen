## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | UC002-ResidentNote-Flow           |
| crossReference    | UC-002                            |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-03-18 | User flow diagram     | Team 6     |


## User Flow Diagram

### Primary Flow
 1.The employee selects a resident  
 2.The system displays the current notes for the selected resident  


---
### Add Note Flow
 1a.The employee selects a resident
 2a.The employee adds a note  
 3a.The system registers the note and saves it  


---

### Delete Note Flow
 1b.The employee selects resident
 2b.The system displays the current notes for the selected resident
 3b.The employee deletes a note  
 4b.The system removes the note  
 5b.The system displays a confirmation that the note has been deleted  

 ---

# User Flow Diagram (Visual)

```mermaid
flowchart TD
 Start([Employee])
    SelectResident[Select a Resident]
    ShowNotes[Show Current Notes]

    AddNote[Add Note]
    SaveNote[System Saves Note]

    DeleteNote[Delete Note]
    DeleteProcess[System Removes Note]
    DeleteConfirm[Confirmation Displayed]

    Start --> SelectResident
    SelectResident --> ShowNotes

    ShowNotes --> AddNote
    AddNote --> SaveNote --> ShowNotes

    ShowNotes --> DeleteNote
    DeleteNote --> DeleteProcess --> DeleteConfirm
    


