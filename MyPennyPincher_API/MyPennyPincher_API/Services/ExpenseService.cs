using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
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

    public async Task<ExpenseResponse> GetUserExpensesForPeriod(string userId, TransactionQueryParams queryParams)
    {
        var expenses = await _expenseRepository.GetUserExpensesForPeriodAsync(userId, queryParams);

        var totalCount = await _expenseRepository.GetUserExpensesForPeriodCountAsync(userId, queryParams);

        if (expenses == null || totalCount == 0)
        {
            return new ExpenseResponse
            {
                Data = new List<Expense>(),
                Count = 0
            };
        }

        return new ExpenseResponse
        {
            Data = expenses.ToList(),
            Count = totalCount
        };
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
