using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Exceptions;
using MyPennyPincher_API.Models.DataModels;
using MyPennyPincher_API.Repositories.Interfaces;

namespace MyPennyPincher_API.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly MyPennyPincherDbContext _context;

    public AuthRepository(MyPennyPincherDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
       return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> FindByIdAsync(string userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserId == new Guid(userId));
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task VerifyUser(string userId)
    {
        var userToVerify = await FindByIdAsync(userId);

        if (userToVerify == null)
        {
            throw new InvalidCredentialsException("Invalid user credentials");
        }

        userToVerify.IsVerified = true;
    }
}
