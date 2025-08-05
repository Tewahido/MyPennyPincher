using Microsoft.Extensions.Caching.Distributed;
using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly IDistributedCache _distributedCache;

    public AuthService(IAuthRepository authRepository, IDistributedCache distributedCache) 
    {  
        _authRepository = authRepository;
        _distributedCache = distributedCache;
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

        var isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

        if (!isValid)
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        return user;
    }

    public async Task VerifyUser(UserAccessToken userAccessToken)
    {
        var userId = userAccessToken.UserId.ToString();
        var storedToken = System.Text.Encoding.UTF8.GetString(_distributedCache.Get(userId)!);

        if (storedToken == null || storedToken != userAccessToken.Token) 
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        var userToVerify = await _authRepository.FindByIdAsync(userId);

        if (userToVerify == null) 
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        userToVerify.Verify();

        await _authRepository.SaveChangesAsync();

        _distributedCache.Remove(userId);
    }
}
