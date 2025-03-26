using Microsoft.EntityFrameworkCore;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;
using PortoFree.Infrastructure.Persistence;

namespace PortoFree.Infrastructure.Repositories;

internal class SkillsRepository : ISkillsRepository
{
    private readonly DataContext _context;

    public SkillsRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<(IEnumerable<Skill> skills, int totalSkillsCount)> GetAllAsync(
        string? searchParams,
        int pageNumber,
        int pageSize)
    {
        var baseQuery = _context.Skills
            .Where(s => searchParams == null || s.Name.Contains(searchParams));

        var totalSkillsCount = baseQuery.Count();

        baseQuery = baseQuery
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize);

        var skills = await baseQuery.ToListAsync();

        return (skills, totalSkillsCount);

    }

    public async Task<Skill?> GetAsync(int id)
    {
        var skill = await _context.Skills
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id);

        return skill;
    }

    public async Task<int> AddAsync(Skill newSkill)
    {
        await _context.Skills.AddAsync(newSkill);

        await _context.SaveChangesAsync();

        return newSkill.Id;
    }

    public async Task UpdateAsync(Skill skill)
    {
        _context.Skills.Update(skill);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Skill skill)
    {
        _context.Skills.Remove(skill);

        await _context.SaveChangesAsync();
    }
}
