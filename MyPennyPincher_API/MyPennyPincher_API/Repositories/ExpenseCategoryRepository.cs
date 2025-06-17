using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories.Interfaces;

namespace MyPennyPincher_API.Repositories;

public class ExpenseCategoryRepository : IExpenseCategoryRepository
{
    private readonly MyPennyPincherDbContext _context;

    public ExpenseCategoryRepository(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<ExpenseCategory>> GetAsync()
    {
        return await _context.ExpenseCategories.ToListAsync();
    }
}
