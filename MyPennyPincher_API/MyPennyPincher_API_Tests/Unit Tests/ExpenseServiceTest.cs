using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models.DataModels;
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
    private readonly User _testUser;

    public ExpenseServiceTest() 
    {
        _context = DbContextFactory.GenerateInMemoryDB();
        _expenseRepository = new ExpenseRepository(_context);
        _expenseService = new ExpenseService(_expenseRepository);
        _testUser = TestDataFactory.CreateTestUser();
    }

    [Fact]
    public async Task GIVEN_NewExpense_WHEN_AddignExpense_THEN_AddExpenseToDb() 
    {
        //Arrange
        Expense expense = TestDataFactory.CreateExpense(1, _testUser);

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
        Expense expense = TestDataFactory.CreateExpense(1, _testUser);

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
        Expense existingExpense = TestDataFactory.CreateExpense(1, _testUser);

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
        Assert.Equal(editedExpense.Amount, expectedExpense!.Amount);
    }

    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserExpenses_THEN_ReturnUsersExpenses()
    {
        //Arrange
        Expense firstExpense = TestDataFactory.CreateExpense(2, _testUser);
        await _expenseService.AddAsync(firstExpense);

        Expense secondExpense = TestDataFactory.CreateExpense(3, _testUser);
        await _expenseService.AddAsync(secondExpense);

        Expense thirdExpense = TestDataFactory.CreateExpense(4, _testUser);
        await _expenseService.AddAsync(thirdExpense);

        //Act
        var expectedExpenses = await _expenseService.GetByUserIdAsync(_testUser.UserId.ToString());

        //Assert
        Assert.Equal(3, expectedExpenses.Count);

        Assert.Contains(firstExpense, expectedExpenses);
        Assert.Contains(secondExpense, expectedExpenses);
        Assert.Contains(thirdExpense, expectedExpenses);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
