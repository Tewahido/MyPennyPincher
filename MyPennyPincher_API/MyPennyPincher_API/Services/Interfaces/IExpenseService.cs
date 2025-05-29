using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IExpenseService
{
    Task<ICollection<Expense>> GetByUserId(string userId);
    Task AddAsync(Expense expense);
    Task DeleteAsync(Expense expense);
    Task EditAsync(Expense updatedExpense);
}
