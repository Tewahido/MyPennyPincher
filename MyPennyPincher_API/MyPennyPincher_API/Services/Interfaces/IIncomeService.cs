using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IIncomeService
{
    Task<ICollection<Income>> GetByUserIdAsync(string userId);
    Task AddAsync(Income income);
    Task DeleteAsync(Income income);
    Task EditAsync(Income updatedIncome);
}
