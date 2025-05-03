using PortoFree.Application.Exceptions;
using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Domain.Constants;
using PortoFree.Domain.Entities;

namespace PortoFree.Application.Services.Auth;

public class ResourceAuthorizationService : IResourceAuthorizationService
{
    public void EnsureUserCanEditWorkExample(CurrentUser user, WorkExample workExample)
    {
        if (workExample.OwnerId != user.Id)
            throw new ForbiddenException("You do not have access to this resource.");
    }

    public void EnsureUserCanDeleteWorkExample(CurrentUser user, WorkExample workExample)
    {
        var isAdmin = user.Roles.Contains(UserRoles.Admin);
        
        if (workExample.OwnerId != user.Id && !isAdmin)
            throw new ForbiddenException("You do not have access to this resource.");
    }
}