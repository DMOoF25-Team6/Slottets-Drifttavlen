# Milestone Plan for Slottet IT System

## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | Milestone                         |
| crossReference    |                                   |

## Version
- **Version**: 0001
- **Date**: 2026-02-07

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-26-07 | 0001    | Initial creation of the document |               |

---

## Phase 1 – Quick Prototype
- **Milestone 1: Citizen Overview (MVP)**
  - Display list of citizens with names and traffic light status (Red/Yellow/Green).
  - Manual update of status.
  - Simple Blazor-based interface.

- **Milestone 2: Tasks and Messages**
  - Add a field per citizen for tasks/messages to the next shift.
  - Store data in the database.

- **Milestone 3: Basic Medicine Status**
  - Checkbox for "Delivered / Not Delivered".
  - Option to register delivery time.

---

## Phase 2 – Extended Functionality
- **Milestone 4: Staff Assignment**
  - Assign 1–2 staff members per citizen.
  - Handle mid-shift staff changes.

- **Milestone 5: Shared Fields**
  - Assign staff to work phones.
  - Assign staff to responsibility areas (e.g., hygiene, dinner, coffee).

- **Milestone 6: User Roles and Login**
  - Implement roles (care staff, shift leader, admin).
  - Basic login and session management.

---

## Phase 3 – Stability and Security
- **Milestone 7: History and Traceability**
  - View previous overlaps.
  - Log changes for audit purposes.

- **Milestone 8: Data Security and GDPR**
  - Access logging.
  - Basic encryption of sensitive data.

- **Milestone 9: Deployment**
  - Standalone installation and client/server setup.
  - Docker containerization.

---

## Future Improvements (Optional)
- Integration with FMK (Shared Medicine Card).
- Notifications (e.g., medicine reminders).
- Reporting and statistics on events.
- Responsive design for tablets and mobile devices.
- Cloud deployment with scalability and high availability.

---

## Suggested Progression
1. **Prototype** → Citizen overview with traffic light model.  
2. **Extension** → Add medicine status and tasks for practical daily use.  
3. **Maturation** → User roles, history, and security for robustness and exam readiness.
