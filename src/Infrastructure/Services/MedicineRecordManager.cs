// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Managers;

using Domain.Entities;

namespace Infrastructure.Services;

public class MedicineRecordManager(HttpClient httpClient) : IMedicineRecordManager
{
    private readonly HttpClient _httpClient = httpClient;

    public Task<MedicineRecord> AddAsync(MedicineRecord entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MedicineRecord>> AddRangeAsync(IEnumerable<MedicineRecord> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(MedicineRecord entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<MedicineRecord> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<MedicineRecord>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<MedicineRecord?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(MedicineRecord entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<MedicineRecord> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
