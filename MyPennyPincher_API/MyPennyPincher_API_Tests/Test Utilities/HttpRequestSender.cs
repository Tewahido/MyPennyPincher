using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace MyPennyPincher_API_Tests.Test_Utilities;

public class HttpRequestSender
{

    public static async Task<HttpResponseMessage> PostAsync(HttpClient client,string route, object postContent)
    {

        var userJson = JsonConvert.SerializeObject(postContent);
        var content = new StringContent(userJson, Encoding.UTF8, "application/json");

        return await client.PostAsync(route, content);
    }

    public static async Task<HttpResponseMessage> PostWithCookiesAsync(HttpClient client, string route, object postContent, string cookie)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(route, UriKind.Relative),
            Content = new StringContent(JsonConvert.SerializeObject(postContent), Encoding.UTF8, "application/json")
        };

        request.Headers.Add("Cookie", cookie);

        return await client.SendAsync(request);
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
