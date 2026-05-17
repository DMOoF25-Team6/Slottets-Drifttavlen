// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using Domain.Entities;

namespace Core.Interfaces.Repositories;

/// <summary>
/// Defines a contract for repository operations specific to <see cref="SubjectAccessRequest"/> entities (UC-010).
/// </summary>
public interface ISubjectAccessRequestRepository : IRepository<SubjectAccessRequest>
{
}
