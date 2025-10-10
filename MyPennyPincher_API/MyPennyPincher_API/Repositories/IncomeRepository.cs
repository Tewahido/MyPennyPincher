using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.QueryParameters;
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

    public async Task<ICollection<Income>> GetUserIncomesForPeriodAsync(string userId, TransactionQueryParams queryParams)
    {
        return await _context.Incomes
            .Where(income => income.UserId.ToString() == userId &&
                    income.Date >= queryParams.PeriodStart &&
                    income.Date <= queryParams.PeriodEnd)
            .Skip(queryParams.Offset)
            .Take(queryParams.Limit)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> GetUserIncomesForPeriodCountAsync(string userId, TransactionQueryParams queryParams)
    {
        return await _context.Incomes
            .Where(income => income.UserId.ToString() == userId &&
                    income.Date >= queryParams.PeriodStart &&
                    income.Date <= queryParams.PeriodEnd)
            .CountAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
