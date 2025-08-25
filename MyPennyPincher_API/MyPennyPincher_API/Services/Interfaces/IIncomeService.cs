using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.QueryParameters;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IIncomeService
{
    Task<IEnumerable<Income>> GetUserMonthlyIncomes(string userId, TransactionQueryParams queryParams);
    Task AddAsync(Income income);
    Task DeleteAsync(Income income);
    Task EditAsync(Income updatedIncome);
}
