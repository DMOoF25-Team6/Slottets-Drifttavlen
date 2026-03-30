# FURPS+
FURPS+ is a model for classifying software quality attributes.
The acronym stands for Functionality, Usability, Reliability, Performance, and Supportability, with the "+" indicating additional considerations such as design constraints, implementation requirements, interface requirements, and physical requirements.

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | FURPS	                            |
| crossReference    |                                   |

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-07 | 0001    | Initial creation of the document | Team 6       |
| 2026-03-30 | 0002    | New po requirements added       | Team 6        |

---

## FURPS+ for Slottet – Digital Handover System

### Functionality
- [REQ-F-001] Digital handover form with citizen fields, traffic light model, medication status, tasks, and messages.
- [REQ-F-002] Support for both Skoven and Slottet departments with the option for separate and combined views.
- [REQ-F-003] User roles: care staff, shift managers, leaders/coordinators, admin.
- [REQ-F-004] Differentiated user permissions (regular staff vs. leaders/coordinators).
- [REQ-F-005] Access control and login (quick login, minimum password requirements, initials/email as username).
- [REQ-F-006] History and traceability for all handovers and changes (audit trail: who wrote/edited what and when).
- [REQ-F-007] Marking of late/retroactive entries with correct timestamp.
- [REQ-F-008] Quick and easy marking of medication and risk assessment (traffic light system).
- [REQ-F-009] Integration with FMK and notification system (extension).

### Usability
- [REQ-U-001] Intuitive user interface adapted for busy shift changes and all employee types (including temps and new hires).
- [REQ-U-002] Clear visual overview of citizen status and tasks.
- [REQ-U-003] Responsive design for tablets, PCs, and work phones (mobile devices).
- [REQ-U-004] Fast navigation and easy reading of critical information.
- [REQ-U-005] Quick and easy login process.
- [REQ-U-006] Possibility for ongoing and subsequent documentation, with clear marking of late entries.

### Reliability
- [REQ-R-001] High availability and failover (cloud-ready, local and network operation).
- [REQ-R-002] Automatic backup and versioning of data.
- [REQ-R-003] Audit log for all changes and user actions.
- [REQ-R-004] Robust handling of network and power outages.

### Performance
- [REQ-P-001] Fast loading and updating of citizen data.
- [REQ-P-002] Scalable solution (horizontal scaling, multiple instances).
- [REQ-P-003] Efficient handling of concurrent users during shift changes.

### Supportability
- [REQ-S-001] Comprehensive user documentation and training.
- [REQ-S-002] Ticketing system and support function.
- [REQ-S-003] Easy updating and maintenance of the system.

### Design Constraints
- [REQ-DC-001] GDPR compliance and secure handling of personal data.

### Implementation
- [REQ-IMPL-001] Containerization via Docker, cloud-ready architecture.

### Interface
- [REQ-INT-001] API for integration with external systems.

### Physical
- [REQ-PHY-001] Central configuration via environment variables and secrets.
- [REQ-PHY-002] No dependencies on local file system, fixed server, or IP.
