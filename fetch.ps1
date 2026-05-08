# Fetch latest changes from git
git fetch

# Start Docker Compose with the 'prod' profile, build, and run detached
docker-compose --profile prod up --menu=false --build -d

# Drop the database using EF Core
dotnet ef database drop --project src/Infrastructure.Data --startup-project src/Api --force --no-build

# Update the database using EF Core
dotnet ef database update --project src/Infrastructure.Data --startup-project src/Api --no-build
