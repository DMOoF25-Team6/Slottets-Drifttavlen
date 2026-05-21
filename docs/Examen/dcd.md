# DCD for hele systemet, inkludert login/auth klasser

```mermaid
classDiagram
  direction TB

  %% Clean architecture

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Domain Layer
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

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

    class ChangeDetail {
      +Id: guid
      +Field: string
      +OldValue: string
      +NewValue: string
      +AuditEntryId: guid
    }
    class Employee {
      +Id: guid
      +FirstName: string
      +LastName: string
      +Initials: string
      +UserId: guid
      +Department: Department
      *StaffAssignments: ICollection<StaffAssignment>
      *User: User
    }
    class LoginAttempt {
      +Id: guid
      +AttemptedAt: DateTime
      +EmailHash: string
      +UserId: guid
      +Timestamp: DateTime
      +Succeeded: bool
      +IpAddress: string
    }
    class MedicineRecord {
      +Id: guid
      +ResidentId: guid
      +MedicineName: string
      +Timestamp: DateTime
      +Given: bool
    }
    class PainkillerRecord {
      +Id: guid
      +ResidentId: guid
      +Type: string
      +GivenAt: DateTime
      +NextAllowedTime: DateTime
    }
    class PhoneAssignment {
      +Id: guid
      +CaregiverId: guid
      +PhoneNumber: string
      +ShiftType: string
    }
    class RefreshToken {
      +Id: guid
      +UserId: guid
      +TokenHash: string
      +ExpiresAt: DateTime
      +CreatedAt: DateTime
      +RevokedAt: DateTime Nullable
      +CreatedByIp: string Nullable
      +RevokedByIp: string Nullable
      +RevokedReason: string Nullable
      *User: User
    }
    class Resident {
      +Id: guid
      +Initials: string
      +FirstName: string
      +LastName: string
      +TrafficLightStatus: TrafficLightStatus
      +Department: Department
      +DischargedAt: DateTime Nullable
      *Notes: ICollection<ResidentNote>
      *Medicines: ICollection<ResidentMedicine>
      *Painkillers: ICollection<ResidentPainkiller>
      *StaffAssignments: ICollection<StaffAssignment>
    }
    class ResidentNote {
      +Id: guid
      +Note: string
      +CreatedAt: DateTime
      +EditedAt: DateTime Nullable
      +ResidentId: guid
    }
    class RetentionPolicy {
      +Id: guid
      +Category: RetentionDataCategory
      +RetentionPeriod: TimeSpan
      +LegalMinimum: TimeSpan
      +EffectiveFrom: TimeDate
      *AuditHistory: ICollection<RetentionPolicyAudit>
      *Candidates: ICollection<AnonymizationCandidate>
    }
    class RetentionPolicyAudit {
      +Id: guid
      +RetentionPolicyId: guid
      +ChangedByEmployeeId: guid
    }
    class SecurityIncident {
      +Id: guid
      +DetectedAt: DateTime
      +Type: string
      +Severity: IncidentSeverity
      +Status: IncidentStatus
      +InvestigationNotes: string
      +ReportedByEmployeeId: guid Nullable
      +ResolvedByEmployeeId: guid Nullable
    }
    class StaffAssignment {
      +Id: guid
      +ResidentId: guid
      +EmployeeId: guid
      +ShiftType: ShiftType
      +AssignmentDate: DateTime
      +CreatedAt: DateTime
      +UpdatedAt: DateTime Nullable
      *Resident: Resident
      *Employee: Employee
    }
    class SubjectAccessRequest {
      +Id: guid
      +ResidentId: guid
      +RequestedByEmployeeId: guid
      +RequestedAt: DateTime
      +ScopeOptions: string
      +ExportFileName: string
      +ExportGeneratedAt: DateTime
      +FulfilledAt: DateTime Nullable
      +FulfilledByEmployeeId: guid Nullable
    }
    class User {
      +Id: guid
    }
  }

  namespace Domain.UiModels {
    class DataConnection {
      +State: DbConnectionState
      +LastChecked: DateTime Nullable
      +ErrorMessage: string
    }
  }

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application Layer
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

  namespace Core.DTOs {
    class AddResidentNoteDto {
      +ResidentId: guid
      +NoteText: string
    }
    class AssignmentOverviewDto {
      +AssignmentId: guid
      +ResidentId: guid
      +ResidentInitials: string
      +EmployeeId: guid
      +EmployeeName: string
      +ShiftType: string
      +AssignmentDate: DateTime
    }
    class EmployeeDto {
      +Id: guid
      +FirstName: string
      +LastName: string
      +Initials: string
      +Department: Department
    }
    class EditResidentNoteDto {
      +ResidentId: guid
      +ResidentNoteId: guid
      +NewNoteText: string
    }
    class ErrorDto {
      +ErrorMessages: IEnumerable<string> Nullable
    }
    class ResidentNoteDto {
      +Id: guid
      +Note: string
      +Timestamp: DateTime
      +Initials: string
    }
    class ResidentCreateRequestDto {
      +Initials: string
      +FirstName: string
      +LastName: string
      +TrafficLightStatus: TrafficLightStatus Nullable
      +Department: Department
    }
    class ResidentUpdateRequestDto {
      +Initials: string
      +FirstName: string
      +LastName: string
      +TrafficLightStatus: TrafficLightStatus Nullable
      +Department: Department
    }
    class ResidentResponseDto {
      +Id: guid
      +Initials: string
      +FirstName: string
      +LastName: string
      +TrafficLightStatus: int Nullable
      +Notes: List<ResidentNoteDto>
      +Department: Department
    }
    class StaffAssignmentDto {
      +ResidentId: guid
      +EmployeeId: guid
      +ShiftType: ShiftType
      +AssignmentDate: DateTime
    }
    class PhoneAssignmentDto {
      +PhoneNumber: string
      +ShiftType: string
      +ShiftTypeEnum: ShiftType
      +AssignedStaffName: string
    }
    class MedicineEntryDto {
      +Name: string
      +Timestamp: DateTime
      +Given: bool
    }
    class MedicineStatusDto {
      +ResidentId: guid
      +Entries: List<MedicineEntryDto>
    }
    class PainkillerStatusDto {
      +ResidentId: guid
      +Types: IEnumerable<string>
      +NextAllowedTime: DateTime
    }
    class TaskListDto {
      +Id: guid
      +Title: string
      +Description: string
      +TaskListStatus: TaskListStatus
      +DueTime: DateTime
      +Department: Department
    }
  }

  namespace Core.DTOs.Anonymization {
    class AnonymizationCandidateDto {
      +Id: guid
      +ResidentId: guid
      +RetentionPolicyId: guid
      +SuggestedAt: DateTime
      +Reason: string
      +Status: AnonymizationStatus
    }
    class AnonymizationResultDto {
      +CandidateId: guid
      +CompletedAt: DateTime
      +Outcome: string
    }
    class ApproveAnonymizationDto {
      +CandidateId: guid
    }
  }

  namespace Core.DTOs.Audit {
    class AuditEntryDto {
      +Id: guid
      +EventTimeUtc: DateTime
      +RegisteredTimeUtc: DateTime
      +Entity: string
      +ChangeType: string
      +Description: string
      +UserId: guid
      +UserName: string
      +Succeeded: bool
      +ChangeDetails: IEnumerable<ChangeDetailDto>
    }
    class ChangeDetailDto {
      +Id: guid
      +Field: string
      +OldValue: string
      +NewValue: string
    }
  }

  namespace Core.DTOs.Identity {
    class DeleteUserRequestDto {
      +UserId: string
    }
    class DeleteUserResponseDto {
      +IsSuccessful: bool
    }
    class GetUserIdByEmailRequestDto {
      +Email: string
    }
    class LoginRequestDto {
      +Email: string
      +Password: string
    }
    class LoginResponseDto {
      +Token: string
      +RefreshToken: string Nullable
    }
    class LogoutRequestDto {
      +RefreshToken: string
    }
    class LogoutResponseDto {
      +IsSuccessful: bool
    }
    class RefreshTokenRequestDto {
      +RefreshToken: string Nullable
    }
    class RefreshTokenResponseDto {
      +JwtToken: string Nullable
      +RefreshToken: string Nullable
      +ErrorMessages: IEnumerable<string> Nullable
    }
    class RegisterRequestDto {
      +Email: string
      +Password: string
    }
    class RegistrationResponseDto {
      +IsSuccessful: bool
      +ErrorMessages: IEnumerable<string>
    }
  }

  namespace Core.DTOs.Retention {
    class RetentionPolicyDto {
      +Id: guid
      +Category: RetentionDataCategory
      +RetentionPeriod: TimeSpan
      +LegalMinimum: TimeSpan
      +EffectiveFrom: DateTime
    }
    class RetentionPolicyAuditDto {
      +Id: guid
      +RetentionPolicyId: guid
      +ChangedByEmployeeId: guid
      +PreviousPeriod: TimeSpan
      +NewPeriod: TimeSpan
      +ChangedAt: DateTime
      +Reason: string
    }
    class UpdateRetentionPolicyDto {
      +Category: RetentionDataCategory
      +RetentionPeriod: TimeSpan
      +Reason: string
    }
  }

  namespace Core.DTOs.Sar {
    class SarExportRequestDto {
      +ResidentId: guid
      +ScopeOptions: string[]
    }
    class SarExportPackageDto {
      +ExportId: guid
      +GeneratedAt: DateTime
      +FileName: string
      +Payload: string
    }
    class SarFulfilledDto {
      +SarId: guid
      +FulfilledAt: DateTime
    }
  }

  namespace Core.DTOs.Tasks {
    class TaskListDto {
      +Id: guid
      +Title: string
      +Description: string
      +TaskListStatus: TaskListStatus
      +DueTime: DateTime
      +Department: Department
    }
  }

namespace Core.DTOs.Security {
    class AddInvestigationNotesDto {
      +IncidentId: guid
      +Notes: string
    }
    class EscalateIncidentDto {
      +IncidentId: guid
      +IsBreach: bool
    }
    class SecurityIncidentDto {
      +Id: guid
      +DetectedAt: DateTime
      +Type: string
      +Severity: IncidentSeverity
      +Status: IncidentStatus
      +InvestigationNotes: string
      +BreachNotificationDeadlineUtc: DateTime
    }
  }

  namespace Core.Helpers {
    class ShiftTypeHelper {
      <<static>>
      +ToDanishString(shiftType: ShiftType): string
    }
  }

  namespace Core.Interfaces {
    class ICRUD~TEntity~ {
      <<interface>>
      +CreateAsync(entity: TEntity, cancellationToken: CancellationToken): Task<TEntity>
      +CreateRangeAsync(entities: IEnumerable<TEntity>, cancellationToken: CancellationToken): Task<IEnumerable<TEntity>>
      +GetByIdAsync(id: guid, cancellationToken: CancellationToken): Task<TEntity?>
      +GetAllAsync(cancellationToken: CancellationToken): Task<IEnumerable<TEntity>>
      +UpdateAsync(entity: TEntity, cancellationToken: CancellationToken): Task
      +UpdateRangeAsync(entities: IEnumerable<TEntity>, cancellationToken: CancellationToken): Task
      +DeleteAsync(entity: TEntity, cancellationToken: CancellationToken): Task
      +DeleteRangeAsync(entities: IEnumerable<TEntity>, cancellationToken: CancellationToken): Task
    }
  }

  namespace Core.Interfaces.Dto.Identity {
    class IDeleteResult {
      <<interface>>
    }
    class ILoginResult {
      <<interface>>
    }
    class ILogoutResult {
      <<interface>>
    }
  }

  namespace Core.Interfaces.Services {
    class IAccountService {
      <<interface>>
      +CreateAccountAsync(registrationRequestDto: RegisterRequestDto): Task<RegistrationResponseDto>
      +LoginAsync(loginRequestDto: LoginRequestDto): Task<ILoginResult>
      +RefreshTokenAsync(refreshTokenRequestDto: RefreshTokenRequestDto): Task<RefreshTokenResponseDto>
      +LogoutAsync(logoutRequestDto: LogoutRequestDto): Task<ILogoutResult>
    }
    class IAnonymizationService {
      <<interface>>
      +GetCandidatesAsync(cancellationToken: CancellationToken): Task<IEnumerable<AnonymizationCandidateDto>>
      +ApproveAnonymizationAsync(candidateId: guid, cancellationToken: CancellationToken): Task<AnonymizationResultDto>
      +RejectAnonymizationAsync(candidateId: guid, reason: string, cancellationToken: CancellationToken): Task<bool>
    }
    class IArt33NotificationService {
      <<interface>>
      +SendNotificationAsync(incidentId: guid, dpoEmail: string, cancellationToken: CancellationToken): Task<bool>
    }
    class IAuditService {
      <<interface>>
      +LogAsync(entityName: string, changeType: string, changedBy: string Nullable, description: string): Task
      +GetRecentAsync(limit: int Nullable, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetByEntityNameAsync(entityName: string, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetWithDetailsAsync(id: guid, cancellationToken: CancellationToken): Task<AuditEntryDto Nullable>
    }
    class IDatabaseConnectionService {
      <<interface>>
      +CheckDatabaseConnectionAsync(): Task
    }
    class IDatabaseService {
      <<interface>>
      +IsConnected(): bool
    }
    class IMedicineStatusService {
      <<interface>>
      +GetMedicineStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<MedicineStatusDto Nullable>
      +GetPainkillerStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<PainkillerStatusDto Nullable>
    }
    class IPhoneAssignmentService {
      <<interface>>
      +GetCurrentPhoneAssignmentsForActiveShiftAsync(cancellationToken: CancellationToken): Task<IEnumerable<PhoneAssignmentDto>>
    }
    class IPseudonymizationService {
      <<interface>>
      +Pseudonymize(identifier: string): string
      +PseudonymizeShort(identifier: string): string
    }
    class IRefreshTokenStore {
      <<interface>>
      +GetByTokenAsync(token: string, tokenService: ITokenService, cancellationToken: CancellationToken): Task<RefreshToken Nullable>
      +RevokeAsync(token: string, tokenService: ITokenService, cancellationToken: CancellationToken): Task
      +SaveAsync(token: RefreshToken, tokenService: ITokenService, cancellationToken: CancellationToken): Task
    }
    class IResidentNoteService {
      <<interface>>
      +AddAsync(residentId: guid, noteText: string, cancellationToken: CancellationToken): Task<bool>
      +DeleteAsync(noteId: guid, cancellationToken: CancellationToken): Task<bool>
      +UpdateAsync(noteId: guid, newText: string, cancellationToken: CancellationToken): Task<bool>
      +GetAllByResidentIdAsync(residentId: guid, cancellationToken: CancellationToken): Task<IEnumerable<ResidentNoteDto>>
    }
    class IResidentService {
      <<interface>>
      +CreateAsync(dto: ResidentCreateRequestDto, ct: CancellationToken): Task
      +DeleteAsync(id: guid, cancellationToken: CancellationToken): Task
      +GetAllAsync(ct: CancellationToken): Task<IEnumerable<Resident>>
      +GetByDepartmentsAsync(departments: IList<Department>, ct: CancellationToken): Task<IEnumerable<Resident>>
      +GetByIdAsync(id: guid, ct: CancellationToken): Task<Resident Nullable>
      +UpdateAsync(id: guid, resident: ResidentUpdateRequestDto, ct: CancellationToken): Task
    }
    class IRetentionPolicyService {
      <<interface>>
      +GetPoliciesAsync(cancellationToken: CancellationToken): Task<IEnumerable<RetentionPolicyDto>>
      +UpdateRetentionPolicyAsync(dto: UpdateRetentionPolicyDto, changedByEmployeeId: guid, cancellationToken: CancellationToken): Task<RetentionPolicyDto>
    }
    class ISecurityIncidentService {
      <<interface>>
      +GetIncidentsAsync(cancellationToken: CancellationToken): Task<IEnumerable<SecurityIncidentDto>>
      +EscalateIncidentAsync(incidentId: guid, isBreach: bool, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +AddInvestigationNotesAsync(dto: AddInvestigationNotesDto, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +CloseIncidentAsync(incidentId: guid, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
    }
    class IStaffAssignmentService {
      <<interface>>
      +AssignAsync(dto: StaffAssignmentDto, cancellationToken: CancellationToken): Task<AssignmentOverviewDto>
      +GetAssignmentsByShiftAsync(shiftType: ShiftType, assignmentDate: DateTime, cancellationToken: CancellationToken): Task<IEnumerable<AssignmentOverviewDto>>
      +DeleteAssignmentAsync(assigmentId: guid, cancellationToken: CancellationToken): Task
      +UpdateAssignmentAsync(assignmentId: guid, dto: StaffAssignmentDto, cancellationToken: CancellationToken): Task<AssignmentOverviewDto>
    }
    class ISubjectAccessRequestService {
      <<interface>>
      +GenerateExportAsync(dto: SarExportRequestDto, cancellationToken: CancellationToken): Task<SarExportPackageDto>
      +MarkFulfilledAsync(dto: SarFulfilledDto, cancellationToken: CancellationToken): Task<bool>
    }
    class ITaskListService {
      <<interface>>
      +GetAvailableTasksByDepartmentAsync(department: Department, cancellationToken: CancellationToken): Task<IEnumerable<TaskListDto>>
    }
    class ITokenService {
      <<interface>>
      +CreateJwtTokenAsync(user: User, roles: IList<string>, permissions: IList<Claim>, cancellationToken: CancellationToken): Task<string>
      +ComputeSha256HashAsync(token: string, cancellationToken: CancellationToken): Task<string>
      +CreateRefreshTokenAsync(user: User, ipAddress: string, cancellationToken: CancellationToken): Task<RefreshToken>
    }
  }

  namespace Core.Interfaces.Managers {
    class IAccountManager {
      <<interface>>
      +CreateAccountAsync(registrationRequestDto: RegisterRequestDto): Task<RegistrationResponseDto>
      +LoginAsync(loginRequestDto: LoginRequestDto): Task<ILoginResult>
      +LogoutAsync(logoutRequestDto: LogoutRequestDto): Task<ILogoutResult>
      +RefreshTokenAsync(refreshTokenRequestDto: RefreshTokenRequestDto): Task<RefreshTokenResponseDto>
    }
    class IAnonymizationManager {
      <<interface>>
      +GetCandidatesAsync(cancellationToken: CancellationToken): Task<IEnumerable<AnonymizationCandidateDto>>
      +ApproveAnonymizationAsync(candidateId: guid, cancellationToken: CancellationToken): Task<AnonymizationResultDto>
      +RejectAnonymizationAsync(candidateId: guid, reason: string, cancellationToken: CancellationToken): Task<bool>
    }
    class IAuditManager {
      <<interface>>
      +GetRecentAsync(limit: int Nullable, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetByEntityNameAsync(entityName: string, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetWithDetailsAsync(id: guid, cancellationToken: CancellationToken): Task<AuditEntryDto Nullable>
    }
    class IDatabaseConnectionManager {
      <<interface>>
      +CheckAndUpdateConnectionStateAsync(): Task
    }
    class IEmployeeManager {
      <<interface>>
      +GetAllAsync(cancellationToken: CancellationToken): Task<IEnumerable<EmployeeDto>>
    }
    class IMedicineRecordManager {
      <<interface>>
    }
    class IMedicineStatusManager {
      <<interface>>
      +GetMedicineStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<MedicineStatusDto Nullable>
      +GetPainkillerStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<PainkillerStatusDto Nullable>
    }
    class IPhoneAssignmentManager {
      <<interface>>
      +GetCurrentPhoneAssignmentsForActiveShift(cancellationToken: CancellationToken): Task<IEnumerable<PhoneAssignmentDto>>
    }
    class IResidentManager {
      <<interface>>
      +CreateAsync(dto: ResidentCreateRequestDto, ct: CancellationToken): Task
      +CreateRangeAsync(dtos: IEnumerable<ResidentCreateRequestDto>, ct: CancellationToken): Task
      +DeleteAsync(id: guid, ct: CancellationToken): Task
      +DeleteRangeAsync(ids: IEnumerable<guid>, ct: CancellationToken): Task
      +GetAllAsync(ct: CancellationToken): Task<IEnumerable<ResidentResponseDto>>
      +GetByIdAsync(id: guid, ct: CancellationToken): Task<ResidentResponseDto Nullable>
      +GetByDepartmentsAsync(departments: IList<Department>, ct: CancellationToken): Task<IEnumerable<ResidentResponseDto>>
      +UpdateAsync(id: guid, dto: ResidentUpdateRequestDto, ct: CancellationToken): Task
      +UpdateRangeAsync(dtos: IEnumerable<ResidentUpdateRequestDto>, ct: CancellationToken): Task
    }
    class IResidentNoteManager {
      <<interface>>
      +GetAllByResidentIdAsync(residentId: guid, cancellationToken: CancellationToken): Task<IEnumerable<ResidentNoteDto>>
      +AddAsync(residentId: guid, noteText: string, cancellationToken: CancellationToken): Task<bool>
      +UpdateAsync(noteId: guid, newText: string, cancellationToken: CancellationToken): Task<bool>
      +DeleteAsync(noteId: guid, cancellationToken: CancellationToken): Task<bool>
    }
    class IRetentionPolicyManager {
      <<interface>>
      +GetPoliciesAsync(cancellationToken: CancellationToken): Task<IEnumerable<RetentionPolicyDto>>
      +UpdateRetentionPolicyAsync(dto: UpdateRetentionPolicyDto, changedByEmployeeId: guid, cancellationToken: CancellationToken): Task<RetentionPolicyDto>
    }
    class ISecurityIncidentManager {
      <<interface>>
      +GetIncidentsAsync(cancellationToken: CancellationToken): Task<IEnumerable<SecurityIncidentDto>>
      +EscalateIncidentAsync(incidentId: guid, isBreach: bool, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +AddInvestigationNotesAsync(dto: AddInvestigationNotesDto, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +CloseIncidentAsync(incidentId: guid, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
    }
    class ISubjectAccessRequestManager {
      <<interface>>
      +GenerateExportAsync(dto: SarExportRequestDto, cancellationToken: CancellationToken): Task<SarExportPackageDto>
      +MarkFulfilledAsync(dto: SarFulfilledDto, cancellationToken: CancellationToken): Task<bool>
    }
    class ITaskListManager {
      <<interface>>
      +GetDashboardTasksByDepartmentAsync(department: Department, cancellationToken: CancellationToken): Task<IEnumerable<TaskListDto>>
    }
  }

  namespace Core.Interfaces.Providers {
    class IDatabaseConnectionStateProvider {
      <<interface>>
      +IsConnected: bool
      +SetConnectionState(isConnected: bool): void
      +StateChanged: Action
    }
  }

  namespace Core.Interfaces.Repositories {
    class IRepository~TEntity~ {
      <<interface>>
    }
    class IAuditRepository {
      <<interface>>
      +GetRecentAsync(limit: int Nullable, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntry>>
      +GetByEntityNameAsync(entityName: string, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntry>>
      +GetWithDetailsAsync(id: guid, cancellationToken: CancellationToken): Task<AuditEntry Nullable>
    }
    class IEmployeeRepository {
      <<interface>>
    }
    class ILoginAttemptRepository {
      <<interface>>
    }
    class IMedicineRepository {
      <<interface>>
      +GetMedicineStatusLast24HoursAsync(residentId: guid, cancellationToken: CancellationToken): Task<IEnumerable<MedicineRecord>>
    }
    class IPainkillerRepository {
      <<interface>>
      +GetPainkillerStatusLast24HoursAsync(residentId: guid, cancellationToken: CancellationToken): Task<IEnumerable<PainkillerRecord>>
    }
    class IPhoneAssignmentRepository {
      <<interface>>
      +GetByShiftTypeAsync(shiftType: ShiftType, cancellationToken: CancellationToken): Task<IEnumerable<PhoneAssignment>>
      +GetDtoByShiftTypeAsync(shiftType: ShiftType, cancellationToken: CancellationToken): Task<IEnumerable<PhoneAssignmentDto>>
    }
    class IResidentNoteRepository {
      <<interface>>
    }
    class IResidentRepository {
      <<interface>>
      +GetAllAsync(department: Department, cancellationToken: CancellationToken): Task<IEnumerable<Resident>>
    }
    class IRetentionPolicyAuditRepository {
      <<interface>>
    }
    class IRetentionPolicyRepository {
      <<interface>>
    }
    class ISecurityIncidentRepository {
      <<interface>>
    }
    class IStaffAssignmentRepository {
      <<interface>>
      +GetByResidentAsync(residentId: guid, cancellationToken: CancellationToken): Task<IEnumerable<StaffAssignment>>
      +GetByShiftAsync(shiftType: ShiftType, assignmentDate: DateTime, cancellationToken: CancellationToken): Task<IEnumerable<StaffAssignment>>
      +GetExistingAssignmentAsync(residentId: guid, employeeId: guid, shiftType: ShiftType, assignmentDate: DateTime, cancellationToken: CancellationToken): Task<StaffAssignment Nullable>
      +GetByIdWithDetailsAsync(assignmentId: guid, cancellationToken: CancellationToken): Task<StaffAssignment Nullable>
    }
    class ISubjectAccessRequestRepository {
      <<interface>>
    }
    class ITaskListRepository {
      <<interface>>
      +GetDashboardTasksByDepartmentAsync(department: Department, cancellationToken: CancellationToken): Task<IEnumerable<TaskList>>
    }
    class IUserRepository {
      <<interface>>
    }
    class IAnonymizationCandidateRepository {
      <<interface>>
    }
  }

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application Mappers
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

  namespace Core.Mappers {
    class AnonymizationCandidateMapper {
      <<static>>
      +ToDto(entity: AnonymizationCandidate): AnonymizationCandidateDto
    }

    class AuditMapper {
      <<static>>
      +ToDto(entity: AuditEntry, userName: string): AuditEntryDto
      +ToDtos(entities: IEnumerable<AuditEntry>): IEnumerable<AuditEntryDto>
    }

    class ChangeDetailMapper {
      <<static>>
      +ToDto(entity: ChangeDetail): ChangeDetailDto
      +ToDtos(entities: IEnumerable<ChangeDetail>): IEnumerable<ChangeDetailDto>
    }

    class MedicineMapper {
      <<static>>
      +ToMedicineStatusDto(residentId: guid, records: IEnumerable<MedicineRecord>): MedicineStatusDto
    }

    class PainKillerMapper {
      <<static>>
      +ToPainkillerStatusDto(residentId: guid, records: IEnumerable<PainkillerRecord>): PainkillerStatusDto
    }

    class PhoneAssignmentMapper {
      <<static>>
      +ToDto(entity: PhoneAssignment): PhoneAssignmentDto
      +ToDtos(entities: IEnumerable<PhoneAssignment>): IEnumerable<PhoneAssignmentDto>
    }

    class ResidentMapper {
      +ToResident(dto: ResidentResponseDto): Resident
      +ToResidentResponseDto(entity: Resident): ResidentResponseDto
      +ToResidentNoteDto(note: ResidentNote): ResidentNoteDto
      +ToResident(dto: ResidentCreateRequestDto): Resident
      +ToResidentNote(dto: ResidentNoteDto): ResidentNote
    }

    class ResidentNoteMapper {
      <<static>>
      +ToDto(entity: ResidentNote): ResidentNoteDto
      +ToDtos(entities: IEnumerable<ResidentNote>): IEnumerable<ResidentNoteDto>
      +ToNewEntity(residentId: guid, noteText: string): ResidentNote
    }

    class RetentionPolicyMapper {
      <<static>>
      +ToDto(entity: RetentionPolicy): RetentionPolicyDto
      +ToAuditDto(entity: RetentionPolicyAudit): RetentionPolicyAuditDto
    }

    class SarExportMapper {
      <<static>>
      +ToPackageDto(exportId: guid, generatedAt: DateTime, fileName: string): SarExportPackageDto
      +ToPackageDto(exportId: guid, generatedAt: DateTime, fileName: string, payload: string): SarExportPackageDto
    }

    class SecurityIncidentMapper {
      <<static>>
      +ToDto(entity: SecurityIncident): SecurityIncidentDto
    }

    class TaskListMapper {
      <<static>>
      +ToTaskListDto(taskList: TaskList): TaskListDto
    }
  }

  namespace Core.Mappers.Accounts {
    class RegistrationMapper {
      <<static>>
      +ToUserEntity(request: RegisterRequestDto): User
    }
  }

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application Providers
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

  namespace Core.Providers {
    class DatabaseConnectionStateProvider {
      <<class>>
      +StateChanged: Action?
      +IsConnected: bool
      +SetConnectionState(isConnected: bool): void
    }
  }

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application Services
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

  namespace Core.Services {
    class AccountService {
      +CreateAccountAsync(registrationRequestDto: RegisterRequestDto): Task<RegistrationResponseDto>
      +LoginAsync(loginRequestDto: LoginRequestDto): Task<ILoginResult>
      +RefreshTokenAsync(refreshTokenRequestDto: RefreshTokenRequestDto): Task<RefreshTokenResponseDto>
      +LogoutAsync(logoutRequestDto: LogoutRequestDto): Task<ILogoutResult>
    }

    class AnonymizationService {
      +GetCandidatesAsync(cancellationToken: CancellationToken): Task<IEnumerable<AnonymizationCandidateDto>>
      +ApproveAnonymizationAsync(candidateId: guid, cancellationToken: CancellationToken): Task<AnonymizationResultDto>
      +RejectAnonymizationAsync(candidateId: guid, reason: string, cancellationToken: CancellationToken): Task<bool>
    }

    class Art33NotificationService {
      +SendNotificationAsync(incidentId: guid, dpoEmail: string, cancellationToken: CancellationToken): Task<bool>
    }

    class AuditService {
      +LogAsync(entityName: string, changeType: string, changedBy: string Nullable, description: string): Task
      +GetRecentAsync(limit: int Nullable, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetByEntityNameAsync(entityName: string, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetWithDetailsAsync(id: guid, cancellationToken: CancellationToken): Task<AuditEntryDto Nullable>
    }

    class DatabaseConnectionService {
      +CheckDatabaseConnectionAsync(): Task
    }

    class MedicineStatusService {
      +GetMedicineStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<MedicineStatusDto Nullable>
      +GetPainkillerStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<PainkillerStatusDto Nullable>
    }

    class PhoneAssignmentService {
      +GetCurrentPhoneAssignmentsForActiveShiftAsync(cancellationToken: CancellationToken): Task<IEnumerable<PhoneAssignmentDto>>
    }

    class PseudonymizationService {
      +Pseudonymize(identifier: string): string
      +PseudonymizeShort(identifier: string): string
    }

    class ResidentNoteService {
      +GetAllByResidentIdAsync(residentId: guid, cancellationToken: CancellationToken): Task<IEnumerable<ResidentNoteDto>>
      +AddAsync(residentId: guid, noteText: string, cancellationToken: CancellationToken): Task<bool>
      +UpdateAsync(noteId: guid, newText: string, cancellationToken: CancellationToken): Task<bool>
      +DeleteAsync(noteId: guid, cancellationToken: CancellationToken): Task<bool>
    }

    class ResidentService {
      +GetByIdAsync(id: guid, ct: CancellationToken): Task<Resident Nullable>
      +GetAllAsync(ct: CancellationToken): Task<IEnumerable<Resident>>
      +GetByDepartmentsAsync(departments: IList<Department>, ct: CancellationToken): Task<IEnumerable<Resident>>
      +CreateAsync(dto: ResidentCreateRequestDto, ct: CancellationToken): Task
      +UpdateAsync(id: guid, resident: ResidentUpdateRequestDto, ct: CancellationToken): Task
      +DeleteAsync(id: guid, ct: CancellationToken): Task
    }

    class RetentionPolicyService {
      +GetPoliciesAsync(cancellationToken: CancellationToken): Task<IEnumerable<RetentionPolicyDto>>
      +UpdateRetentionPolicyAsync(dto: UpdateRetentionPolicyDto, changedByEmployeeId: guid, cancellationToken: CancellationToken): Task<RetentionPolicyDto>
    }

    class SecurityIncidentService {
      +GetIncidentsAsync(cancellationToken: CancellationToken): Task<IEnumerable<SecurityIncidentDto>>
      +EscalateIncidentAsync(incidentId: guid, isBreach: bool, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +AddInvestigationNotesAsync(dto: AddInvestigationNotesDto, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +CloseIncidentAsync(incidentId: guid, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
    }

    class SubjectAccessRequestService {
      +GenerateExportAsync(dto: SarExportRequestDto, cancellationToken: CancellationToken): Task<SarExportPackageDto>
      +MarkFulfilledAsync(dto: SarFulfilledDto, cancellationToken: CancellationToken): Task<bool>
    }

    class TaskListService {
      +GetAvailableTasksByDepartmentAsync(department: Department, cancellationToken: CancellationToken): Task<IEnumerable<TaskListDto>>
    }

    class TokenService {
      +CreateJwtTokenAsync(user: User, roles: IList<string>, permissions: IList<Claim>, cancellationToken: CancellationToken): Task<string>
      +CreateRefreshTokenAsync(user: User, ipAddress: string, cancellationToken: CancellationToken): Task<RefreshToken>
      +ComputeSha256HashAsync(token: string, cancellationToken: CancellationToken): Task<string>
    }
  }

  namespace Core {
    class DependencyInjection {
      <<static>>
      +AddCore(services: IServiceCollection): IServiceCollection
    }
  }

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Infrastructure Managers Layer
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  namespace Infrastructure.Managers {
    class AccountManager {
      <<class>>
      +CreateAccountAsync(registrationRequestDto: RegisterRequestDto): Task<RegistrationResponseDto>
      +LoginAsync(loginRequestDto: LoginRequestDto): Task<ILoginResult>
      +LogoutAsync(logoutRequestDto: LogoutRequestDto): Task<ILogoutResult>
      +RefreshTokenAsync(refreshTokenRequestDto: RefreshTokenRequestDto): Task<RefreshTokenResponseDto>
    }

    class AnonymizationManager {
      <<class>>
      +GetCandidatesAsync(cancellationToken: CancellationToken): Task<IEnumerable<AnonymizationCandidateDto>>
      +ApproveAnonymizationAsync(candidateId: guid, cancellationToken: CancellationToken): Task<AnonymizationResultDto>
      +RejectAnonymizationAsync(candidateId: guid, reason: string, cancellationToken: CancellationToken): Task<bool>
    }

    class AuditManager {
      <<class>>
      +GetRecentAsync(limit: int Nullable, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetByEntityNameAsync(entityName: string, cancellationToken: CancellationToken): Task<IEnumerable<AuditEntryDto>>
      +GetWithDetailsAsync(id: guid, cancellationToken: CancellationToken): Task<AuditEntryDto Nullable>
    }

    class DatabaseConnectionManager {
      <<class>>
      +CheckAndUpdateConnectionStateAsync(): Task
    }

    class EmployeeManager {
      <<class>>
      +GetAllAsync(cancellationToken: CancellationToken): Task<IEnumerable<EmployeeDto>>
    }

    class HttpApiManagerBase {
      <<abstract>>
    }

    class MedicineRecordManager {
      <<class>>
    }

    class MedicineStatusManager {
      <<class>>
      +GetMedicineStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<MedicineStatusDto Nullable>
      +GetPainkillerStatusAsync(residentId: guid, cancellationToken: CancellationToken): Task<PainkillerStatusDto Nullable>
    }

    class PhoneAssignmentManager {
      <<class>>
      +GetCurrentPhoneAssignmentsForActiveShift(cancellationToken: CancellationToken): Task<IEnumerable<PhoneAssignmentDto>>
    }

    class ResidentManager {
      <<class>>
      +CreateAsync(dto: ResidentCreateRequestDto, ct: CancellationToken): Task
      +CreateRangeAsync(dtos: IEnumerable<ResidentCreateRequestDto>, ct: CancellationToken): Task
      +DeleteAsync(id: guid, ct: CancellationToken): Task
      +DeleteRangeAsync(ids: IEnumerable<guid>, ct: CancellationToken): Task
      +GetAllAsync(ct: CancellationToken): Task<IEnumerable<ResidentResponseDto>>
      +GetByIdAsync(id: guid, ct: CancellationToken): Task<ResidentResponseDto Nullable>
      +GetByDepartmentsAsync(departments: IList<Department>, ct: CancellationToken): Task<IEnumerable<ResidentResponseDto>>
      +UpdateAsync(id: guid, dto: ResidentUpdateRequestDto, ct: CancellationToken): Task
      +UpdateRangeAsync(dtos: IEnumerable<ResidentUpdateRequestDto>, ct: CancellationToken): Task
    }

    class ResidentNoteManager {
      <<class>>
      +GetAllByResidentIdAsync(residentId: guid, cancellationToken: CancellationToken): Task<IEnumerable<ResidentNoteDto>>
      +AddAsync(residentId: guid, noteText: string, cancellationToken: CancellationToken): Task<bool>
      +UpdateAsync(noteId: guid, newText: string, cancellationToken: CancellationToken): Task<bool>
      +DeleteAsync(noteId: guid, cancellationToken: CancellationToken): Task<bool>
    }

    class RetentionPolicyManager {
      <<class>>
      +GetPoliciesAsync(cancellationToken: CancellationToken): Task<IEnumerable<RetentionPolicyDto>>
      +UpdateRetentionPolicyAsync(dto: UpdateRetentionPolicyDto, changedByEmployeeId: guid, cancellationToken: CancellationToken): Task<RetentionPolicyDto>
    }

    class SecurityIncidentManager {
      <<class>>
      +GetIncidentsAsync(cancellationToken: CancellationToken): Task<IEnumerable<SecurityIncidentDto>>
      +EscalateIncidentAsync(incidentId: guid, isBreach: bool, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +AddInvestigationNotesAsync(dto: AddInvestigationNotesDto, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
      +CloseIncidentAsync(incidentId: guid, cancellationToken: CancellationToken): Task<SecurityIncidentDto>
    }

    class StaffAssignmentManager {
      <<class>>
      +AssignAsync(dto: StaffAssignmentDto, cancellationToken: CancellationToken): Task<AssignmentOverviewDto>
      +GetAssignmentsByShiftAsync(shiftType: ShiftType, assignmentDate: DateTime, cancellationToken: CancellationToken): Task<IEnumerable<AssignmentOverviewDto>>
      +DeleteAssignmentAsync(assigmentId: guid, cancellationToken: CancellationToken): Task
      +UpdateAssignmentAsync(assignmentId: guid, dto: StaffAssignmentDto, cancellationToken: CancellationToken): Task<AssignmentOverviewDto>
    }

    class SubjectAccessRequestManager {
      <<class>>
      +GenerateExportAsync(dto: SarExportRequestDto, cancellationToken: CancellationToken): Task<SarExportPackageDto>
      +MarkFulfilledAsync(dto: SarFulfilledDto, cancellationToken: CancellationToken): Task<bool>
    }

    class TaskListManager {
      <<class>>
      +GetDashboardTasksByDepartmentAsync(department: Department, cancellationToken: CancellationToken): Task<IEnumerable<TaskListDto>>
    }
  }

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Domain Associations
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

  %% Associations
  AnonymizationCandidate --> AnonymizationStatus : Status
  
  AnonymizationCandidate -- User : ResidentId
  AnonymizationCandidate -- RetentionDataCategory : RetentionPolicyId

  AuditEntry --> User : UserId

  ChangeDetail "*" o-- "1" AuditEntry : AuditEntryId
  Employee "*" o-- "1" User : UserId
  LoginAttempt "*" o-- "1" User : UserId
  MedicineRecord "*" o-- "1" Resident : ResidentId
  PainkillerRecord "*" o-- "1" Resident : ResidentId
  PhoneAssignment "*" o-- "1" Employee : CaregiverId
  RefreshToken "*" o-- "1" User : UserId
  Resident "*" o-- "1" ResidentNote : Notes
  Resident "*" o-- "1" MedicineRecord : Medicines
  Resident "*" o-- "1" PainkillerRecord : Painkillers
  Resident "*" o-- "1" StaffAssignment : StaffAssignments
  ResidentNote "*" o-- "1" Resident : ResidentId
  RetentionPolicyAudit "*" o-- "1" RetentionPolicy : RetentionPolicyId
  RetentionPolicyAudit "*" o-- "1" Employee : ChangedByEmployeeId
  SecurityIncident "*" o-- "1" Employee : ReportedByEmployeeId
  SecurityIncident "*" o-- "1" Employee : ResolvedByEmployeeId
  StaffAssignment "*" o-- "1" Resident : ResidentId
  StaffAssignment "*" o-- "1" Employee : EmployeeId

  SubjectAccessRequest "*" o-- "1" Resident : ResidentId
  SubjectAccessRequest "*" o-- "1" Employee : RequestedByEmployeeId
  SubjectAccessRequest "*" o-- "1" Employee : FulfilledByEmployeeId

  %% Enumeration associations
  AnonymizationCandidate "1..*" *-- "1" AnonymizationStatus : Status
  Employee "*" *-- "1" Department : Department
  Employee "0..*" *-- "0..*" StaffAssignment : StaffAssignments
  Resident "*" *-- "1" Department : Department
  Resident "*" *-- "1" TrafficLightStatus : TrafficLightStatus
  RetentionPolicy "*" *-- "1" RetentionDataCategory : Category
  SecurityIncident "*" *-- "1" IncidentSeverity : Severity
  SecurityIncident "*" *-- "1" IncidentStatus : Status
  StaffAssignment "*" *-- "1" ShiftType : ShiftType

  %% Realization (Interface Implementation)
  AnonymizationCandidate --|> IEntity : implements
  AuditEntry --|> IEntity : implements
  ChangeDetail --|> IEntity : implements
  Employee --|> IEntity : implements
  LoginAttempt --|> IEntity : implements
  MedicineRecord --|> IEntity : implements
  PainkillerRecord --|> IEntity : implements
  PhoneAssignment --|> IEntity : implements
  RefreshToken --|> IEntity : implements
  Resident --|> IEntity : implements
  ResidentNote --|> IEntity : implements
  RetentionPolicy --|> IEntity : implements
  RetentionPolicyAudit --|> IEntity : implements
  SecurityIncident --|> IEntity : implements
  StaffAssignment --|> IEntity : implements
  SubjectAccessRequest --|> IEntity : implements
  TaskList --|> IEntity : implements
  User --|> IEntity : implements

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application DTO Implementsations
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  DeleteUserResponseDto --|> IDeleteResult : implements
  LoginResponseDto --|> ILoginResult : implements
  LogoutResponseDto --|> ILogoutResult : implements
  ErrorDto --|> ILoginResult : implements
  ErrorDto --|> ILogoutResult : implements
  ErrorDto --|> IDeleteResult : implements

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application DTO Associations
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  EmployeeDto *-- Department : Department
  ResidentResponseDto *-- TrafficLightStatus : TrafficLightStatus
  RetentionPolicyDto *-- RetentionDataCategory : Category

  MedicineStatusDto --|> MedicineEntryDto : Entries
  ResidentCreateRequestDto --|> TrafficLightStatus : TrafficLightStatus
  ResidentCreateRequestDto --|> Department : Department
  ResidentUpdateRequestDto --|> TrafficLightStatus : TrafficLightStatus
  ResidentUpdateRequestDto --|> Department : Department
  ResidentResponseDto --|> ResidentNote : Notes
  TaskListDto --|> TaskListStatus : TaskListStatus
  TaskListDto --|> Department : Department

  ApproveAnonymizationDto --|> AnonymizationCandidate : CandidateId
  AuditEntryDto --|> ChangeDetailDto : ChangeDetails
  RetentionPolicyAuditDto --> RetentionPolicy : RetentionPolicyId
  RetentionPolicyAuditDto --> Employee : ChangedByEmployeeId
  SarExportRequestDto --|> Resident : ResidentId
  SarFulfilledDto --|> SubjectAccessRequest : SarId
  UpdateRetentionPolicyDto --> RetentionDataCategory : Category

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application Interface Associations
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Realization (Interface Implementation)
  IRepository~TEntity~ --|> ICRUD~TEntity~ : extends
  IAuditRepository --|> IRepository~AuditEntry~ : extends
  IAnonymizationCandidateRepository --|> IRepository~AnonymizationCandidate~ : extends
  IEmployeeRepository --|> IRepository~Employee~ : extends
  ILoginAttemptRepository --|> IRepository~LoginAttempt~ : extends
  IMedicineRepository --|> IRepository~MedicineRecord~ : extends
  IPainkillerRepository --|> IRepository~PainkillerRecord~ : extends
  IPhoneAssignmentRepository --|> IRepository~PhoneAssignment~ : extends
  IResidentNoteRepository --|> IRepository~ResidentNote~ : extends
  IResidentRepository --|> IRepository~Resident~ : extends
  IRetentionPolicyAuditRepository --|> IRepository~RetentionPolicyAudit~ : extends
  IRetentionPolicyRepository --|> IRepository~RetentionPolicy~ : extends
  ISecurityIncidentRepository --|> IRepository~SecurityIncident~ : extends
  IStaffAssignmentRepository --|> IRepository~StaffAssignment~ : extends
  ISubjectAccessRequestRepository --|> IRepository~SubjectAccessRequest~ : extends
  ITaskListRepository --|> IRepository~TaskList~ : extends
  IUserRepository --|> IRepository~User~ : extends

  IAccountService --> ILoginResult : LoginAsync
  IAccountService --> ILogoutResult : LogoutAsync
  IAccountService --> RegisterRequestDto : CreateAccountAsync
  IAccountService --> RegistrationResponseDto : CreateAccountAsync
  IAccountService --> LoginRequestDto : LoginAsync
  IAccountService --> RefreshTokenRequestDto : RefreshTokenAsync
  IAccountService --> RefreshTokenResponseDto : RefreshTokenAsync
  IAccountService --> LogoutRequestDto : LogoutAsync

  IAnonymizationService --> AnonymizationCandidateDto : GetCandidatesAsync
  IAnonymizationService --> AnonymizationResultDto : ApproveAnonymizationAsync

  IArt33NotificationService --> SecurityIncidentDto : SendNotificationAsync

  IAuditService --> AuditEntryDto : GetRecentAsync
  IAuditService --> AuditEntryDto : GetByEntityNameAsync
  IAuditService --> AuditEntryDto : GetWithDetailsAsync
  IAuditService --> ChangeDetailDto : GetWithDetailsAsync

  IDatabaseConnectionService --> DataConnection : CheckDatabaseConnectionAsync
  IDatabaseService --> DataConnection : IsConnected

  IMedicineStatusService --> MedicineStatusDto : GetMedicineStatusAsync
  IMedicineStatusService --> PainkillerStatusDto : GetPainkillerStatusAsync

  IPhoneAssignmentService --> PhoneAssignmentDto : GetCurrentPhoneAssignmentsForActiveShiftAsync

  IRefreshTokenStore --> RefreshToken : GetByTokenAsync
  IRefreshTokenStore --> RefreshToken : RevokeAsync
  IRefreshTokenStore --> RefreshToken : SaveAsync
  IRefreshTokenStore --> ITokenService : GetByTokenAsync
  IRefreshTokenStore --> ITokenService : RevokeAsync
  IRefreshTokenStore --> ITokenService : SaveAsync

  IResidentNoteService --> ResidentNoteDto : GetAllByResidentIdAsync
  IResidentNoteService --> ResidentNote : AddAsync
  IResidentNoteService --> ResidentNote : UpdateAsync
  IResidentNoteService --> ResidentNote : DeleteAsync

  IResidentService --> ResidentCreateRequestDto : CreateAsync
  IResidentService --> ResidentUpdateRequestDto : UpdateAsync
  IResidentService --> Resident : GetAllAsync
  IResidentService --> Resident : GetByDepartmentsAsync
  IResidentService --> Resident : GetByIdAsync

  IRetentionPolicyService --> RetentionPolicyDto : GetPoliciesAsync
  IRetentionPolicyService --> UpdateRetentionPolicyDto : UpdateRetentionPolicyAsync

  ISecurityIncidentService --> SecurityIncidentDto : GetIncidentsAsync
  ISecurityIncidentService --> SecurityIncidentDto : EscalateIncidentAsync
  ISecurityIncidentService --> SecurityIncidentDto : AddInvestigationNotesAsync
  ISecurityIncidentService --> SecurityIncidentDto : CloseIncidentAsync
  ISecurityIncidentService --> AddInvestigationNotesDto : AddInvestigationNotesAsync

  IStaffAssignmentService --> StaffAssignmentDto : AssignAsync
  IStaffAssignmentService --> AssignmentOverviewDto : AssignAsync
  IStaffAssignmentService --> AssignmentOverviewDto : GetAssignmentsByShiftAsync
  IStaffAssignmentService --> AssignmentOverviewDto : DeleteAssignmentAsync
  IStaffAssignmentService --> AssignmentOverviewDto : UpdateAssignmentAsync

  ISubjectAccessRequestService --> SarExportRequestDto : GenerateExportAsync
  ISubjectAccessRequestService --> SarExportPackageDto : GenerateExportAsync
  ISubjectAccessRequestService --> SarFulfilledDto : MarkFulfilledAsync

  ITaskListService --> TaskListDto : GetAvailableTasksByDepartmentAsync

  ITokenService --> User : CreateJwtTokenAsync
  ITokenService --> RefreshToken : CreateRefreshTokenAsync

  DependencyInjection --> IServiceCollection : AddCore
  DependencyInjection --> IResidentService : AddCore
  DependencyInjection --> IResidentNoteService : AddCore
  DependencyInjection --> IMedicineStatusService : AddCore
  DependencyInjection --> IPhoneAssignmentService : AddCore
  DependencyInjection --> IAccountService : AddCore
  DependencyInjection --> ITaskListService : AddCore
  DependencyInjection --> IDatabaseConnectionService : AddCore
  DependencyInjection --> ITokenService : AddCore
  DependencyInjection --> IDatabaseConnectionStateProvider : AddCore
  DependencyInjection --> IRetentionPolicyService : AddCore
  DependencyInjection --> IAnonymizationService : AddCore
  DependencyInjection --> ISecurityIncidentService : AddCore
  DependencyInjection --> ISubjectAccessRequestService : AddCore
  DependencyInjection --> IArt33NotificationService : AddCore
  DependencyInjection --> IPseudonymizationService : AddCore

  %% Manager associations
  IResidentNoteManager --> ResidentNoteDto : GetAllByResidentIdAsync
  IResidentNoteManager --> ResidentNote : AddAsync
  IResidentNoteManager --> ResidentNote : UpdateAsync
  IResidentNoteManager --> ResidentNote : DeleteAsync

  IRetentionPolicyManager --> RetentionPolicyDto : GetPoliciesAsync
  IRetentionPolicyManager --> UpdateRetentionPolicyDto : UpdateRetentionPolicyAsync

  ISecurityIncidentManager --> SecurityIncidentDto : GetIncidentsAsync
  ISecurityIncidentManager --> SecurityIncidentDto : EscalateIncidentAsync
  ISecurityIncidentManager --> SecurityIncidentDto : AddInvestigationNotesAsync
  ISecurityIncidentManager --> SecurityIncidentDto : CloseIncidentAsync
  ISecurityIncidentManager --> AddInvestigationNotesDto : AddInvestigationNotesAsync

  ISubjectAccessRequestManager --> SarExportRequestDto : GenerateExportAsync
  ISubjectAccessRequestManager --> SarExportPackageDto : GenerateExportAsync
  ISubjectAccessRequestManager --> SarFulfilledDto : MarkFulfilledAsync

  ITaskListManager --> TaskListDto : GetDashboardTasksByDepartmentAsync

  IDatabaseConnectionStateProvider --> DataConnection : IsConnected

  %% Service class associations
  AccountService --> IAccountManager : delegates
  AnonymizationService --> IAnonymizationManager : delegates
  Art33NotificationService --> ILogger : logs
  AuditService --> IAuditManager : delegates
  DatabaseConnectionService --> IDatabaseConnectionManager : delegates
  MedicineStatusService --> IMedicineStatusManager : delegates
  PhoneAssignmentService --> IPhoneAssignmentManager : delegates
  PseudonymizationService --> IConfiguration : configuration
  ResidentNoteService --> IResidentNoteManager : delegates
  ResidentService --> IResidentManager : delegates
  RetentionPolicyService --> IRetentionPolicyManager : delegates
  SecurityIncidentService --> ISecurityIncidentManager : delegates
  SecurityIncidentService --> IArt33NotificationService : delegates
  SubjectAccessRequestService --> ISubjectAccessRequestManager : delegates
  TaskListService --> ITaskListManager : delegates
  TokenService --> IConfiguration : configuration
  TokenService --> ILogger : logs
  TokenService --> User : CreateJwtTokenAsync
  TokenService --> RefreshToken : CreateRefreshTokenAsync

  %% Repository associations
  IAnonymizationCandidateRepository --> AnonymizationCandidate : Repository
  IAuditRepository --> AuditEntry : GetRecentAsync
  IAuditRepository --> ChangeDetail : GetWithDetailsAsync
  IEmployeeRepository --> Employee : Repository
  ILoginAttemptRepository --> LoginAttempt : Repository
  IMedicineRepository --> MedicineRecord : Repository
  IMedicineRepository --> Resident : GetMedicineStatusLast24HoursAsync
  IPainkillerRepository --> PainkillerRecord : Repository
  IPainkillerRepository --> Resident : GetPainkillerStatusLast24HoursAsync
  IPhoneAssignmentRepository --> PhoneAssignment : Repository
  IPhoneAssignmentRepository --> PhoneAssignmentDto : GetDtoByShiftTypeAsync
  IPhoneAssignmentRepository --> ShiftType : GetByShiftTypeAsync
  IResidentNoteRepository --> ResidentNote : Repository
  IResidentRepository --> Resident : Repository
  IResidentRepository --> Department : GetAllAsync
  IRetentionPolicyAuditRepository --> RetentionPolicyAudit : Repository
  IRetentionPolicyRepository --> RetentionPolicy : Repository
  ISecurityIncidentRepository --> SecurityIncident : Repository
  IStaffAssignmentRepository --> StaffAssignment : Repository
  IStaffAssignmentRepository --> Resident : GetByResidentAsync
  IStaffAssignmentRepository --> Employee : GetExistingAssignmentAsync
  IStaffAssignmentRepository --> ShiftType : GetByShiftAsync
  ISubjectAccessRequestRepository --> SubjectAccessRequest : Repository
  ITaskListRepository --> TaskList : Repository
  ITaskListRepository --> Department : GetDashboardTasksByDepartmentAsync
  IUserRepository --> User : Repository

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Application Mappers
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  AnonymizationCandidateMapper --> AnonymizationCandidate : ToDto
  AnonymizationCandidateMapper --> AnonymizationCandidateDto : ToDto
  RegistrationMapper --> RegisterRequestDto : ToUserEntity
  RegistrationMapper --> User : ToUserEntity
  AuditMapper --> AuditEntry : ToDto
  AuditMapper --> AuditEntryDto : ToDto
  AuditMapper --> AuditEntry : ToDtos
  AuditMapper --> AuditEntryDto : ToDtos
  ChangeDetailMapper --> ChangeDetail : ToDto
  ChangeDetailMapper --> ChangeDetailDto : ToDto
  ChangeDetailMapper --> ChangeDetail : ToDtos
  ChangeDetailMapper --> ChangeDetailDto : ToDtos
  MedicineMapper --> MedicineRecord : ToMedicineStatusDto
  MedicineMapper --> MedicineStatusDto : ToMedicineStatusDto
  MedicineMapper --> MedicineEntryDto : ToMedicineStatusDto
  PainKillerMapper --> PainkillerRecord : ToPainkillerStatusDto
  PainKillerMapper --> PainkillerStatusDto : ToPainkillerStatusDto
  PhoneAssignmentMapper --> PhoneAssignment : ToDto
  PhoneAssignmentMapper --> PhoneAssignmentDto : ToDto
  PhoneAssignmentMapper --> PhoneAssignment : ToDtos
  PhoneAssignmentMapper --> PhoneAssignmentDto : ToDtos
  ResidentMapper --> ResidentResponseDto : ToResident
  ResidentMapper --> Resident : ToResident
  ResidentMapper --> Resident : ToResidentResponseDto
  ResidentMapper --> ResidentResponseDto : ToResidentResponseDto
  ResidentMapper --> ResidentNote : ToResidentNoteDto
  ResidentMapper --> ResidentNoteDto : ToResidentNoteDto
  ResidentMapper --> ResidentCreateRequestDto : ToResident
  ResidentMapper --> ResidentNoteDto : ToResidentNote
  ResidentMapper --> ResidentNote : ToResidentNote
  ResidentNoteMapper --> ResidentNote : ToDto
  ResidentNoteMapper --> ResidentNoteDto : ToDto
  ResidentNoteMapper --> ResidentNote : ToDtos
  ResidentNoteMapper --> ResidentNoteDto : ToDtos
  ResidentNoteMapper --> ResidentNote : ToNewEntity
  RetentionPolicyMapper --> RetentionPolicy : ToDto
  RetentionPolicyMapper --> RetentionPolicyDto : ToDto
  RetentionPolicyMapper --> RetentionPolicyAudit : ToAuditDto
  RetentionPolicyMapper --> RetentionPolicyAuditDto : ToAuditDto
  SarExportMapper --> SarExportPackageDto : ToPackageDto
  SecurityIncidentMapper --> SecurityIncident : ToDto
  SecurityIncidentMapper --> SecurityIncidentDto : ToDto
  TaskListMapper --> TaskList : ToTaskListDto
  TaskListMapper --> TaskListDto : ToTaskListDto

  ShiftTypeHelper --> ShiftType : ToDanishString

  %% Service associations

  DataConnection --> DbConnectionState : State

  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Infrastructure Managers Assorcations
  %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
  %% Infrastructure manager associations
  AccountManager --> IAccountManager : implements
  AccountManager --> IHttpClientFactory : CreateClient
  AnonymizationManager --> IAnonymizationManager : implements
  AnonymizationManager --> IHttpClientFactory : CreateClient
  AuditManager --> IAuditManager : implements
  AuditManager --> IHttpClientFactory : CreateClient
  DatabaseConnectionManager --> IDatabaseConnectionManager : implements
  DatabaseConnectionManager --> IHttpClientFactory : CreateClient
  DatabaseConnectionManager --> IDatabaseConnectionStateProvider : updates
  EmployeeManager --> IEmployeeManager : implements
  EmployeeManager --> IHttpClientFactory : CreateClient
  HttpApiManagerBase --> IHttpClientFactory : CreateClient
  MedicineRecordManager --> IMedicineRecordManager : implements
  MedicineRecordManager --> HttpClient : uses
  MedicineStatusManager --> IMedicineStatusManager : implements
  MedicineStatusManager --> IHttpClientFactory : CreateClient
  PhoneAssignmentManager --> IPhoneAssignmentManager : implements
  PhoneAssignmentManager --> IHttpClientFactory : CreateClient
  ResidentManager --> IResidentManager : implements
  ResidentManager --> IHttpClientFactory : CreateClient
  ResidentNoteManager --> IResidentNoteManager : implements
  ResidentNoteManager --> IHttpClientFactory : CreateClient
  RetentionPolicyManager --> IRetentionPolicyManager : implements
  RetentionPolicyManager --> IHttpClientFactory : CreateClient
  SecurityIncidentManager --> ISecurityIncidentManager : implements
  SecurityIncidentManager --> IHttpClientFactory : CreateClient
  StaffAssignmentManager --> IStaffAssignmentService : implements
  StaffAssignmentManager --> IStaffAssignmentRepository : uses
  SubjectAccessRequestManager --> ISubjectAccessRequestManager : implements
  SubjectAccessRequestManager --> IHttpClientFactory : CreateClient
  TaskListManager --> ITaskListManager : implements
  TaskListManager --> IHttpClientFactory : CreateClient
  
  
  ```
