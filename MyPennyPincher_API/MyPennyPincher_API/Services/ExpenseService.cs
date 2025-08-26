using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.QueryParameters;
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

    public async Task<IEnumerable<Expense>> GetUserMonthlyExpenses(string userId, TransactionQueryParams queryParams)
    {
        var expenses = await _expenseRepository.GetUserMonthlyExpenses(userId, queryParams.PeriodStart, queryParams.PeriodEnd);

        if (expenses == null || expenses.Count() == 0)
        {
            return new List<Expense>();
        }

        return expenses.Skip(queryParams.Offset)
                .Take(queryParams.Limit);
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
