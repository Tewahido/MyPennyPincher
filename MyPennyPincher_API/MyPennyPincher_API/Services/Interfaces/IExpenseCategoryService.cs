using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IExpenseCategoryService
{
    Task<ICollection<ExpenseCategory>> GetExpenseCategories();
}
