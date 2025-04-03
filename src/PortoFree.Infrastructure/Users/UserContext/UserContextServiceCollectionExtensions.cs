using Microsoft.Extensions.DependencyInjection;
using PortoFree.Application.Features.Users.UserContext;

namespace PortoFree.Infrastructure.Users.UserContext;

internal static class UserContextServiceCollectionExtensions
{
    public static void AddUserContextServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserContext,CurrentUserContext>();
    }
}