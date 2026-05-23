# UC-019 — Delete task/message (administration)

| Key | Value |
|-----|-------|
| ID | uc-019 |
| Gateway | Gateway 12 |
| Cross-reference | REQ-F-001 |

## Actor
- Admin

## Preconditions
- Note exists; user has admin role

## Postconditions
- Note removed; audit log entry created

## Main flow
1. User clicks delete on a note
2. Confirmation modal
3. On confirm → `DELETE /residentnote/{id}`
4. Note removed from UI

## Acceptance criteria
- [ ] Confirmation required
- [ ] Note removed server-side
- [ ] UI updated
- [ ] Audit logged
