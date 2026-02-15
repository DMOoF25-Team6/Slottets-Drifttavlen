################################################################################
# Base SDK image
################################################################################
FROM tirsvad/tirsvadcli_debian13_nginx AS base
#USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
RUN ["apt-get", "update"]
RUN ["apt-get", "install", "-y", "aspnetcore-runtime-8.0" ]
RUN ["dotnet", "tool", "install", "--global", "dotnet-ef"]
ENV PATH="$PATH:/root/.dotnet/tools"

# This stage is used to build the service project
FROM base AS build-stage
SHELL ["/bin/bash"]
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY Slottet.CareManagement.slnx ./
COPY Directory.Build.props ./
COPY Directory.Build.targets ./
COPY Directory.Packages.props ./
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
WORKDIR /workspace
COPY tools/docker/run_tests.sh /run_tests.sh
RUN ["chmod", "+x", "/run_tests.sh"]

ENTRYPOINT ["/run_tests.sh"]

################################################################################
# publish stage: This stage is used to publish the service project to be copied to the final stage
################################################################################
FROM build-stage AS publish-stage
ARG BUILD_CONFIGURATION=Release
#RUN ["dotnet", "publish", "-c", "$BUILD_CONFIGURATION", "/p:UseAppHost=false"]
#dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

################################################################################
# Final stage: production or when running from VS in regular mode (Default when not using the Debug configuration)
################################################################################
FROM base AS final-stage
WORKDIR /app
COPY --from=publish-stage /app/publish .
ENTRYPOINT ["dotnet", "Slottet.CareManagement.dll"]
