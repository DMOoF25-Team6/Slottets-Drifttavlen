// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

using Infrastructure.Data.Persistent;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for <see cref="Resident"/> entities.
/// </summary>
/// <remarks>
/// This class provides data access logic for <see cref="Resident"/> objects,
/// following the repository pattern as part of the Clean Architecture approach.
/// It inherits generic CRUD operations from <see cref="Repository{Resident}"/>
/// and implements <see cref="IResidentRepository"/> for domain-specific queries.
/// </remarks>

public class ResidentRepository(AppDbContext context) : Repository<Resident>(context), IResidentRepository
{
    /// <summary>
    /// Retrieves all residents belonging to a specific department.
    /// </summary>
    /// <param name="department">The department to filter residents by.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A list of residents in the specified department.</returns>
    public async Task<IEnumerable<Resident>> GetAllAsync(Department department, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(department);
        return await _dbSet.Where(r => r.Department == department).ToListAsync(cancellationToken);
    }
}
