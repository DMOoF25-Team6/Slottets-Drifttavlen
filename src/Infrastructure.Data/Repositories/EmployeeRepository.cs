using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository for employee data access.
/// </summary>
public class EmployeeRepository(AppDbContext context)
    : IEmployeeRepository
{
    private readonly AppDbContext _context = context;

    /// <summary>
    /// Retrieves all employees.
    /// </summary>
    
    public async Task<IEnumerable<Employee>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Employees
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

}
