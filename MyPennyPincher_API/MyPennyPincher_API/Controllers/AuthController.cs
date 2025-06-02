using Microsoft.AspNetCore.Mvc;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController( IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
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

        string token = _tokenService.GenerateAccessToken(user.UserId);

        LoginResponse loginResponse= new LoginResponse
        {
            UserId = user.UserId,
            Token = token,
        };

        var refreshToken = _tokenService.GenerateRefreshToken(user);

        await _tokenService.AddRefreshToken(refreshToken);

        int tokenValiditySeconds = (int)(refreshToken.ExpiryDate - DateTime.UtcNow).TotalSeconds;

        Response.Cookies.Append("refreshToken", refreshToken.Token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/auth/refresh",
            MaxAge = TimeSpan.FromSeconds(tokenValiditySeconds)
        });

        return Ok(loginResponse);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout([FromBody] string userId)
    {
        await _tokenService.DeleteRefreshToken(userId);

        return Ok();
    }
}
