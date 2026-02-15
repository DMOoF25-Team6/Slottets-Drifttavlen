# Copilot Instructions for DMOoF25-Team6.Slottets-Drifttavlen

## Project Overview
- This is a .NET solution for Slottet.CareManagement, using Clean Architecture principles.
- The project use MySQL and Blazor for frontend development.
- Solution should could run standalone, local network and cloud-ready (Azure, AWS).
- Containerization is supported via Docker.
- Major components are in `src/` (production code) and `tests/` (test projects). Each layer (Core, Domain, Infrastructure) is separated into its own project.
- The solution is managed via `Slottets-Drifttavlen.slnx` and `global.json`.

## Architecture & Patterns
- **Clean Architecture**: Core business logic is in `Core`, domain models in `Domain`, and infrastructure (e.g., data access) in `Infrastructure`.
- **Testing**: Test projects mirror the main projects (e.g., `Core.Tests` for Core). Place new tests in the corresponding `tests/` subfolder.
- **Build System**: Uses solution-wide MSBuild props/targets (`Directory.Build.props`, `Directory.Build.targets`).
- **NuGet**: Local NuGet packages are in `LocalNuget/`. Use `nuget.config` for custom feeds.

## Developer Workflows
- **Build**: Run `dotnet build Slottets-Drifttavlen.slnx` from the repo root.
- **Test**: Run `dotnet test Slottets-Drifttavlen.slnx` to execute all tests.
- **Debug**: Attach to the CLI process or use launch profiles in your IDE.
- **Docs**: Architecture decisions and use cases are in `docs/`. Doxygen config is in `Doxyfile`.
- **Docker**: Use `docker-compose.yml` and `Dockerfile` for containerized workflows.

## Conventions
- **Naming**: Test projects use `.Test` or `.Tests` suffixes, matching the main project name.
- **Folders**: All source code is under `src/`, all tests under `tests/`.
- **Documentation**: Use markdown in `docs/` for architecture, use cases, and analysis.
- **CI/CD**: (If present) Place pipeline configs in `.github/workflows/`.

## Integration Points
- **External Dependencies**: Managed via NuGet; see `Directory.Packages.props` for versions.
- **Database**: (If used) Connection/configuration is handled in Infrastructure layer.
- **Doxygen**: For API docs, run Doxygen using the provided config.

## Examples
- To add a new domain entity: place it in `src/Domain/` and add related logic in `Core` and `Infrastructure` as needed.
- To add a new test: mirror the structure in `tests/` and use the same namespace as the code under test.
- Update `README.md` with any new setup or usage instructions.

---

For more details, see `README.md` and `docs/`.
