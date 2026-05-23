# UC-018 — Update task/message (administration)

| Key | Value |
|-----|-------|
| ID | uc-018 |
| Gateway | Gateway 12 |
| Cross-reference | REQ-F-001 |

## Actor
- Admin / Caretaker

## Preconditions
- Note exists; user is authorised

## Postconditions
- Note text is updated; EditedAt timestamp refreshed

## Main flow
1. User clicks edit on existing note
2. Adjusts text
3. WebUI calls `PUT /residentnote/{noteId}` with new text
4. UI shows updated note

## Acceptance criteria
- [ ] Note text updated server-side
- [ ] UI reflects change immediately
- [ ] Audit logged
