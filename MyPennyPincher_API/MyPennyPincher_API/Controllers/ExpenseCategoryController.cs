using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Services;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ExpenseCategoryController : ControllerBase
{
    private readonly ExpenseCategoryService _service;

    public ExpenseCategoryController(ExpenseCategoryService service) 
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<ExpenseCategory>>> GetExpenseCategories()
    {
        var expenseCategories = await _service.GetExpenseCategories();
        
        return Ok(expenseCategories);
    }
}
