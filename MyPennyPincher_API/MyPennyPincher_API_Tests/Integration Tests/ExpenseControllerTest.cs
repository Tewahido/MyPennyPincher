using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API_Tests.Test_Utilities;
using MyPennyPincher_API_Tests.WebApplicationFactory;
using Newtonsoft.Json;

namespace MyPennyPincher_API_Tests.Integration_Tests;

public class ExpenseControllerTest : IClassFixture<CustomWebApplicationFactory<Program>>
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
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = TestUtils.GenerateRandomEmail(),
            Password = "password"
        };

        var userResponse = await TestUtils.PostAsync(_client, AuthRoute + "/register", user);
        userResponse.EnsureSuccessStatusCode();

        Expense expense = new Expense
        {
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };

        //Act
        var response = await TestUtils.PostAsync(_client, BaseRoute, expense);

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

        Expense expense = new Expense
        {
            ExpenseId = 2,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };

        var addIncomeResponse = await TestUtils.PostAsync(_client, BaseRoute, expense);
        addIncomeResponse.EnsureSuccessStatusCode();

        //Act
        var response = await TestUtils.DeleteAsync(_client, BaseRoute, expense);

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

        Expense expense = new Expense
        {
            ExpenseId = 3,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };

        var addIncomeResponse = await TestUtils.PostAsync(_client, BaseRoute, expense);
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
        var response = await TestUtils.PutAsync(_client, BaseRoute, editedExpense);

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

        Expense firstExpense = new Expense
        {
            ExpenseId = 4,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };

        Expense secondExpense = new Expense
        {
            ExpenseId = 5,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };

        Expense thirdExpense = new Expense
        {
            ExpenseId = 6,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };

        var firstResponse = await TestUtils.PostAsync(_client, BaseRoute, firstExpense);
        firstResponse.EnsureSuccessStatusCode();

        var secondResponse = await TestUtils.PostAsync(_client, BaseRoute, secondExpense);
        secondResponse.EnsureSuccessStatusCode();

        var thirdResponse = await TestUtils.PostAsync(_client, BaseRoute, thirdExpense);
        thirdResponse.EnsureSuccessStatusCode();

        //Act
        var response = await TestUtils.GetAsync(_client, BaseRoute, token);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var expenses = JsonConvert.DeserializeObject<List<Income>>(json);

        //Assert

        Assert.NotNull(expenses);
        Assert.All(expenses, income => Assert.IsType<Income>(income));
        Assert.Equal(expenses.Count, 3);
    }

}
