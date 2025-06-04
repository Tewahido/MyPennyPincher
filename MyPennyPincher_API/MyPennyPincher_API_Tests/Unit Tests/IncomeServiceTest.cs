using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.Test_Utilities;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class IncomeServiceTest
{
    private readonly IIncomeService _incomeService;
    private readonly IIncomeRepository _incomeRepository;
    private readonly MyPennyPincherDbContext _context;
    private readonly User _testUser;

    public IncomeServiceTest()
    {
        _context = DbContextFactory.GenerateInMemoryDB();
        _incomeRepository = new IncomeRepository(_context);
        _incomeService = new IncomeService(_incomeRepository);
        _testUser = TestDataFactory.CreateTestUser();
    }

    [Fact]
    public async Task GIVEN_NewIncome_WHEN_AddingIncome_THEN_AddIncomeToDb()
    {
        //Arrange
        Income income = TestDataFactory.CreateIncome(1, _testUser);


        //Act
        await _incomeService.AddAsync(income);

        var expectedIncome = _context.Incomes.FirstOrDefaultAsync(exp => exp.IncomeId == income.IncomeId);

        //Assert
        Assert.NotNull(expectedIncome);
    }

    [Fact]
    public async Task GIVEN_ExistingIncome_WHEN_DeletingIncome_THEN_DeleteIncomeFromDb()
    {
        //Arrange
        Income income = TestDataFactory.CreateIncome(1, _testUser);

        await _incomeService.AddAsync(income);

        //Act
        await _incomeService.DeleteAsync(income);

        var expectedIncome = await _context.Incomes.FirstOrDefaultAsync(exp => exp.IncomeId == income.IncomeId);

        //Assert
        Assert.Null(expectedIncome);
    }


    [Fact]
    public async Task GIVEN_ExistingIncome_WHEN_EditingIncome_THEN_OverwriteExistingIncome()
    {
        //Arrange
        Income existingIncome = TestDataFactory.CreateIncome(1, _testUser);

        await _incomeService.AddAsync(existingIncome);

        Income editedIncome = new Income
        {
            IncomeId = 1,
            Source = existingIncome.Source,
            Amount = 500,
            Date = existingIncome.Date,
            Monthly = existingIncome.Monthly,
            UserId = existingIncome.UserId,
        };

        //Act
        await _incomeService.EditAsync(editedIncome);

        var expectedIncome = await _context.Incomes.FirstOrDefaultAsync(exp => exp.IncomeId == editedIncome.IncomeId);

        //Assert
        Assert.Equal(editedIncome.Amount, expectedIncome.Amount);
    }

    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserIncomes_THEN_ReturnUserIncomes()
    {
        //Arrange
        Income firstIncome = TestDataFactory.CreateIncome(2, _testUser);
        await _incomeService.AddAsync(firstIncome);

        Income secondIncome = TestDataFactory.CreateIncome(3, _testUser);
        await _incomeService.AddAsync(secondIncome);

        Income thirdIncome = TestDataFactory.CreateIncome(4, _testUser);
        await _incomeService.AddAsync(thirdIncome);

        //Act
        var expectedIncomes = await _incomeService.GetByUserIdAsync(_testUser.UserId.ToString());

        //Assert
        Assert.Equal(3, expectedIncomes.Count);

        Assert.Contains(firstIncome, expectedIncomes);
        Assert.Contains(secondIncome, expectedIncomes);
        Assert.Contains(thirdIncome, expectedIncomes);
    }

}
