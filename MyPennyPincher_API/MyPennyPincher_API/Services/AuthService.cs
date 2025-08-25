using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;
using MyPennyPincher_API.Repositories.Interfaces;
using MyPennyPincher_API.Services.Interfaces;
using System.Text.RegularExpressions;

namespace MyPennyPincher_API.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository) 
    {  
        _authRepository = authRepository;
    }

    public async Task<User> Register(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException();
        }

        if (!PasswordIsValid(user.Password))
        {
            throw new PasswordTooWeakException();
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
 
    private bool PasswordIsValid(string password)
    {
        var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        return passwordRegex.IsMatch(password);
    }
}
