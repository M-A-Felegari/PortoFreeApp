using PortoFree.Application.Exceptions;
using PortoFree.Domain.Entities;

namespace PortoFree.Application.Services.Auth;

public class ResourceOwnershipAuthorization : IResourceOwnershipAuthorization
{
    public void EnsureUserOwnsWorkExample(int userId, WorkExample workExample)
    {
        if (workExample.OwnerId != userId)
            throw new ForbiddenException("You do not have access to this resource.");
    }
}