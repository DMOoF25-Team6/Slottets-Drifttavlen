// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Repositories;


/// <summary>
/// Core only knows about this interface.
/// EF Core or DbContext lives in Infrastructure.
/// </summary>
public interface IMedicineRepository
{
    //Task<MedicineStatusDto> GetMedicineStatusAsync(Guid residentId, CancellationToken cancellationToken = default);
    //Task<PainkillerStatusDto> GetPainkillerStatusAsync(Guid residentId, CancellationToken cancellationToken = default);
    Task<IEnumerable<MedicineRecord>> GetMedicineStatusLast24HoursAsync(Guid residentId, CancellationToken cancellationToken = default);
}
