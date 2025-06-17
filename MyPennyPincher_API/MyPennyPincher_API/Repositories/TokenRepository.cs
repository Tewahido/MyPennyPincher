using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories.Interfaces;

namespace MyPennyPincher_API.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly MyPennyPincherDbContext _context;

    public TokenRepository(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
    }

    public Task DeleteAsync(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Remove(refreshToken);
        return Task.CompletedTask;
    }

    public async Task<RefreshToken?> GetTokenAsync(Guid userId)
    {
        return await _context.RefreshTokens.FirstOrDefaultAsync(token => token.UserId == userId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
