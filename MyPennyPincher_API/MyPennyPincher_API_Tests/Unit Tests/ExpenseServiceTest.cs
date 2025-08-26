using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.QueryParameters;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.Test_Utilities;
using NSubstitute;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class ExpenseServiceTest
{
    private readonly IExpenseService _expenseService;
    private readonly IExpenseRepository _expenseRepository;
    private readonly User _testUser;

    public ExpenseServiceTest() 
    {
        _expenseRepository = Substitute.For<IExpenseRepository>();
        _expenseService = new ExpenseService(_expenseRepository);
        _testUser = TestDataFactory.CreateTestUser();
    }

    [Fact]
    public async Task GIVEN_NonExistentExpense_WHEN_EditingExpense_THEN_ThrowExpenseNotFoundException()
    {
        //Arrange
        var existingExpense = TestDataFactory.CreateExpense(1, _testUser);

        await _expenseService.AddAsync(existingExpense);
        
        var editedExpense = new Expense
        {
            ExpenseId = existingExpense.ExpenseId,
            Description = existingExpense.Description,
            Amount = 500,
            Date = existingExpense.Date,
            Recurring = existingExpense.Recurring,
            ExpenseCategoryId = existingExpense.ExpenseCategoryId,
            UserId = existingExpense.UserId,
        };

        _expenseRepository.GetByIdAsync(existingExpense.ExpenseId)
                .Returns(Task.FromResult<Expense?>(null));

        //Act & Assert
        await Assert.ThrowsAsync<ExpenseNotFoundException>(() => _expenseService.EditAsync(existingExpense));
    }

    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserExpenses_THEN_ReturnUsersExpenses()
    {
        //Arrange
        var queryParams = new TransactionQueryParams();

        var firstExpense = TestDataFactory.CreateExpense(2, _testUser);
        await _expenseService.AddAsync(firstExpense);

        var secondExpense = TestDataFactory.CreateExpense(3, _testUser);
        await _expenseService.AddAsync(secondExpense);

        var thirdExpense = TestDataFactory.CreateExpense(4, _testUser);
        await _expenseService.AddAsync(thirdExpense);

        _expenseRepository.GetUserMonthlyExpenses(_testUser.UserId.ToString(), queryParams.PeriodStart, queryParams.PeriodEnd)
                .Returns(new List<Expense> { firstExpense, secondExpense, thirdExpense });

        //Act
        var expectedExpenseResponse = await _expenseService.GetUserMonthlyExpenses(_testUser.UserId.ToString(), queryParams);

        //Assert
        Assert.Equal(3, expectedExpenseResponse.Count);

        Assert.Contains(firstExpense, expectedExpenseResponse.Data);
        Assert.Contains(secondExpense, expectedExpenseResponse.Data);
        Assert.Contains(thirdExpense, expectedExpenseResponse.Data);
    }

    [Fact]
    public async Task GIVEN_NoUserExpenses_WHEN_GettingUserExpenses_THEN_ReturnEmptyExpenseResponse()
    {
        //Arrange
        var queryParams = new TransactionQueryParams();

        _expenseRepository.GetUserMonthlyExpenses(_testUser.UserId.ToString(), queryParams.PeriodStart, queryParams.PeriodEnd)
                .Returns(new List<Expense>());

        //Act
        var expectedExpenseResponse = await _expenseService.GetUserMonthlyExpenses(_testUser.UserId.ToString(), queryParams);

        //Assert
        Assert.Equal(0, expectedExpenseResponse.Count);
    }
}
