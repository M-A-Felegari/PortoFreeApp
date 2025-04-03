namespace PortoFree.Application.Features.Users.UserContext;

public interface ICurrentUserContext
{
    public CurrentUser? GetCurrentUser();
}