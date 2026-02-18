#!/bin/bash

# ============================================================================
# run_tests.sh - Build, test, and generate coverage reports for .NET solution
# ============================================================================

set -euo pipefail

export NUGET_PACKAGES=/root/.nuget/packages

readonly framework="net8.0"
readonly timestamp="$(date -u +%Y%m%dT%H%M%SZ)"
readonly resultDir="/artifacts/TestResults/$timestamp"
readonly coverageDir="/artifacts/TestResults/CoverageReport/$timestamp"
readonly latestCoverageDir="/artifacts/TestResults/CoverageReport/Latest"

CONFIGURATION=${CONFIGURATION:-Development}
exit_code=0

cleanup() {
  # No temp files, but clean up if needed
  :
}
trap cleanup EXIT

init_dirs() {
  mkdir -p "${resultDir}"
  mkdir -p "${coverageDir}/Markdown" "${coverageDir}/Html" "${coverageDir}/Cobertura" "${coverageDir}/Badges"
  # clean up old results (keep last 5)
  ls -1dt /artifacts/TestResults/*/ | tail -n +6 | xargs rm -rf || true
  # Clean Latest coverage report
  rm -rf "${latestCoverageDir}"
}

build_solution() {
  echo "Restoring and building solution..."
  dotnet restore || exit_code=$?
  dotnet build -c Debug --no-restore || exit_code=$?
}

run_tests() {
  if [[ $exit_code -ne 0 ]]; then
    echo "Build failed with exit code $exit_code"
    exit $exit_code
  fi
  # Check if .runsettings exists before running tests
  if [[ ! -f .runsettings ]]; then
    echo ".runsettings file not found in $(pwd). Aborting tests."
    exit 1
  fi
  dotnet test -c Debug --results-directory "${resultDir}" --collect:"XPlat Code Coverage" --logger "trx" --settings .runsettings || exit_code=$?
  if [[ $exit_code -ne 0 ]]; then
    echo "Some tests failed with exit code $exit_code"
    exit $exit_code
  fi
}

generate_coverage_report() {
  echo "Combining coverage reports (results in: ${resultDir})..."
  if [[ -d "${resultDir}" ]]; then
    mapfile -t coverage_files < <(find "${resultDir}" -type f -name "coverage.cobertura.xml")
    if [[ "${#coverage_files[@]}" -gt 0 ]]; then
      echo "Found coverage files. Generating combined report..."
      rm -rf "${latestCoverageDir}"
      reportgenerator -reports:"${resultDir}/**/coverage.cobertura.xml" -targetdir:"${latestCoverageDir}" -reporttypes:"Cobertura" -title:"Slottests CareManagement - coverage report" || echo "reportgenerator failed"
      reportgenerator -reports:"${resultDir}/**/coverage.cobertura.xml" -targetdir:"${latestCoverageDir}/Html" -reporttypes:"Html_Dark" -title:"Slottests Care - coverage report" || echo "reportgenerator failed"
      reportgenerator -reports:"${resultDir}/**/coverage.cobertura.xml" -targetdir:"${latestCoverageDir}/Markdown" -reporttypes:"Markdown" -title:"Slottests Care - coverage report" || echo "reportgenerator failed"
      reportgenerator -reports:"${resultDir}/**/coverage.cobertura.xml" -targetdir:"${latestCoverageDir}/Badges" -reporttypes:"Badges" -title:"Slottests Care - coverage report" || echo "reportgenerator failed"
    else
      echo "No coverage files found in ${resultDir}"
    fi
  else
    echo "Test results directory ${resultDir} does not exist. No coverage files to process."
  fi
  echo "Test results and coverage reports are available in /artifacts/TestResults/"
}

main() {
  echo "Test results and coverage reports are available in /artifacts/TestResults/"
  # Ensure we're in workspace containing the solution/repo
  cd /workspace || exit 1
  # Ensure tests use Development environment
  export ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT:-Development}
  init_dirs
  build_solution
  #echo "Update databses for tests..."
  #dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/Infrastructure --configuration Debug || true
  #echo "Discovering test projects..."
  dotnet clean
  dotnet nuget locals all --clear
  run_tests
  generate_coverage_report
  exit $exit_code
}

main "$@"
