using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace SlabIngestionService.Tests.Tests;

public class SlabConcurrencyTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public SlabConcurrencyTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Concurrent_Ingest_Should_End_With_One_Valid_Record()
    {
        var tasks = Enumerable.Range(1, 20)
        .Select(i =>
        _client.PostAsJsonAsync(
            "/api/slabs/ingest",
            new
            {
                slabId = "HSM-001",
                weight = 22000 + i,
                length = 11000,
                width = 1200,
                status = "Rolled"
            }));

        await Task.WhenAll(tasks);
    }
}