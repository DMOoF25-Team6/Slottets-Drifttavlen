# Glossary

## Terms
| Term | Definition | Notes |
|------|------------|-------|
| GDPR Compliance Dashboard | A dedicated admin view for managing GDPR-related operational tasks. | Entry point for UC-010 user journeys. |
| Data retention policy | Rules that define how long specific categories of data are kept and on what legal basis. | Must respect legal minimums. |
| Legal minimum retention | A mandatory minimum retention period required by law for specific record types. | Example: medicine documentation retention in Danish law. |
| Anonymization candidate | A record or dataset suggested for anonymization based on retention rules and status. | Reviewed and actioned by Admin. |
| Hold (legal/audit hold) | A constraint that prevents anonymization or deletion while an investigation, audit, or legal need is active. | Used in the anonymization queue decisions. |
| Security incident | An event indicating suspicious activity or a potential security problem requiring investigation. | Managed in the incidents view. |
| Personal data breach | A security incident involving unauthorized access, loss, or disclosure of personal data. | May require notification obligations. |
| Subject Access Request (SAR) | A request by a data subject to access their personal data (GDPR Art. 15). | Handled as an export workflow. |
| SAR export package | The generated export/report containing the data subject’s personal data and related processing records. | Reviewed for minimization before fulfillment. |
| Data minimization | Principle of limiting disclosed/processed personal data to what is necessary for a purpose. | Applied during SAR review. |
| Audit log / audit trail | A tamper-evident record of user actions and state changes. | Used for traceability (UC-009). |
| Art. 33 notification | A notification step for reporting certain personal data breaches (GDPR Art. 33). | Triggered when breach conditions apply. |
| DPO (Data Protection Officer) | The person/role responsible for oversight of data protection compliance and breach communication. | Recipient in breach notification confirmation. |
| Phone assignments | Information about how phones or phone responsibilities are assigned (e.g., per shift/team). | Included as an optional item in SAR exports. |

## Artifact Term Change Table
| High-Level Term | Low-Level Term | Reason/Context |
|-----------------|---------------|---------------|
| N/A | N/A | No renamed terms recorded yet. |
