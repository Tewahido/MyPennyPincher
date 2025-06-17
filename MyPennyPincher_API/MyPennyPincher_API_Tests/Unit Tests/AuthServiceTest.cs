using MyPennyPincher_API.Context;
using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
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

    public AuthServiceTest() 
    {
        _context = DbContextFactory.GenerateInMemoryDB();
        _authRepository = new AuthRepository(_context);
        _authService = new AuthService(_authRepository);
    }

    [Fact]
    public async Task GIVEN_User_WHEN_Registering_THEN_ReturnNewUser()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        //Act
        User registeredUser = await _authService.Register(user);

        bool passwordIsHashed = BCrypt.Net.BCrypt.Verify(user.Password, registeredUser.Password);

        var expectedUser = _context.Users.FirstOrDefault(u => u.UserId == user.UserId);

        //Assert
        Assert.NotNull(registeredUser);
        Assert.True(passwordIsHashed);
        Assert.NotNull(expectedUser);
    }

    [Fact]
    public async Task GIVEN_NullUser_WHEN_Registering_THEN_ThrowNullArgumentException()
    {
        //Arrange
        User? nullUser = null;

        //Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => _authService.Register(nullUser!));
    }

    [Fact]
    public async Task GIVEN_ValidLoginDetails_WHEN_LoggingIn_THEN_ReturnAuthenticatedUser()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        User registeredUser = await _authService.Register(user);

        Login login = TestDataFactory.CreateUserLogin(user);

        //Act
        var expectedUser = await _authService.Login(login);

        //Assert
        Assert.NotNull(expectedUser);
        Assert.Equal(user.Email, expectedUser.Email);
    }

    [Fact]
    public async Task GIVEN_InvalidLoginDetails_WHEN_LoggingIn_THEN_ReturnNull()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        await _authService.Register(user);

        Login login = new Login
        {
            Email = "invalidEmail",
            Password = "invalidPassword"
        };

        //Act & Assert
        await Assert.ThrowsAsync<InvalidCredentialsException>(async () => await _authService.Login(login));
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
