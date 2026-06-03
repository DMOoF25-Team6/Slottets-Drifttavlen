
```mermaid
erDiagram
    AspNetUsers {
        Guid Id PK
        string UserName
        string NormalizedUserName
        string Email
        string NormalizedEmail
        bool EmailConfirmed
        string PasswordHash
        string SecurityStamp
        string ConcurrencyStamp
        string PhoneNumber
        bool PhoneNumberConfirmed
        bool TwoFactorEnabled
        DateTime LockoutEnd
        bool LockoutEnabled
        int AccessFailedCount
    }
    AspNetRoles {
        Guid Id PK
        string Name
        string NormalizedName
        string ConcurrencyStamp
    }
    AspNetUserRoles {
        Guid UserId FK
        Guid RoleId FK
    }
    AspNetRoleClaims {
        int Id PK
        Guid RoleId FK
        string ClaimType
        string ClaimValue
    }
    AspNetUserClaims {
        int Id PK
        Guid UserId FK
        string ClaimType
        string ClaimValue
    }
    Employee {
        Guid Id PK
        string FirstName
        string LastName
        string Initials
        Guid UserId FK
        int Department
    }
    Resident {
        Guid Id PK
        string FirstName
        string LastName
        string Initials
        int TrafficLightStatus
        int Department
        string Info
        string Activity
        string Amount
        string Companion
        DateTime DischargedAt
    }
    ResidentNote {
        Guid Id PK
        Guid ResidentId FK
        string Note
        string Initials
        DateTime CreatedAt
    }
    MedicineRecord {
        Guid Id PK
        Guid ResidentId FK
        string MedicineName
        bool Given
        DateTime Timestamp
    }
    PainkillerRecord {
        Guid Id PK
        Guid ResidentId FK
        string Type
        DateTime GivenAt
        DateTime NextAllowedTime
    }
    PhoneAssignment {
        Guid Id PK
        Guid CaregiverId FK
        string PhoneNumber
        string ShiftType
    }
    StaffAssignment {
        Guid Id PK
        Guid EmployeeId FK
        Guid ResidentId FK
        int ShiftType
        DateTime AssignmentDate
        DateTime CreatedAt
        DateTime UpdatedAt
    }
    TaskList {
        Guid Id PK
        string Title
        string Description
        DateTime CreatedAt
        DateTime CompletedAt
    }
    RetentionPolicy {
        Guid Id PK
        int Category
        TimeSpan RetentionPeriod
        TimeSpan LegalMinimum
        DateTime EffectiveFrom
    }
    RetentionPolicyAudit {
        Guid Id PK
        Guid RetentionPolicyId FK
        Guid ChangedByEmployeeId
        DateTime ChangedAt
        TimeSpan PreviousPeriod
        TimeSpan NewPeriod
        string Reason
    }
    AnonymizationCandidate {
        Guid Id PK
        Guid ResidentId FK
        Guid RetentionPolicyId FK
        string Reason
        int Status
        DateTime SuggestedAt
    }
    SecurityIncident {
        Guid Id PK
        DateTime DetectedAt
        string Type
        int Severity
        int Status
        string InvestigationNotes
        Guid ReportedByEmployeeId FK
        Guid ResolvedByEmployeeId FK
    }
    SubjectAccessRequest {
        Guid Id PK
        Guid ResidentId FK
        Guid FulfilledByEmployeeId FK
        string ExportFileName
        DateTime ExportGeneratedAt
        DateTime FulfilledAt
        DateTime RequestedAt
        Guid RequestedByEmployeeId
    }
    LoginAttempt {
        Guid Id PK
        DateTime AttemptedAt
        string EmailHash
        Guid UserId FK
        bool Succeeded
        string IpAddress
        string FailureReason
    }
    RefreshToken {
        Guid Id PK
        Guid UserId FK
        string TokenHash
        DateTime CreatedAt
        string CreatedByIp
        DateTime ExpiresAt
        DateTime RevokedAt
        string RevokedReason
    }
    AuditEntry {
        Guid Id PK
        Guid UserId FK
        string Metadata
        DateTime StartTimeUtc
        DateTime EndTimeUtc
        bool Succeeded
        string ErrorMessage
    }
    ChangeDetail {
        Guid Id PK
        Guid AuditEntryId FK
        string Field
        string OldValue
        string NewValue
    }

    %% Relationships for Identity
    AspNetUserRoles }o--|| AspNetUsers : "UserId"
    AspNetUserRoles }o--|| AspNetRoles : "RoleId"
    AspNetRoleClaims }o--|| AspNetRoles : "RoleId"
    AspNetUserClaims }o--|| AspNetUsers : "UserId"

    %% Relationships
    Employee }o--|| AspNetUsers : "UserId"
    Employee ||--o{ StaffAssignment : "EmployeeId"
    Resident ||--o{ ResidentNote : "ResidentId"
    Resident ||--o{ MedicineRecord : "ResidentId"
    Resident ||--o{ PainkillerRecord : "ResidentId"
    Resident ||--o{ StaffAssignment : "ResidentId"
    Resident ||--o{ AnonymizationCandidate : "ResidentId"
    Resident ||--o{ SubjectAccessRequest : "ResidentId"
    PhoneAssignment }o--|| Employee : "CaregiverId"
    RetentionPolicy ||--o{ RetentionPolicyAudit : "RetentionPolicyId"
    RetentionPolicy ||--o{ AnonymizationCandidate : "RetentionPolicyId"
    SecurityIncident }o--|| Employee : "ReportedByEmployeeId"
    SecurityIncident }o--|| Employee : "ResolvedByEmployeeId"
    SubjectAccessRequest }o--|| Employee : "FulfilledByEmployeeId"
    LoginAttempt }o--|| AspNetUsers : "UserId"
    RefreshToken }o--|| AspNetUsers : "UserId"
    AuditEntry }o--|| AspNetUsers : "UserId"
    ChangeDetail }o--|| AuditEntry : "AuditEntryId"
