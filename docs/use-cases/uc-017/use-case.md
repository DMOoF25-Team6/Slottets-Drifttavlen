# UC-017 — Create task/message (administration)

| Key | Value |
|-----|-------|
| ID | uc-017 |
| Gateway | Gateway 12: Task/Message Administration Management |
| Cross-reference | REQ-F-001 |

## Actor
- Admin / Caretaker

## Preconditions
- User authenticated
- Target Resident exists

## Postconditions
- New ResidentNote attached to Resident
- Audit log entry created

## Main flow
1. User opens Resident card
2. Clicks "Tilføj note" in NotesSection
3. Enters note text
4. WebUI calls `POST /residentnote` with `AddResidentNoteDto`
5. New note appears in NotesSection

## Acceptance criteria
- [ ] Note saved with timestamp + author initials
- [ ] Note appears immediately in UI
- [ ] Audit logged
