using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IExpenseCategoryRepository _categoryRepository;

    public ExpenseCategoryService(IExpenseCategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ICollection<ExpenseCategory>> GetExpenseCategories()
    {
        return await _categoryRepository.GetAsync();
    }
}
