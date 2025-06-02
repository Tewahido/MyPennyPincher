using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services.Interfaces;

public interface ITokenService
{
    RefreshToken GenerateRefreshToken(User user);
    string GenerateAccessToken(Guid userId);
    Task AddRefreshToken(RefreshToken refreshToken);
    Task DeleteRefreshToken(string userId);
    Task<bool> ValidateToken(Guid userId, string token);
}
