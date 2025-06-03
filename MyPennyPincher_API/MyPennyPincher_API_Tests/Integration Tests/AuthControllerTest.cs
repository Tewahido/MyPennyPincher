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
    private const string BaseRoute= "/Auth";
    
    public AuthControllerTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GIVEN_NewUser_WHEN_Registering_THEN_ReturnOkStatus()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        //Act
        var response = await HttpRequestSender.PostAsync(_client, BaseRoute + "/register", user);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GIVEN_ExistingUser_WHEN_AttemptingToRegister_THEN_ReturnConflictStatus()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        var firstResponse = await HttpRequestSender.PostAsync(_client, BaseRoute + "/register",  user);
        firstResponse.EnsureSuccessStatusCode();

        //Act
        var response = await HttpRequestSender.PostAsync(_client, BaseRoute + "/register", user);

        //Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
    }

    [Fact]
    public async Task GIVEN_ValidLoginDetails_WHEN_LoggingIn_THEN_ReturnLoginResponse()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        var registeredUserResult = await HttpRequestSender.PostAsync(_client, BaseRoute + "/register", user);
        registeredUserResult.EnsureSuccessStatusCode();

        Login login = TestDataFactory.CreateUserLogin(user);

        //Act
        var response = await HttpRequestSender.PostAsync(_client, BaseRoute + "/login", login);

        //Assert
        response.EnsureSuccessStatusCode();

        var responseString = await response.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<UserAccessToken>(responseString);

        Assert.NotNull(loginResponse);
        Assert.Equal(user.UserId, loginResponse.UserId);
    }

    [Fact]
    public async Task GIVEN_InvalidLoginDetails_WHEN_AttemptingToLogin_THEN_ReturnUnauthorized()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        var registeredUserResult = await HttpRequestSender.PostAsync(_client, BaseRoute + "/register", user);

        registeredUserResult.EnsureSuccessStatusCode();

        Login login = new Login
        {
            Email = user.Email,
            Password = "incorrectPassword"
        };

        //Act
        var response = await HttpRequestSender.PostAsync(_client, BaseRoute + "/login", login);

        //Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task GIVEN_UserIdAndValidRefreshTokenCookie_WHEN_RefreshingToken_THEN_ReturnNewAccessToken()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        var registeredUserResult = await HttpRequestSender.PostAsync(_client, BaseRoute + "/register", user);
        registeredUserResult.EnsureSuccessStatusCode();

        Login login = TestDataFactory.CreateUserLogin(user);

        var response = await HttpRequestSender.PostAsync(_client, BaseRoute + "/login", login);
        response.EnsureSuccessStatusCode();

        var cookies = response.Headers.GetValues("Set-Cookie");
        Assert.NotNull(cookies);

        var refreshTokenCookie = cookies.FirstOrDefault(c => c.StartsWith("refreshToken="));
        Assert.NotNull(refreshTokenCookie);

        //Act
        var refreshTokenResponse = await HttpRequestSender.PostWithCookiesAsync(_client, BaseRoute + "/refresh", user.UserId.ToString(), refreshTokenCookie);

        //Assert
        refreshTokenResponse.EnsureSuccessStatusCode();

        var responseString = await refreshTokenResponse.Content.ReadAsStringAsync();
        var newAccessToken = JsonConvert.DeserializeObject<UserAccessToken>(responseString);

        Assert.NotNull(newAccessToken);
        Assert.Equal(user.UserId, newAccessToken.UserId);
    }
}
