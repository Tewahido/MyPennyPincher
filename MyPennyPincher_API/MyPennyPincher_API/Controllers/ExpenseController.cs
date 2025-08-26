using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Models.QueryParameters;
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

    [HttpGet("month")]
    public async Task<ActionResult<IEnumerable<Expense>>> GetUserExpensesForMonth([FromQuery] TransactionQueryParams queryParams)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized();
        }

        var expenseResponse = await _expenseService.GetUserMonthlyExpenses(userId, queryParams);

        if(expenseResponse.Count < 1)
        {
            return NoContent();
        }

        return Ok(expenseResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddExpense([FromBody]Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _expenseService.AddAsync(expense);

        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteExpense(Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _expenseService.DeleteAsync(expense);

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> EditExpense([FromBody]Expense expense)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _expenseService.EditAsync(expense);

        return NoContent();
    }
}
