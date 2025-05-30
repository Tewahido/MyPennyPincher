using System.Net;
using Microsoft.AspNetCore.Components;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API_Tests.Test_Utilities;
using MyPennyPincher_API_Tests.WebApplicationFactory;
using Newtonsoft.Json;

namespace MyPennyPincher_API_Tests.Integration_Tests;

public class IncomeControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private const string BaseRoute = "/Income";
    private const string AuthRoute = "/Auth";

    public IncomeControllerTest(CustomWebApplicationFactory<Program> factory)
    {
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

        var userResponse = await TestUtils.PostAsync(_client, AuthRoute + "/register", user);

        userResponse.EnsureSuccessStatusCode();

        Income income = new Income
        {
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        //Act
        var response = await TestUtils.PostAsync(_client, BaseRoute, income);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GIVEN_ExistingIncome_WHEN_DeletingIncome_THEN_ReturnOkStatus()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = TestUtils.GenerateRandomEmail(),
            Password = "password"
        };

        var userResponse = await TestUtils.PostAsync(_client, AuthRoute + "/register", user);

        userResponse.EnsureSuccessStatusCode();

        Income income = new Income
        {
            IncomeId = 2,
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        var addIncomeResponse = await TestUtils.PostAsync(_client, BaseRoute, income);

        addIncomeResponse.EnsureSuccessStatusCode();

        //Act
        var response = await TestUtils.DeleteAsync(_client, BaseRoute, income);

        //Assert
        response.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task GIVEN_ExistingIncome_WHEN_EditingIncome_THEN_ReturnOkStatus()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = TestUtils.GenerateRandomEmail(),
            Password = "password"
        };

        var userResponse = await TestUtils.PostAsync(_client, AuthRoute + "/register", user);

        userResponse.EnsureSuccessStatusCode();

        Income income = new Income
        {
            IncomeId = 3,
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        var addIncomeResponse = await TestUtils.PostAsync(_client, BaseRoute, income);

        addIncomeResponse.EnsureSuccessStatusCode();

        Income editedIncome = new Income
        {
            IncomeId = 3,
            Source = "Test",
            Amount = 500,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        //Act
        var response = await TestUtils.PutAsync(_client, BaseRoute, editedIncome);

        //Assert
        response.EnsureSuccessStatusCode();
    }


    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserExpenses_THEN_ReturnOkAndUserExpenses()
    {
        //Arrange
        var randomEmail = TestUtils.GenerateRandomEmail();
        var userId = new Guid();

        User user = new User
        {
            UserId = userId,
            FullName = "Test User",
            Email = randomEmail,
            Password = "password"
        };

        var registerUserResponse = await TestUtils.PostAsync(_client, AuthRoute + "/register", user);
        registerUserResponse.EnsureSuccessStatusCode();

        Login login = new Login
        {
            Email = randomEmail,
            Password = user.Password
        };

        var loginUserResponse = await TestUtils.PostAsync(_client, AuthRoute + "/login", login);
        loginUserResponse.EnsureSuccessStatusCode();

        var responseString = await loginUserResponse.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseString);

        var token = loginResponse?.Token;

        Console.WriteLine(token);

        Income firstIncome = new Income
        {
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        Income secondIncome = new Income
        {
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        Income thirdIncome = new Income
        {
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };

        var firstResponse = await TestUtils.PostAsync(_client, BaseRoute, firstIncome);
        firstResponse.EnsureSuccessStatusCode();

        var secondResponse = await TestUtils.PostAsync(_client, BaseRoute, secondIncome);
        secondResponse.EnsureSuccessStatusCode();

        var thirdResponse = await TestUtils.PostAsync(_client, BaseRoute, thirdIncome);
        thirdResponse.EnsureSuccessStatusCode();

        //Act
        var response = await TestUtils.GetAsync(_client, BaseRoute, token);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var incomes = JsonConvert.DeserializeObject<List<Income>>(json);

        //Assert

        Assert.NotNull(incomes);
        Assert.All(incomes, income => Assert.IsType<Income>(income));
        Assert.Equal(incomes.Count, 3);
    }
}
