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
        return _context.Incomes
            .Where(user => user.UserId.ToString() == userId)
            .ToList();

        
    }

    public async Task AddIncome(Income income)
    {
        Console.WriteLine(income);
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
        var existingIncome = _context.Incomes.Where(income => income.IncomeId == updatedIncome.IncomeId).FirstOrDefault();

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
