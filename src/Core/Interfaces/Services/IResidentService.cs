// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Services;

/// <summary>
/// Defines a contract for managing residents in the system.
/// </summary>
/// <remarks>
/// Inherits CRUD operations from <see cref="ICRUD{Resident}"/>.
/// </remarks>
public interface IResidentService : ICRUD<Resident>
{
    /// <inheritdoc/>
}
