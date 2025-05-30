using System.Text;
using MyPennyPincher_API.Models;
using MyPennyPincher_API_Tests.Test_Utilities;
using MyPennyPincher_API_Tests.WebApplicationFactory;
using Newtonsoft.Json;

namespace MyPennyPincher_API_Tests.Integration_Tests;

public class IncomeControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program>
        _factory;
    private const string BaseRoute = "/Income";

    public IncomeControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GIVEN_NewIncome_WHEN_AddingIncome_THEN_ReturnOkStatus()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = TestUtils.GenerateRandomEmail(),
            Password = "password"
        };

        var userResponse = await TestUtils.RegisterTestUserAsync(_client, user);

        userResponse.EnsureSuccessStatusCode();

        Income income = new Income
        {
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        var json = JsonConvert.SerializeObject(income);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync(BaseRoute, content);

        //Assert
        response.EnsureSuccessStatusCode();
    }
}
