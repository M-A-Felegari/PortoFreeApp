namespace PortoFree.Application.Users.UserContext;

public interface ICurrentUserContext
{
    public CurrentUser? GetCurrentUser();
}