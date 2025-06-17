using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IExpenseCategoryService
{
    Task<ICollection<ExpenseCategory>> GetExpenseCategories();
}
