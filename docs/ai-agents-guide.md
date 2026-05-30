# Guide: Opsætning og udvidelse af AI Agents i et nyt projekt

Denne guide beskriver præcis, hvordan projektets AI agent-infrastruktur er opsat, og hvordan du kan replikere eller udvide den i et nyt projekt.

---

## 1. Hvad er en AI Agent i denne kontekst?

En **AI Agent** er en markdown-fil (`.agent.md`) der instruerer en AI-assistent (GitHub Copilot, Claude osv.) i at udføre en afgrænset opgave – f.eks. at generere et use case-dokument, opdatere et klassediagram eller køre tests. Agenten definerer:

- **Formål** – hvad agenten gør
- **Ansvarsområde** – hvilke filer den arbejder med
- **Instruktioner** – trin-for-trin vejledning til AI'en
- **Referencedokumenter** – hvad agenten læser for kontekst
- **Værktøjer** – hvilke handlinger agenten må udføre
- **Triggere** – hvornår agenten aktiveres, og hvilke agenter den kalder videre

---

## 2. Mappestruktur der skal oprettes

```
.github/
├── agents/                          # Alle agentdefinitioner
│   ├── usecase-artifact.agent.md
│   ├── dm-artifact.agent.md
│   ├── ssd-artifact.agent.md
│   ├── sd-artifact.agent.md
│   ├── dcd-artifact.agent.md
│   ├── oc-artifact.agent.md
│   ├── erd-artifact.agent.md
│   ├── userflow-artifact.agent.md
│   ├── wireframe-artifact.agent.md
│   ├── furps-artifact.agent.md
│   ├── kpi-artifact.agent.md
│   ├── milestones-gateways.agent.md
│   ├── csharp-dotnet-janitor.agent.md
│   ├── xunit-agent.md
│   ├── playwright-tester.agent.md
│   └── my-agent.agent.md            # Skabelon til egne agenter
├── instructions/                    # Standarder og skabeloner
│   ├── usecase.instructions.md
│   ├── dm.instructions.md
│   ├── dcd.instructions.md
│   ├── sd.instructions.md
│   ├── ssd.instructions.md
│   ├── oc.instruction.md
│   ├── erd.instructions.md
│   ├── userflow.instructions.md
│   ├── wireframe.instructions.md
│   ├── furps.intructions.md
│   ├── kpi.instructions.md
│   ├── milestones-gateways.instructions.md
│   ├── bc.instructions.md
│   ├── glossary.instructions.md
│   ├── csharp.instructions.md
│   ├── blazor.instructions.md
│   └── xunit-agent.instructions.md
├── prompts/                         # Genanvendelige prompt-skabeloner
│   ├── create-architectural-decision-record.prompt.md
│   ├── csharp-docs.prompt.md
│   ├── csharp-xunit.prompt.md
│   └── dotnet-design-pattern-review.prompt.md
├── skills/                          # Specialiserede skill-filer
│   ├── csharp-async/SKILL.md
│   └── xunit/xunit-best-practices.skill.md
├── copilot-instructions.md          # Overordnede projektinstruktioner
└── workflows/
    └── ci-cd.yml                    # CI/CD pipeline

docs/
├── bc.md                            # Business Case (SKAL oprettes først)
├── dm.md                            # Løsnings-niveau domænemodel
├── dcd.md                           # Løsnings-niveau klassediagram
├── furps.md                         # Kravdokumentation
├── kpi.md                           # KPI-dokumentation
├── glossary.md                      # Ordliste (engelsk)
├── glossary.da.md                   # Ordliste (dansk)
├── milestones-gateways.md           # Milepæle
├── risk-analysis.md                 # Risikoanalyse
└── use-cases/
    └── uc-001/                      # Et use case pr. mappe
        ├── uc-001.usecase.md
        ├── uc-001.dm.md
        ├── uc-001.ssd.md
        ├── uc-001.sd.md
        ├── uc-001.oc.md
        ├── uc-001.dcd.md
        ├── uc-001.erd.md
        ├── uc-001.userflow.md
        └── uc-001.wireframe.md
```

