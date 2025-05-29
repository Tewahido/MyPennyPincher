using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
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

    public async Task DeleteAsync(Income income)
    {
        _context.Incomes.Remove(income);
    }

    public async Task<Income?> GetByIdAsync(int incomeId)
    {
        return await _context.Incomes.FirstOrDefaultAsync(income => income.IncomeId == incomeId);
    }

    public async Task<ICollection<Income>> GetByUserIdAsync(string userId)
    {
        return await _context.Incomes
            .Where(user => user.UserId.ToString() == userId)
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
