using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API_Tests.Test_Utilities;

namespace MyPennyPincher_API_Tests;

public class AuthServiceTest : IDisposable
{
    private readonly AuthService _authService;
    private readonly MyPennyPincherDbContext _context;
    private readonly IAuthRepository _authRepository;    
    private readonly IConfiguration _config;

    public AuthServiceTest() 
    {
        var configData = new Dictionary<string, string>
        {
            { "Jwt:Key", "X2s#9f!zLq@84hT%vG^7Rb*eWkP$JmN+ZcA!uYdE6rOi$0MbTpLgVsWx1QdHzFnCy" },
            { "Jwt:Issuer", "https://localhost:7053" },
            { "Jwt:TokenValidityHrs", "1" }
        };

        _config = new ConfigurationBuilder()
            .AddInMemoryCollection(configData)
            .Build();

        _context = TestUtils.GenerateInMemoryDB();

        _authRepository = new AuthRepository(_context);

        _authService = new AuthService(_authRepository, _config);
    }

    [Fact]
    public async Task GIVEN_User_WHEN_Registering_THEN_ReturnNewUser()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = "test@email.com",
            Password = "password"
        };

        //Act
        User registeredUser = await _authService.Register(user);

        bool passwordIsHashed = BCrypt.Net.BCrypt.Verify(user.Password, registeredUser.Password);

        User expectedUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);

        //Assert
        Assert.NotNull(registeredUser);
        Assert.True(passwordIsHashed);
        Assert.NotNull(expectedUser);
    }

    [Fact]
    public async Task GIVEN_NullUser_WHEN_Registering_THEN_ThrowNullArgumentException()
    {
        //Arrange
        User nullUser = null;

        //Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _authService.Register(nullUser));
    }

    [Fact]
    public async Task GIVEN_ValidLoginDetails_WHEN_LoggingIn_THEN_ReturnAuthenticatedUser()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = "test@email.com",
            Password = "password",
        };

        User registeredUser = await _authService.Register(user);

        Login login = new Login
        {
            Email = user.Email,
            Password = user.Password
        };

        //Act
        User expectedUser = await _authService.Login(login);

        //Assert
        Assert.NotNull(expectedUser);
        Assert.Equal(user.Email, expectedUser.Email);
    }

    [Fact]
    public async Task GIVEN_InvalidLoginDetails_WHEN_LoggingIn_THEN_ReturnNull()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = "test@email.com",
            Password = "password",
        };

        await _authService.Register(user);

        Login login = new Login
        {
            Email = "invalidEmail",
            Password = "invalidPassword"
        };

        //Act
        User? expectedUser = await _authService.Login(login);

        //Assert
        Assert.Null(expectedUser);
    }

    [Fact]
    public void GIVEN_LoggedInUser_WHEN_GeneratingToken_THEN_ReturnNewJWT()
    {
        //Arrange
        User user = new User
        {
            UserId = Guid.NewGuid(),
            FullName = "Test User",
            Email = "test@email.com",
            Password = "password",
        };

        //Act
        var token = _authService.GenerateToken(user);

        var handler = new JwtSecurityTokenHandler();
        var readToken = handler.ReadJwtToken(token);

        //Assert
        Assert.Contains(readToken.Claims, claim => claim.Type == ClaimTypes.NameIdentifier && claim.Value == user.UserId.ToString());
        Assert.Contains(readToken.Claims, claim => claim.Type == ClaimTypes.Name && claim.Value == user.FullName);

        Assert.Equal(_config["Jwt:Issuer"], readToken.Issuer);
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
