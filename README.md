# Slottets Drifttavlen

## Indholdsfortegnelse

- [Virksomhedsprofil](#virksomhedsprofil)
- [Arbejdsgang](#arbejdsgang)
- [Problemstilling](#problemstilling)
- [Systemdesign og Funktionalitet](#systemdesign-og-funktionalitet)
- [Teknologi og Arkitektur](#teknologi-og-arkitektur)
- [Udvidelsesmuligheder](#udvidelsesmuligheder)
- [Diagrammer & Dokumentation](#diagrammer--dokumentation)
- [Sikkerhedsovervejelser](#sikkerhedsovervejelser)
- [Forbedringsforslag](#forbedringsforslag)
- [Folder Structure & Responsibilities](#folder-structure--responsibilities)
- [Opsætningsvejledning](#opsætningsvejledning)

## Dokumentationsoversigt

- [FURPS+](docs/furps.0001.md)
- [KPI](docs/kpi.0001.md)
- [Milepæle og leverancer](docs/milestones.0001.md)
- [Risikoanalyse](docs/risk-analysis.0001.md)

### Quality Criteria
- [BMC kvalitetskriterier](docs/quality-criteria/artifact/qc-bmc.0001.md)
- [BPMN kvalitetskriterier](docs/quality-criteria/artifact/qc-bpmn.0001.md)
- [C# kodekvalitet](docs/quality-criteria/qc-csharp-code.0001.md)
- [SQL kvalitetskriterier](docs/quality-criteria/qc-sql.0001.md)

## Virksomhedsprofil
Slottet er et døgnbemandet bosted, der yder omsorg og pleje til borgere med særlige behov.
Personalet arbejder i dag-, aften- og nattevagter og sikrer kontinuerlig støtte og pleje.
Teamet samarbejder dagligt om at levere høj kvalitet og sikre borgernes trivsel og sikkerhed.

## Arbejdsgang
Ved vagtskifte udveksler afgående og tilgående vagthold kritisk information om borgernes tilstand, medicinhåndtering og opgaver. Overlapsskemaet indeholder:
- Borgerens aktuelle tilstand (trafiklysmodel)
- Medicinstatus og udlevering
- Personale tilknyttet borgeren
- Opgaver og beskeder til næste vagthold
- Særlige hændelser

## Problemstilling
Slottet anvender i dag et papirbaseret overlapsskema, hvilket medfører risiko for fejl, manglende historik og tidskrævende håndtering.
Projektet har til formål at udvikle et digitalt IT-system, der erstatter det papirbaserede skema og understøtter personalets daglige arbejde mere effektivt og sikkert.

## Systemdesign og Funktionalitet
- **Vagtvalg:** Personalet vælger vagttype ved login.
- **Borgeroversigt:** Individuelle felter for hver borger med trafiklysmodel, medicinstatus, personale, opgaver og hændelser.
- **Fælles rubrikker:** Arbejdstelefoner og ansvarsområder kan tildeles medarbejdere.
- **Brugerstyring:** Rollebaseret adgangskontrol (plejepersonale, vagtansvarlige, admin).
- **UI/UX:** Intuitivt, responsivt design med klart visuelt overblik.
- **Datasikkerhed:** Adgangslogning, GDPR-compliance, historik for sporbarhed.

## Teknologi og Arkitektur
- **Backend:** ASP.NET Core
- **Frontend:** Blazor
- **Database:** Microsoft SQL Server eller MySQL via Entity Framework
- **Containerisering:** Docker
- **API:** Eksponering af funktioner til integration
- **Cloud-ready:** Kan køre standalone, client/server eller cloud-baseret (Azure, AWS)
- **Skalering:** Horisontal skalering, central konfiguration, load balancing

## Udvidelsesmuligheder
- Integration med FMK (Fælles Medicinkort)
- Notifikationer og påmindelser
- Rapportering af historik og hændelser

## Diagrammer & Dokumentation
Se `docs/` for:
- Klassediagram
- ER-diagram
- Sekvensdiagram
- Deployment-diagram
- Aktivitetsdiagram
- Sikkerhedsovervejelser

## Sikkerhedsovervejelser
- Kryptering af følsomme data
- GDPR og håndtering af personoplysninger
- Adgangskontrol og autentificering
- Logning af brugeraktivitet

## Forbedringsforslag
- Automatisk integration med FMK
- Notifikationer for opgaver og medicin
- Udvidet rapportering og analyse

---

## Folder Structure & Responsibilities

Løsningen følger Clean Architecture-principper og er organiseret således:

```
├── src/                # Al produktionskode
│   ├── Core/           # Kerne forretningslogik (use cases, interfaces)
│   ├── Domain/         # Domænemodeller og forretningsenheder
│   ├── Infrastructure/ # Infrastruktur (dataadgang, eksterne services)
│   └── WebUI/          # Blazor frontend (UI og klientlogik)
├── tests/              # Alle testprojekter, spejler src/ strukturen
├── docs/               # Dokumentation (arkitektur, use cases, analyse)
├── LocalNuget/         # Lokale NuGet-pakker
├── .github/            # GitHub workflows og Copilot-instruktioner
├── docker-compose.yml  # Docker Compose-konfiguration
├── Directory.Build.props/targets # Løsningsdækkende MSBuild-indstillinger
├── Slottet.CareManagement.slnx # Løsningsfil
└── global.json         # .NET SDK versionstyring
```

### Mappeansvar
- **src/Core/**: Indeholder kerne forretningslogik, applikationstjenester og interfaces. Ingen afhængigheder til andre lag.
- **src/Domain/**: Indeholder domæneentiteter, value objects og domænelogik. Rene forretningsregler.
- **src/Infrastructure/**: Implementerer interfaces fra Core, håndterer dataadgang (f.eks. MySQL) og eksterne integrationer.
- **src/WebUI/**: Blazor frontend-projekt til brugergrænseflade og klientlogik.
- **tests/**: Indeholder testprojekter for hvert hovedlag, følger samme struktur som `src/`.
- **docs/**: Markdown-dokumentation for arkitektur, use cases og analyse.
- **LocalNuget/**: Lokal NuGet-pakkelager.
- **.github/**: CI/CD workflows og Copilot-instruktioner.

For more details, see the documentation in `docs/` and `.github/copilot-instructions.md`.

---

## Opsætningsvejledning

### Krav
- .NET 8 SDK
- Docker (valgfrit, anbefales til database)
- SQL Server eller MySQL

### Klon Repository
```sh
git@github.com:DMOoF25-Team6/Slottets-Drifttavlen.git
cd DMOoF25-Team6.Slottets-Drifttavlen
```

### Docker database
Opret en `.env`-fil i projektroden med følgende indhold:
```
MYSQL_ROOT_PASSWORD=your_root_password
MYSQL_DATABASE=your_database_name
MYSQL_USER=your_username
MYSQL_PASSWORD=your_user_password
```
Ændre værdierne til dine ønskede databaseindstillinger.

#### Start MySQL-containeren:
```sh
docker-compose up
```
Alternativt direkte med Docker:
```sh
docker run --name slottets-sqlserver -e MYSQL_ROOT_PASSWORD=your_root_password -e MYSQL_DATABASE=your_database_name -e MYSQL_USER=your_username -e MYSQL_PASSWORD=your_user_password -p 3307:3306 -d mysql
```

### Byg og kør applikationen
Byg løsningen:
```sh
dotnet build Slottet.CareManagement.slnx
```
Kør tests:
```sh
dotnet test Slottet.CareManagement.slnx
```
Start applikationen:
```sh
dotnet run --project src/WebUI/WebUI/WebUI.csproj
```

### Windows sandbox
For at oprette et isoleret testmiljø på Windows kan du aktivere Windows Sandbox (version 2) med:
```powershell
Enable-WindowsOptionalFeature -Online -FeatureName "Containers-DisposableClientVM" -All
```
Kør PowerShell som administrator og genstart computeren hvis nødvendigt.


## For developers

### Copilot Agent Instructions
Visual Studio Code's Copilot Agent kan hjælpe med at automatisere opgaver og generere kode baseret på dine instruktioner. 
BC skal være placeret i docs/bc.md for at Copilot Agent kan bruge det som reference til at generere relevante i relavant sprog og kontekst.
For at få mest muligt ud af Copilot Agent, følg disse retningslinjer:

---

#### Domain Model (DM) Automation
Usecase skal være det aktive dokument i editoren, og DM skal være i samme mappe som usecase-dokumentet. 
Sørg for at have en klar og detaljeret usecase beskrivelse, da Copilot Agent vil bruge denne information til at generere et relevant og præcist domænemodeldiagram.


Skriv føllgende instruktion for at generere DM:
```plaintext
#dm-artifact.agent.md
Create a DM for usecase uc-xxx
```
Tilpas attributter og relationer i DM baseret på usecase-beskrivelsen for at sikre, at det afspejler de nødvendige forretningsregler og entiteter korrekt.

---

#### System Sequence Diagram (SSD) Automation
Usecase skal være det aktive dokument i editoren, og SSD skal være i samme mappe som usecase-dokumentet. 
Sørg for at have en klar og detaljeret usecase beskrivelse, da Copilot Agent vil bruge denne information til at generere et relevant og præcist systemsekvensdiagram.

Skriv følgende instruktion for at generere SSD:
```plaintext
#ssd-artifact.agent.md
Create an SSD for usecase uc-xxx
```
Tilpas aktører, beskeder og systemkomponenter i SSD baseret på usecase-beskrivelsen for at sikre, at det afspejler de nødvendige interaktioner og systemadfærd korrekt.
