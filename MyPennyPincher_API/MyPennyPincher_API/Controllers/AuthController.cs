﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;

    public AuthController( IAuthService authService, ITokenService tokenService, IEmailService emailService)
    {
        _authService = authService;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User? registredUser = await _authService.Register(user);

        return Ok();
    }

    [EnableRateLimiting("sliding")]
    [HttpPost("login")]
    public async Task<ActionResult<UserAccessToken>> Login([FromBody] Login login)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        User user = await _authService.Login(login);

        var userAccessToken = _tokenService.GenerateAccessToken(user.UserId);

        var refreshToken = _tokenService.GenerateRefreshToken(user.UserId);

        await _tokenService.AddRefreshToken(refreshToken);

        CookieOptions refreshTokenCookieOptions = _tokenService.CreateRefreshTokenCookieOptions(refreshToken);

        Response.Cookies.Append("refreshToken", refreshToken.Token, refreshTokenCookieOptions);

        return Ok(userAccessToken);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout([FromBody] string userId)
    {
        await _tokenService.DeleteRefreshToken(userId);

        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<UserAccessToken>> Refresh([FromBody]string userId)
    {
        var refreshToken = Request.Cookies["refreshToken"];

        if(refreshToken == null)
        {
            return Unauthorized();
        }

        var convertedUserId = new Guid(userId);

        var accessToken = await _tokenService.RefreshToken(convertedUserId, refreshToken);

        if (accessToken == null)
        {
            return Unauthorized();
        }

        return Ok(accessToken);
    }
}
