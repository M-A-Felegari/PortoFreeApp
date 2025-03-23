using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class WorkExample : BaseEntity
{
    [MaxLength(50)]
    public string Title { get; set; } = default!;
    [MaxLength(300)]
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public string? ImagePath { get; set; }
}
