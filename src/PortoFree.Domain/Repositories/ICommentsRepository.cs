using PortoFree.Domain.Entities;

namespace PortoFree.Domain.Repositories;

interface ICommentsRepository
{
    public Task<(IEnumerable<Comment> comments, int? nextCursor)> GetAllBySeekPagination(
        int fromId,
        int limit
        );

    public Task<Comment?> GetAsync(int id);
    public Task<int> AddAsync(Comment newComment);
    public Task UpdateAsync(Comment comment);
    public Task DeleteAsync(Comment comment);
}
