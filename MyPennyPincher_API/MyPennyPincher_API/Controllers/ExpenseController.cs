using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Services;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly ExpenseService _expenseService;

    public ExpenseController(ExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Income>>> GetUserIncomes()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var expenses = await _expenseService.GetUserExpenses(userId);

        if (expenses == null || expenses.Count() == 0)
        {
            return NotFound("No user expenses found");
        }

        return Ok(expenses);
    }

    [HttpPost("addExpense")]
    public async Task<ActionResult> AddExpense([FromBody]Expense expense)
    {
        await _expenseService.AddExpense(expense);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteExpense(Expense expense)
    {
        await _expenseService.DeleteExpense(expense);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> EditExpense([FromBody]Expense expense)
    {
        await _expenseService.EditExpense(expense);

        return Ok();
    }
}
