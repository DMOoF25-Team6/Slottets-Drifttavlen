using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository for employee data access.
/// </summary>
/// <summary>
/// Repository for employee data access.
/// </summary>
public class EmployeeRepository(AppDbContext context)
    : Repository<Employee>(context), IEmployeeRepository
{
}
