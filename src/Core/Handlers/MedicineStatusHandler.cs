// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Core.DTOs;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces.Repositories;

namespace Core.Handlers;

/// <summary>
/// Handles retrieval of medicine and painkiller status for a resident.
/// This class acts as a thin abstraction layer over <see cref="IMedicineRepository"/>.
/// </summary>
/// <remarks>
/// This handler does not contain business logic. It simply forwards calls to the repository.
/// It exists to decouple higher-level services from direct repository access.
/// </remarks>

public class MedicineStatusHandler
{

    //Repository used to fetch medicine-related data
    private readonly IMedicineRepository _repository;

    //Repository implementation used to retrieve medicine and painkiller status data
    public MedicineStatusHandler(IMedicineRepository repository)
    {
           _repository = repository;
    }


    /// Retrieves the medicine status for a specific resident by delegating to the repository.
    /// A task that represents the asynchronous operation. 
    /// The task result contains the <see cref="MedicineStatusDto"/> for the resident.
    /// </returns>
    public Task<MedicineStatusDto> GetMedicineStatusAsync(Guid residentId, CancellationToken cancellationToken = default) =>
    
         _repository.GetMedicineStatusAsync(residentId, cancellationToken);


    
    // Retrieves the painkiller status for a specific resident.
    /// A task that represents the asynchronous operation. 
    /// The task result contains the <see cref="PainkillerStatusDto"/> for the resident
    public Task<PainkillerStatusDto> GetPainkillerStatusAsync(Guid residentId, CancellationToken cancellationToken = default) =>

        _repository.GetPainkillerStatusAsync(residentId, cancellationToken);



}
