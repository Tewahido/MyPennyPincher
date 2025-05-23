﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Services;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class IncomeController : ControllerBase
{
    private readonly IncomeService _incomeService;

    public IncomeController(IncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Income>>> GetUserIncomes()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        var incomes = await _incomeService.GetUserIncomes(userId);

        if(incomes == null || incomes.Count() == 0)
        {
            return NotFound("No user incomes found");
        }

        return Ok(incomes);
    }

    [HttpPost("addIncome")]
    public async Task<ActionResult> AddIncome([FromBody] Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.AddIncome(income);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteIncome(Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.DeleteIncome(income);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult> EditIncome([FromBody] Income income)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _incomeService.EditIncome(income);

        return Ok();
    }
}
