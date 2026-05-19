# DCD for hele systemet, inkludert login/auth klasser

```mermaid
classDiagram
  direction TB

  %% Clean architecture

  namespace Domain.Enums {
    class AnonymizationStatus {
      <<enumeration>>
      Suggested
      Approved
      Rejected
      Anonymized
    }
    class DbConnectionState {
      <<enumeration>>
      Online,
      Offline,
      Reconnecting,
      Unknown
    }
    class Department {
      <<enumeration>>
      Slottet,
      Skoven,
      Marken
    }
    class IncidentSeverity {
      <<enumeration>>
      Low,
      Medium,
      High,
      Critical
    }
    class IncidentStatus {
      <<enumeration>>
      Open,
      UnderInvestigation,
      Closed,
      Escalated,
      BreachNotified
    }
    class RetentionDataCategory {
      <<enumeration>>
      MedicineLogs,
      ResidentNotes,
      AuditLogs,
      LoginLogs,
      InactiveUsers,
      AnonymizationTrigger
    }
    class ShiftType {
      <<enumeration>>
      Day,
      Evening,
      Night
    }
    class TrafficLightStatus {
      <<enumeration>>
      Green
      Yellow
      Red
    }
  }

  namespace Domain.Interfaces {
    class IEntity {
      <<interface>>
      +Id: guid
    }
  }

  namespace Domain.Entities {
    class AnonymizationCandidate {
      +Id: guid
      +ResidentId: guid
      +RetentionPolicyId: guid
      +SuggestedAt: DateTime
      +Reason: string
      +Status: AnonymizationStatus
    }

    class AuditEntry {
      +Id: guid
      +Metadata: string
      +StartTimeUtc: DateTime
      +EndTimeUtc: DateTime
      +Succeeded: bool
      +UserId: guid
      +ErrorMessage: string
    }
  }

  %% Association (Standard)

  %% Directed Association
  AnonymizationCandidate --> AnonymizationStatus : Status

  %% Aggregation

  %% Composition

  %% Generalization (Inheritance)

  %% Dependency

  %% Realization (Interface Implementation)
  AnonymizationCandidate --|> IEntity : implements
  AuditEntry --|> IEntity : implements

  %% Reflexive Association


```
