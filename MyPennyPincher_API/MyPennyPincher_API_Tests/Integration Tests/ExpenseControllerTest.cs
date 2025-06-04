using System.Net;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API_Tests.Test_Utilities;
using MyPennyPincher_API_Tests.WebApplicationFactory;
using Newtonsoft.Json;

namespace MyPennyPincher_API_Tests.Integration_Tests;

public class ExpenseControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>, IDisposable
{
    private readonly HttpClient _client;
    private const string BaseRoute = "/Expense";
    private const string AuthRoute = "/Auth";

    public ExpenseControllerTest(CustomWebApplicationFactory<Program> factory)
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

        Expense expense = TestDataFactory.CreateExpense(1, user);

        //Act
        var response = await HttpRequestSender.PostAsync(_client, BaseRoute, expense);

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

        Expense expense = TestDataFactory.CreateExpense(2, user);

        var addIncomeResponse = await HttpRequestSender.PostAsync(_client, BaseRoute, expense);
        addIncomeResponse.EnsureSuccessStatusCode();

        //Act
        var response = await HttpRequestSender.DeleteAsync(_client, BaseRoute, expense);

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

        Expense expense = TestDataFactory.CreateExpense(3, user);


        var addIncomeResponse = await HttpRequestSender.PostAsync(_client, BaseRoute, expense);
        addIncomeResponse.EnsureSuccessStatusCode();

        Expense editedExpense = new Expense
        {
            ExpenseId = 3,
            Description = "Test",
            Amount = 500,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };

        //Act
        var response = await HttpRequestSender.PutAsync(_client, BaseRoute, editedExpense);

        //Assert
        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserExpenses_THEN_ReturnUserExpenses()
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
        Assert.NotNull(token);

        List<Expense> userExpenses = new List<Expense>();

        Expense firstExpense = TestDataFactory.CreateExpense(4, user);
        userExpenses.Add(firstExpense);

        Expense secondExpense = TestDataFactory.CreateExpense(5, user);
        userExpenses.Add(secondExpense);

        Expense thirdExpense = TestDataFactory.CreateExpense(6, user);
        userExpenses.Add(thirdExpense);

        foreach(var expense in userExpenses)
        {
            var expenseResponse = await HttpRequestSender.PostAsync(_client, BaseRoute, expense);
            expenseResponse.EnsureSuccessStatusCode();
        }

        //Act
        var response = await HttpRequestSender.GetAsync(_client, BaseRoute, token);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var expenses = JsonConvert.DeserializeObject<List<Income>>(json);

        //Assert
        Assert.NotNull(expenses);
        Assert.All(expenses, income => Assert.IsType<Income>(income));
        Assert.Equal(3, expenses.Count);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}
