using System.Net;
using System.Text;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API_Tests.Test_Utilities;
using MyPennyPincher_API_Tests.WebApplicationFactory;
using Newtonsoft.Json;

namespace MyPennyPincher_API_Tests.Integration_Tests;

public class AuthControllerTest : IClassFixture<CustomWebApplicationFactory<Program>> 
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program>
        _factory;
    private const string BaseRoute= "/Auth";
    
    public AuthControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GIVEN_NewUser_WHEN_Registering_THEN_ReturnOkStatus()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = TestUtils.GenerateRandomEmail(),
            Password = "password"
        };

        var json = JsonConvert.SerializeObject(user);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync(BaseRoute + "/register", content);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GIVEN_ExistingUser_WHEN_AttemptingToRegister_THEN_ReturnConflictStatus()
    {
        //Arrange
        var randomEmail = TestUtils.GenerateRandomEmail();

        Console.WriteLine(randomEmail);
        Console.WriteLine(randomEmail);

        User user = new User
        {
            FullName = "Test User",
            Email = randomEmail,
            Password = "password"
        };

        var json = JsonConvert.SerializeObject(user);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var firstResponse = await TestUtils.RegisterTestUserAsync(_client, user);

        firstResponse.EnsureSuccessStatusCode();

        //Act
        var response = await TestUtils.RegisterTestUserAsync(_client, user);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task GIVEN_ExistingUser_WHEN_LoggingIn_THEN_ReturnLoginResponse()
    {
        //Arrange
        var randomEmail = TestUtils.GenerateRandomEmail();

        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = randomEmail,
            Password = "password"
        };

        var registeredUserResult = await TestUtils.RegisterTestUserAsync(_client, user);

        registeredUserResult.EnsureSuccessStatusCode();

        Login login = new Login
        {
            Email = randomEmail,
            Password = user.Password
        };

        var json = JsonConvert.SerializeObject(login);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync(BaseRoute + "/login", content);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseString);

        Assert.NotNull(loginResponse);
        Assert.Equal(user.UserId, loginResponse.UserId);
    }
}
