# Gateway for Slottet IT System

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | MILESTONE                         |
| crossReference    | BC<br>FURPS<br>KPI                |

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-03-26 | 0001    | Initial creation of the document | Team 6        |
| 2026-04-29 | 0002    | Added milestones, gateways, and use cases | Team 6        |

---

## Gantt diagram
```mermaid
gantt
 title Project Milestones and Gateways
 section Phase 1 – Quick Prototype 
 Gateway 1: Resident Overview :a1, 2026-03-31, 21d 
 Gateway 2: Tasks and Messages :a2, after a1, 7d 
 Gateway 3: Basic Medicine Status :a3, after a2, 7d 
 section Phase 2 – Extended Functionality 
 Gateway 4: Staff Assignment :a4, after a3, 7d 
 Gateway 5: Shared Fields :a5, after a4, 7d 
 Gateway 6: User Roles and Login :a6, after a5, 7d 
 Gateway 11: Resident Administration Management :a11, after a6, 3d
 Gateway 12: Task/Message Administration Management :a12, after a11, 3d
 Gateway 13: Medicine Administration Management :a13, after a12, 3d
 section Phase 3 – Stability and Security 
 Gateway 7: History and Traceability :a7, after a6, 7d 
 Gateway 8: Data Security and GDPR :a8, after a7, 7d 
 Gateway 9: Deployment :a9, after a8, 7d
 Gateway 10: Status Bar :a10, after a9, 3d
```

## Gateways & Milestones 
| Milestone/Gateway         | Description                                                                 | Entry Criteria                                 | Exit Criteria                                  |
|--------------------------|-----------------------------------------------------------------------------|-----------------------------------------------|-----------------------------------------------|
| Gateway 1: Resident Overview | Display Residents with traffic light status. Manual update. Simple UI.         | System setup, Resident data available           | List visible, status updatable                |
| Gateway 2: Tasks and Messages | Add per-Resident tasks/messages for next shift. DB storage.                  | Gateway 1 complete, DB ready                   | Tasks/messages can be added/viewed            |
| Gateway 3: Basic Medicine Status | Checkbox for medicine delivered. Register delivery time.                    | Gateway 2 complete, medicine data available    | Medicine status tracked per Resident           |
| Gateway 4: Staff Assignment    | Assign staff to Residents. Handle mid-shift changes.                        | Gateway 3 complete, staff data available       | Staff assigned, changes possible              |
| Gateway 5: Shared Fields       | Assign staff to phones/areas (hygiene, dinner, coffee).                    | Gateway 4 complete, phone/area data available  | Staff assigned to shared fields               |
| Gateway 6: User Roles and Login| Implement roles (staff, leader, admin). Basic login/session.               | Gateway 5 complete, user data available        | Role-based access, login/session working      |
| Gateway 7: History and Traceability | View previous overlaps. Log changes for audit.                              | Gateway 6 complete, audit log infra ready      | History viewable, changes logged              |
| Gateway 8: Data Security and GDPR | Access logging. Basic encryption of sensitive data.                         | Gateway 7 complete, security infra ready       | Access logged, sensitive data encrypted       |
| Gateway 9: Deployment         | Standalone install, client/server, Docker containerization.                | Gateway 8 complete, deployment infra ready     | System deployable in all target environments  |
| Gateway 10: Status Bar           | Show data connection status and current user in a status bar.                | Gateway 9 complete, user and connection logic available | Status bar displays connection and user info  |
| Gateway 11: Resident Administration Management | Create, update, and delete Residents (admin). | Gateway 6 complete, Resident admin logic available | Residents can be created, updated, deleted |
| Gateway 12: Task/Message Administration Management | Create, update, and delete tasks/messages (admin). | Gateway 11 complete, task/message admin logic available | Tasks/messages can be created, updated, deleted |
| Gateway 13: Medicine Administration Management | Register, update, and delete medicine deliveries (admin). | Gateway 12 complete, medicine admin logic available | Medicine deliveries can be registered, updated, deleted |

## Use cases associated with each milestone/gateway
| Use Case ID | Description                                 | Milestone/Gateway           | CrossReference |
|-------------|---------------------------------------------|-----------------------------|----------------|
| uc-001      | View Resident overview                       | Gateway 1: Resident Overview | REQ-F-001     |
| uc-002      | View tasks/messages per Resident             | Gateway 2: Tasks and Messages | REQ-F-001     |
| uc-003      | View medicine status                        | Gateway 3: Basic Medicine Status | REQ-F-001     |
| uc-004      | User login and role-based access            | Gateway 6: User Roles and Login | REQ-F-003, REQ-F-004, REQ-F-005     |
| uc-005      | Assign staff to phones/areas                | Gateway 5: Shared Fields        | REQ-F-001     |
| uc-006      | View dashboard task list                    | Gateway 2: Tasks and Messages   | REQ-U-002     |
| uc-007      | Authenticate to access dashboard data       | Gateway 6: User Roles and Login | REQ-F-005     |
| uc-008      | Assign staff to Residents                    | Gateway 4: Staff Assignment     | REQ-F-001     |
| uc-009      | View history and traceability               | Gateway 7: History and Traceability | REQ-F-006 |
| uc-010      | Ensure data security and GDPR compliance    | Gateway 8: Data Security and GDPR | REQ-DC-001 |
| uc-011      | Deploy system in various environments       | Gateway 9: Deployment           | REQ-IMPL-001 |
| uc-012      | Show data connection status in status bar  | Gateway 10: Status Bar          | REQ-R-004, REQ-U-002 |
| uc-013      | Show current user in status bar            | Gateway 10: Status Bar          | REQ-U-002, REQ-F-005 |
| uc-014      | Create Resident (administration)             | Gateway 11: Resident Administration Management | REQ-F-001     |
| uc-015      | Update Resident (administration)             | Gateway 11: Resident Administration Management | REQ-F-001     |
| uc-016      | Delete Resident (administration)             | Gateway 11: Resident Administration Management | REQ-F-001     |
| uc-017      | Create task/message (administration)        | Gateway 12: Task/Message Administration Management | REQ-F-001     |
| uc-018      | Update task/message (administration)        | Gateway 12: Task/Message Administration Management | REQ-F-001     |
| uc-019      | Delete task/message (administration)        | Gateway 12: Task/Message Administration Management | REQ-F-001     |
| uc-020      | Register medicine delivery (administration) | Gateway 13: Medicine Administration Management | REQ-F-001     |
| uc-021      | Update medicine delivery (administration)   | Gateway 13: Medicine Administration Management | REQ-F-001     |
| uc-022      | Delete medicine delivery (administration)   | Gateway 13: Medicine Administration Management | REQ-F-001     |
| uc-023      | Add navigation bar to main layout           | Gateway 10: Status Bar                        | REQ-U-003     |

---

This structure ensures all CRUD use cases are explicitly listed for each relevant entity.
