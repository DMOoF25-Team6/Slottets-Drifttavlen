# KPI for Slottet – Digital Overlap System (FURPS+)

## Requirements

| ID         | Type                      | Requirement                                                                                      |
|------------|---------------------------|---------------------------------------------------------------------------------------------------|
| KPI-R-001  | Functional requirement    | Systemet skal registrere og opdatere borgerstatus, medicin, personale og opgaver digitalt.        |
| KPI-R-002  | Functional requirement    | Systemet skal understøtte vagtvalg (dag, aften, nat) og vise borgeroversigt.                      |
| KPI-R-003  | Non-functional requirement| Systemet skal være brugervenligt og intuitivt for personalet.                                     |
| KPI-R-004  | Non-functional requirement| Systemet skal være tilgængeligt 99,9% af tiden.                                                   |
| KPI-R-005  | Non-functional requirement| Systemet skal kunne håndtere minimum 20 samtidige brugere.                                        |
| KPI-R-006  | Non-functional requirement| Systemet skal logge alle ændringer og sikre sporbarhed.                                           |
| KPI-R-007  | Business requirement      | Systemet skal understøtte fremtidig udvidelse (integration, rapportering, FMK).                   |
| KPI-R-008  | Business requirement      | Systemet skal overholde GDPR og datasikkerhed.                                                    |
| KPI-R-009  | Business requirement      | Systemet skal holdes inden for projektbudget.                                                     |
| KPI-R-010  | Non-functional requirement| CRUD-operationer skal udføres inden for acceptabel tid (< 1 sekund).                              |

## Functional KPI measurements

| ID         | KPI                                 | Measurement method                   | Target | Frequency         |
|------------|-------------------------------------|--------------------------------------|--------|-------------------|
| KPI-K-001  | Digital dokumentation af vagtskifte | Antal vagtskifter dokumenteret digitalt | 100%   | Monthly           |
| KPI-K-002  | Opdateret borgerstatus              | Antal borgere med opdateret status      | 100%   | Monthly           |
| KPI-K-003  | Opgaver og beskeder                 | Antal opgaver/beskeder registreret      | 100%   | Monthly           |

## Non-functional KPI measurements

| ID         | KPI                         | Measurement method                       | Target             | Frequency         |
|------------|-----------------------------|------------------------------------------|--------------------|-------------------|
| KPI-K-004  | Brugervenlighed             | Brugerundersøgelse (1-5)                 | avg >= 4           | Annual            |
| KPI-K-005  | Tilgængelighed              | System uptime                            | 99,9%              | Monthly           |
| KPI-K-006  | Samtidige brugere           | Stress test                              | 20 brugere         | Annual            |
| KPI-K-007  | Sporbarhed                  | Audit-log for alle ændringer             | 100%               | Monthly           |
| KPI-K-008  | CRUD-operationer            | Gennemsnitstid for CRUD                  | < 1 sekund         | Monthly           |

## Business KPI measurements

| ID         | KPI                         | Measurement method                       | Target             | Frequency         |
|------------|-----------------------------|------------------------------------------|--------------------|-------------------|
| KPI-K-009  | Fremtidig udvidelse         | Bygget til integration og rapportering   | Testet             | One-time test     |
| KPI-K-010  | GDPR-overholdelse           | Compliance audit                         | 100%               | Annual            |
| KPI-K-011  | Budget                      | Projektregnskab                          | <= projektbudget   | One-time test     |
