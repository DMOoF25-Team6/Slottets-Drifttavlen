# Slottets Drifttavlen

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Repo Size][repo-size-shield]][repo-size-url]
[![Issues][issues-shield]][issues-url]

## 📚 Indholdsfortegnelse

- [Dokumentationsoversigt](#dokumentationsoversigt)
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

<a name="dokumentationsoversigt"></a>
## Dokumentationsoversigt

- [Business Case](docs/bc.md)
- [FURPS+](docs/furps.md)
- [KPI](docs/kpi.md)
- [Domænemodel](docs/dm.da.md)
- [Milepæle og leverancer](docs/milestones.md)
- [Risikoanalyse](docs/risk-analysis.md)
- [Milepæleplan / Leverancer](docs/milestones.md)

<a name="virksomhedsprofil"></a>
## Virksomhedsprofil
Slottet er et døgnbemandet bosted, der yder omsorg og pleje til borgere med særlige behov.
Personalet arbejder i dag-, aften- og nattevagter og sikrer kontinuerlig støtte og pleje.
Teamet samarbejder dagligt om at levere høj kvalitet og sikre borgernes trivsel og sikkerhed.

<a name="arbejdsgang"></a>
## Arbejdsgang
Ved vagtskifte udveksler afgående og tilgående vagthold kritisk information om borgernes tilstand, medicinhåndtering og opgaver. Overlapsskemaet indeholder:
- Borgerens aktuelle tilstand (trafiklysmodel)
- Medicinstatus og udlevering
- Personale tilknyttet borgeren
- Opgaver og beskeder til næste vagthold
- Særlige hændelser

<a name="problemstilling"></a>
## Problemstilling
Slottet anvender i dag et papirbaseret overlapsskema, hvilket medfører risiko for fejl, manglende historik og tidskrævende håndtering.
Projektet har til formål at udvikle et digitalt IT-system, der erstatter det papirbaserede skema og understøtter personalets daglige arbejde mere effektivt og sikkert.

<a name="systemdesign-og-funktionalitet"></a>
## Systemdesign og Funktionalitet
- **Vagtvalg:** Personalet vælger vagttype ved login.
- **Borgeroversigt:** Individuelle felter for hver borger med trafiklysmodel, medicinstatus, personale, opgaver og hændelser.
- **Fælles rubrikker:** Arbejdstelefoner og ansvarsområder kan tildeles medarbejdere.
- **Brugerstyring:** Rollebaseret adgangskontrol (plejepersonale, vagtansvarlige, admin).
- **UI/UX:** Intuitivt, responsivt design med klart visuelt overblik.
- **Datasikkerhed:** Adgangslogning, GDPR-compliance, historik for sporbarhed.

<a name="teknologi-og-arkitektur"></a>
## Teknologi og Arkitektur
- **Backend:** ASP.NET Core
- **Frontend:** Blazor
- **Database:** Microsoft SQL Server eller MySQL via Entity Framework
- **Containerisering:** Docker
- **API:** Eksponering af funktioner til integration
- **Cloud-ready:** Kan køre standalone, client/server eller cloud-baseret (Azure, AWS)
- **Skalering:** Horisontal skalering, central konfiguration, load balancing

<a name="udvidelsesmuligheder"></a>
## Udvidelsesmuligheder
- Integration med FMK (Fælles Medicinkort)
- Notifikationer og påmindelser
- Rapportering af historik og hændelser

<a name="diagrammer--dokumentation"></a>
## Diagrammer & Dokumentation
Se `docs/` for:
- Klassediagram
- ER-diagram
- Sekvensdiagram
- Deployment-diagram
- Aktivitetsdiagram
- Sikkerhedsovervejelser

<a name="sikkerhedsovervejelser"></a>
## Sikkerhedsovervejelser
- Kryptering af følsomme data
- GDPR og håndtering af personoplysninger
- Adgangskontrol og autentificering
- Logning af brugeraktivitet

<a name="forbedringsforslag"></a>
## Forbedringsforslag
- Automatisk integration med FMK
- Notifikationer for opgaver og medicin
- Udvidet rapportering og analyse

---

<a name="opsætningsvejledning"></a>
## Opsætningsvejledning

### Krav
- .NET 8 SDK
- Docker

### Klon Repository
```sh
git@github.com:DMOoF25-Team6/Slottets-Drifttavlen.git
cd DMOoF25-Team6.Slottets-Drifttavlen
```

### Opret env-filer
Opret en `.env`-fil i projektroden med følgende indhold:
```sh
MYSQL_ROOT_PASSWORD=rootpassword
MYSQL_DATABASE=slottetsdb
MYSQL_USER=appuser
MYSQL_PASSWORD=apppassword
MYSQL_HOST=localhost
ConnectionStrings__AppDbContext=Server={DB_HOST};Port=3307;Database={DB_NAME};User={DB_USER};Password={DB_PASSWORD};
TokenValidationParameters__IssuerSigningKey=YOUR_SECRET_KEY_HERE
TokenValidationParameters__Issuer=slottets-drifttavlen
TokenValidationParameters__Audience=slottets-drifttavlen
```

### Docker database
Ændrer værdierne i `.env`-filen i projektroden til dine ønskede databaseindstillinger:
```
MYSQL_ROOT_PASSWORD=your_root_password
MYSQL_DATABASE=your_database_name
MYSQL_USER=your_username
MYSQL_PASSWORD=your_user_password
MYSQL_HOST=localhost
ConnectionStrings__AppDbContext=Server={DB_HOST};Port=3307;Database={DB_NAME};User={DB_USER};Password={DB_PASSWORD};
```

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

Migrere og opdatere databasen:
```sh
dotnet ef migrations add IdentityFramework --project src/Infrastructure.Data --startup-project src/Api
dotnet ef database update --project src/Infrastructure.Data --startup-project src/Api
```

---

<a name="for-udviklere"></a>
## For developers
To ensure consistency and quality in the code, follow these guidelines:

### Folder Structure & Responsibilities

The solution follows Clean Architecture principles and is organized as follows:

```
├── src/                          # All production code
│   ├── Core/                     # Core business logic (use cases, interfaces)
│   │   ├── DTOs                  # Data Transfer Objects for use cases
│   │   ├── Handlers              # Use case handlers implementing business logic
│   │   ├── Helpers               # Utility classes and functions
│   │   ├── Interfaces            # Interfaces for services and repositories
│   │   ├── Managers              # Service classes that implement interfaces and contain business logic
│   │   ├── Mappers               # Mapping logic between domain models and DTOs
│   │   └── Services              # Application services that orchestrate use cases
│   ├── Domain/                   # Domain models and business entities
│   │   ├── Attributes/           # Custom attributes for domain models
│   │   ├── Entities/             # Core domain entities
│   │   ├── Enums/                # Enumerations used in the domain
│   │   └── Interfaces/           # Domain interfaces
│   ├── Infrastructure/           # Infrastructure (data access, external services)
│   │   ├── Persistents           # Database context and repositories
│   │   │   └── Configurations    # Database configurations and migrations
│   │   └── Repositories          # Repository implementations for data access
│   └── WebUI/                    # Blazor frontend (UI and client logic)
├── tests/                        # All test projects, mirrors src/ structure
├── docs/                         # Documentation (architecture, use cases, analysis)
├── LocalNuget/                   # Local NuGet packages
├── .github/                      # GitHub workflows and Copilot instructions
├── docker-compose.yml            # Docker Compose configuration
├── Directory.Build.props/targets # Solution-wide MSBuild settings
├── Slottet.CareManagement.slnx   # Solution file
└── global.json                   # .NET SDK version management
```

### Folder Responsibilities
- **src/Core/**: Contains core business logic, application services, and interfaces. No dependencies on other layers.
- **src/Domain/**: Contains domain entities, value objects, and domain logic. Pure business rules.
- **src/Infrastructure/**: Implements interfaces from Core, handles data access (e.g., MySQL) and external integrations.
- **src/WebUI/**: Blazor frontend project for user interface and client logic.
- **tests/**: Contains test projects for each main layer, follows the same structure as `src/`.
- **docs/**: Markdown documentation for architecture, use cases, and analysis.
- **LocalNuget/**: Local NuGet package repository.
- **.github/**: CI/CD workflows and Copilot instructions.

For more details, see the documentation in `docs/` and `.github/copilot-instructions.md`.

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

<!-- MARKDOWN LINKS & IMAGES -->
[contributors-shield]: https://img.shields.io/github/contributors/DMOoF25-Team6/Slottets-Drifttavlen.svg?style=for-the-badge
[contributors-url]: https://github.com/DMOoF25-Team6/Slottets-Drifttavlen/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/DMOoF25-Team6/Slottets-Drifttavlen.svg?style=for-the-badge
[forks-url]: https://github.com/DMOoF25-Team6/Slottets-Drifttavlen/network/members
[stars-shield]: https://img.shields.io/github/stars/DMOoF25-Team6/Slottets-Drifttavlen.svg?style=for-the-badge
[stars-url]: https://github.com/DMOoF25-Team6/Slottets-Drifttavlen/stargazers
[repo-size-shield]: https://img.shields.io/github/repo-size/DMOoF25-Team6/Slottets-Drifttavlen.svg?style=for-the-badge
[repo-size-url]: https://github.com/DMOoF25-Team6/Slottets-Drifttavlen
[issues-shield]: https://img.shields.io/github/issues/DMOoF25-Team6/Slottets-Drifttavlen.svg?style=for-the-badge
[issues-url]: https://github.com/DMOoF25-Team6/Slottets-Drifttavlen/issues

[Coverage-shield]: https://img.shields.io/codecov/c/github/DMOoF25-Team6/Slottets-Drifttavlen.svg?style=for-the-badge
[Coverage-url]: https://codecov.io/gh/DMOoF25-Team6/Slottets-Drifttavlen
