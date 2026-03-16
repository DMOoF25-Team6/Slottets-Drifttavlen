## Metadata
| Nøgle             | Værdi                            |
|-------------------|----------------------------------|
| Id                | SSD-UC-002                       |
| crossReference    | UC-002                           |

## Version
- **Version**: 0001
- **Dato**: 2026-03-06

## Versionslog
| Version | Dato      | Beskrivelse             | Forfatter  |
|---------|-----------|-------------------------|------------|
| 0001    | 2026-03-06| Initial                 | Team 6     |

## Systemsekvensdiagram
```mermaid
sequenceDiagram
    actor Medarbejder
    participant System
    Medarbejder ->> System: Vælg beboer
    System -->> Medarbejder: Vis aktuelle beskeder
    Medarbejder ->> System: Tilføj besked
    alt Tilføj besked flow
        System -->> Medarbejder: Bekræft besked tilføjet
    else Fejl ved tilføjelse
        System -->> Medarbejder: Fejl: besked kan ikke gemmes
    end
    Medarbejder ->> System: Rediger besked
    alt Rediger besked flow
        System -->> Medarbejder: Bekræft besked redigeret
    else Fejl ved redigering
        System -->> Medarbejder: Fejl: besked kan ikke gemmes
    end
    Medarbejder ->> System: Slet besked
    System -->> Medarbejder: Bekræft besked slettet
```
