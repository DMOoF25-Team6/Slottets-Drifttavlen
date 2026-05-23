// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Core.DTOs;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Mappers;
using Domain.Entities;

namespace Core.Services;

public class MedicineDeliveryService : IMedicineDeliveryService
{
    private readonly IMedicineRepository _repository;

    public MedicineDeliveryService(IMedicineRepository repository)
    {
        ArgumentNullException.ThrowIfNull(repository);
        _repository = repository;
    }

    public async Task<MedicineDeliveryResponseDto> CreateAsync(MedicineDeliveryCreateRequestDto dto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        MedicineRecord record = MedicineDeliveryMapper.ToMedicineRecord(dto);
        MedicineRecord created = await _repository.CreateAsync(record, cancellationToken);
        return MedicineDeliveryMapper.ToResponseDto(created);
    }

    public async Task<bool> UpdateAsync(Guid id, MedicineDeliveryUpdateRequestDto dto, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(dto);
        MedicineRecord? existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        MedicineDeliveryMapper.ApplyUpdate(existing, dto);
        await _repository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        MedicineRecord? existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }
        await _repository.DeleteAsync(existing, cancellationToken);
        return true;
    }

    public async Task<IEnumerable<MedicineDeliveryResponseDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<MedicineRecord> records = await _repository.GetAllAsync(cancellationToken);
        return records.Select(MedicineDeliveryMapper.ToResponseDto);
    }

    public async Task<MedicineDeliveryResponseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        MedicineRecord? record = await _repository.GetByIdAsync(id, cancellationToken);
        return record is null ? null : MedicineDeliveryMapper.ToResponseDto(record);
    }
}
