using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IAuthService
{
    Task<User> Register(User user);
    Task<User?> Login(Login login);
    string GenerateToken(User user);
}
