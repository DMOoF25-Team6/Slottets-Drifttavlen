## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | UC-002                            |
| crossReference    | BC                                  |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-03-06 | Initial                  | Team 6 |

## Use Case Description

## User story
Som medarbejder vil jeg kunne se aktuelle beskeder om beboerne på tavlen/skærmen, tilføje en besked på en beboer, rette/redigere en eksisterende besked på en beboer, og slette en nuværende besked på en beboer, så beskederne altid er opdaterede og relevante.

### Brief Use Case
**Use Case Number**: UC-002
**Title**: Håndtering af beskeder på beboer
**Success Flow**:
Medarbejder vælger en beboer. Systemet viser aktuelle beskeder. Medarbejder tilføjer, retter eller sletter en besked på en beboer efter behov.

### Casual Use Case
**Use Case Number**: UC-002
**Title**: Håndtering af beskeder på beboer
**Actors**:
- Medarbejder
- System
**Summary**: Medarbejder håndterer beskeder om beboere på tavlen/skærmen.
**Preconditions**:
- Medarbejder er logget ind
- Beboer eksisterer i systemet
**Main Flow**:
1. Medarbejder vælger en beboer 
2. Systemet viser aktuelle beskeder for beboeren
3. Medarbejder tilføjer besked
4. Systemet viser bekræftelse på tilføjelse
5. Medarbejder retter besked  
6. Systemet viser bekræftelse på redigering
7. Medarbejder sletter besked   
8. Systemet viser bekræftelse på sletning
**Main Exceptions**:
3.a Besked kan ikke gemmes – Medarbejder får fejlmeddelelse
5.a Besked kan ikke gemmes – Medarbejder får fejlmeddelelse
**Postconditions**:
- Beskeder er opdateret og synlige
- Slettede beskeder eksisterer ikke længere
