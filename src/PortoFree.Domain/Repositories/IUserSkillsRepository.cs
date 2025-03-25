using PortoFree.Domain.Entities;

namespace PortoFree.Domain.Repositories;

interface IUserSkillsRepository
{
    public Task<(IEnumerable<UserSkill> userSkills, int? nextCursor)> GetAllBySeekPaginationAsync(
        int fromId,
        int limit);

    public Task<UserSkill?> GetAsync(int id);

    public Task<int> AddAsync(UserSkill newUserSkill);
    public Task UpdateAsync(UserSkill userSkill);
    public Task DeleteAsync(UserSkill userSkill);
}
