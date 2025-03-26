using Microsoft.EntityFrameworkCore;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;
using PortoFree.Infrastructure.Persistence;

namespace PortoFree.Infrastructure.Repositories;

internal class CommentsRepository : ICommentsRepository
{
    private readonly DataContext _context;

    public CommentsRepository(DataContext context)
    {
        _context = context;
    }

    //todo: think about make Pagination service to keep DRY
    public async Task<(IEnumerable<Comment> comments, int? nextCursor)> GetAllBySeekPagination(
        int fromId,
        int limit)
    {
        var items = await _context.Comments
            .Where(c => c.Id > fromId)
            .Take(limit + 1)
            .ToListAsync();

        var hasNextCursor = items.Count > limit;

        var commentsResult = hasNextCursor ? items.Take(limit) : items;
        var nextCursor = hasNextCursor ? items.Last().Id : (int?)null;

        return (commentsResult, nextCursor);
    }

    public async Task<Comment?> GetAsync(int id)
    {
        var comment = await _context.Comments
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        return comment;
    }

    public async Task<int> AddAsync(Comment newComment)
    {
        await _context.Comments.AddAsync(newComment);

        await _context.SaveChangesAsync();

        return newComment.Id;
    }

    public async Task UpdateAsync(Comment comment)
    {
        _context.Comments.Update(comment);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Comment comment)
    {
        _context.Comments.Remove(comment);

        await _context.SaveChangesAsync();
    }
}
