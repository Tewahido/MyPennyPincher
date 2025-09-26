using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.QueryParameters;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IIncomeRepository
{
    Task<ICollection<Income>> GetUserIncomesForPeriodAsync(string userId, TransactionQueryParams queryParams);
    Task<int> GetUserIncomesForPeriodCountAsync(string userId, TransactionQueryParams queryParams);
    Task<Income?> GetByIdAsync(int incomeId);
    Task AddAsync(Income income);
    Task DeleteAsync(Income income);
    Task SaveChangesAsync();
}