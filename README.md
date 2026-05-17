
<a name="forside"></a>
# Slottets Drifttavlen

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Repo Size][repo-size-shield]][repo-size-url]
[![Issues][issues-shield]][issues-url]
[![Coverage][Coverage-shield]][Coverage-url]
[![GitHub Pages][GithubPages-shield]][GithubPages-url]


<a name="indholdsfortegnelse"></a>

## 📚 Indholdsfortegnelse

- [Dokumentationsoversigt](#dokumentationsoversigt)
- [Om projektet](#om-projektet)
- [Problemstilling](#problemstilling)
- [Funktionalitet](#funktionalitet)
- [Arkitektur & Teknologi](#arkitektur--teknologi)
- [Udvidelsesmuligheder](#udvidelsesmuligheder)
- [Dokumentation](#dokumentation)
- [Sikkerhed](#sikkerhed)
- [Claims-baseret adgangskontrol](#claims-baseret-adgangskontrol)
- [Kom i gang](#kom-i-gang)
- [Adgang](#adgang)
- [Vigtigt](#vigtigt)
- [Database migration](#database-migration)
- [For udviklere](#for-udviklere)
- [Copilot Agent (valgfrit)](#copilot-agent-valgfrit)
- [CI/CD](#cicd)

<a name="dokumentationsoversigt"></a>
## Dokumentationsoversigt

- [Business Case](docs/bc.md)
- [FURPS+](docs/furps.md)
- [KPI](docs/kpi.md)
- [Domænemodel](docs/dm.da.md)
- [Milepæle og leverancer](docs/milestones.md)
- [Risikoanalyse](docs/risk-analysis.md)
- [Milepæleplan / Leverancer](docs/milestones.md)

<a name="om-projektet"></a>
## 🧭 Om projektet
**Slottets Drifttavle** er et digitalt system til håndtering af vagtskifte i et døgnbemandet bosted.

Systemet erstatter et papirbaseret overlapsskema og forbedrer:
- Overblik
- Datasikkerhed
- Kommunikation mellem vagthold
- Sporbarhed og historik
 
<a name="problemstilling"></a>
## 🚨 Problemstilling
Den nuværende papirbaserede løsning medfører:
- Risiko for fejl og manglende information
- Ingen historik
- Tidskrævende arbejdsgange

👉 Løsningen er et digitalt system med struktureret data og real-time adgang.


---

<a name="funktionalitet"></a>
## ⚙️ Funktionalitet
- **Vagtvalg ved login**
- **Borgeroversigt med trafiklysstatus**
- **Medicin- og opgavestyring**
- **Hændelsesregistrering**
- **Fælles ressourcer (telefoner, ansvar)**
- **Rollebaseret adgang (RBAC)**
- **Audit log & historik**

---

<a name="arkitektur--teknologi"></a>
## 🏗️ Arkitektur & Teknologi
- **Backend:** ASP.NET Core
- **Frontend:** Blazor
- **Database:** MySQL / SQL Server (EF Core)
- **Container:** Docker
- **Arkitektur:** Clean Architecture
- **Deployment:** Cloud-ready (Azure / AWS)

---

<a name="udvidelsesmuligheder"></a>
## 📈 Udvidelsesmuligheder
- Integration til FMK
- Notifikationer (medicin/opgaver)
- Rapportering & analyse

---

<a name="dokumentation"></a>
## 📚 Dokumentation
Findes i `docs/`:

- Business Case  
- FURPS+  
- KPI  
- Domænemodel  
- Risikoanalyse  
- Milepæle  

---

<a name="sikkerhed"></a>

## 🔐 Sikkerhed
- GDPR-compliant databehandling
- Kryptering af følsomme data
- Rollebaseret adgangskontrol
- Logging og sporbarhed

---

<a name="claims-baseret-adgangskontrol"></a>
## 🛡️ Claims-baseret adgangskontrol

Systemet anvender claims-baseret adgangskontrol for at styre adgang til funktioner og data. Claims tildeles brugere og roller under identitets-seeding og kontrolleres ved kørsel for at afgøre, hvilke handlinger en bruger må udføre.

### Claim-struktur

- **Claim Type:** `permission`
- **Claim Value:** Struktureret som `ressource:scope:handling`, fx:
  - `department:skoven:basic`
  - `department:all:view`
  - `manage:residents`

### Eksempel: Adgang til afdeling

For at se borgere i en bestemt afdeling (fx Skoven) skal brugeren have et claim med den relevante tilladelse:

- **Påkrævet claim:** `permission = department:skoven:*` (eller mere specifikt `department:skoven:basic`)
- Kun brugere med dette claim kan tilgå borgerdata for Skoven.

#### Eksempel

| Bruger       | Claim Value                | Kan se Skoven-borgere?      |
|--------------|---------------------------|-----------------------------|
| normal2User  | department:skoven:basic   | Ja                          |
| normal3User  | department:skoven:basic   | Ja                          |
| normal1User  | department:slottets:basic | Nej                         |
| adminUser    | department:all:view       | Ja (alle afdelinger)        |

### Sådan tjekkes claims

Når en bruger forsøger at tilgå en beskyttet ressource (fx se borgere i Skoven), tjekker systemet for det nødvendige claim:

- Hvis brugeren har `permission = department:skoven:*` eller et mere generelt claim som `department:all:view`, gives adgang.
- Ellers nægtes adgang.

### Udvidelse af claims

For at tilføje nye tilladelser defineres nye claim-værdier efter det etablerede mønster og tildeles relevante brugere eller roller.

Se `src/Infrastructure.Data/Persistent/Configurations/IdentitySeed.cs` for detaljer om claims-seeding.

---

<a name="kom-i-gang"></a>
## 🚀 Kom i gang

### Krav
- .NET 8 SDK  
- Docker  

---

### 1. Klon projektet
```sh
git clone git@github.com:DMOoF25-Team6/Slottets-Drifttavlen.git
cd Slottets-Drifttavlen
```

### 2. Opret .env fil
```plain
MYSQL_ROOT_PASSWORD=rootpassword
MYSQL_DATABASE=slottetsdb
MYSQL_USER=appuser
MYSQL_PASSWORD=apppassword
MYSQL_HOST=slottets-sqlserver

TokenValidationParameters__IssuerSigningKey=YOUR_SECRET
TokenValidationParameters__Issuer=http://localhost
TokenValidationParameters__Audience=http://localhost

ExpireMinutes=60

# UC-010 GDPR pseudonymisation salt (HMAC-SHA256 key).
# Required by PseudonymizationService. MUST be kept outside source control and
# rotated only in coordination with the Data Protection Officer; rotating
# invalidates existing pseudonyms (Datatilsynet "Pseudonymisering og anonymisering").
GDPR_PSEUDO_SALT=CHANGE_ME_TO_A_LONG_RANDOM_SECRET
```

### 3. Opret nødvendige mapper
```sh
mkdir -p Data DataProtection-Keys src/WebUI/WebUI/DataProtection-Keys
```

### 4. Start systemet
```sh
docker-compose --profile prod up
```

---

<a name="adgang"></a>
## 🔗 Adgang

| Service | URL                                            |
| ------- | ---------------------------------------------- |
| WebUI   | [http://localhost:5050](http://localhost:5050) |
| API     | [http://localhost:5051](http://localhost:5051) |
| DB      | localhost:3307                                 |

---

<a name="vigtigt"></a>
## ⚠️ Vigtigt

Projektet kører kun via Docker Compose

❌ Brug ikke:

```sh
dotnet run
```

✔ Docker sikrer ens miljø for hele teamet

---

<a name="forslag-til-systemforbedringer-og-automatisering"></a>
## Forslag til systemforbedringer og automatisering

- Tynd klient til dedikeret Dashboard
  - Automatiseret opstart af Dashboard ved login 
- Tynd klient til Webserser / API / Database
  - Nginx som reverse proxy for WebUI
  - Nginx som reverse proxy for API
  - Mysql server
  - Cronjobs for backup og vedligeholdelse

<a name="automatic-dashboard-start"></a>
### Cronjob for automatisk start af Dashboard på skærm nummer to (valgfrit)

I folder `tools/` findes eksempel på et script `start-dashboard.bat` som kan bruges til at starte Dashboardet på skærm nummer to ved opstart af computeren.

1. Placer `start-dashboard.bat` i din opstartsmappe (Windows: `shell:startup`)
1. Åbn Opgaveplanlægger: Tryk på Win+R, skriv taskschd.msc, og tryk på Enter.
1. Opret opgave: Klik på Opret opgave, angiv et navn, og klik på Ny i fanen Handlinger for at køre launch_browser.bat.
1. Planlæg: Klik på Ny i fanen Udløsere for at indstille tidspunktet (f.eks. ved login)

**Problemstilling**: Dashboardet skal starte automatisk på skærm nummer to ved opstart af computeren og autologin til systemet for at sikre, at det altid er tilgængeligt for personalet uden behov for manuel indgriben.

**Alternativt**: Man kan investere i en tynd klient eller mini-pc, der er dedikeret til at køre Dashboardet, og konfigurere den til at starte Dashboardet ved opstart. Isoleret netværk og begrænset adgang for at øge sikkerheden.

---

<a name="for-udviklere"></a>

## 👨‍💻 For udviklere

### Struktur (Clean Architecture)
src/
 ├── Core/
 ├── Domain/
 ├── Infrastructure/
 └── WebUI/
tests/
docs/

### Ansvar
Core → Use cases & interfaces  
Domain → Forretningslogik  
Infrastructure → Database & eksterne services  
WebUI → Frontend

### Om pull.ps1

Scriptet [`pull.ps1`](#e:\repos\other.dmoof25-team6.slottets-drifttavlen\pull.ps1-context) automatiserer følgende for udviklere:

- Henter seneste kode fra main-branchen
- Genstarter systemet i Docker (prod-profil)
- Dropper og opretter databasen på ny (EF Core)
- Sikrer at du altid arbejder på en frisk database og opdateret kodebase

Dette sikrer et ensartet udviklingsmiljø og minimerer fejl pga. lokale forskelle.


---

<a name="copilot-agent-valgfrit"></a>
## 🤖 Copilot Agent (valgfrit)

Eksempler:

```sh
#uc-artifact.agent.md
Create use case for "Vagtvalg"
```

```sh
#dm-artifact.agent.md
Update domain model for use case 003
```

---

<a name="cicd"></a>
## 🔄 CI/CD

Script: `ci-cd.ps1`

Automatiserer:

- Encoding (UTF-8 + LF)
- Tests via Docker
- Git push

<a name="database-migration"></a>
## 🧪 Database migration

```sh
dotnet ef migrations add <name> --project src/Infrastructure.Data --startup-project src/Api
dotnet ef database update --project src/Infrastructure.Data --startup-project src/Api
```

### UC-010 phase 3 schema changes

After pulling this branch, the model contains a new nullable column
`Resident.DischargedAt` and a new table `SubjectAccessRequests`. Generate
the migration with:

```sh
dotnet ef migrations add Phase3_SubjectAccessRequestAndDischargedAt \
    --project src/Infrastructure.Data --startup-project src/Api
```

Review the generated `Up`/`Down` methods (the `Up` should only `AddColumn`
on `Residents` and `CreateTable` for `SubjectAccessRequests`; if anything
else appears, ensure your local snapshot matches the merged branch before
applying). Then apply to your environment with:

```sh
dotnet ef database update --project src/Infrastructure.Data --startup-project src/Api
```


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
[GithubPages-shield]: https://img.shields.io/badge/GitHub%20Pages-Online-brightgreen?style=for-the-badge
[GithubPages-url]: https://dmoof25-team6.github.io/Slottets-Drifttavlen/
