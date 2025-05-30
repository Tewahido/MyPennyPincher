using System.Text;
using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
using Newtonsoft.Json;

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

    public static int GenerateRandomId()
    {
        var random = new Random();
        return random.Next(1, int.MaxValue);
    }
    public static async Task<HttpResponseMessage> RegisterTestUserAsync(HttpClient client, User user)
    {

        var userJson = JsonConvert.SerializeObject(user);
        var content = new StringContent(userJson, Encoding.UTF8, "application/json");

        return await client.PostAsync("Auth/register", content);
    }

}
