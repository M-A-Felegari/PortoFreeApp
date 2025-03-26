using Microsoft.EntityFrameworkCore;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;
using PortoFree.Infrastructure.Persistence;

namespace PortoFree.Infrastructure.Repositories;

internal class WorkExamplesRepository : IWorkExamplesRepository
{
    private readonly DataContext _context;

    public WorkExamplesRepository(DataContext context)
    {
        _context = context;
    }

    //todo: think about make Pagination service to keep DRY
    public async Task<(IEnumerable<WorkExample> workExamples, int? nextCursor)> GetAllBySeekPagination(
        int fromId,
        int limit)
    {
        var items = await _context.WorkExamples
            .Where(w => w.Id > fromId)
            .Take(limit + 1)
            .ToListAsync();

        var hasNextCursor = items.Count > limit;

        var workExamplesResult = hasNextCursor ? items.Take(limit) : items;
        var nextCursor = hasNextCursor ? items.Last().Id : (int?)null;

        return (workExamplesResult, nextCursor);
    }

    public async Task<WorkExample?> GetAsync(int id)
    {
        var workExample = await _context.WorkExamples
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.Id == id);

        return workExample;
    }

    public async Task<int> AddAsync(WorkExample newWorkExample)
    {
        await _context.WorkExamples.AddAsync(newWorkExample);

        await _context.SaveChangesAsync();

        return newWorkExample.Id;
    }

    public async Task UpdateAsync(WorkExample workExample)
    {
        _context.WorkExamples.Update(workExample);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(WorkExample workExample)
    {
        _context.WorkExamples.Remove(workExample);

        await _context.SaveChangesAsync();
    }
}
