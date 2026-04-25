using System.Security.Claims;
using ZeusApp.Application.Interfaces.Shared;

namespace ZeusApp.WebApi.Services;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
    }

    public string UserId { get; }
    public string Username { get; }
}
