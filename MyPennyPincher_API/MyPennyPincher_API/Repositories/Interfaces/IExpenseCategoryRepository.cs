using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IExpenseCategoryRepository
{
    Task<ICollection<ExpenseCategory>> GetAsync();
}
