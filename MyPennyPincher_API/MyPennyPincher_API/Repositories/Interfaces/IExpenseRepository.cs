using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Models.QueryParameters;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IExpenseRepository
{
    Task<ICollection<Expense>> GetUserExpensesForPeriodAsync(string userId, TransactionQueryParams queryParams);
    Task<int> GetUserExpensesForPeriodCountAsync(string userId, TransactionQueryParams queryParams);
    Task<Expense?> GetByIdAsync(int expenseId);
    Task AddAsync(Expense expense);
    Task DeleteAsync(Expense expense);
    Task SaveChangesAsync();
}
