using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly MyPennyPincherDbContext _context;

    public ExpenseCategoryService(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<ExpenseCategory>> GetExpenseCategories()
    {
        return await _context.ExpenseCategories.ToListAsync();
    }
}
