# Domain Model (DM) for Slottets Drifttavlen
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | DM                                |
| crossReference    |                                   |

## Version Log
| Version | Date       | Description              | Author     |
|---------|------------|--------------------------|------------|
| 0001    | 2026-03-07 | Initial                  | Team 6     |
| 0002    | 2026-03-31 | Tilføjet Medarbejder og TelefonTildeling (UC-004, UC-005) | Team 6 |

## Diagram
```mermaid
%% Domænemodeldiagram for Slottets Drifttavlen
classDiagram
    class Medarbejder {
        Brugernavn
        Adgangskode
        Kontostatus
    }
    class Beboer {
        Initialer
    }
    class Notat {
        Note
    }
    class Trafiklys {
        Grøn
        Gul
        Rød
    }
    class TelefonTildeling {
        Telefonnummer
        Vagttype
    }
    Beboer "1" -- "0..*" Notat : har
    Beboer "0..*" -- "1" Trafiklys : status
    Medarbejder "1" -- "0..*" TelefonTildeling : tildeler
```

## Noter
- Beboer repræsenterer en person, der modtager pleje.
- Beboer har en trafiklysstatus (Trafiklys), der angiver aktuel tilstand (Grøn, Gul, Rød).
- Beboer kan have flere noter (Notat), hver med tekst, tidsstempel og reference til medarbejder.
- Initialer bruges til identifikation af beboer for at sikre GDPR-overholdelse.

- Medarbejder repræsenterer en medarbejder (introduceret i UC-004).
- TelefonTildeling repræsenterer tildeling af et fast telefonnummer til en vagt (introduceret i UC-005). Telefonnummer er én af: 41522, 41523, 41524, 41525, 41526, 41527, 41529. Vagttype er én af: Dag, Aften, Nat.
