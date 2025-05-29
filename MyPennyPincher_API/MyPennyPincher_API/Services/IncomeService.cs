using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories.Interfaces;

namespace MyPennyPincher_API.Services;

public class IncomeService
{
    private readonly IIncomeRepository _incomeRepository;

    public IncomeService( IIncomeRepository incomeRepository)
    {
        _incomeRepository = incomeRepository;
    }

    public async Task<ICollection<Income>> GetUserIncomes(string userId)
    {
        return await _incomeRepository.GetByUserIdAsync(userId);      
    }

    public async Task AddIncome(Income income)
    {
        await _incomeRepository.AddAsync(income);

        await _incomeRepository.SaveChangesAsync();
    }

    public async Task DeleteIncome(Income income)
    {
        await _incomeRepository.DeleteAsync(income);

        await _incomeRepository.SaveChangesAsync();
    }

    public async Task EditIncome(Income updatedIncome)
    {
        var existingIncome = await _incomeRepository.GetByIdAsync(updatedIncome.IncomeId);

        if(existingIncome != null)
        {
            existingIncome.Amount = updatedIncome.Amount;
            existingIncome.Source = updatedIncome.Source;
            existingIncome.Date = updatedIncome.Date;
            existingIncome.Monthly = updatedIncome.Monthly;

            await _incomeRepository.SaveChangesAsync();
        }
    }
}
