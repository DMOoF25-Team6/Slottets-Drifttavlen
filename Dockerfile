################################################################################
# Base SDK image
################################################################################
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
#USER $APP_UID
WORKDIR /app
#EXPOSE 8080
#EXPOSE 8081
RUN ["dotnet", "tool", "install", "--global", "dotnet-ef"]
RUN ["dotnet", "dev-certs", "https", "--trust"]
ENV PATH="$PATH:/root/.dotnet/tools"

################################################################################
# This stage is used to build the service project
################################################################################
FROM base AS build-stage
SHELL ["/bin/bash"]
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY Slottet.CareManagement.sln ./
COPY Directory.Build.props ./
COPY Directory.Build.targets ./
COPY .env ./
COPY src/ ./src/
COPY tests/ ./tests/
RUN ["dotnet", "restore"]
RUN ["dotnet", "build", "-c", "Release" ]

################################################################################
# Tests stage: runtime container that executes tests when started
################################################################################
FROM base AS tests-stage
SHELL ["/bin/bash"]
WORKDIR /workspace
COPY --from=build-stage /src/ ./
COPY .runsettings ./.runsettings
COPY .env ./.env
COPY tools/docker/run_tests.sh /run_tests.sh
RUN ["dotnet", "tool", "install", "--global", "dotnet-reportgenerator-globaltool"]
RUN ["chmod", "+x", "/run_tests.sh"]

ENTRYPOINT ["/run_tests.sh"]

################################################################################
# publish stage: This stage is used to publish the service project to be copied to the final stage
################################################################################
FROM build-stage AS publish-stage
RUN ["dotnet", "publish", "src/WebUI/WebUI/WebUI.csproj", "-c", "Release", "-o", "/app/publish/WebUI", "/p:UseAppHost=false"]
RUN ["dotnet", "publish", "src/Api/Api.csproj", "-c", "Release", "-o", "/app/publish/WebApi", "/p:UseAppHost=false"]

################################################################################
# Web Api Final stage: This stage is used to create the final runtime container for the service project
################################################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS webapi-final-stage
WORKDIR /app
COPY --from=publish-stage /app/publish/WebApi ./
ENTRYPOINT ["dotnet", "/app/Api.dll"]

################################################################################
# Web UI Final stage: This stage is used to create the final runtime container for the service project
################################################################################
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS webui-final-stage
WORKDIR /app
COPY --from=publish-stage /app/publish/WebUI ./
ENTRYPOINT ["dotnet", "/app/WebUI.dll"]
