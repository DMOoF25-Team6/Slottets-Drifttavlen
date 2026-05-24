// Copyright (c) 2026 Team6. All rights reserved.
// No warranty, explicit or implicit, provided.

using System.Net;
using System.Text;
using System.Text.Json;

using Moq;
using Moq.Protected;

namespace Infrastructure.Tests.Managers;

/// <summary>
/// Shared scaffolding for HTTP-backed manager tests: a mocked <see cref="HttpMessageHandler"/>
/// wired into a named "SlottetApi" client via a mocked <see cref="IHttpClientFactory"/>.
/// </summary>
public abstract class HttpManagerTestBase
{
    protected readonly Mock<HttpMessageHandler> HandlerMock;
    protected readonly Mock<IHttpClientFactory> FactoryMock;
    protected readonly HttpClient Client;

    protected HttpManagerTestBase()
    {
        HandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        Client = new HttpClient(HandlerMock.Object) { BaseAddress = new Uri("http://localhost/") };
        FactoryMock = new Mock<IHttpClientFactory>();
        _ = FactoryMock.Setup(f => f.CreateClient("SlottetApi")).Returns(Client);
    }

    protected static HttpResponseMessage Json<T>(HttpStatusCode status, T payload) =>
        new(status) { Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json") };

    protected static HttpResponseMessage RawJson(HttpStatusCode status, string json) =>
        new(status) { Content = new StringContent(json, Encoding.UTF8, "application/json") };

    protected static HttpResponseMessage Status(HttpStatusCode status) => new(status);

    protected void Setup(string urlContains, HttpResponseMessage response) =>
        _ = HandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains(urlContains)),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

    protected void VerifySent(string urlContains, Times times) =>
        HandlerMock.Protected().Verify(
            "SendAsync",
            times,
            ItExpr.Is<HttpRequestMessage>(req => req.RequestUri!.ToString().Contains(urlContains)),
            ItExpr.IsAny<CancellationToken>());
}
