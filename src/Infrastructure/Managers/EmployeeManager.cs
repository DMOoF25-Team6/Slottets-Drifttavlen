using System.Net.Http.Json;

using Core.DTOs;
using Core.Interfaces.Managers;

namespace Infrastructure.Managers;

/// <summary>
/// Provides operations for retrieving employees through the API.
/// </summary>
public class EmployeeManager(IHttpClientFactory httpClientFactory)
    : HttpApiManagerBase(httpClientFactory, "SlottetApi"), IEmployeeManager
{
    /// <summary>
    /// Retrieves all employees.
    /// </summary>
    public async Task<IEnumerable<EmployeeDto>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        IEnumerable<EmployeeDto>? response =
            await HttpClient.GetFromJsonAsync<IEnumerable<EmployeeDto>>(
                "employees",
                cancellationToken);

        return response ?? [];
    }
}