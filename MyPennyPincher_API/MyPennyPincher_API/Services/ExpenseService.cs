using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
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
        var expenses = await _expenseRepository.GetByUserIdAsync(userId);

        if (expenses == null || expenses.Count() == 0)
        {
            throw new ExpensesNotFoundException("No expenses found");
        }

        return expenses;
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
        int expenseId = updatedExpense.ExpenseId;

        var existingExpense = await _expenseRepository.GetByIdAsync(expenseId);

        if (existingExpense == null)
        {
            throw new ExpenseNotFoundException(expenseId);
        }

        existingExpense.Amount = updatedExpense.Amount;
        existingExpense.Description = updatedExpense.Description;
        existingExpense.Date = updatedExpense.Date;
        existingExpense.Recurring = updatedExpense.Recurring;
        existingExpense.ExpenseCategoryId = updatedExpense.ExpenseCategoryId;

        await _expenseRepository.SaveChangesAsync();

    }
}
