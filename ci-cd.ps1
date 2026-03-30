# Check if the SQL Server container is running
$containerName = "otherdmoof25-team6slottets-drifttavlen-slottets-sqlserver-1"
$containerStatus = docker ps --filter "name=$containerName" --filter "status=running" --format "{{.Names}}"
if ($containerStatus -eq $containerName) {
    # The container is running
} else {
    # The container is NOT running
    docker-compose up --menu=false --build slottets-sqlserver
}

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
