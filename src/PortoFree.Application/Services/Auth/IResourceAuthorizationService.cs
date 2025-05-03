using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Domain.Entities;

namespace PortoFree.Application.Services.Auth;

public interface IResourceAuthorizationService
{
    public void EnsureUserCanEditWorkExample(CurrentUser user, WorkExample workExample);
    public void EnsureUserCanDeleteWorkExample(CurrentUser user, WorkExample workExample);
}