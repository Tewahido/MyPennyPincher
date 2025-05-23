using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services;

public class ExpenseCategoryService
{
    private readonly MyPennyPincherDbContext _context;

    public ExpenseCategoryService(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<ExpenseCategory>> GetExpenseCategories()
    {
        var expenseCategories = await _context.ExpenseCategories.ToListAsync();
        return expenseCategories;
    }
}
