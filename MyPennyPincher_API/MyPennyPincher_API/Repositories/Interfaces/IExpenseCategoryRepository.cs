using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IExpenseCategoryRepository
{
    public ICollection<ExpenseCategory> getExpenseCategories();
}
