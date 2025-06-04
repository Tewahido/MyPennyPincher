using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.Test_Utilities;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class TokenServiceTest : IDisposable
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _config;
    private readonly MyPennyPincherDbContext _context;
    private readonly ITokenService _tokenService;

    public TokenServiceTest()
    {
        var configData = new Dictionary<string, string>
        {
            { "Jwt:Key", "X2s#9f!zLq@84hT%vG^7Rb*eWkP$JmN+ZcA!uYdE6rOi$0MbTpLgVsWx1QdHzFnCy" },
            { "Jwt:Issuer", "https://localhost:7053" },
            { "Jwt:TokenValidityMins", "15" }
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();

        _context = DbContextFactory.GenerateInMemoryDB();

        _tokenRepository = new TokenRepository(_context);

        _tokenService = new TokenService(_config, _tokenRepository);
    }

    [Fact]
    public void GIVEN_UserId_WHEN_GeneratingRefreshToken_THEN_ReturnRefreshToken()
    {
        //Arrange
        var userId = Guid.NewGuid();

        //Act
        var generatedToken = _tokenService.GenerateRefreshToken(userId);

        //Assert
        Assert.Equal(generatedToken.UserId, userId);
        Assert.IsType<RefreshToken>(generatedToken);
    }
    
    [Fact]
    public void GIVEN_UserId_WHEN_GeneratingAccessToken_THEN_ReturnNewJWT()
    {
        //Arrange
        var userId = Guid.NewGuid();

        //Act
        var token = _tokenService.GenerateAccessToken(userId);

        var handler = new JwtSecurityTokenHandler();
        var readToken = handler.ReadJwtToken(token.Token);

        //Assert
        Assert.Contains(readToken.Claims, claim => claim.Type == ClaimTypes.NameIdentifier && claim.Value == userId.ToString());

        Assert.Equal(_config["Jwt:Issuer"], readToken.Issuer);
    }

    [Fact]
    public async Task GIVEN_UserIdAndValidRefreshToken_WHEN_RefreshingAccessToken_THEN_ReturnNewAccessToken()
    {
        //Arrange
        var userId = Guid.NewGuid();

        var generatedToken = _tokenService.GenerateRefreshToken(userId);

        await _tokenService.AddRefreshToken(generatedToken);

        //Act
        var refreshedToken = await _tokenService.RefreshToken(userId, generatedToken.Token);

        var handler = new JwtSecurityTokenHandler();
        var readToken = handler.ReadJwtToken(refreshedToken.Token);

        //Assert
        Assert.Contains(readToken.Claims, claim => claim.Type == ClaimTypes.NameIdentifier && claim.Value == userId.ToString());
        Assert.Equal(_config["Jwt:Issuer"], readToken.Issuer);
    }

    [Fact]
    public async Task GIVEN_UserIdAndInvalidRefreshToken_WHEN_RefreshingAccessToken_THEN_ReturnNull()
    {
        //Arrange
        var userId = Guid.NewGuid();
        var generatedToken = _tokenService.GenerateRefreshToken(userId);

        await _tokenService.AddRefreshToken(generatedToken);

        //Act & Assert
        await Assert.ThrowsAsync<InvalidRefreshTokenException>(async () => await _tokenService.RefreshToken(userId, "invalidToken"));
    }

    [Fact]
    public async Task GIVEN_NewRefreshToken_WHEN_AddingRefreshToken_THEN_AddRefreshTokenToDb()
    {
        //Arrange
        var userId = Guid.NewGuid();

        var refreshToken = new RefreshToken
        {
            Token = "token",
            UserId = userId,
            ExpiryDate = DateTime.UtcNow,
        };

        //Act
        await _tokenService.AddRefreshToken(refreshToken);
        
        var expectedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId == userId && token.Token == refreshToken.Token);

        //Assert
        Assert.NotNull(expectedRefreshToken);
        Assert.Equal(expectedRefreshToken, refreshToken);
    }

    [Fact]
    public async Task GIVEN_ExistingRefreshToken_WHEN_DeletingRefreshToken_THEN_RemoveRefreshTokenFromDb()
    {
        //Arrange
        var userId = Guid.NewGuid();

        var refreshToken = new RefreshToken
        {
            Token = "token",
            UserId = userId,
            ExpiryDate = DateTime.UtcNow,
        };

        await _tokenService.AddRefreshToken(refreshToken);
        
        //Act
        await _tokenService.DeleteRefreshToken(userId.ToString());

        var expectedRefreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId == userId && token.Token == refreshToken.Token);

        //Assert
        Assert.Null(expectedRefreshToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
