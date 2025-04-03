namespace PortoFree.Application.Users.UserContext;

public record CurrentUser(int Id, string Username, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
};