using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseService(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<ICollection<Expense>> GetByUserIdAsync(string userId)
    {
        return await _expenseRepository.GetByUserIdAsync(userId);
    }

    public async Task AddAsync(Expense expense)
    {
        await _expenseRepository.AddAsync(expense);

        await _expenseRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Expense expense)
    {
        await _expenseRepository.DeleteAsync(expense);

        await _expenseRepository.SaveChangesAsync();
    }

    public async Task EditAsync(Expense updatedExpense)
    {
        var existingExpense = await _expenseRepository.GetByIdAsync(updatedExpense.ExpenseId);

        if (existingExpense != null)
        {
            existingExpense.Amount = updatedExpense.Amount;
            existingExpense.Description = updatedExpense.Description;
            existingExpense.Date = updatedExpense.Date;
            existingExpense.Recurring = updatedExpense.Recurring;
            existingExpense.ExpenseCategoryId = updatedExpense.ExpenseCategoryId;

            await _expenseRepository.SaveChangesAsync();
        }

    }
}
