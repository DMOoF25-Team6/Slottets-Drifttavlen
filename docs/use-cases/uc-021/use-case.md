# UC-021 — Update medicine delivery (administration)

| Key | Value |
|-----|-------|
| ID | uc-021 |
| Gateway | Gateway 13 |
| Cross-reference | REQ-F-001 |

## Actor
- Admin

## Preconditions
- Levering eksisterer

## Postconditions
- MedicineRecord opdateret

## Main flow
1. Admin klikker [Edit] paa en levering
2. Justerer felter
3. WebUI calls `PUT /medicinedelivery/{id}`
4. Listen opdateres

## Acceptance criteria
- [ ] Only admin
- [ ] Record updated
- [ ] Audit logged
