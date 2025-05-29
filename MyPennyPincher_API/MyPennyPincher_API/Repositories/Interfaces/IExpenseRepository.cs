using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<ICollection<Expense>> GetByUserIdAsync(string userId);
    Task<Expense?> GetByIdAsync(int expenseId);
    Task AddAsync(Expense expense);
    Task DeleteAsync(Expense expense);
    Task SaveChangesAsync();
}
