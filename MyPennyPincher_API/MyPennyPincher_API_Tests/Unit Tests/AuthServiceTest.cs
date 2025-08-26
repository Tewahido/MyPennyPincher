using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API_Tests.Test_Utilities;
using NSubstitute;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class AuthServiceTest
{
    private readonly AuthService _authService;
    private readonly IAuthRepository _authRepository; 

    public AuthServiceTest() 
    {
        _authRepository = Substitute.For<IAuthRepository>();
        _authService = new AuthService(_authRepository);
    }

    [Fact]
    public async Task GIVEN_User_WHEN_Registering_THEN_ReturnNewUser()
    {
        //Arrange
        var user = TestDataFactory.CreateTestUser();

        _authRepository.FindByEmailAsync(user.Email)
            .Returns(Task.FromResult<User?>(null));

        //Act
        var registeredUser = await _authService.Register(user);

        var passwordIsHashed = BCrypt.Net.BCrypt.Verify(user.Password, registeredUser.Password);

        //Assert
        Assert.NotNull(registeredUser);
        Assert.True(passwordIsHashed);
    }

    [Fact]
    public async Task GIVEN_ExistingUser_WHEN_Registering_THEN_ReturnUserAlreadyExistsException()
    {
        //Arrange
        var user = TestDataFactory.CreateTestUser();

        _authRepository.FindByEmailAsync(user.Email)
            .Returns(user);

        //Act & Assert
        await Assert.ThrowsAsync<UserAlreadyExistsException>(() => _authService.Register(user));
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
    public async Task GIVEN_WeakPassword_WHEN_RegisteringUser_THEN_ThrowPasswordTooWeakException()
    {
        //Arrange
        var weakPasswordUser = new User
        {
            UserId = Guid.NewGuid(),
            Email = "test@gmail.com",
            Password = "testPassword",
            FullName = "Test User"
        };

        //Act & Assert
        await Assert.ThrowsAsync<PasswordTooWeakException>(() => _authService.Register(weakPasswordUser));
    }

    [Fact]
    public async Task GIVEN_ValidLoginDetails_WHEN_LoggingIn_THEN_ReturnAuthenticatedUser()
    {
        //Arrange
        var user = TestDataFactory.CreateTestUser();

        _authRepository.FindByEmailAsync(user.Email)
           .Returns(Task.FromResult<User?>(null));

        var registeredUser = await _authService.Register(user);

        var login = TestDataFactory.CreateUserLogin(user);

        _authRepository.FindByEmailAsync(login.Email)
            .Returns(registeredUser);

        //Act
        var expectedUser = await _authService.Login(login);

        //Assert
        Assert.NotNull(expectedUser);
        Assert.Equal(user.Email, expectedUser.Email);
    }

    [Fact]
    public async Task GIVEN_InvalidLoginDetails_WHEN_LoggingIn_THEN_ThrowInvalidCredentialsError()
    {
        //Arrange
        var user = TestDataFactory.CreateTestUser();

        await _authService.Register(user);

        var login = new Login
        {
            Email = "invalidEmail",
            Password = "invalidPassword"
        };

        //Act & Assert
        await Assert.ThrowsAsync<InvalidCredentialsException>(async () => await _authService.Login(login));
    }
}
