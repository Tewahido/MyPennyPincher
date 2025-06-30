using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories.Interfaces;

namespace MyPennyPincher_API.Repositories;

public class ExpenseRepository : IExpenseRepository
{
    private readonly MyPennyPincherDbContext _context;

    public ExpenseRepository(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);
    }

    public Task DeleteAsync(Expense expense)
    {
        _context.Expenses.Remove(expense);
        return Task.CompletedTask;
    }

    public async Task<Expense?> GetByIdAsync(int expenseId)
    {
        return await _context.Expenses.FirstOrDefaultAsync(expense => expense.ExpenseId == expenseId);
    }

    public async Task<ICollection<Expense>> GetByUserIdAsync(string userId)
    {
        return await _context.Expenses
            .Where(user => user.UserId.ToString() == userId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
