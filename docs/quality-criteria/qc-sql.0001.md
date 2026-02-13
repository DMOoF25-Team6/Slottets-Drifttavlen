# SQL Quality Criteria
The SQL Quality Criteria focus on ensuring that SQL code is well-structured, maintainable, and adheres to best practices.
This includes aspects such as code readability, performance, security, and adherence to coding standards.
## Metadata
| Key               | Value                             |
|-------------------|-----------------------------------|
| Id                | QC-SQL                            |

## Version
- **Version**: 0001
- **Date**: 2026-02-07

### Change Log
| Date       | Version | Description                     | Author        |
|------------|---------|---------------------------------|---------------|
| 2026-02-07 | 0001    | Initial creation of the document |               |

## MSSQL Quality Criteria
- **Naming conventions**: Use PascalCase for naming tables, columns, procedures and other database objects (e.g., `CustomerOrder`, `GetCustomerById`).
    - Tables should be named in singular form (e.g., `Customer`, not `Customers`).
    - Database objects should use the prefix `usp` for stored procedures and `vw` for views.
    - All tables must have a primary key.

### Code Quality
- **dbo**: All objects should be contained in the `dbo` schema.
- **Indentation**: Use consistent indentation with 4 spaces per level.
- **Comments**: Use comments to explain complex queries or logic in SQL code.
