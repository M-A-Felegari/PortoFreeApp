using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PortoFree.Application.Features.Users.UserContext;

namespace PortoFree.Infrastructure.Users.UserContext;

internal class CurrentUserContext : ICurrentUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public CurrentUser? GetCurrentUser()
    {
        var user = _httpContextAccessor.HttpContext?.User ??
                   throw new InvalidOperationException("HttpContext is not available.");

        if (user.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var username = user.FindFirst(c => c.Type == ClaimTypes.Name)!.Value;
        var roles = user.Claims.Where(c=>c.Type == ClaimTypes.Role).Select(c=>c.Value);
        
        return new CurrentUser(Convert.ToInt32(userId), username, roles);
    }
}