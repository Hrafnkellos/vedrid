using System.Net.Http.Json;

namespace Vedrid.IntegrationTests;

public static class Extensions
{
    public static async Task<T> GetAndDeserialize<T>(this HttpClient client, string requestUri)
    {
        var response = await client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();

        return await client.GetFromJsonAsync<T>(requestUri);
    }
}