---

## 3. Anatomien i en agentfil

Alle agentfiler følger samme struktur. Her er skabelonen:

```markdown
---
description: >
  Kort beskrivelse af agentens formål. Denne tekst vises
  når agenten vælges i Copilot.
tools:
  - new
  - edit
  - editFiles
  - search
  - lookup
---

# [AgentNavn] – [Formål]

## Rolle og ansvar
Beskriv hvad agenten gør, og hvilke filer den ejer.

## Referencedokumenter
- `docs/bc.md` – primær forretningskontekst
- `.github/instructions/xxx.instructions.md` – standarder

## Arbejdsproces
1. Læs referencedokumenter
2. Generer eller opdatér artefakt
3. Validér output
4. Opdatér ordliste og krydsreferencer
5. Notér afhængige agenter der skal opdateres

## Output-format
Beskriv præcis filnavn, mappeplacering og format.

## Triggere
- Aktiveres manuelt via `#usecase-artifact.agent.md`
- Eller når [forudgående agent] har afsluttet sit arbejde

## Afhængige agenter
Efter afslutning bør følgende agenter opdateres:
- `#dm-artifact.agent.md`
- `#ssd-artifact.agent.md`
```

---

## 4. Trin-for-trin: Opsætning i et nyt projekt

### Trin 1: Opret mappestruktur

```bash
mkdir -p .github/agents
mkdir -p .github/instructions
mkdir -p .github/prompts
mkdir -p .github/skills
mkdir -p docs/use-cases
mkdir -p docs/quality-criteria
```

### Trin 2: Opret Business Case (obligatorisk startpunkt)

Filen `docs/bc.md` er **primær kontekst** for alle agenter. Opret den med:

```markdown
# Business Case: [Projektnavn]

## Forretningsformål
[Beskriv problemet der løses]

## Målgruppe
[Hvem er brugerne?]

## Primære funktioner
[Liste over kernefeatures]

## Teknisk stack
[Teknologier, frameworks, arkitekturvalg]

## Domænebegreber
[Nøgleord og definitioner fra forretningsdomænet]
```

### Trin 3: Opret `copilot-instructions.md`

```markdown
# Projektinstruktioner

## Arkitektur
Dette projekt følger Clean Architecture med lagene:
- Domain (entiteter, forretningslogik)
- Application (use case handlers, services)
- Infrastructure (databaser, externe services)
- Presentation (UI, API)

## Build og test
```bash
dotnet build
dotnet test
```

## Konventioner
- Sprog: C# / .NET
- Test: XUnit
- UI: Blazor
```

### Trin 4: Opret instruktionsfiler

Hver agent har en tilhørende instruktionsfil der definerer standarder og skabeloner. Eksempel for use cases:

**`.github/instructions/usecase.instructions.md`:**

```markdown
# Use Case Instruktioner

## Filnavn
`docs/use-cases/uc-NNN/uc-NNN.usecase.md`

## Skabelon
### UC-NNN: [Titel]

**Aktør:** [Primær aktør]
**Forudsætning:** [Hvad skal være sandt]
**Postkondition:** [Hvad er sandt efter]

#### Primært flow
1. [Aktør gør X]
2. [System svarer Y]
...

#### Alternative flows
- **Alt A:** ...

## Navnekonventioner
- ID format: UC-001, UC-002, ...
- Titel: Kort verbum + substantiv ("Opret Booking")
```

### Trin 5: Opret agentfilerne

Kopier agentfilerne fra dette projekt og tilpas til dit domæne. Start med de grundlæggende:

1. `usecase-artifact.agent.md` – opret use cases
2. `dm-artifact.agent.md` – domænemodel
3. `furps-artifact.agent.md` – krav

Tilføj derefter de mere specialiserede:
4. `ssd-artifact.agent.md`, `sd-artifact.agent.md`, `dcd-artifact.agent.md`
5. `erd-artifact.agent.md`, `oc-artifact.agent.md`
6. `userflow-artifact.agent.md`, `wireframe-artifact.agent.md`

### Trin 6: Aktivér agenterne

Agenterne aktiveres manuelt i din AI-assistent. Eksempler fra `README.md`:

```
#usecase-artifact.agent.md
Opret use case for "Brugerlogin"

