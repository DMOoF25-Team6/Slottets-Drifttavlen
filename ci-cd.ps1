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
  $containerName = "otherdmoof25-team6slottets-drifttavlen-slottets-sqlserver-1"
  $containerStatus = docker ps --filter "name=$containerName" --filter "status=running" --format "{{.Names}}"
  if ($containerStatus -eq $containerName) {
      # The container is running
  } else {
      # The container is NOT running
      docker-compose up --menu=false --build slottets-sqlserver
  }
}

Convert-LineEndingsToLF
Start-SqlServer

# Check if the SQL Server container is running
docker-compose up --menu=false --build build-stage
if ($LASTEXITCODE -ne 0) {
    # Not success: add your error handling commands here
    Write-Host "Build failed failed with exit code $LASTEXITCODE"
    exit $LASTEXITCODE
}

docker-compose up --menu=false --build tests-stage
if ($LASTEXITCODE -ne 0) {
    # Not success: add your error handling commands here
    Write-Host "Tests failed with exit code $LASTEXITCODE"
    exit $LASTEXITCODE
}

# If git is not in main branch, try to push; if on main, pull latest changes
$branch = git rev-parse --abbrev-ref HEAD
if ($branch -ne "main") {
    git push
} else {
    Write-Host "Are in main branch and can not push."
}
