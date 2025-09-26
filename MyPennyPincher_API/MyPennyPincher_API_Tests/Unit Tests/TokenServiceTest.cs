using Microsoft.Extensions.Configuration;
using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.ConfigModels;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using NSubstitute;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class TokenServiceTest
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IConfiguration _config;
    private readonly ITokenService _tokenService;
    private readonly JwtOptions _jwtOptions;

    public TokenServiceTest()
    {
        var configData = new Dictionary<string, string?>
        {
            { "Jwt:Key", "X2s#9f!zLq@84hT%vG^7Rb*eWkP$JmN+ZcA!uYdE6rOi$0MbTpLgVsWx1QdHzFnCy" },
            { "Jwt:Issuer", "https://localhost:7053" },
            { "Jwt:TokenValidityMins", "15" }
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();

        _jwtOptions = _config.GetSection("Jwt").Get<JwtOptions>()!;

        _tokenRepository = Substitute.For<ITokenRepository>();

        _tokenService = new TokenService(_config, _tokenRepository, _jwtOptions);
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

        _tokenRepository.GetTokenAsync(userId).Returns(generatedToken);

        //Act
        var refreshedToken = await _tokenService.RefreshToken(userId, generatedToken.Token);

        var handler = new JwtSecurityTokenHandler();
        var readToken = handler.ReadJwtToken(refreshedToken!.Token);

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

        _tokenRepository.GetTokenAsync(userId).Returns(generatedToken);

        await _tokenService.AddRefreshToken(generatedToken);

        //Act & Assert
        await Assert.ThrowsAsync<InvalidRefreshTokenException>(async () => await _tokenService.RefreshToken(userId, "invalidToken"));
    }
}