#dm-artifact.agent.md
Opdatér domænemodel baseret på uc-001

#furps-artifact.agent.md
Tilføj performance-krav til FURPS
```

---

## 5. Agenternes trigger-kæde i detaljer

| Agent aktiveret | Trigger type | Kalder videre til |
|----------------|--------------|-------------------|
| UC Agent | Manuel | DM Agent, UFD Agent, Wireframe Agent |
| DM Agent | UC afsluttet | SSD Agent, Wireframe Agent |
| SSD Agent | DM afsluttet | OC Agent, SD Agent |
| OC Agent | SSD afsluttet | SD Agent |
| SD Agent | SSD + OC afsluttet | DCD Agent |
| DCD Agent | SD afsluttet | ERD Agent |
| ERD Agent | DCD afsluttet | (slut på kæden) |
| FURPS Agent | Manuel | KPI Agent |
| KPI Agent | FURPS afsluttet | Milestones Agent |
| Janitor Agent | Manuel / PR | (uafhængig) |
| XUnit Agent | Manuel / PR | (uafhængig) |
| Playwright Agent | Manuel / PR | (uafhængig) |

---

## 6. Sproghåndtering

Projektet understøtter to sprog parallelt:

| Filtype | Engelsk version | Dansk version |
|---------|----------------|---------------|
| Ordliste | `glossary.md` | `glossary.da.md` |
| FURPS | `furps.md` | `furps.da.md` |
| Domænemodel | `dm.md` | `dm.da.md` |

**Konvention:**
- Metadata, versionering og standard filnavne: **Professionel engelsk**
- Produktejerens domænesprog: **Dansk** (`.da.md` suffix)
- Begge versioner vedligeholdes parallelt af de respektive agenter

---

## 7. Kvalitetskriterier (Quality Criteria)

Agenter som DM, UC og ERD refererer til kvalitetsfiler i `docs/quality-criteria/`. Disse definerer hvad et "godkendt" artefakt er:

```
docs/quality-criteria/
├── artifact/
│   ├── qc-usecase.md          # Krav til use cases
│   ├── qc-userflow.md         # Krav til user flows
│   ├── qc-wireframe.md        # Krav til wireframes
│   └── lld/
│       └── qc-erd.0001.md     # Krav til ERD
├── ood/
│   └── hld/
│       └── qc-dm.md           # Krav til domænemodel
└── OOA/
    ├── qc-furps.md            # FURPS format-regler
    └── qc-kpi.md              # KPI format-regler
```

Opret disse filer med tjeklister der beskriver:
- Obligatoriske felter
- Navnekonventioner
- Diagramsyntaks-regler
- Fuldstændighedskrav

---

## 8. Skab din egen agent (my-agent.agent.md)

Brug skabelonagenten som udgangspunkt:

```markdown
---
description: >
  [Beskriv din agents formål på 1-2 linjer]
tools:
  - new
  - edit
  - editFiles
  - search
  - lookup
---

# [Din Agent Navn]

## Rolle
[Hvad gør denne agent?]

## Input
[Hvilke filer læser agenten?]

## Output
[Hvilke filer opretter/opdaterer agenten?]

## Arbejdsproces
1. [Trin 1]
2. [Trin 2]
3. [Trin 3]

## Triggere
[Hvornår skal denne agent aktiveres?]
```

Gem filen som `.github/agents/min-agent.agent.md` og aktiver med:
```
#min-agent.agent.md
[Din instruktion her]
```

---

## 9. Forbedringer og udvidelsesmuligheder

### 9.1 Automatiske triggere via CI/CD

Nuværende setup kræver **manuel aktivering** af agenter. Det kan automatiseres:

```yaml
# .github/workflows/agent-trigger.yml
on:
  push:
    paths:
      - 'docs/use-cases/**/*.usecase.md'
