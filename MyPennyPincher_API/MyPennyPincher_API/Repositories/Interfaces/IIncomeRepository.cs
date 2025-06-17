using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IIncomeRepository
{
    Task<ICollection<Income>> GetByUserIdAsync(string userId);
    Task<Income?> GetByIdAsync(int incomeId);
    Task AddAsync(Income income);
    Task DeleteAsync(Income income);
    Task SaveChangesAsync();
}