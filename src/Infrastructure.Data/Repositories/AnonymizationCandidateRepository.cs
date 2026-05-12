// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using Infrastructure.Data.Persistent;

namespace Infrastructure.Data.Repositories;

/// <summary>
/// Repository implementation for <see cref="AnonymizationCandidate"/> entities (UC-010).
/// </summary>
/// <remarks>
/// Provides EF Core data access for anonymization candidates pending Admin review.
/// </remarks>
public class AnonymizationCandidateRepository(AppDbContext context) : Repository<AnonymizationCandidate>(context), IAnonymizationCandidateRepository
{
}
