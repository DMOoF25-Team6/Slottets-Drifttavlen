# FURPS+ (Dansk)
FURPS+ er en model til klassificering af softwarekvalitetsattributter.
Forkortelsen står for Funktionalitet, Usability, Reliability, Performance og Supportability, hvor "+" angiver yderligere hensyn såsom designbegrænsninger, implementeringskrav, interfacekrav og fysiske krav.

## Metadata
| Nøgle             | Værdi                             |
|-------------------|-----------------------------------|
| Id                | FURPS                             |
| crossReference    | BC                                |

### Ændringslog
| Dato       | Version | Beskrivelse                     | Forfatter     |
|------------|---------|---------------------------------|---------------|
| 2026-02-07 | 0001    | Oprettelse af dokumentet        | Team 6        |
| 2026-03-30 | 0002    | Nye po-krav tilføjet            | Team 6        |

---

## FURPS+ for Slottet – Digital Overlap System

### Funktionalitet
- [REQ-F-001] Digitalt overlapsskema med borgerfelter, trafiklysmodel, medicinstatus, opgaver og beskeder.
- [REQ-F-002] Understøttelse af både Skoven og Slottet afdelinger med mulighed for separat og samlet visning.
- [REQ-F-003] Brugerroller: plejepersonale, vagtansvarlige, ledere/koordinatorer, admin.
- [REQ-F-004] Differentierede brugerrettigheder (almindelige medarbejdere vs. ledere/koordinatorer).
- [REQ-F-005] Adgangskontrol og login (hurtigt login, minimumskrav til kodeord, initialer/e-mail som brugernavn).
- [REQ-F-006] Historik og sporbarhed for alle overlaps og ændringer (audit trail: hvem har skrevet/redigeret hvad og hvornår).
- [REQ-F-007] Markering af sene/tilbagevirkende indtastninger med korrekt tidsstempel.
- [REQ-F-008] Hurtig og nem markering af medicin og risikovurdering (trafiklys-system).
- [REQ-F-009] Integration med FMK og notifikationssystem (udvidelse).

### Usability
- [REQ-U-001] Intuitiv brugergrænseflade tilpasset travle vagtskifter og alle medarbejdertyper (inkl. vikarer og nyansatte).
- [REQ-U-002] Klart visuelt overblik over borgerstatus og opgaver.
- [REQ-U-003] Responsivt design til tablets, PC og arbejdstelefoner (mobile enheder).
- [REQ-U-004] Hurtig navigation og let aflæsning af kritisk information.
- [REQ-U-005] Hurtig og nem login-proces.
- [REQ-U-006] Mulighed for løbende og efterfølgende dokumentation, med tydelig markering af sene indtastninger.

### Reliability
- [REQ-R-001] Høj tilgængelighed og failover (cloud-ready, lokal og netværksdrift).
- [REQ-R-002] Automatisk backup og versionering af data.
- [REQ-R-003] Audit-log for alle ændringer og brugerhandlinger.
- [REQ-R-004] Robust håndtering af netværks- og strømudfald.

### Performance
- [REQ-P-001] Hurtig indlæsning og opdatering af borgerdata.
- [REQ-P-002] Skalerbar løsning (horisontal skalering, flere instanser).
- [REQ-P-003] Effektiv håndtering af samtidige brugere ved vagtskifte.

### Supportability
- [REQ-S-001] Omfattende brugerdokumentation og træning.
- [REQ-S-002] Ticketing-system og supportfunktion.
- [REQ-S-003] Let opdatering og vedligeholdelse af systemet.

### Designbegrænsninger
- [REQ-DC-001] GDPR-compliance og sikker behandling af persondata.

### Implementering
- [REQ-IMPL-001] Containerisering via Docker, cloud-ready arkitektur.

### Interface
- [REQ-INT-001] API til integration med eksterne systemer.

### Fysisk
- [REQ-PHY-001] Central konfiguration via miljøvariabler og secrets.
- [REQ-PHY-002] Ingen bindinger til lokalt filsystem, fast server eller IP.
