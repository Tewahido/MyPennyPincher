using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;

namespace MyPennyPincher_API_Tests.Test_Utilities;

public class TestDataFactory
{
    private static string GenerateRandomEmail()
    {
        var randomPrefix = Guid.NewGuid().ToString().Substring(0, 10);
        return $"{randomPrefix}@example.com";
    }

    public static User CreateTestUser()
    {
        var userId = Guid.NewGuid();
        var randomEmail = GenerateRandomEmail();

        return new User
        {
            UserId = userId,
            FullName = "Test User",
            Email = randomEmail,
            Password = "password"
        };
    }

    public static User CreateAuthenticatedTestUser()
    {
        var userId = new Guid();
        var randomEmail = GenerateRandomEmail();

        return new User
        {
            UserId = userId,
            FullName = "Test User",
            Email = randomEmail,
            Password = "password"
        };
    }

    public static Login CreateUserLogin(User user)
    {
        return new Login
        {
            Email = user.Email,
            Password = user.Password,
        };
    }

    public static Expense CreateExpense(int id, User user)
    {
        return new Expense
        {
            ExpenseId = id,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = user.UserId,
        };
    }

    public static Income CreateIncome(int id, User user)
    {
        return new Income
        {
            IncomeId = id,
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = user.UserId,
        };
    }
}
