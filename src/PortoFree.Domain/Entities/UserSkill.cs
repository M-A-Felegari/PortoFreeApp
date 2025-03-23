namespace PortoFree.Domain.Entities;

public class UserSkill : BaseEntity
{
    public int UserId { get; set; }
    public required virtual Skill Skill { get; set; }
    public int SkillId { get; set; }
    public ushort Level { get; set; }

}
