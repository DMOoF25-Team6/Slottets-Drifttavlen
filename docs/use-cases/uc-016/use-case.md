# UC-016 — Delete Resident (administration)

| Key | Value |
|-----|-------|
| ID | uc-016 |
| Gateway | Gateway 11: Resident Administration Management |
| Cross-reference | REQ-F-001 |

## Actor
- Admin

## Preconditions
- User logged in with role `admin`
- Target Resident exists

## Postconditions
- Resident is removed from the system
- Audit log entry created

## Main flow
1. Admin opens `Management > Residents`
2. Admin clicks delete on a Resident row
3. `DeleteConfirmationModal` opens
4. Admin confirms
5. WebUI calls `DELETE /residents/{id}`
6. Resident disappears from the list

## Alternative flow
- A1 Cancel → modal closes, no change
- A2 Backend returns error → error shown, list unchanged

## Acceptance criteria
- [ ] Only `admin` can delete
- [ ] Confirmation required
- [ ] Resident removed from UI on success
- [ ] Audit logged