jobs:
  update-artifacts:
    steps:
      - name: Trigger DM Agent
        # Kald agenten via Copilot API eller GitHub Actions
```

### 9.2 Tilføj valideringstrin

Agenter som DCD og ERD bruger allerede `validate/mermaid`. Udvid dette mønster til alle diagramagenter:

```markdown
## Validering (tilføj til alle diagramagenter)
- Kør `validate/mermaid` på hvert diagram
- Tjek at alle nøgleord fra `docs/bc.md` er repræsenteret
- Verificér krydsreferencer til relaterede artefakter
```

### 9.3 Central orchestrator-agent

Opret en **Master Orchestrator Agent** der koordinerer hele kæden:

```markdown
# master-orchestrator.agent.md
Givet et nyt use case navn:
1. Aktiver UC Agent → vent på completion
2. Aktiver DM Agent med UC output → vent
3. Aktiver SSD Agent med DM output → vent
...osv.
```

### 9.4 Versionshistorik i artefakter

Tilføj automatisk version-log til alle agenter:

```markdown
## Ændringslog (auto-genereret af agent)
| Version | Dato | Agent | Ændring |
|---------|------|-------|---------|
| 1.0 | 2025-01-01 | UC Agent | Initial oprettelse |
| 1.1 | 2025-01-15 | DM Agent | Tilføjet ny entitet |
```

### 9.5 Testdækning-agent

Udvid XUnit Agenten til at:
- Analysere code coverage rapporter
- Identificere utestede metoder
- Automatisk generere test-stubs

### 9.6 AI-modellens parametre

Agenter som Janitor og Playwright specificerer `claude-sonnet-4` som model. For et nyt projekt, overvej:

- **Hurtige/enkle agenter** (UC, DM): Kan bruge en hurtigere model
- **Komplekse agenter** (DCD, SD med Clean Architecture): Brug den mest capable model
- **Kodegenerering** (Janitor, Playwright): Brug en code-optimeret model

### 9.7 Parallelisering

UC Agent kan trigge **UFD, Wireframe og DM parallelt** – de er uafhængige. Dokumentér dette eksplicit i agentfilerne for at understøtte parallelle AI-kald.

---

## 10. Hurtig-reference: Agent og output-filformat

| Agent | Input | Output fil | Format |
|-------|-------|-----------|--------|
| UC Agent | bc.md | `uc-NNN.usecase.md` | Markdown |
| DM Agent | bc.md, uc | `uc-NNN.dm.md` | Mermaid classDiagram |
| SSD Agent | bc.md, uc | `uc-NNN.ssd.md` | Mermaid sequenceDiagram |
| SD Agent | bc.md, dcd | `uc-NNN.sd.md` | Mermaid sequenceDiagram |
| DCD Agent | uc sd filer | `uc-NNN.dcd.md` | Mermaid classDiagram (3 lag) |
| OC Agent | ssd filer | `uc-NNN.oc.md` | Markdown tabel |
| ERD Agent | dcd, dm | `uc-NNN.erd.md` | Mermaid erDiagram |
| UFD Agent | bc.md | `uc-NNN.userflow.md` | Mermaid flowchart |
| Wireframe Agent | bc.md, uc | `uc-NNN.wireframe.md` | ASCII art |
| FURPS Agent | bc.md | `furps.md` | Markdown tabel (REQ-X-NNN) |
| KPI Agent | furps.md | `kpi.md` | Markdown tabel (KPI-X-NNN) |
| Milestones Agent | kpi.md, bc.md | `milestones-gateways.md` | Markdown + Mermaid |
| Janitor Agent | kodebase | Kode-filer | C# / .NET |
| XUnit Agent | test-projekter | Test-filer | C# XUnit |
| Playwright Agent | website | `*.spec.ts` filer | TypeScript Playwright |
