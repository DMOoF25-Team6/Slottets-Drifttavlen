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
Slottet er et døgnbemandet bosted, der yder omsorg og pleje til borgere med særlige behov. Personalet arbejder i dag-, aften- og nattevagter og sikrer kontinuerlig støtte og pleje. Teamet samarbejder dagligt om at levere høj kvalitet og sikre borgernes trivsel og sikkerhed.

## Arbejdsgang
Ved vagtskifte udveksler afgående og tilgående vagthold kritisk information om borgernes tilstand, medicinhåndtering og opgaver. Overlapsskemaet indeholder:
- Borgerens aktuelle tilstand (trafiklysmodel)
- Medicinstatus og udlevering
- Personale tilknyttet borgeren
- Opgaver og beskeder til næste vagthold
- Særlige hændelser

## Problemstilling
Slottet anvender i dag et papirbaseret overlapsskema, hvilket medfører risiko for fejl, manglende historik og tidskrævende håndtering. Projektet har til formål at udvikle et digitalt IT-system, der erstatter det papirbaserede skema og understøtter personalets daglige arbejde mere effektivt og sikkert.

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

This solution follows Clean Architecture principles and is organized as follows:

```
├── src/                # All production source code
│   ├── Core/           # Core business logic (use cases, interfaces)
│   ├── Domain/         # Domain models and business entities
│   ├── Infrastructure/ # Infrastructure (data access, external services)
│   └── WebUI/          # Blazor frontend (UI and client logic)
├── tests/              # All test projects, mirroring src/ structure
├── docs/               # Documentation (architecture, use cases, analysis)
├── LocalNuget/         # Local NuGet packages
├── .github/            # GitHub workflows and Copilot instructions
├── docker-compose.yml  # Docker Compose configuration
├── Directory.Build.props/targets # Solution-wide MSBuild settings
├── DMOoF25-Team6.Slottets-Drifttavlen.slnx # Solution file
└── global.json         # .NET SDK version management
```

### Folder Responsibilities
- **src/Core/**: Contains core business logic, application services, and interfaces. No dependencies on other layers.
- **src/Domain/**: Contains domain entities, value objects, and domain logic. Pure business rules.
- **src/Infrastructure/**: Implements interfaces from Core, handles data access (e.g., MySQL), and external integrations.
- **src/WebUI/**: Blazor frontend project for user interface and client-side logic.
- **tests/**: Contains test projects for each main layer, following the same structure as `src/`.
- **docs/**: Markdown documentation for architecture, use cases, and analysis.
- **LocalNuget/**: Local NuGet package storage.
- **.github/**: CI/CD workflows and Copilot instructions.

For more details, see the documentation in `docs/` and `.github/copilot-instructions.md`.

---

## Opsætningsvejledning
1. **Krav:** .NET 8 SDK, Docker (valgfrit), SQL Server/MySQL
2. **Clone repo:**  
```sh
git clone https://github.com/Tirsvad/DMOoF25-Team6.Slottets-Drifttavlen.git
cd DMOoF25-Team6.Slottets-Drifttavlen
```
3. **Build solution:**  
```sh
dotnet build DMOoF25-Team6-Slottets-Drifttavlen.slnx
```
4. **Kør tests:**  
```sh
dotnet test DMOoF25-Team6-Slottets-Drifttavlen.slnx
```
5. **Start applikation:**  
```sh
dotnet run --project src/WebUI/WebUI/WebUI.csproj
```
6. **Docker (valgfrit):**  
```sh
docker-compose up
```

## Windows Sandbox (version 2)

I version 2 af projektet anvendes Windows Sandbox for at isolere miljøet, så der kun er adgang til lokalt netværk og ikke WAN. Dette øger sikkerheden og muliggør test i et afskærmet miljø.

For at oprette et isoleret testmiljø på Windows kan du aktivere Windows Sandbox (version 2) med følgende PowerShell-kommando:

```powershell
Enable-WindowsOptionalFeature -Online -FeatureName "Containers-DisposableClientVM" -All
```

Kør PowerShell som administrator. Genstart computeren hvis nødvendigt. Dette giver mulighed for at køre en sikker sandbox til test og udvikling.


## Docker Compose MySQL Setup

To run MySQL with Docker Compose, you need a `.env` file in your project root. This file provides environment variables for the MySQL container.

### Create `.env` file
Add a file named `.env` in the same directory as `docker-compose.yml` with the following content:

```
MYSQL_ROOT_PASSWORD=your_root_password
MYSQL_DATABASE=your_database_name
MYSQL_USER=your_username
MYSQL_PASSWORD=your_user_password
```

Replace the values with your desired credentials.

### Example
```
MYSQL_ROOT_PASSWORD=rootpassword
MYSQL_DATABASE=slottetsdb
MYSQL_USER=appuser
MYSQL_PASSWORD=apppassword
```

### Usage
When you run `docker-compose up`, these variables will be used to configure the MySQL container.
or just only run mysql container:
`docker-compose up slottets-sqlserver`

Alternatively, you can run the MySQL container directly with Docker using the following command:
```sh
docker run --name slottets-sqlserver -e MYSQL_ROOT_PASSWORD=your_root_password -e MYSQL_DATABASE=your_database_name -e MYSQL_USER=your_username -e MYSQL_PASSWORD=your_user_password -p 3307:3306 -d mysql
```



---
