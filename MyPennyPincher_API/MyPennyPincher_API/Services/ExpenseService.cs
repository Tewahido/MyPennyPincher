using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services;

public class ExpenseService
{
    private readonly MyPennyPincherDbContext _context;

    public ExpenseService(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Expense>> GetUserExpenses(string userId)
    {
        return await _context.Expenses
           .Where(user => user.UserId.ToString() == userId)
           .ToListAsync();
    }

    public async Task AddExpense(Expense expense)
    {
        await _context.Expenses.AddAsync(expense);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteExpense(Expense expense)
    {
        _context.Expenses.Remove(expense);

        await _context.SaveChangesAsync();
    }

    public async Task EditExpense(Expense updatedExpense)
    {
        var existingExpense = await _context.Expenses.FirstOrDefaultAsync(income => income.ExpenseId == updatedExpense.ExpenseId);

        if (existingExpense != null)
        {
            existingExpense.Amount = updatedExpense.Amount;
            existingExpense.Description = updatedExpense.Description;
            existingExpense.Date = updatedExpense.Date;
            existingExpense.Recurring = updatedExpense.Recurring;
            existingExpense.ExpenseCategoryId = updatedExpense.ExpenseCategoryId;

            await _context.SaveChangesAsync();
        }

    }
}
