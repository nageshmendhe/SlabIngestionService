using System.Net.Http.Json;

HttpClient client = new HttpClient();

// Replace with your API URL
client.BaseAddress = new Uri("https://localhost:7120");

Console.WriteLine("===========================================");
Console.WriteLine("Starting Concurrency Test...");
Console.WriteLine("Sending 20 concurrent requests...");
Console.WriteLine("===========================================");

var tasks = Enumerable.Range(1, 20)
    .Select(async i =>
    {
        Console.WriteLine($"Sending Request {i}...");

        var response = await client.PostAsJsonAsync(
            "/api/Slabs/ingest",
            new
            {
                slabId = "HSM-2024-00142",
                weight = 22000 + i,
                length = 11000,
                width = 1200,
                status = "Shipped"
            });

        var responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Request {i} -> {response.StatusCode}");
        Console.WriteLine($"Response: {responseBody}");
        Console.WriteLine(new string('-', 50));
    });

await Task.WhenAll(tasks);

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