
<a name="forside"></a>
# Slottets Drifttavlen

[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Repo Size][repo-size-shield]][repo-size-url]
[![Issues][issues-shield]][issues-url]



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

<a name="database-migration"></a>
## 🧪 Database migration

dotnet ef migrations add <name> --project src/Infrastructure.Data --startup-project src/Api
dotnet ef database update --project src/Infrastructure.Data --startup-project src/Api

<a name="for-udviklere"></a>
## 👨‍💻 For udviklere

Struktur (Clean Architecture)
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
