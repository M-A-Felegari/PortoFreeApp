using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class WorkExample : BaseEntity
{
    public virtual required User Owner { get; set; }
    public int OwnerId { get; set; }
    [MaxLength(50)]
    public string Title { get; set; } = null!;
    [MaxLength(300)]
    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    [MaxLength(100)]
    public string? ImagePath { get; set; }
}
