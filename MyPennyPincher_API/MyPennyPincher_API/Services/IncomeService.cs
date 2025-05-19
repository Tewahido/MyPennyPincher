using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services;

public class IncomeService
{
    private readonly MyPennyPincherDbContext _context;

    public IncomeService(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<Income>?> GetUserIncomes(string userId)
    {
        var user = await _context.Users
            .Include(user => user.Incomes)
            .FirstOrDefaultAsync(user => user.UserId.ToString() == userId);

        return user?.Incomes ?? null;
    }

    public async Task AddIncome(Income income)
    {
        _context.Incomes.Add(income);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteIncome(Income income)
    {
        _context.Incomes.Remove(income);

        await _context.SaveChangesAsync();
    }

    public async Task EditIncome(Income updatedIncome)
    {
        var existingIncome = await _context.Incomes.FirstOrDefaultAsync(income => income.IncomeId == updatedIncome.IncomeId);

        if(existingIncome != null)
        {
            existingIncome.Amount = updatedIncome.Amount;
            existingIncome.Source = updatedIncome.Source;
            existingIncome.Date = updatedIncome.Date;
            existingIncome.Monthly = updatedIncome.Monthly;

            await _context.SaveChangesAsync();
        }
    }
}
