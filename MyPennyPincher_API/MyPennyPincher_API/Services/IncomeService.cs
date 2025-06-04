using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class IncomeService : IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;

    public IncomeService( IIncomeRepository incomeRepository)
    {
        _incomeRepository = incomeRepository;
    }

    public async Task<ICollection<Income>> GetByUserIdAsync(string userId)
    {
        var incomes = await _incomeRepository.GetByUserIdAsync(userId);

        if (incomes == null || incomes.Count() == 0)
        {
            throw new IncomesNotFoundException("User incomes not found");
        }

        return incomes;
    }

    public async Task AddAsync(Income income)
    {
        await _incomeRepository.AddAsync(income);

        await _incomeRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Income income)
    {
        await _incomeRepository.DeleteAsync(income);

        await _incomeRepository.SaveChangesAsync();
    }

    public async Task EditAsync(Income updatedIncome)
    {
        int incomeId = updatedIncome.IncomeId;

        var existingIncome = await _incomeRepository.GetByIdAsync(incomeId);

        if(existingIncome != null)
        {
            throw new IncomeNotFoundException(incomeId);
        }
            existingIncome.Amount = updatedIncome.Amount;
            existingIncome.Source = updatedIncome.Source;
            existingIncome.Date = updatedIncome.Date;
            existingIncome.Monthly = updatedIncome.Monthly;

            await _incomeRepository.SaveChangesAsync();
    }
}
