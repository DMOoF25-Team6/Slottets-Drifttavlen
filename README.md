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
- Docker

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
MYSQL_HOST=localhost
```
Ændre værdierne til dine ønskede databaseindstillinger.

#### Kør SQL-server:
```sh
docker-compose up slottets-sqlserver
```

#### Fjern containeren:
```sh
docker-compose down slottets-sqlserver
```

### Kør applikationen
```sh
docker-compose up
```

Alternativt direkte med Docker:
```sh
docker run --name slottets-sqlserver -e MYSQL_ROOT_PASSWORD=your_root_password -e MYSQL_DATABASE=your_database_name -e MYSQL_USER=your_username -e MYSQL_PASSWORD=your_user_password -p 3307:3306 -d mysql
```

---
## For developers
To ensure consistency and quality in the code, follow these guidelines:

### Copilot Agent Instructions
Visual Studio Coplit Agent can help automate tasks and generate code based on your instructions.
BC should be placed in docs/bc.md for Copilot Agent to use it as a reference to generate relevant code in the relevant language and context.
To get the most out of Copilot Agent, follow these guidelines:

#### Create Use Cases (Coming soon)
```plaintext
#uc-artifact.agent.md
Create use case for "Vagtvalg og Borgeroversigt"
```

#### Create Domain Models

```plaintext
#dm-artifact.agent.md
Update dm for usecase 003
```

#### Create SSD
```plaintext
#ssd-artifact.agent.md
Create ssd for use case 003
```

#### Create OC
```plaintext
#oc-artifact.agent.md
Create oc for use case 003
```

#### Create SD
```plaintext
#sd-artifact.agent.md
Create sd for use case 003
We are using webapi for data access
```

#### Create DCD / Code (Coming soon)
```plaintext
#dcd-artifact.agent.md
Create dcd for use case 003
```

##### Note
- Trigger words are create / update `[dm,sd,oc,ssd,dcd,uc]` for "Use Case Name"
