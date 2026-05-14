// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

namespace Core.Interfaces.Managers;

public interface IEmployeeManager
{
    Task<IEnumerable<EmployeeDto>> GetAllAsync(
       CancellationToken cancellationToken = default);
}
