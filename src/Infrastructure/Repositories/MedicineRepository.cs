// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs;
using Core.Interfaces;
using Core.Interfaces.Repositories;

using Infrastructure.Database.Entities;
using Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;

/// <summary>
/// EF Core implementation of IMedicineRepository.
/// Uses AppDbContext to access the database.
/// </summary>

public class MedicineRepository : IMedicineRepository
{
    // EF Core database context used to query the database
    private readonly AppDbContext _context;


    //Constructor that injects the AppDbContext
    public MedicineRepository(AppDbContext context)
    {

        _context = context;
    }


    // Retrieves all medicine administration records for a specific resident
    /// <returns>
    /// MedicineStatusDto containing medicine names, timestamps, and given status
    /// </returns>

    public async Task<MedicineStatusDto> GetMedicineStatusAsync(Guid residentId, CancellationToken ct = default)
    {
        var medicines = await _context.MedicineAdministrationEntities
            .Where(m => m.ResidentId == residentId)
            .OrderBy(m => m.Timestamp)
            .ToListAsync(ct);

        return new MedicineStatusDto
        {
            ResidentId = residentId.ToString(),
            Medicine = medicines.Select(m => m.MedicineName).ToArray(),
            Timestamps = medicines.Select(m => m.Timestamp).ToArray(),
            Given = medicines.Select(m => m.Given).ToArray()
        };
    }


    //Retrieves painkiller administration data for the last 24 hours
    /// <returns>
    /// PainkillerStatusDto with types and next allowed time
    /// </returns>
    public async Task<PainkillerStatusDto> GetPainkillerStatusAsync(Guid residentId, CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;
        var pks = await _context.PainkillerAdministrationEntities
            .Where(p => p.ResidentId == residentId && p.GivenAt >= now.AddHours(-24))
            .OrderByDescending(p => p.GivenAt)
            .ToListAsync(ct);


        //calculates the next allowed time based on previous administrations
        return new PainkillerStatusDto
        {
            ResidentId = residentId,
            Types = pks.Select(p => p.Type).ToArray(),
            NextAllowedTime = pks.Any() ? pks.Max(p => p.NextAllowedTime) : now
        };
    }



}
