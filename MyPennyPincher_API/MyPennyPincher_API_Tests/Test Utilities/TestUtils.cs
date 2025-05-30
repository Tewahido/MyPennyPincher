using System.Net.Http.Headers;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using MyPennyPincher_API.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyPennyPincher_API_Tests.Test_Utilities;

public class TestUtils
{
    public static MyPennyPincherDbContext GenerateInMemoryDB()
    {
        var options = new DbContextOptionsBuilder<MyPennyPincherDbContext>()
       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
        .Options;

        return new MyPennyPincherDbContext(options);
    }

    public static string GenerateRandomEmail()
    {
        var randomPrefix = Guid.NewGuid().ToString().Substring(0, 10);
        return $"{randomPrefix}@example.com";
    }

    public static async Task<HttpResponseMessage> PostAsync(HttpClient client,string route, object postContent)
    {

        var userJson = JsonConvert.SerializeObject(postContent);
        var content = new StringContent(userJson, Encoding.UTF8, "application/json");

        return await client.PostAsync(route, content);
    }

    public static async Task<HttpResponseMessage> DeleteAsync(HttpClient client, string route , object deleteContent)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(route, UriKind.Relative),
            Content = new StringContent(JsonConvert.SerializeObject(deleteContent), Encoding.UTF8, "application/json")
        };

        return await client.SendAsync(request);
    }

    public static async Task<HttpResponseMessage> PutAsync(HttpClient client, string route, object putContent)
    {

        var userJson = JsonConvert.SerializeObject(putContent);
        var content = new StringContent(userJson, Encoding.UTF8, "application/json");

        return await client.PutAsync(route, content);
    }

    public static async Task<HttpResponseMessage> GetAsync(HttpClient client, string route, string token)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(route, UriKind.Relative)
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await client.SendAsync(request);
    }

}
