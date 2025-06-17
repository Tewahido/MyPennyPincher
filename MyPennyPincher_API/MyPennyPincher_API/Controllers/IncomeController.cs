using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Income>>> GetUserIncomes()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId == null)
        {
            return Unauthorized();
        }

        var incomes = await _incomeService.GetByUserIdAsync(userId);

        return Ok(incomes);
    }

    [HttpPost]
    public async Task<ActionResult> AddIncome([FromBody] Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.AddAsync(income);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteIncome(Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.DeleteAsync(income);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> EditIncome([FromBody] Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.EditAsync(income);

        return Ok();
    }
}
