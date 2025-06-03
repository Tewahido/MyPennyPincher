using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IAuthService
{
    Task<User> Register(User user);
    Task<User?> Login(Login login);
}
