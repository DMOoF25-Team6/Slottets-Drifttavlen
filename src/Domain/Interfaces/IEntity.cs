// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

namespace Domain.Interfaces;

/// <summary>
/// Represents a domain entity with a unique identifier.
/// <para>
/// This interface is used as a base contract for all domain entities in the Clean Architecture solution.
/// Implementing this interface ensures that each entity has a <see cref="Guid"/> <c>Id</c> property,
/// which is required for persistence and identification.
/// </para>
/// <example>
/// <code>
/// public class User : IEntity
/// {
///     public Guid Id { get; set; }
///     // Additional properties...
/// }
/// </code>
/// </example>
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    Guid Id { get; set; }
}
