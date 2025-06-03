using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Income>>> GetUserExpenses()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var expenses = await _expenseService.GetByUserIdAsync(userId);

        if (expenses == null || expenses.Count() == 0)
        {
            return NotFound("No user expenses found");
        }

        return Ok(expenses);
    }

    [HttpPost]
    public async Task<ActionResult> AddExpense([FromBody]Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _expenseService.AddAsync(expense);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteExpense(Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _expenseService.DeleteAsync(expense);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> EditExpense([FromBody]Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _expenseService.EditAsync(expense);

        return Ok();
    }
}
