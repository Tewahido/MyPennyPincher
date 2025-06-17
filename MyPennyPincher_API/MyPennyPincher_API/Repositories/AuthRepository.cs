using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
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

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
