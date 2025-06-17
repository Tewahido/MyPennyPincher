using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Models.ConfigModels;
using MyPennyPincher_API.Exceptions;

namespace MyPennyPincher_API.Services;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _config;
    private readonly JwtOptions _jwtOptions;

    public TokenService(IConfiguration config, ITokenRepository tokenRepository, JwtOptions jwtOptions)
    {
        _tokenRepository = tokenRepository;
        _config = config;
        _jwtOptions = jwtOptions;

    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await _tokenRepository.AddAsync(refreshToken);

        await _tokenRepository.SaveChangesAsync();
    }

    public async Task DeleteRefreshToken(string userId)
    {
        var existingToken = await GetUserToken(userId);

        if (existingToken == null)
        {
            return;
        }
        
        await _tokenRepository.DeleteAsync(existingToken);
        
        await _tokenRepository.SaveChangesAsync();
    }

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        string generatedToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        RefreshToken refreshToken = new RefreshToken
        {
            Token = generatedToken,
            UserId = userId,
            ExpiryDate = DateTime.UtcNow.AddDays(1),
        };

        return refreshToken;
    }

    public UserAccessToken GenerateAccessToken(Guid userId)
    {

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var convertedUserId = userId.ToString();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, convertedUserId),
        };

        var tokenValidityTime = DateTime.Now.AddMinutes(_config.GetValue<double>("Jwt:TokenValidityMins"));

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Issuer,
            claims: claims,
            expires: tokenValidityTime,
            signingCredentials: credentials);

        var userToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new UserAccessToken
        {
            UserId = userId,
            Token = userToken,
        };
    }

    public async Task<UserAccessToken?> RefreshToken(Guid userId, string refreshToken)
    {
        bool tokenIsValid = await ValidateToken(userId, refreshToken);

        if (!tokenIsValid)
        {
            throw new InvalidRefreshTokenException("Refresh token is invalid");
        }
        
        return GenerateAccessToken(userId);
    }

    private async Task<bool> ValidateToken(Guid userId, string token)
    {
        var refreshToken = await _tokenRepository.GetTokenAsync(userId);

        bool tokenIsValid = refreshToken!.ExpiryDate > DateTime.UtcNow 
            && refreshToken != null 
            && token == refreshToken.Token;

        return tokenIsValid;
    }

    private async Task<RefreshToken?> GetUserToken(string userId)
    {
        return await _tokenRepository.GetTokenAsync(new Guid(userId));
    }

    public CookieOptions CreateRefreshTokenCookieOptions(RefreshToken refreshToken)
    {
        int tokenValiditySeconds = (int)(refreshToken.ExpiryDate - DateTime.UtcNow).TotalSeconds;

        return new CookieOptions
        {
            HttpOnly = false,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/auth/refresh",
            MaxAge = TimeSpan.FromSeconds(tokenValiditySeconds),
        };
    }
}
