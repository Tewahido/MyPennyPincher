using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class ExpenseServiceTest : IDisposable
{
    private readonly ExpenseService _expenseService;
    private readonly MyPennyPincherDbContext _context;

    public ExpenseServiceTest() 
    {
        var options = new DbContextOptionsBuilder<MyPennyPincherDbContext>()
           .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
           .Options;

        _context = new MyPennyPincherDbContext(options);

        _expenseService = new ExpenseService(_context);
    }

    [Fact]
    public async Task GIVEN_Expense_WHEN_AddignExpense_THEN_AddExpenseToDb() 
    {
        //Arrange
        Expense expense = new Expense
        {
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            UserId = Guid.NewGuid(),
        };

        //Act
        await _expenseService.AddExpense(expense);

        var expectedExpense = _context.Expenses.FirstOrDefaultAsync(exp => exp.ExpenseId == expense.ExpenseId);

        //Assert
        Assert.NotNull(expectedExpense);
    }

    [Fact]
    public async Task GIVEN_ExistingExpense_WHEN_DeletingExpense_THEN_RemoveExpenseFromDb()
    {
        //Arrange
        Expense expense = new Expense
        {
            ExpenseId = 1,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            UserId = Guid.NewGuid(),
        };

        await _expenseService.AddExpense(expense);

        //Act
        await _expenseService.DeleteExpense(expense);

        var expectedExpense = await _context.Expenses.FirstOrDefaultAsync(exp => exp.ExpenseId == expense.ExpenseId);

        //Assert
        Assert.Null(expectedExpense);

    }

    [Fact]
    public async Task GIVEN_ExistingExpense_WHEN_EditingExpense_THEN_EditOverwriteExistingExpense()
    {
        //Arrange
        Expense existingExpense = new Expense
        {
            ExpenseId = 2,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            UserId = Guid.NewGuid(),
        };

        await _expenseService.AddExpense(existingExpense);
        
        Expense editedExpense = new Expense
        {
            ExpenseId = existingExpense.ExpenseId,
            Description = existingExpense.Description,
            Amount = 500,
            Date = existingExpense.Date,
            Recurring = existingExpense.Recurring,
            UserId = existingExpense.UserId,
        };

        //Act
        await _expenseService.EditExpense(editedExpense);

        var expectedExpense = await _context.Expenses.FirstOrDefaultAsync(exp => exp.ExpenseId == editedExpense.ExpenseId);
       
        //Assert
        Assert.Equal(editedExpense.Amount, expectedExpense.Amount);
    }

    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserExpenses_THEN_ReturnUsersExpenses()
    {
        //Arrange
        Guid userId = Guid.NewGuid();

        Expense firstExpense = new Expense
        {

            ExpenseId = 1,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            UserId = userId,
        };

        Expense secondExpense = new Expense
        {

            ExpenseId = 2,
            Description = "Test2",
            Amount = 1400,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            UserId = userId,
        };

        Expense thirdExpense = new Expense
        {

            ExpenseId = 3,
            Description = "Test3",
            Amount = 10540,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            UserId = userId,
        };

        _expenseService.AddExpense(firstExpense);
        _expenseService.AddExpense(secondExpense);
        _expenseService.AddExpense(thirdExpense);


        //Act
        var expectedExpenses = await _expenseService.GetUserExpenses(userId.ToString());

        //Assert
        Assert.Equal(expectedExpenses.Count, 3);
        Assert.Contains(firstExpense, expectedExpenses);
        Assert.Contains(secondExpense, expectedExpenses);
        Assert.Contains(thirdExpense, expectedExpenses);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
