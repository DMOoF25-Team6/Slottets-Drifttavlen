// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Domain.Entities;


namespace Core.Interfaces.ApiClients;

public interface IResidentApiClient
{
    Task<Resident?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Resident>> GetAllAsync(CancellationToken cancellationToken = default);

}
