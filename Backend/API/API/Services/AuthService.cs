using System.Security.Claims;
using API.Data;
using API.Model.User;
using API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using static BCrypt.Net.BCrypt;

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
        var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginDto.Identifier.ToLower() || u.Email == loginDto.Identifier.ToLower());

        if (user == null)
        {
            throw new Exception("User not found");
        }

        if(!Verify(loginDto.Password, user.HashedPassword))
        {
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
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddSeconds(20),
                signingCredentials: credentials
            );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}