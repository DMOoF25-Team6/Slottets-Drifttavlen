// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Domain.Entities;


namespace Core.Interfaces.Managers;

/// <summary>
/// Provides business logic operations for managing <see cref="Resident"/> entities.
/// </summary>
/// <remarks>
/// Implements the Clean Architecture pattern as a manager for resident domain entities.
/// Inherits CRUD operations from <see cref="ICRUD{Resident}"/>.
/// </remarks>
/// <seealso cref="ICRUD{Resident}"/>
public interface IResidentManager : ICRUD<Resident>
{
}
