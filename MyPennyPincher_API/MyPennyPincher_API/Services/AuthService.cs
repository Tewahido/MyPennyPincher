using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;

namespace MyPennyPincher_API.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IAuthRepository authRepository, ITokenService tokenService) 
    {  
        _authRepository = authRepository;
        _tokenService = tokenService;
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
            throw new InvalidOperationException("A user with this email already exists.");
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

    public async Task<User?> Login(Login login)
    {
        var user = await _authRepository.FindByEmailAsync(login.Email);

        if (user == null) 
        {
            return null;
        }

        bool isValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

        if (!isValid)
        {
            return null;
        }

        var existingToken = await _tokenService.GetUserToken(user);

        if (existingToken != null)
        {
            await _tokenService.DeleteRefreshToken(existingToken);
        }

        var refreshToken = _tokenService.GenerateRefreshToken(user);

        await _tokenService.AddRefreshToken(refreshToken);

        return user;
    }

 
    public async Task<string?> RefreshToken(Guid userId, string refreshToken)
    {
        bool tokenIsValid = await _tokenService.ValidateToken(userId, refreshToken);

        if (tokenIsValid) 
        {
            return _tokenService.GenerateAccessToken(userId);
        }

        return null;
    }
}
