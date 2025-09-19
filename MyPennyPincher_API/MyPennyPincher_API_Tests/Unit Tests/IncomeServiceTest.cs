using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.QueryParameters;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.Test_Utilities;
using NSubstitute;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class IncomeServiceTest
{
    private readonly IIncomeService _incomeService;
    private readonly IIncomeRepository _incomeRepository;
    private readonly User _testUser;

    public IncomeServiceTest()
    {
        _incomeRepository = Substitute.For<IIncomeRepository>();
        _incomeService = new IncomeService(_incomeRepository);
        _testUser = TestDataFactory.CreateTestUser();
    }

    [Fact]
    public async Task GIVEN_NoUserExpenses_WHEN_GettingUserExpenses_THEN_ReturnEmptyExpenseResponse()
    {
        //Arrange
        var queryParams = new TransactionQueryParams();

        _incomeRepository.GetUserIncomesForPeriodAsync(_testUser.UserId.ToString(), queryParams)
                .Returns(new List<Income>());

        //Act
        var expectedExpenseResponse = await _incomeService.GetUserIncomesForPeriod(_testUser.UserId.ToString(), queryParams);

        //Assert
        Assert.Equal(0, expectedExpenseResponse.Count);
    }


    [Fact]
    public async Task GIVEN_NoUserIncomes_WHEN_EditingIncome_THEN_ThrowIncomeNotFoundException()
    {
        //Arrange
        var existingIncome = TestDataFactory.CreateIncome(1, _testUser);

        await _incomeService.AddAsync(existingIncome);

        var editedIncome = new Income
        {
            IncomeId = 1,
            Source = existingIncome.Source,
            Amount = 500,
            Date = existingIncome.Date,
            Monthly = existingIncome.Monthly,
            UserId = existingIncome.UserId,
        };

        _incomeRepository.GetByIdAsync(existingIncome.IncomeId)
            .Returns(Task.FromResult<Income?>(null));

        //Act
        await Assert.ThrowsAsync<IncomeNotFoundException>(() => _incomeService.EditAsync(editedIncome));
    }

    [Fact]
    public async Task GIVEN_UserId_WHEN_GettingUserIncomes_THEN_ReturnUserIncome()
    {
        //Arrange
        var queryParams = new TransactionQueryParams();

        var firstIncome = TestDataFactory.CreateIncome(2, _testUser);

        var secondIncome = TestDataFactory.CreateIncome(3, _testUser);

        var thirdIncome = TestDataFactory.CreateIncome(4, _testUser);

        _incomeRepository.GetUserIncomesForPeriodAsync(_testUser.UserId.ToString(), queryParams)
                .Returns(Task.FromResult<ICollection<Income>>(new List<Income> { firstIncome, secondIncome, thirdIncome }));

        _incomeRepository.GetUserIncomesForPeriodCountAsync(_testUser.UserId.ToString(), queryParams)
                .Returns(Task.FromResult(3));

        //Act
        var expectedIncomeResponse = await _incomeService.GetUserIncomesForPeriod(_testUser.UserId.ToString(), queryParams);

        //Assert
        Assert.Equal(3, expectedIncomeResponse.Count);

        Assert.Contains(firstIncome, expectedIncomeResponse.Data);
        Assert.Contains(secondIncome, expectedIncomeResponse.Data);
        Assert.Contains(thirdIncome, expectedIncomeResponse.Data);
    }

}
