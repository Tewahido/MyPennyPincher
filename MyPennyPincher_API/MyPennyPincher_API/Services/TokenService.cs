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
using Microsoft.Extensions.Options;

namespace MyPennyPincher_API.Services;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly JwtOptions _jwtOptions;

    public TokenService(ITokenRepository tokenRepository, IOptions<JwtOptions> options)
    {
        _tokenRepository = tokenRepository;
        _jwtOptions = options.Value;

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
        var generatedToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var refreshToken = new RefreshToken
        {
            Token = generatedToken,
            UserId = userId,
            ExpiryDate = DateTime.UtcNow.AddDays(1),
        };

        return refreshToken;
    }

    public UserAccessToken GenerateAccessToken(Guid userId, bool isVerified)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var convertedUserId = userId.ToString();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, convertedUserId),
            new Claim("2FA","true")
        };

        var tokenValidityTime = DateTime.Now.AddMinutes(_jwtOptions.TokenValidityMins);

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
            IsVerified = isVerified
        };
    }

    public async Task<UserAccessToken?> RefreshToken(Guid userId, string refreshToken, bool isVerified)
    {
        var tokenIsValid = await ValidateToken(userId, refreshToken);

        if (!tokenIsValid)
        {
            throw new InvalidRefreshTokenException("Refresh token is invalid");
        }
        
        return GenerateAccessToken(userId, isVerified);
    }

    private async Task<bool> ValidateToken(Guid userId, string token)
    {
        var refreshToken = await _tokenRepository.GetTokenAsync(userId);

        var tokenIsValid = refreshToken!.ExpiryDate > DateTime.UtcNow 
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
        var tokenValiditySeconds = (int)(refreshToken.ExpiryDate - DateTime.UtcNow).TotalSeconds;

        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Path = "/auth/refresh",
            MaxAge = TimeSpan.FromSeconds(tokenValiditySeconds),
        };
    }
}
