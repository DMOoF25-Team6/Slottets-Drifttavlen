# Domain Model (DM) for Slottets-Drifttavlen Resident Dashboard
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | DM                               |
| crossReference    |                                   |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-02-25 | Initial                  | Team 6     |

## Diagram

```mermaid
%% Domain Model Diagram for Resident Dashboard

classDiagram
    class Beboer {
        Initialer
    }
    class Trafiklys {
        grøn
        gul
        rød
    }
    class Noter {
        Note
        Forfatter
    }
    Beboer "*" --> "1" Trafiklys : har én
    Beboer "1" --> "0..*" Noter : har 
```

## Noter
- Beboer har en trafiklysstatus, som kan være grøn, gul eller rød.
- Beboer kan have flere noter, som indeholder tekst, datotid og forfatterinformation
- Initialer er kun et bogstav fra fornavnet, da ledelsen ønsker at bevare anonymiteten for beboerne. (GDPR-compliant)
 
