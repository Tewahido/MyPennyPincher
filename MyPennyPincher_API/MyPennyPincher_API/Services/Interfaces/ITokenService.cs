using MyPennyPincher_API.Models;
using MyPennyPincher_API.Models.DTO;

namespace MyPennyPincher_API.Services.Interfaces;

public interface ITokenService
{
    RefreshToken GenerateRefreshToken(Guid userId);
    UserAccessToken GenerateAccessToken(Guid userId);
    Task AddRefreshToken(RefreshToken refreshToken);
    Task DeleteRefreshToken(string userId);
    Task<UserAccessToken?> RefreshToken(Guid userId, string refreshToken);
    CookieOptions CreateRefreshTokenCookieOptions(RefreshToken refreshToken);
}
