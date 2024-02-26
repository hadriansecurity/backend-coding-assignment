using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hadrian.CodingAssignment.Tests.Extensions;

internal static class HttpResponseMessageExtensions
{
    public static async Task<T> AsAsync<T>(this Task<HttpResponseMessage> task)
    {
        var response = await task;
        response.EnsureSuccessStatusCode();
        return await response.AsAsync<T>();
    }

    public static async Task<T> AsAsync<T>(this HttpResponseMessage response)
    {
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.Converters.Add(new JsonStringEnumConverter());
        return (await response.Content.ReadFromJsonAsync<T>(options))!;
    }
}
