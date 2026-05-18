# Pull the latest changes from the remote repository
git pull origin main

# Start Docker Compose with the 'prod' profile, build, and run detached
docker compose --profile prod up --build -d

# Wait for the database container to be healthy before running EF Core migrations
Write-Host "Waiting for database to be ready..."
$timeout = 60
$elapsed = 0
do {
    Start-Sleep -Seconds 2
    $elapsed += 2
    $status = docker compose --profile prod ps --format json 2>$null |
        ConvertFrom-Json |
        Where-Object { $_.Service -match "db|mysql|postgres|mssql|sqlserver" } |
        Select-Object -ExpandProperty Health -ErrorAction SilentlyContinue
} while ($status -ne "healthy" -and $elapsed -lt $timeout)

if ($elapsed -ge $timeout) {
    Write-Warning "Database did not become healthy within $timeout seconds. Proceeding anyway..."
} else {
    Write-Host "Database is ready."
}

# Drop the database using EF Core
dotnet ef database drop --project src/Infrastructure.Data --startup-project src/Api --force --no-build

# Update the database using EF Core
dotnet ef database update --project src/Infrastructure.Data --startup-project src/Api --no-build
