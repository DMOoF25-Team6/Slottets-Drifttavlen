// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;

using System.Net.Http;

namespace Infrastructure.Repositories;

/// <summary>
/// Repository implementation for <see cref="Resident"/> entities.
/// </summary>
/// <remarks>
/// This class provides data access logic for <see cref="Resident"/> objects,
/// following the repository pattern as part of the Clean Architecture approach.
/// It inherits generic CRUD operations from <see cref="Repository{Resident}"/>
/// and implements <see cref="IResidentRepository"/> for domain-specific queries.
/// </remarks>



//repository fetches Resident data from the API
public class ResidentRepository : Repository<Resident>, IResidentRepository
{

    // readonly HttpClient is injected to allow for potential API calls or external data fetching related to residents.
    private readonly HttpClient _httpClient;
    

    // constructor to recive HttpClient  
    public ResidentRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    // Api refactoring for future use, currently not used.
    // string apiUrl to pass any API endpoint to fetch residents.
    public async Task<IEnumerable<Resident>> GetResidentsFromApiAsync(string apiUrl)
    {

        // GetAsync(apiUrl) sends a GET request to the URL.
        var response = await _httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        // Assuming the API returns a JSON array of residents, we would deserialize it here.
        // For example, using System.Text.Json:
        // return JsonSerializer.Deserialize<IEnumerable<Resident>>(content);
        return new List<Resident>(); // Placeholder return statement
    }


}
