using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IAuthRepository
{
    public void AddUser(User user);
    public User FindUserByEmail(string email);
}
