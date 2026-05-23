# UC-020 — Register medicine delivery (administration)

| Key | Value |
|-----|-------|
| ID | uc-020 |
| Gateway | Gateway 13: Medicine Administration Management |
| Cross-reference | REQ-F-001 |

## Actor
- Admin

## Preconditions
- Admin logged in
- Target Resident exists

## Postconditions
- New MedicineRecord created

## Main flow
1. Admin opens `Management > Medicine`
2. Clicks "Tilfoej levering"
3. Indtaster Resident, medicin navn, tidspunkt, givet ja/nej
4. WebUI calls `POST /medicinedelivery`
5. Levering vises i tabellen

## Acceptance criteria
- [ ] Only admin can create
- [ ] Validation on required fields
- [ ] Record persisted to DB
- [ ] Audit logged
