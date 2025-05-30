using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Builder;
using MyPennyPincher_API.Models;
using MyPennyPincher_API_Tests.Test_Utilities;
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
    public async Task GIVEN_NewUser_WHEN_Registering_THEN_ReturnStatusOk()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = "test@email.com",
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
    public async Task GIVEN_ExistingUser_WHEN_LoggingIn_THEN_ReturnLoginResponse()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = "test@email.com",
            Password = "password"
        };

        var registrationJson = JsonConvert.SerializeObject(user);
        var registrationContent = new StringContent(registrationJson, Encoding.UTF8, "application/json");

        var registeredUserResult = await _client.PostAsync(BaseRoute + "/register", registrationContent);

        Login login = new Login
        {
            Email = user.Email,
            Password = user.Password
        };

        var json = JsonConvert.SerializeObject(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        //Act
        var response = await _client.PostAsync(BaseRoute + "/login", content);

        //Assert
        response.EnsureSuccessStatusCode();
    }
}
