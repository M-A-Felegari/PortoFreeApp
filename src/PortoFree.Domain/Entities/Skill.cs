using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class Skill : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    [MaxLength(100)]
    public string ImagePath { get; set; } = null!;
    public bool IsSharableSkill { get; set; }
    public int ReferenceCount { get; set; }
}
