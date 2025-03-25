using PortoFree.Domain.Entities;

namespace PortoFree.Domain.Repositories;

interface IEmploymentHistoriesRepository
{
    public Task<(IEnumerable<EmploymentHistory> comments, int? nextCursor)> GetAllBySeekPagination(
        int fromId,
        int limit);

    public Task<EmploymentHistory?> GetAsync(int id);

    public Task<int> AddAsync(EmploymentHistory employmentHistory);
    public Task UpdateAsync(EmploymentHistory employmentHistory);
    public Task DeleteAsync(EmploymentHistory employmentHistory);
}
