using Microsoft.EntityFrameworkCore;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;
using PortoFree.Infrastructure.Persistence;

namespace PortoFree.Infrastructure.Repositories;

internal class EmploymentHistoriesRepository : IEmploymentHistoriesRepository
{
    private readonly DataContext _context;

    public EmploymentHistoriesRepository(DataContext context)
    {
        _context = context;
    }

    //todo: think about make Pagination service to keep DRY
    public async Task<(IEnumerable<EmploymentHistory> comments, int? nextCursor)> GetAllBySeekPagination(
        int fromId,
        int limit)
    {
        var items = await _context.EmploymentHistories
            .Where(e => e.Id > fromId)
            .Take(limit + 1)
            .ToListAsync();

        var hasNextCursor = items.Count > limit;

        var employmentHistoriesResult = hasNextCursor ? items.Take(limit) : items;
        var nextCursor = hasNextCursor ? items.Last().Id : (int?) null;

        return (employmentHistoriesResult, nextCursor);
    }

    public async Task<EmploymentHistory?> GetAsync(int id)
    {
        var employmentHistory = await _context.EmploymentHistories
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        return employmentHistory;
    }

    public async Task<int> AddAsync(EmploymentHistory employmentHistory)
    {
        await _context.EmploymentHistories.AddAsync(employmentHistory);

        await _context.SaveChangesAsync();

        return employmentHistory.Id;
    }

    public async Task UpdateAsync(EmploymentHistory employmentHistory)
    {
        _context.Update(employmentHistory);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(EmploymentHistory employmentHistory)
    {
        _context.Remove(employmentHistory);

        await _context.SaveChangesAsync();
    }
}
