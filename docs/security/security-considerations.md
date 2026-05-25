# Security Considerations

Slottets Drifttavlen processes sensitive personal data about vulnerable residents — including health observations and medication handling — and is therefore subject to **GDPR Article 9**. Security and *privacy by design* are built into the architecture itself rather than added as an extra layer.

## Authentication and access control

The system uses **ASP.NET Core Identity** as its authentication framework (see *ADR-0003*). Passwords are hashed with Identity's default algorithm (**PBKDF2**) and never stored in plain text, and **password complexity** is enforced (minimum 7 characters with upper/lower case, digit, and special character). Accounts are automatically locked after **5 failed attempts within 5 minutes**, which slows down brute force attacks. Login is handled by `AccountController.Login`, which validates credentials and issues a **JWT access token** along with a **refresh token**; refresh tokens are stored hashed, so they cannot be misused even if the database is compromised. The JWT is validated on *issuer, audience, lifetime, and signing key* without clock skew. Access control is role-based via `[Authorize(Roles = "admin")]` on sensitive endpoints — for example, user creation, deletion, and anonymization are restricted to the admin role.

## Input validation and protection against injection

All communication between client and API flows through **DTOs** in the Core layer, which validate input with DataAnnotations (`[Required]`, `[EmailAddress]`, `[MinLength]`). Controllers reject invalid requests via `ModelState.IsValid` before data reaches the business logic. The DTO pattern also protects against **mass assignment**, since clients can only set explicitly exposed fields — a user cannot assign themselves the admin role, for example. All database access goes exclusively through **Entity Framework Core** in the Infrastructure layer, which generates parameterized SQL statements via LINQ and thereby eliminates **SQL injection**. *Clean Architecture* enforces that no other layer has direct database access.

## Encryption and secrets

Communication between WebUI, WebApi, and client must run over **HTTPS/TLS** in production (redirect is activated at deployment). Database credentials and JWT signing keys live in a `.env` file outside version control and are injected as environment variables via Docker Compose — `appsettings.json` contains only placeholders, so *secrets never exist in Git history*. Output in Blazor is automatically encoded in Razor templates, which prevents **cross-site scripting (XSS)**.

## Traceability and GDPR mechanisms (implemented)

An **audit interceptor** automatically logs all data changes on `SaveChanges` (traceability per *GDPR Art. 5*), and the audit history can only be read by authenticated users. All login attempts (success and failure, including IP) are recorded, and an **incident detection service** analyzes them for brute force activity and access outside normal working hours. The system also supports **automatic data retention/deletion**, **subject access requests** (*Art. 15*), and **anonymization**.

## Diagrams

### Security layers in the login flow

This diagram shows how a login request passes through five security layers in chronological order, and the concrete threats each layer blocks (*defense in depth*).

![Security layers in the login flow with threats](./diagrams/login_flow_with_threats.svg)

### Security components in Clean Architecture

This diagram shows where each security responsibility is placed in the codebase. Dependencies point inward toward Domain, and each layer blocks specific attacks.

![Security components in Clean Architecture with threats](./diagrams/clean_architecture_with_threats.svg)

## Improvement suggestions (production version)

- **Forced HTTPS redirect** and a stricter **CORS policy** (currently all origins are allowed)
- **Two-factor authentication (2FA)** for admin roles
- **Encryption at rest** for particularly sensitive database fields
- **Automated scanning** of NuGet dependencies for known vulnerabilities
- **Penetration testing** before deployment
