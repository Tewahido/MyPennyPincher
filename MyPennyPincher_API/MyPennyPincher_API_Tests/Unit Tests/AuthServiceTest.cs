using Microsoft.Extensions.Caching.Distributed;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Repositories;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services;
using MyPennyPincher_API.Services.Interfaces;
using MyPennyPincher_API_Tests.Test_Utilities;
using NSubstitute;

namespace MyPennyPincher_API_Tests.Unit_Tests;

public class AuthServiceTest
{
    private readonly IAuthService _authService;
    private readonly IAuthRepository _authRepository; 
    private readonly IDistributedCache _distributedCache;

    public AuthServiceTest() 
    {
        _authRepository = Substitute.For<IAuthRepository>();
        _distributedCache = Substitute.For<IDistributedCache>();
        _authService = new AuthService(_authRepository,_distributedCache);
    }

    [Fact]
    public async Task GIVEN_User_WHEN_Registering_THEN_ReturnNewUser()
    {
        //Arrange
        User user = TestDataFactory.CreateTestUser();

        //Act
        User registeredUser = await _authService.Register(user);

        bool passwordIsHashed = BCrypt.Net.BCrypt.Verify(user.Password, registeredUser.Password);

        //Assert
        Assert.True(passwordIsHashed);
        await _authRepository.Received().AddAsync(Arg.Is<User>(u => u.Email == user.Email && u.FullName == user.FullName && u.UserId == user.UserId));
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
        Login login = TestDataFactory.CreateUserLogin(user);

        _authRepository.FindByEmailAsync(login.Email)
            .Returns(new User
            {
                Email = user.Email,
                FullName = user.FullName,
                UserId = user.UserId,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
            });

        //Act
        var expectedUser = await _authService.Login(login);

        //Assert
        await _authRepository.Received().FindByEmailAsync(login.Email);

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
}
