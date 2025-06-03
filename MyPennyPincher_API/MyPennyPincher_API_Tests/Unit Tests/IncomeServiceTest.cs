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

    public IncomeServiceTest()
    {
        _context = DbContextUtils.GenerateInMemoryDB();
        _incomeRepository = new IncomeRepository(_context);
        _incomeService = new IncomeService(_incomeRepository);
    }

    [Fact]
    public async Task GIVEN_NewIncome_WHEN_AddingIncome_THEN_AddIncomeToDb()
    {
        //Arrange
        Income income = new Income
        {
            IncomeId = 1,
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = Guid.NewGuid(),
        };

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
        Income income = new Income
        {
            IncomeId = 1,
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = Guid.NewGuid(),
        };

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
        Income existingIncome = new Income
        {
            IncomeId = 1,
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = Guid.NewGuid(),
        };

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
        Guid userId = Guid.NewGuid();

        Income firstIncome = new Income
        {
            IncomeId = 1,
            Source = "Test",
            Amount = 100,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = userId,
        };
        Income secondIncome = new Income
        {
            IncomeId = 2,
            Source = "Test2",
            Amount = 1200,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = userId,
        };
        Income thirdIncome = new Income
        {
            IncomeId = 3,
            Source = "Test3",
            Amount = 1300,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Monthly = false,
            UserId = userId,
        };


        await _incomeService.AddAsync(firstIncome);
        await _incomeService.AddAsync(secondIncome);
        await _incomeService.AddAsync(thirdIncome);

        //Act

        var expectedIncomes = await _incomeService.GetByUserIdAsync(userId.ToString());

        //Assert
        Assert.Equal(expectedIncomes.Count, 3);
        Assert.Contains(firstIncome, expectedIncomes);
        Assert.Contains(secondIncome, expectedIncomes);
        Assert.Contains(thirdIncome, expectedIncomes);
    }

}
