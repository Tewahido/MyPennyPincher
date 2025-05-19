using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyPennyPincher_API.Context;
using MyPennyPincher_API.Models;

namespace MyPennyPincher_API.Services;

public class AuthService
{
    private readonly MyPennyPincherDbContext _context;
    private readonly IConfiguration _config;

    public AuthService(MyPennyPincherDbContext context, IConfiguration config) 
    {  
        _context = context;
        _config = config;
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

    public string GenerateToken(User user)
    {

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var userId = user.UserId.ToString();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, user.FullName)
        };

        var tokenValidityTime = DateTime.Now.AddHours(_config.GetValue<double>("Jwt:TokenValidityHrs"));

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Issuer"],
            claims: claims,
            expires: tokenValidityTime,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
