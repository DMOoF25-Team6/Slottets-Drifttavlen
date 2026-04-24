<#
  .SYNOPSIS
   CI/CD automation script for the Slottets Drifttavlen project.
  .DESCRIPTION
   This script automates the build, test, and deployment process using Docker and PowerShell.
   It also ensures all text files use LF line endings before running CI/CD steps.
   .NOTES
   Author: Team 6
   Project: DMOoF25-Team6.Slottets-Drifttavlen
   Last updated: 2024-06
   For more information, see the project documentation.
 #>


<#
 # .SYNOPSIS
 #   Converts all text file line endings in the repository to LF (Unix style).
 # .DESCRIPTION
 #   Recursively scans all files (excluding .git directory) and replaces CRLF or CR line endings with LF.
 #>
function Convert-LineEndingsToLF {
    $gitFiles = git ls-files
    foreach ($relativePath in $gitFiles) {
        $fullPath = Join-Path -Path (Get-Location) -ChildPath $relativePath
        if (Test-Path $fullPath) {
            $lines = Get-Content $fullPath
            $lfContent = ($lines -join "`n") + "`n"
            $utf8NoBom = New-Object System.Text.UTF8Encoding($false)
            [System.IO.File]::WriteAllText($fullPath, $lfContent, $utf8NoBom)
        }
    }
    Write-Host "All git-tracked file line endings converted to LF."
}

<#
 # .SYNOPSIS
 #   Starts the SQL Server container if it is not already running.
 # .DESCRIPTION
 #   Checks the status of the specified SQL Server container and starts it using Docker Compose if it
#>
function Start-SqlServer {
  $containerName = "otherdmoof25-team6slottets-drifttavlen"
  $containerStatus = docker ps --filter "name=$containerName" --filter "status=running" --format "{{.Names}}"
  if ($containerStatus -eq $containerName) {
      # The container is running
  } else {
      # The container is NOT running
      docker-compose up --menu=false --build slottets-sqlserver
  }
}

<#
 # .SYNOPSIS
 #   Ensures that the CI/CD pipeline is not running on the main branch.
 # .DESCRIPTION
 #   Checks the current Git branch and exits with an error if it is "main" to prevent unintended deployments.
 #>
function Ensure-NotOnMainBranch {
    $branch = git rev-parse --abbrev-ref HEAD
    if ($branch -eq "main") {
        Write-Host "CI/CD should not run on the main branch. Exiting."
        exit 1
    }
}

<#
 # .SYNOPSIS
 #   Ensures there are no uncommitted changes in the repository.
 # .DESCRIPTION
 #   Checks for uncommitted changes and exits with an error if any are found.
 #>
function Ensure-NoUncommittedChanges {
    $gitStatus = git status --porcelain
    if ($gitStatus) {
        Write-Host "There are uncommitted changes. Please commit or stash them before running CI/CD."
        exit 1
    }
}

<#
 # .SYNOPSIS
 #   Commits line ending fixes if there are any changes after conversion.
 # .DESCRIPTION
 #   Checks for changes and commits them with a standard message if present.
 #>
function Commit-LineEndingFixIfNeeded {
    $gitStatus = git status --porcelain
    if ($gitStatus) {
        git add .
        git commit -m "fix line endings"
    }
}

<#
 # .SYNOPSIS
 #   Runs the build stage using Docker Compose and handles errors.
 # .DESCRIPTION
 #   Executes the build-stage target and exits if the build fails.
 #>
function Run-BuildStage {
    docker-compose up --menu=false --build build-stage
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Build failed with exit code $LASTEXITCODE"
        exit $LASTEXITCODE
    }
}

<#
 # .SYNOPSIS
 #   Runs the tests stage using Docker Compose and handles errors.
 # .DESCRIPTION
 #   Executes the tests-stage target and exits if the tests fail.
 #>
function Run-TestsStage {
    docker-compose up --menu=false --build tests-stage
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Tests failed with exit code $LASTEXITCODE"
        exit $LASTEXITCODE
    }
}

Ensure-NotOnMainBranch
Ensure-NoUncommittedChanges
Convert-LineEndingsToLF
Start-SqlServer
Commit-LineEndingFixIfNeeded
Run-BuildStage
Run-TestsStage
git push
