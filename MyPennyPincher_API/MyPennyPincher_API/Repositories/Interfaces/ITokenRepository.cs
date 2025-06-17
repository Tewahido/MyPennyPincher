using MyPennyPincher_API.Models.DataModels;

namespace MyPennyPincher_API.Repositories.Interfaces;

public interface ITokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task DeleteAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetTokenAsync(Guid userId);
    Task SaveChangesAsync();
}
