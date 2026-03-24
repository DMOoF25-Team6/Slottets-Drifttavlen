---
title: "ADR-0001: Database Integration (MySQL)"
status: "Proposed"
date: "2026-02-07"
authors: "Team6"
tags: ["architecture", "decision"]
supersedes: ""
superseded_by: ""
---


# ADR-0002: Database Integration (MySQL)


## Status

**Accepted**

## Context

The Dashboard System requires persistent storage for citizens and related data. The system must efficiently store and retrieve citizen information and other relevant entities. Built with ASP.NET Core in Visual Studio, a relational database is necessary to manage entity relationships, ensure data integrity, and support business requirements for real-time dashboard updates.


---

## Decision

The system will use **MySQL** as the primary database. Data access will be managed via **Entity Framework Core** with a custom `ApplicationDbContext` class. This approach enables efficient storage and retrieval of citizen data, maintains code-database synchronization, and leverages the team's familiarity with MySQL and EF Core.


---

## Consequences

### Positive

- **ADR-0002-POS-001**: Enables efficient, scalable storage and retrieval of citizen and related data
- **ADR-0002-POS-002**: Supports maintainability and code-database synchronization via EF Core migrations
- **ADR-0002-POS-003**: Aligns with Clean Architecture and leverages team expertise

### Negative

- **ADR-0002-NEG-001**: Introduces dependency on MySQL server availability and configuration
- **ADR-0002-NEG-002**: Requires ongoing maintenance to keep EF Core models and database schema in sync
- **ADR-0002-NEG-003**: Potential risk of downtime or data loss if database is misconfigured or unavailable


---

## Alternatives Considered

### Microsoft SQL Server

- **ADR-0002-ALT-001**: **Description**: Enterprise-grade relational database with advanced features
- **ADR-0002-ALT-002**: **Rejection Reason**: Higher licensing and operational complexity; team has more experience with MySQL

## Implementation Notes

- **ADR-0002-IMP-001**: Configure EF Core with MySQL provider and implement `ApplicationDbContext`
- **ADR-0002-IMP-002**: Use EF Core migrations for schema management and updates
- **ADR-0002-IMP-003**: Monitor database health and performance; establish backup and recovery procedures

## References

- **ADR-0002-REF-001**: ADR-0001: .NET 8 Adoption
- **ADR-0002-REF-002**: Entity Framework Core documentation
- **ADR-0002-REF-003**: MySQL documentation
