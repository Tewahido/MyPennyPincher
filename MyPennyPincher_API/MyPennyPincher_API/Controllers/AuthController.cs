using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MyPennyPincher_API.Models.ConfigModels;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Models.Emails;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly GeneralSettings _generalSettings;
    private readonly IDistributedCache _distributedCache;

    public AuthController(IAuthService authService, ITokenService tokenService, IEmailService emailService, IOptions<GeneralSettings> generalSettings, IDistributedCache distributedCache)
    {
        _authService = authService;
        _tokenService = tokenService;
        _emailService = emailService;
        _generalSettings = generalSettings.Value;
        _distributedCache = distributedCache;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var registredUser = await _authService.Register(user);

        var userAccessToken = _tokenService.GenerateAccessToken(user.UserId, user.IsVerified);

        var verificationEmail = new VerificationEmail(userAccessToken, _generalSettings);

        _emailService.SendVerificationEmail(verificationEmail, user.Email);

        _distributedCache.Set(userAccessToken.UserId.ToString(), System.Text.Encoding.UTF8.GetBytes(userAccessToken.Token), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

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

        var user = await _authService.Login(login);

        var userAccessToken = _tokenService.GenerateAccessToken(user.UserId, user.IsVerified);

        var refreshToken = _tokenService.GenerateRefreshToken(user.UserId);

        await _tokenService.AddRefreshToken(refreshToken);

        var refreshTokenCookieOptions = _tokenService.CreateRefreshTokenCookieOptions(refreshToken);

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

        var accessToken = await _tokenService.RefreshToken(convertedUserId, refreshToken, true);

        if (accessToken == null)
        {
            return Unauthorized();
        }

        return Ok(accessToken);
    }

    [HttpPost("verify")]
    public async Task<ActionResult> VerifyUser([FromBody] UserAccessToken userAccessToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _authService.VerifyUser(userAccessToken);

        return Ok();
    }
}
