using PortoFree.Domain.Entities;

namespace PortoFree.Domain.Repositories;

public interface IUsersRepository
{
    public Task<(IEnumerable<User> users, int totalUsersCount)> GetAllAsync(
        string? searchParams,
        int pageNumber,
        int pageSize
        );

    public Task<User?> GetByUsernameAsync(string name);
    public Task<User?> GetByIdAsync(int id);
}
