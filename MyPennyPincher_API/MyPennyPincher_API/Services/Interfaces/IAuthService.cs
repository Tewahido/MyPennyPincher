using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;

namespace MyPennyPincher_API.Services.Interfaces;

public interface IAuthService
{
    Task<User> Register(User user);
    Task<User> Login(Login login);
}
