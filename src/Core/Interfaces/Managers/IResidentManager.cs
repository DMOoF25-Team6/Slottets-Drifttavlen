// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.DTOs;

using Domain.Entities;
using Domain.Enums;

namespace Core.Interfaces.Managers;

/// <summary>
/// Defines operations for managing <see cref="Resident"/> entities.
/// </summary>
/// <remarks>
/// This interface abstracts the management of resident records, including creation, retrieval, update, and deletion.
/// </remarks>
/// <seealso cref="Resident"/>
/// <seealso cref="ResidentCreateRequestDto"/>
/// <seealso cref="ResidentUpdateRequestDto"/>
/// <seealso cref="ResidentResponseDto"/>
public interface IResidentManager
{
    /// <summary>
    /// Creates a new resident entity asynchronously.
    /// </summary>
    /// <param name="dto">An object containing the data for the resident to create.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Throws an exception if <paramref name="dto"/> is <see langword="null"/>.
    /// </remarks>
    /// <example>
    /// <code language="csharp">
    /// await residentManager.CreateAsync(new ResidentCreateRequestDto { ... }, cancellationToken);
    /// </code>
    /// </example>
    Task CreateAsync(ResidentCreateRequestDto dto, CancellationToken ct = default);

    /// <summary>
    /// Creates multiple resident entities asynchronously.
    /// </summary>
    /// <param name="dtos">A collection containing the data for the residents to create.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Throws an exception if <paramref name="dtos"/> is <see langword="null"/>.
    /// </remarks>
    Task CreateRangeAsync(IEnumerable<ResidentCreateRequestDto> dtos, CancellationToken ct = default);

    /// <summary>
    /// Deletes a resident entity by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">A unique identifier for the resident to delete.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Deletes multiple resident entities by their unique identifiers asynchronously.
    /// </summary>
    /// <param name="ids">A collection of unique identifiers for the residents to delete.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DeleteRangeAsync(IEnumerable<Guid> ids, CancellationToken ct = default);

    /// <summary>
    /// Retrieves all residents asynchronously.
    /// </summary>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains a collection of resident response DTOs.</returns>
    Task<IEnumerable<ResidentResponseDto>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Retrieves a resident by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">A unique identifier for the resident to retrieve.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the resident response DTO if found; otherwise, <see langword="null"/>.</returns>
    Task<ResidentResponseDto?> GetByIdAsync(Guid id, CancellationToken ct = default);

    /// <summary>
    /// Asynchronously retrieves a collection of residents associated with the specified departments.
    /// </summary>
    /// <param name="departments">A list of departments for which to retrieve the associated residents. Cannot be null.</param>
    /// <param name="ct">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains an enumerable collection of resident
    /// response DTOs corresponding to the specified departments.</returns>
    Task<IEnumerable<ResidentResponseDto>> GetByDepartmentsAsync(IList<Department> departments, CancellationToken ct = default);

    /// <summary>
    /// Updates an existing resident entity asynchronously.
    /// </summary>
    /// <param name="id">A unique identifier for the resident to update.</param>
    /// <param name="dto">An object containing the updated values for the resident.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Throws an exception if <paramref name="dto"/> is <see langword="null"/>.
    /// </remarks>
    Task UpdateAsync(Guid id, ResidentUpdateRequestDto dto, CancellationToken ct = default);

    /// <summary>
    /// Updates multiple resident entities asynchronously.
    /// </summary>
    /// <param name="dtos">A collection containing the updated values for the residents.</param>
    /// <param name="ct">A cancellation token to observe while waiting for the operation to complete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <remarks>
    /// Throws an exception if <paramref name="dtos"/> is <see langword="null"/>.
    /// </remarks>
    Task UpdateRangeAsync(IEnumerable<ResidentUpdateRequestDto> dtos, CancellationToken ct = default);
}
