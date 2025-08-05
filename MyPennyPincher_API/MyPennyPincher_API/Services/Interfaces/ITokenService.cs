using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Models.DTO;

namespace MyPennyPincher_API.Services.Interfaces;

public interface ITokenService
{
    RefreshToken GenerateRefreshToken(Guid userId);
    UserAccessToken GenerateAccessToken(Guid userId, bool isVerified);
    Task AddRefreshToken(RefreshToken refreshToken);
    Task DeleteRefreshToken(string userId);
    Task<UserAccessToken?> RefreshToken(Guid userId, string refreshToken, bool isVerified);
    CookieOptions CreateRefreshTokenCookieOptions(RefreshToken refreshToken);
}
