using PortoFree.Domain.Entities;

namespace PortoFree.Domain.Repositories;

interface IWorkExamplesRepository
{
    public Task<(IEnumerable<WorkExample>, int? nextCursor)> GetAllBySeekPagination(
        int fromId,
        int limit);

    public Task<WorkExample?> GetAsync(int id);
    public Task<int> AddAsync(WorkExample newWorkExample);
    public Task UpdateAsync(WorkExample workExample);
    public Task DeleteAsync(WorkExample workExample);
}
