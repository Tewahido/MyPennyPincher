using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<ICollection<Expense>> GetUserMonthlyExpenses(string userId, DateOnly periodStart, DateOnly periodEnd);
    Task<Expense?> GetByIdAsync(int expenseId);
    Task AddAsync(Expense expense);
    Task DeleteAsync(Expense expense);
    Task SaveChangesAsync();
}
