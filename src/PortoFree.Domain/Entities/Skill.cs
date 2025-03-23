using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class Skill : BaseEntity
{
    [MaxLength(50)]
    public string Name { get; set; } = default!;
    public string ImagePath { get; set; } = default!;
    public bool IsSharableSkill { get; set; }
    public int ReferenceCount { get; set; }
}
