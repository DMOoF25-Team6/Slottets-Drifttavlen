# AI Agents – Overblik og Flowdiagram

Dette dokument giver et visuelt overblik over alle AI-agenter i projektet, deres ansvarsområder og den måde de arbejder sammen på.

---

## 1. Mindmap – Alle agenter

```mermaid
mindmap
  root((AI Agents))
    Artefakt-generering
      UC Agent
        usecase-artifact.agent.md
        Opretter og vedligeholder use cases
      DM Agent
        dm-artifact.agent.md
        Domænemodel med Mermaid
      SSD Agent
        ssd-artifact.agent.md
        System Sequence Diagrams
      SD Agent
        sd-artifact.agent.md
        Detaljerede Sequence Diagrams
      DCD Agent
        dcd-artifact.agent.md
        Domain Class Diagrams
      ERD Agent
        erd-artifact.agent.md
        Entity Relationship Diagrams
      OC Agent
        oc-artifact.agent.md
        Operation Contracts
      UFD Agent
        userflow-artifact.agent.md
        User Flow Diagrams
      Wireframe Agent
        wireframe-artifact.agent.md
        ASCII Wireframes
    Krav og Planlægning
      FURPS Agent
        furps-artifact.agent.md
        Funktionelle og ikke-funktionelle krav
      KPI Agent
        kpi-artifact.agent.md
        Key Performance Indicators
      Milestones Agent
        milestones-gateways.agent.md
        Milepæle og gateways
    Kodekvalitet
      Janitor Agent
        csharp-dotnet-janitor.agent.md
        C# kode-oprydning og modernisering
      XUnit Agent
        xunit-agent.md
        Test-standarder og best practices
    Test
      Playwright Agent
        playwright-tester.agent.md
        E2E browser-automatisering
    Skabelon
      My Agent
        my-agent.agent.md
        Template til egne agenter
```

---

## 2. Primær pipeline – Artefaktgenerering

```mermaid
flowchart TD
    BC[📄 docs/bc.md\nBusiness Case] --> UC

    UC["🎯 UC Agent\nusecase-artifact.agent.md\n─────────────────\nOpretter Use Cases\ndocs/use-cases/uc-NNN/uc-NNN.usecase.md"]

    UC --> DM
    UC --> UFD
    UC --> WF

    DM["🗂️ DM Agent\ndm-artifact.agent.md\n─────────────────\nDomænemodel med Mermaid\ndocs/use-cases/uc-NNN/uc-NNN.dm.md"]

    DM --> SSD
    DM --> WF

    SSD["📋 SSD Agent\nssd-artifact.agent.md\n─────────────────\nSystem Sequence Diagram\ndocs/use-cases/uc-NNN/uc-NNN.ssd.md"]

    SSD --> OC
    SSD --> SD

    OC["📝 OC Agent\noc-artifact.agent.md\n─────────────────\nOperation Contracts\ndocs/use-cases/uc-NNN/uc-NNN.oc.md"]
    OC --> SD

    SD["🔄 SD Agent\nsd-artifact.agent.md\n─────────────────\nSequence Diagram (objektniveau)\ndocs/use-cases/uc-NNN/uc-NNN.sd.md"]

    SD --> DCD

    DCD["🏛️ DCD Agent\ndcd-artifact.agent.md\n─────────────────\nDomain Class Diagram\n(opdelt i Domain/Application/Infrastructure)\ndocs/use-cases/uc-NNN/uc-NNN.dcd.md"]

    DCD --> ERD

    ERD["🗃️ ERD Agent\nerd-artifact.agent.md\n─────────────────\nEntity Relationship Diagram\ndocs/use-cases/uc-NNN/uc-NNN.erd.md"]

    UFD["🔀 UFD Agent\nuserflow-artifact.agent.md\n─────────────────\nUser Flow Diagram (Mermaid flowchart)\ndocs/use-cases/uc-NNN/uc-NNN.userflow.md"]

    WF["🖼️ Wireframe Agent\nwireframe-artifact.agent.md\n─────────────────\nASCII UI wireframes\ndocs/use-cases/uc-NNN/uc-NNN.wireframe.md"]

    style BC fill:#f5f0e8,stroke:#c8a96e
    style UC fill:#dbeafe,stroke:#3b82f6
    style DM fill:#dbeafe,stroke:#3b82f6
    style SSD fill:#dbeafe,stroke:#3b82f6
    style OC fill:#dbeafe,stroke:#3b82f6
    style SD fill:#dbeafe,stroke:#3b82f6
    style DCD fill:#dbeafe,stroke:#3b82f6
    style ERD fill:#dbeafe,stroke:#3b82f6
    style UFD fill:#dcfce7,stroke:#16a34a
    style WF fill:#dcfce7,stroke:#16a34a
```

---

## 3. Krav og planlægnings-pipeline

```mermaid
flowchart TD
    BC2[📄 docs/bc.md\nBusiness Case] --> FURPS

    FURPS["📊 FURPS Agent\nfurps-artifact.agent.md\n─────────────────\nFunctionality, Usability, Reliability,\nPerformance, Supportability krav\ndocs/furps.md\nID-format: REQ-F-001, REQ-U-001..."]

    FURPS -->|"Trigger: efter FURPS ændringer"| KPI

    KPI["📈 KPI Agent\nkpi-artifact.agent.md\n─────────────────\nKey Performance Indicators\ndocs/kpi.md\nID-format: KPI-FIN-001, KPI-CUST-001..."]

    KPI --> MS

    MS["🏁 Milestones Agent\nmilestones-gateways.agent.md\n─────────────────\nMilepæle og projektgateways\ndocs/milestones-gateways.md"]

    style BC2 fill:#f5f0e8,stroke:#c8a96e
    style FURPS fill:#fef9c3,stroke:#ca8a04
    style KPI fill:#fef9c3,stroke:#ca8a04
    style MS fill:#fef9c3,stroke:#ca8a04
```

