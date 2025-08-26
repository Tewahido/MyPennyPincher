using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IIncomeRepository
{
    Task<ICollection<Income>> GetUserMonthlyIncomes(string userId, DateOnly periodStart, DateOnly periodEnd);
    Task<Income?> GetByIdAsync(int incomeId);
    Task AddAsync(Income income);
    Task DeleteAsync(Income income);
    Task SaveChangesAsync();
}