using API.Model.User;

namespace API.Services.Interfaces;

public interface IAuthService
{
    Task<string> Login(LoginDto loginDto);
}