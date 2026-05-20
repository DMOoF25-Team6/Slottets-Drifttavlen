# MDA Artifacts

This directory contains UML artifacts organized according to the **Object Management Group's (OMG) Model Driven Architecture (MDA)** framework.

## Purpose

While `docs/use-cases/` organizes artifacts by use case to match our agile/Scrum workflow, this directory provides a parallel **MDA-structured view** that demonstrates academic traceability from business requirements through platform-independent analysis to platform-specific design.

## Structure

```
mda-artifacts/
├── cim/        Computation Independent Model (business perspective)
├── pim/        Platform Independent Model (analysis perspective)
├── psm/        Platform Specific Model (design perspective)
└── mappings/   Transformation specifications between layers
```

## MDA Abstraction Levels

### CIM — Computation Independent Model

**What it describes:** The business domain and requirements, without reference to any system or technology.

**Perspective:** Conceptualization. Focused on the problem domain.

**Artifacts in this project:**
- Use case overview (UML)
- Domain glossary (UML class diagram, conceptual)
- Business rules and constraints

**External references:** Business Case (`docs/Business_Case`), FURPS+ (`docs/furps.md`), and Stakeholder Analysis remain in their original locations as they predate this MDA structure.

### PIM — Platform Independent Model

**What it describes:** The system's operation, independent of any implementation technology. Could be implemented in Java, .NET, Python, or any platform.

**Perspective:** Specification. Focused on analysis activities.

**Artifacts:**
- Detailed use case
- Domain model (system-level entities, no language types)
- System sequence diagram (system as black box)
- Operation contracts

### PSM — Platform Specific Model

**What it describes:** The system's operation using specific platform technologies. In this project: **.NET 8, Blazor, EF Core, MySQL, Clean Architecture**.

**Perspective:** Specification. Focused on design activities.

**Artifacts:**
- Design class diagram (with .NET types, Clean Architecture layers)
- Sequence diagram (with concrete services, managers, repositories)
- Entity relationship diagram (MySQL schema)

### Mappings

**What they describe:** The rules and decisions for transforming one model into the next (CIM→PIM→PSM).

In a fully automated MDA process, these would be machine-executable. In this project they are documented manually as traceability evidence.

## File Naming Convention

```
uc-<id>.<artifact>.<level>.<extension>

Examples:
  uc-005.usecase.cim.puml      ← CIM use case overview
  uc-005.dm.pim.puml           ← PIM domain model
  uc-005.dcd.psm.puml          ← PSM design class diagram
```

The `.cim`/`.pim`/`.psm` infix makes the abstraction level visible in the filename itself.

## Pilot Scope

Currently only **UC-005 (Dashboard PhoneList)** is implemented as a pilot. If this proves valuable for the team and exam preparation, additional use cases will follow in subsequent pull requests.

## Relationship to Other Documentation

| Directory | Organized by | Purpose |
|-----------|--------------|---------|
| `docs/use-cases/` | Use case | Agile working artifacts (Mermaid in Markdown) |
| `docs/mda-artifacts/` | MDA level | Academic MDA-structured view (PlantUML) |
| `docs/Business_Case` | — | Business case document (CIM-level, textual) |
| `docs/furps.md` | — | FURPS+ requirements (CIM-level, structured) |

## References

- OMG Model Driven Architecture: https://www.omg.org/mda/
- UML 2.5.1 Specification: OMG document formal/2017-12-05
- Cephas Consulting Corp. "The Fast Guide to Model Driven Architecture" (2006)

## Version Log

| Version | Date       | Description                                  | Author |
|---------|------------|----------------------------------------------|--------|
| 0001    | 2026-05-19 | Initial structure with UC-005 pilot          | Team 6 |
