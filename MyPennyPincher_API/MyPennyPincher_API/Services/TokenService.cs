using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _config;

    public TokenService(IConfiguration config, ITokenRepository tokenRepository)
    {
        _tokenRepository = tokenRepository;
        _config = config;
    }

    public async Task AddRefreshToken(RefreshToken refreshToken)
    {
        await _tokenRepository.AddAsync(refreshToken);
        await _tokenRepository.SaveChangesAsync();
    }

    public async Task DeleteRefreshToken(string userId)
    {

        var existingToken = await GetUserToken(userId);

        if (existingToken != null)
        {
            await _tokenRepository.DeleteAsync(existingToken);
        }
        
        await _tokenRepository.SaveChangesAsync();
    }

    public RefreshToken GenerateRefreshToken(User user)
    {
        string generatedToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        RefreshToken refreshToken = new RefreshToken
        {
            Token = generatedToken,
            UserId = user.UserId,
            ExpiryDate = DateTime.UtcNow.AddDays(1),
        };

        return refreshToken;
    }

    public string GenerateAccessToken(Guid userId)
    {

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var convertedUserId = userId.ToString();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, convertedUserId),
        };

        var tokenValidityTime = DateTime.Now.AddHours(_config.GetValue<double>("Jwt:TokenValidityHrs"));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: claims,
            expires: tokenValidityTime,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> ValidateToken(Guid userId, string token)
    {
        RefreshToken refreshToken = await _tokenRepository.GetTokenAsync(userId);

        bool tokenIsValid = refreshToken.ExpiryDate > DateTime.UtcNow 
            && refreshToken != null 
            && token == refreshToken.Token;

        return tokenIsValid;
    }

    private async Task<RefreshToken?> GetUserToken(string userId)
    {
        return await _tokenRepository.GetTokenAsync(new Guid(userId));
    }
}
