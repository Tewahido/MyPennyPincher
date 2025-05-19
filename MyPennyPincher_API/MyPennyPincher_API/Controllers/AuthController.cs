using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Services;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private AuthService _authService;

    public AuthController( AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        User? registredUser = await _authService.Register(user);

        if (registredUser == null)
        {
            Console.WriteLine("User is null, unable to register user");
            return BadRequest("Could not register user");
        }

        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult<User?>> Login([FromBody] Login login)
    {
        var user = await _authService.Login(login.Email, login.Password);

        if(user == null)
        {
            return Unauthorized("Invalid Credentials");
        }

        string token = _authService.GenerateToken(user);

        LoggedInUser loggedInUser = new LoggedInUser
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            Token = token
        };

        return Ok(loggedInUser);
    }
}
