using System.Security.Claims;
using API.Data;
using API.Model.User;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;

    public AuthService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<string> Login(LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Identifier || u.Email == loginDto.Identifier);

        if (user == null)
        {
            Console.WriteLine("LoginDto: User not found");
            throw new Exception("User not found");
        }

        if (user.HashedPassword != loginDto.Password.Trim())
        {
            Console.WriteLine("Password doesn't match");
            throw new Exception("Wrong password");
        }


        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        };

        var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
        var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        var secret = Environment.GetEnvironmentVariable("JWT_KEY");

        if (string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(audience) || string.IsNullOrEmpty(issuer))
        {
            throw new Exception("JTW configuration missing");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddSeconds(20),
                signingCredentials: creds
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}