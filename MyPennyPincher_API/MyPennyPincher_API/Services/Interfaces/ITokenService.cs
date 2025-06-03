using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services.Interfaces;

public interface ITokenService
{
    RefreshToken GenerateRefreshToken(Guid userId);
    string GenerateAccessToken(Guid userId);
    Task AddRefreshToken(RefreshToken refreshToken);
    Task DeleteRefreshToken(string userId);
    Task<string?> RefreshToken(Guid userId, string refreshToken);
}
