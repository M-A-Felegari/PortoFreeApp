using PortoFree.Domain.Entities;

namespace PortoFree.Application.Services.Auth;

public interface IResourceOwnershipAuthorization
{
    public void EnsureUserOwnsWorkExample(int userId, WorkExample workExample);
}