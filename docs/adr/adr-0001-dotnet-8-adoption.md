---
title: "ADR-0001: .NET 8 Adoption"
status: "Proposed"
date: "2024-06-07"
authors: "Team6"
tags: ["architecture", "decision"]
supersedes: ""
superseded_by: ""
---

## ADR-0001: .NET 8 Adoption

## Status

**Proposed**

## Context

Kunden har stillet krav om brug af .NET 8 som platform for applikationen. Dette valg er baseret på forretningsbehov, teknisk kompatibilitet, support og fremtidig vedligeholdelse.

## Decision

Løsningen implementeres på .NET 8 for at opfylde kundens krav om moderne, supporteret og cloud-ready platform.

## Consequences

### Positive

 - **ADR-0001-POS-001**: Forbedret performance, maintainability og cloud readiness
 - **ADR-0001-POS-002**: Overholder Clean Architecture og containerization best practices

### Negative

 - **ADR-0001-NEG-001**: Potentiel lock-in til .NET 8 features og API'er
 - **ADR-0001-NEG-002**: Kræver opgradering af udviklingsmiljø og CI/CD pipelines
 - **ADR-0001-NEG-003**: Fremtidige migreringer kan kræve ekstra ressourcer
 - **ADR-0001-NEG-004**: End Of Life for .NET 8 forventes i maj 2026, hvilket kan kræve opgradering inden da

## Alternatives Considered

### DOTNET 10

 - **ADR-0001-ALT-001**: **Description**: Nyere version med potentielle forbedringer og features
 - **ADR-0001-ALT-002**: **Rejection Reason**: Kunden ønskede ikke dette på nuværende tidspunkt, da .NET 10 endnu ikke er bredt adopteret og supporteret.

## Implementation Notes

 - **ADR-0001-IMP-001**: Alle projekter konfigureres til .NET 8 i global.json og projektfiler
 - **ADR-0001-IMP-002**: CI/CD og Docker opsætning tilpasses .NET 8
 - **ADR-0001-IMP-003**: Overvågning af fremtidige .NET releases for migreringsmuligheder

## References

 - **ADR-0001-REF-001**: Clean Architecture principper
 - **ADR-0001-REF-002**: .NET 8 dokumentation
 - **ADR-0001-REF-003**: Projektets README.md og global.json
