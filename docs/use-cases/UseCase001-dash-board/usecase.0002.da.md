## Metadata
| Key           | Value                        |
|---------------|-----------------------------|
| Id            | UC-001                      |
| crossreference| BC-001                      |

## Version Log
| Version | Date       | Description          | Author        |
|---------|------------|----------------------|---------------|
| 0001    | 2026-02-25 | Initial use case     | Team 6        |
| 0002    | 2026-02-26 | Updated actor to System Time Event | Team 6        |

## Use Case Beskrivelse

## Brugerhistorie
En systemtidsbegivenhed for beboer-dashboard-skærmen  
ønsker at opdatere beboer-dashboardet  
for at få status på en beboer og noter om beboeren.

### Kort Use Case
**Use Case Nummer**: UC-001  
**Titel**: Opdater beboer-dashboard  
**Resumé**: Omsorgspersonen opdaterer et dashboard for at se den aktuelle status og noter for en beboer.  
**Forudsætninger**:
- Applikationen er startet og dashboardet er tilgængeligt på skærm 2
- Beboeren findes i systemet
**Succesflow**:
1. Tidsbegivenhed udløses og anmodning sendes om at opdatere beboer-dashboardet
2. Systemet viser beboerens status og noter
**Efterbetingelser**:
- Omsorgspersonen har opdateret information om beboeren

#### Note:
- Tidsbegivenheden (en del af systemet) udløses, når der er gået 60 sekunder siden sidste opdatering.
