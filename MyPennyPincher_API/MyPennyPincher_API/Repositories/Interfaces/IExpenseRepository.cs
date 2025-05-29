using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IExpenseRepository
{
    public ICollection<Expense> GetUserExpenses(string userId);
    public 
}
