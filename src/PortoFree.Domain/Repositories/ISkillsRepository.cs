using PortoFree.Domain.Entities;

namespace PortoFree.Domain.Repositories;

public interface ISkillsRepository
{
    public Task<(IEnumerable<Skill> skills, int totalSkills)> GetAllAsync(
        string? searchParams,
        int pageNumber,
        int pageSize);

    public Task<Skill?> GetAsync(int id);
    public Task<int> AddAsync(Skill newSkill);
    public Task UpdateAsync(Skill skill);
    public Task DeleteAsync(Skill skill);
}
