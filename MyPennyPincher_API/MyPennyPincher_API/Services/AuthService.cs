using Microsoft.Extensions.Caching.Memory;
using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IMemoryCache _memoryCache;

    public AuthService(IAuthRepository authRepository, IMemoryCache memoryCache) 
    {  
        _authRepository = authRepository;
        _memoryCache = memoryCache;
    }

    public async Task<User> Register(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException();
        }

        var existingUser = await _authRepository.FindByEmailAsync(user.Email);

        if (existingUser != null)
        {
            throw new UserAlreadyExistsException("A user with this email already exists.");
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var newUser = new User
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            Password = hashedPassword,
        };

        await _authRepository.AddAsync(newUser);

        await _authRepository.SaveChangesAsync();

        return newUser;
    }

    public async Task<User> Login(Login login)
    {
        var user = await _authRepository.FindByEmailAsync(login.Email);

        if (user == null) 
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

        if (!isValid)
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        return user;
    }

    public async Task VerifyUser(UserAccessToken userAccessToken)
    {
        var storedToken = _memoryCache.Get(userAccessToken.UserId)?.ToString();

        if (storedToken == null || storedToken != userAccessToken.Token) 
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        var userToVerify = await _authRepository.FindByIdAsync(userAccessToken.UserId.ToString());

        if (userToVerify == null) 
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        userToVerify.IsVerified = true;

        _memoryCache.Remove(userAccessToken.UserId);

        await _authRepository.SaveChangesAsync();
    }
}
