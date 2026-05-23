# UC-022 — Delete medicine delivery (administration)

| Key | Value |
|-----|-------|
| ID | uc-022 |
| Gateway | Gateway 13 |
| Cross-reference | REQ-F-001 |

## Actor
- Admin

## Preconditions
- Levering eksisterer

## Postconditions
- MedicineRecord slettet
- Audit log entry

## Main flow
1. Admin klikker [X]
2. Bekraefter
3. `DELETE /medicinedelivery/{id}`
4. Raekke forsvinder

## Acceptance criteria
- [ ] Only admin
- [ ] Confirmation required
- [ ] Record removed
- [ ] Audit logged