---

## 4. Kodekvalitets- og testpipeline

```mermaid
flowchart LR
    CODE[💻 Kodebase\n.NET / C#]

    CODE --> JAN
    CODE --> XU
    CODE --> PW

    JAN["🧹 Janitor Agent\ncsharp-dotnet-janitor.agent.md\n─────────────────\nKode-oprydning og modernisering\nTeknisk gæld, performance,\ntest coverage, dokumentation"]

    XU["✅ XUnit Agent\nxunit-agent.md\n─────────────────\nTest-standarder\nTrait-brug, region-gruppering,\nhelper-placering"]

    PW["🎭 Playwright Agent\nplaywright-tester.agent.md\n─────────────────\nE2E browser-tests\nWebsite exploration → locators\n→ test generation → execution"]

    style CODE fill:#f1f5f9,stroke:#64748b
    style JAN fill:#fce7f3,stroke:#db2777
    style XU fill:#fce7f3,stroke:#db2777
    style PW fill:#fce7f3,stroke:#db2777
```

---

## 5. Agenternes værktøjsadgang

```mermaid
flowchart TD
    subgraph BASIC["Basis-værktøjer (alle dokumentagenter)"]
        T1[new – opret filer]
        T2[edit / editFiles – redigér filer]
        T3[search – søg i indhold]
        T4[lookup – slå referencer op]
    end

    subgraph ADVANCED["Udvidede værktøjer (udvalgte agenter)"]
        T5[validate/mermaid – valider Mermaid syntax]
        T6[evaluate – kvalitetsvurdering]
        T7[update/glossary – opdatér ordliste]
        T8[update/crossReference – krydsreferencer]
        T9[delete – slet filer]
    end

    subgraph CODE_TOOLS["Kode-agenternes værktøjer"]
        T10[runCommands / runTasks / runTests]
        T11[problems / terminalLastCommand]
        T12[playwright – browserautomatisering]
        T13[microsoft.docs.mcp – Microsoft docs]
        T14[githubRepo / github – GitHub API]
        T15[codebase – kodebaseanalyse]
    end

    UC_DM[UC, DM, SSD, OC,\nUFD, Wireframe agenter] --> BASIC
    DCD_ERD[DCD, ERD, Milestones agenter] --> BASIC
    DCD_ERD --> ADVANCED
    FURPS_KPI[FURPS, KPI agenter] --> BASIC
    FURPS_KPI --> T6

    JAN_XU[Janitor, XUnit agenter] --> CODE_TOOLS
    PW2[Playwright Agent] --> CODE_TOOLS
```

---

## 6. Filstruktur – genereret af agenter

```mermaid
flowchart TD
    ROOT[📁 docs/] --> BC_F[bc.md – Business Case]
    ROOT --> DM_F[dm.md – Løsnings-DM]
    ROOT --> DCD_F[dcd.md – Løsnings-DCD]
    ROOT --> FURPS_F[furps.md + furps.da.md]
    ROOT --> KPI_F[kpi.md]
    ROOT --> GL[glossary.md + glossary.da.md]
    ROOT --> MS_F[milestones-gateways.md]
    ROOT --> UC_DIR[📁 use-cases/]

    UC_DIR --> UC_N[📁 uc-NNN/]
    UC_N --> UC1[uc-NNN.usecase.md ← UC Agent]
    UC_N --> UC2[uc-NNN.dm.md ← DM Agent]
    UC_N --> UC3[uc-NNN.ssd.md ← SSD Agent]
    UC_N --> UC4[uc-NNN.sd.md ← SD Agent]
    UC_N --> UC5[uc-NNN.oc.md ← OC Agent]
    UC_N --> UC6[uc-NNN.dcd.md ← DCD Agent]
    UC_N --> UC7[uc-NNN.erd.md ← ERD Agent]
    UC_N --> UC8[uc-NNN.userflow.md ← UFD Agent]
    UC_N --> UC9[uc-NNN.wireframe.md ← Wireframe Agent]
```

---

## 7. Clean Architecture – håndhævet af SD og DCD agenter

```mermaid
flowchart BT
    INFRA["🔧 Infrastructure Layer\nDatabaseadgang, eksterne services,\nrepos, API-klienter"]
    APP["⚙️ Application Layer\nUse case handlers, services,\nkommandoer, forespørgsler"]
    DOMAIN["🏛️ Domain Layer\nEntiteter, value objects,\nforretningslogik, domæneevents"]
    PRES["🖥️ Presentation Layer\nBlazor UI, API controllers,\nviewmodels, mappers"]

    INFRA -->|"afhænger af"| APP
    APP -->|"afhænger af"| DOMAIN
    PRES -->|"afhænger af"| APP

    style DOMAIN fill:#dbeafe,stroke:#3b82f6
    style APP fill:#dcfce7,stroke:#16a34a
    style INFRA fill:#fef9c3,stroke:#ca8a04
    style PRES fill:#fce7f3,stroke:#db2777
```

> **Regel:** Afhængigheder peger altid indad. Domain Layer kender intet til Infrastructure eller Presentation.
```
