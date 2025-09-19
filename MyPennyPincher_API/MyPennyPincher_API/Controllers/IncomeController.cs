using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Models.QueryParameters;
using MyPennyPincher_API.Services.Interfaces;
using System.Security.Claims;

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
    public async Task<ActionResult<IncomeResponse>> GetUserIncomesForPeriod([FromQuery] TransactionQueryParams queryParams)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (userId == null)
        {
            return Unauthorized();
        }

        var incomeResponse = await _incomeService.GetUserMonthlyIncomes(userId, queryParams);

        if (incomeResponse.Count < 1)
        {
            return NoContent();
        }

        return Ok(incomeResponse);
    }

    [HttpPost]
    public async Task<ActionResult> AddIncome([FromBody] Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.AddAsync(income);

        return Created();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteIncome(Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.DeleteAsync(income);

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult> EditIncome([FromBody] Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.EditAsync(income);

        return NoContent();
    }
}
