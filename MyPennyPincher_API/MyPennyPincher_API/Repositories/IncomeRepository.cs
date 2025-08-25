using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories.Interfaces;

namespace MyPennyPincher_API.Repositories;

public class IncomeRepository : IIncomeRepository
{
    private readonly MyPennyPincherDbContext _context;

    public IncomeRepository(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Income income)
    {
        await _context.Incomes.AddAsync(income);
    }

    public Task DeleteAsync(Income income)
    {
        _context.Incomes.Remove(income);
        return Task.CompletedTask;
    }

    public async Task<Income?> GetByIdAsync(int incomeId)
    {
        return await _context.Incomes.FirstOrDefaultAsync(income => income.IncomeId == incomeId);
    }

    public async Task<ICollection<Income>> GetUserMonthlyIncomes(string userId, int year, int month)
    {
        return await _context.Incomes
            .Where(income => income.UserId.ToString() == userId &&
                    income.Date.Year == year &&
                    income.Date.Month == month)
                .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
