using System.Security.Claims;
using API.Data;
using API.Services.Interfaces;

namespace API.Services;

public class UserContextService : IUserContextService
{

    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (int.TryParse(userId, out int parsedUserId))
        {
            return parsedUserId;
        }

        throw new UnauthorizedAccessException();
    }
}