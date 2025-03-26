using Microsoft.EntityFrameworkCore;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;
using PortoFree.Infrastructure.Persistence;

namespace PortoFree.Infrastructure.Repositories;

internal class UserSkillsRepository : IUserSkillsRepository
{
    private readonly DataContext _context;

    public UserSkillsRepository(DataContext context)
    {
        _context = context;
    }

    //todo: think about make Pagination service to keep DRY
    public async Task<(IEnumerable<UserSkill> userSkills, int? nextCursor)> GetAllBySeekPaginationAsync(
        int fromId,
        int limit)
    {
        var items = await _context.UserSkills
            .Where(u => u.Id > fromId)
            .Take(limit + 1)
            .ToListAsync();

        var hasNextCursor = items.Count > limit;

        var userSkillsResult = hasNextCursor ? items.Take(limit) : items;
        var nextCursor = hasNextCursor ? items.Last().Id : (int?) null;

        return (userSkillsResult, nextCursor);
    }

    public async Task<UserSkill?> GetAsync(int id)
    {
        var userSkill = await _context.UserSkills
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return userSkill;
    }

    public async Task<int> AddAsync(UserSkill newUserSkill)
    {
        await _context.UserSkills.AddAsync(newUserSkill);

        await _context.SaveChangesAsync();

        return newUserSkill.Id;
    }

    public async Task UpdateAsync(UserSkill userSkill)
    {
        _context.UserSkills.Update(userSkill);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserSkill userSkill)
    {
        _context.UserSkills.Remove(userSkill);

        await _context.SaveChangesAsync();
    }
}
