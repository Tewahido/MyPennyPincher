using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services;

public class AuthService
{
    private readonly MyPennyPincherDbContext _context;

    public AuthService(MyPennyPincherDbContext context) 
    {  
        _context = context; 
    }
    
    public async Task<User> Register(User user)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

        var newUser = new User
        {
            FullName = user.FullName,
            Email = user.Email,
            Password = hashedPassword,
            Expenses = user.Expenses,
            Incomes = user.Incomes,
        };

        _context.Users.Add(newUser);

        await _context.SaveChangesAsync();

        return newUser;
    }

    public async Task<User?> Login(string email, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null) 
        {
            return null;
        }

        bool isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

        if (!isValid)
        {
            return null;
        }

        return user;
    }


}
