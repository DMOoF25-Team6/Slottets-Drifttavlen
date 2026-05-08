// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Net.Http;

namespace Infrastructure.Managers;

/// <summary>
/// Base class for managers that communicate with backend APIs over HTTP.
/// Provides a protected <see cref="HttpClient"/> instance for derived classes.
/// </summary>
public abstract class HttpApiManagerBase
{
    /// <summary>
    /// The HTTP client used for API communication.
    /// </summary>
    protected readonly HttpClient HttpClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpApiManagerBase"/> class.
    /// </summary>
    /// <param name="httpClientFactory">The HTTP client factory to create named clients.</param>
    /// <param name="clientName">The name of the HTTP client to create.</param>
    /// <exception cref="InvalidOperationException">Thrown if the client cannot be created.</exception>
    protected HttpApiManagerBase(IHttpClientFactory httpClientFactory, string clientName)
    {
        ArgumentNullException.ThrowIfNull(httpClientFactory);
        ArgumentNullException.ThrowIfNull(clientName);
        HttpClient = httpClientFactory.CreateClient(clientName)
            ?? throw new InvalidOperationException($"Failed to create HttpClient for '{clientName}'.");
    }
}
