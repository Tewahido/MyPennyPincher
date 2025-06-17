using System.Net;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API_Tests.Test_Utilities;
using MyPennyPincher_API_Tests.WebApplicationFactory;
using Newtonsoft.Json;

namespace MyPennyPincher_API_Tests.Integration_Tests;

public class IncomeControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
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
        User user = TestDataFactory.CreateTestUser();

        var userResponse = await HttpRequestSender.PostAsync(_client, AuthRoute + "/register", user);

        userResponse.EnsureSuccessStatusCode();

        Income income = TestDataFactory.CreateIncome(1, user);

        //Act
        var response = await HttpRequestSender.PostAsync(_client, BaseRoute, income);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GIVEN_ExistingIncome_WHEN_DeletingIncome_THEN_ReturnOkStatus()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        var userResponse = await HttpRequestSender.PostAsync(_client, AuthRoute + "/register", user);

        userResponse.EnsureSuccessStatusCode();

        Income income = TestDataFactory.CreateIncome(2, user);

        var addIncomeResponse = await HttpRequestSender.PostAsync(_client, BaseRoute, income);

        addIncomeResponse.EnsureSuccessStatusCode();

        //Act
        var response = await HttpRequestSender.DeleteAsync(_client, BaseRoute, income);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GIVEN_ExistingIncome_WHEN_EditingIncome_THEN_ReturnOkStatus()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        var userResponse = await HttpRequestSender.PostAsync(_client, AuthRoute + "/register", user);

        userResponse.EnsureSuccessStatusCode();

        Income income = TestDataFactory.CreateIncome(3, user);

        var addIncomeResponse = await HttpRequestSender.PostAsync(_client, BaseRoute, income);

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
        var response = await HttpRequestSender.PutAsync(_client, BaseRoute, editedIncome);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserExpenses_THEN_ReturnOkAndUserExpenses()
    {
        //Arrange
        User user = TestDataFactory.CreateAuthenticatedTestUser();

        var registerUserResponse = await HttpRequestSender.PostAsync(_client, AuthRoute + "/register", user);
        registerUserResponse.EnsureSuccessStatusCode();

        Login login = TestDataFactory.CreateUserLogin(user);

        var loginUserResponse = await HttpRequestSender.PostAsync(_client, AuthRoute + "/login", login);
        loginUserResponse.EnsureSuccessStatusCode();

        var responseString = await loginUserResponse.Content.ReadAsStringAsync();
        var loginResponse = JsonConvert.DeserializeObject<UserAccessToken>(responseString);

        var token = loginResponse?.Token;

        List<Income> userIncomes = new List<Income>();

        Income firstIncome = TestDataFactory.CreateIncome(4, user);
        userIncomes.Add(firstIncome);

        Income secondIncome = TestDataFactory.CreateIncome(5, user);
        userIncomes.Add(secondIncome);

        Income thirdIncome = TestDataFactory.CreateIncome(6, user);
        userIncomes.Add(thirdIncome);

        foreach (var income in userIncomes)
        {
            var incomeResponse = await HttpRequestSender.PostAsync(_client, BaseRoute, income);
            incomeResponse.EnsureSuccessStatusCode();
        }

        //Act
        var response = await HttpRequestSender.GetAsync(_client, BaseRoute, token!);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var incomes = JsonConvert.DeserializeObject<List<Income>>(json);

        //Assert

        Assert.NotNull(incomes);
        Assert.All(incomes, income => Assert.IsType<Income>(income));
        Assert.Equal(3, incomes.Count);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
