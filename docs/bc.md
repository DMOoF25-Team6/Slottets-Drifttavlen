# Business Case for Slottets Drifttavlen

## Metadata
| Nøgle            | Værdi                             |
|------------------|-----------------------------------|
| Id               | BC                                |
| crossReference   |                                   |
| ProductOwnerLanguage | Danish Healthcare             |

## Versionslog
| Version | Dato       | Beskrivelse              | Forfatter     |
|---------|------------|--------------------------|---------------|
| 0001    | 2026-02-14 | Initial                  | Team 6        |

## Executive Summary
Slottets Drifttavlen er et digitalt system designet til at optimere arbejdsgange og informationsdeling i pleje- og omsorgsmiljøer. Systemet skal erstatte papirbaserede rutiner og sikre effektiv kommunikation, opgavestyring og dokumentation.

## Problem Statement
Papirbaserede processer og manuelle rutiner medfører ineffektivitet, risiko for fejl og manglende overblik. Der er behov for en digital løsning, der kan understøtte daglige opgaver, sikre dataintegritet og lette informationsudveksling.

## Objectives
- Digitalisere arbejdsgange og opgavestyring
- Forbedre informationsdeling mellem skift
- Sikre GDPR-overholdelse og datasikkerhed
- Muliggøre fremtidig udvidelse og integration
- Understøtte både Skoven og Slottet afdelinger med mulighed for separat og samlet visning
- Gøre det nemt og hurtigt at logge ind (minimumskrav til kodeord, initialer/e-mail som brugernavn)
- Differentierede brugerrettigheder (almindelige medarbejdere vs. ledere/koordinatorer)
- Audit trail: Det skal fremgå hvem der har skrevet/redigeret hvad og hvornår
- Markering af sene/tilbagevirkende indtastninger med korrekt tidsstempel
- Understøtte brug på arbejdstelefoner og mobile enheder
- Intuitivt og letforståeligt interface, også for vikarer og nyansatte
- Hurtig og nem markering af medicin og risikovurdering (trafiklys-system)

## Options and Alternatives (0-scenario, 1-scenario, 2-scenario)
- 0-scenario: Fortsætte med nuværende papirbaserede rutiner
- 1-scenario: Implementere et simpelt digitalt system med grundlæggende funktioner
- 2-scenario: Udvikle et skalerbart, cloud-ready system med integration og rapportering

## Benefits
- Øget effektivitet og overblik
- Reduceret risiko for fejl
- Bedre dokumentation og sporbarhed
- Forbedret arbejdsmiljø og brugertilfredshed

## Costs
- Udviklingsomkostninger (software, infrastruktur)
- Uddannelse og implementering
- Løbende vedligeholdelse og support

## Risks and Mitigations
| ID        | Risiko                        | Beskrivelse                                 | Afværge            |
|----------|-------------------------------|---------------------------------------------|--------------------|
| R-PM-001 | Tidsplanoverskridelser         | Forsinkelser i projektleverancer            | Realistisk planlægning, løbende status      |
| R-PM-002 | Budgetoverskridelser           | Uventede omkostninger                       | Kontinuerlig budgetopfølgning               |
| R-PM-003 | Ressourceallokering            | Utilstrækkelige ressourcer                  | Prioritering, fleksibel bemanding           |
| R-PM-005 | Modstand mod forandring        | Personalet foretrækker papir                | Involvering, træning, kommunikation         |
| R-EX-001 | Lovgivningsændringer           | Nye regler påvirker projektet               | Overvågning, tilpasning af system           |
| R-EX-004 | Netværks- og strømafbrydelse   | System utilgængeligt                        | Backup, redundans, offline-funktionalitet   |
| R-UX-001 | Lav brugeradoption             | Systemet opleves som besværligt eller ulogisk | Brugerinvolvering, brugertest, fokus på intuitiv UX |
| R-UX-002 | Login opleves som for langsomt | Personalet undlader at bruge systemet       | Optimeret loginflow, minimumskrav men hurtig adgang |
| R-UX-003 | Manglende audit trail          | Manglende ansvarlighed og opfølgning        | Implementering af logning og visning af brugerhandlinger |
| R-UX-004 | Manglende mobilunderstøttelse  | Personalet kan ikke dokumentere løbende     | Responsivt design, test på arbejdstelefoner |
| R-UX-005 | Manglende rollebaseret adgang  | Utilsigtet adgang til følsomme funktioner   | Differentierede rettigheder, kodebeskyttelse af adminfunktioner |

## Timeline
- Fase 1: Prototype (se milestones)
- Fase 2: Udvidelse med opgaver og beskeder
- Fase 3: Medicinstatus og rapportering

## Success Criteria
- Systemet er i drift og bruges dagligt
- GDPR-overholdelse er dokumenteret
- Projektet holder budget og tidsplan
- Brugertilfredshed og forbedret arbejdsgang
