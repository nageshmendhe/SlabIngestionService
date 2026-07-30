using System.Net.Http.Json;

/// <summary>
/// Initializes a new instance of the <see cref=".$Program"/> class.
/// </summary>
HttpClient client = new HttpClient();

// Replace with your API URL
client.BaseAddress = new Uri("https://localhost:7120");

Console.WriteLine("===========================================");
Console.WriteLine("Starting Concurrency Test...");
Console.WriteLine("Sending 20 concurrent requests...");
Console.WriteLine("===========================================");

var requestTasks = Enumerable.Range(1, 20)
    .Select(i =>
    {
        Console.WriteLine($"Sending Request {i}...");

        return client.PostAsJsonAsync(
            "/api/Slabs/ingest",
            new
            {
                slabId = "HSM-2024-00142",
                weight = 22000 + i,
                length = 11000,
                width = 1200,
                status = "InProduction"
            });
    })
    .ToArray();

var responses = await Task.WhenAll(requestTasks);
var responseTasks = responses.Select(async (response, index) =>
{
    var body = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Request {index + 1} -> {response.StatusCode}");
    Console.WriteLine(body);
    Console.WriteLine(new string('-', 50));
});

await Task.WhenAll(responseTasks);

Console.WriteLine();
Console.WriteLine("===========================================");
Console.WriteLine("All requests completed.");
Console.WriteLine("===========================================");

Console.WriteLine();
Console.WriteLine("Fetching final slab...");

var responseMessage = await client.GetAsync("/api/Slabs/HSM-2024-00142");

if (responseMessage.IsSuccessStatusCode)
{
    var json = await responseMessage.Content.ReadAsStringAsync();

    Console.WriteLine();
    Console.WriteLine("Final Slab Data:");
    Console.WriteLine("-------------------------------------------");
    Console.WriteLine(json);
    Console.WriteLine("-------------------------------------------");
}
else
{
    Console.WriteLine($"Failed to fetch slab. Status Code: {responseMessage.StatusCode}");
}

Console.WriteLine();
Console.WriteLine("Concurrency Test Completed.");
Console.WriteLine("Press Enter to exit...");

Console.ReadLine();