using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController( IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            User? registredUser = await _authService.Register(user);
            
            if (registredUser == null)
            {
                return BadRequest("Could not register user");
            }
        }

        catch(InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<User?>> Login([FromBody] Login login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _authService.Login(login);

        if(user == null)
        {
            return Unauthorized("Invalid Credentials");
        }

        string token = _authService.GenerateToken(user);

        LoginResponse loginResponse= new LoginResponse
        {
            UserId = user.UserId,
            Token = token,
        };

        return Ok(loginResponse);
    }
}
