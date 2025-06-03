using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.Test_Utilities;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class ExpenseServiceTest : IDisposable
{
    private readonly IExpenseService _expenseService;
    private readonly IExpenseRepository _expenseRepository;
    private readonly MyPennyPincherDbContext _context;

    public ExpenseServiceTest() 
    {
        _context = DbContextUtils.GenerateInMemoryDB();
        _expenseRepository = new ExpenseRepository(_context);
        _expenseService = new ExpenseService(_expenseRepository);
    }

    [Fact]
    public async Task GIVEN_NewExpense_WHEN_AddignExpense_THEN_AddExpenseToDb() 
    {
        //Arrange
        Expense expense = new Expense
        {
            ExpenseId=1,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = Guid.NewGuid(),
        };

        //Act
        await _expenseService.AddAsync(expense);

        var expectedExpense = _context.Expenses.FirstOrDefaultAsync(exp => exp.ExpenseId == expense.ExpenseId);

        //Assert
        Assert.NotNull(expectedExpense);
    }

    [Fact]
    public async Task GIVEN_ExistingExpense_WHEN_DeletingExpense_THEN_DeleteExpenseFromDb()
    {
        //Arrange
        Expense expense = new Expense
        {
            ExpenseId = 1,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = Guid.NewGuid(),
        };

        await _expenseService.AddAsync(expense);

        //Act
        await _expenseService.DeleteAsync(expense);

        var expectedExpense = await _context.Expenses.FirstOrDefaultAsync(exp => exp.ExpenseId == expense.ExpenseId);

        //Assert
        Assert.Null(expectedExpense);

    }

    [Fact]
    public async Task GIVEN_ExistingExpense_WHEN_EditingExpense_THEN_OverwriteExistingExpense()
    {
        //Arrange
        Expense existingExpense = new Expense
        {
            ExpenseId = 2,
            Description = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 3,
            UserId = Guid.NewGuid(),
        };

        await _expenseService.AddAsync(existingExpense);
        
        Expense editedExpense = new Expense
        {
            ExpenseId = existingExpense.ExpenseId,
            Description = existingExpense.Description,
            Amount = 500,
            Date = existingExpense.Date,
            Recurring = existingExpense.Recurring,
            ExpenseCategoryId = existingExpense.ExpenseCategoryId,
            UserId = existingExpense.UserId,
        };

        //Act
        await _expenseService.EditAsync(editedExpense);

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
            ExpenseCategoryId = 3,
            UserId = userId,
        };

        Expense secondExpense = new Expense
        {

            ExpenseId = 2,
            Description = "Test2",
            Amount = 1400,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 4,
            UserId = userId,
        };

        Expense thirdExpense = new Expense
        {

            ExpenseId = 3,
            Description = "Test3",
            Amount = 10540,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Recurring = false,
            ExpenseCategoryId = 5,
            UserId = userId,
        };

        _expenseService.AddAsync(firstExpense);
        _expenseService.AddAsync(secondExpense);
        _expenseService.AddAsync(thirdExpense);


        //Act
        var expectedExpenses = await _expenseService.GetByUserIdAsync(userId.ToString());

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
