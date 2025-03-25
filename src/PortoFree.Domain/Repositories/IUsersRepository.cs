using PortoFree.Domain.Entities;

namespace PortoFree.Domain.Repositories;

interface IUsersRepository
{
    public Task<(IEnumerable<User> users, int totalPages)> GetAllAsync(
        string? searchParams,
        int pageNumber,
        int pageSize
        );

    public Task<User?> GetByUsernameAsync(string name);
    public Task<User?> GetByIdAsync(int id);
}
