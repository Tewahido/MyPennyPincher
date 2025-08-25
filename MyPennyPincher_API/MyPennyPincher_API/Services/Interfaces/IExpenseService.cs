using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.QueryParameters;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IExpenseService
{
    Task<IEnumerable<Expense>> GetUserMonthlyExpenses(string userId, TransactionQueryParams queryParams);
    Task AddAsync(Expense expense);
    Task DeleteAsync(Expense expense);
    Task EditAsync(Expense updatedExpense);
}
