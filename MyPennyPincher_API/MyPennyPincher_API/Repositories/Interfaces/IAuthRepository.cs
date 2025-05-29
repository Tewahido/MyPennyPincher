using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface IAuthRepository
{
    Task AddAsync(User user);
    Task<User?> FindByEmailAsync(string email);
    Task SaveChangesAsync();

}
