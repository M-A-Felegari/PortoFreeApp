
using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class EmploymentHistory : BaseEntity
{
    public virtual required User Owner { get; set; }
    public int OwnerId { get; set; }
    [MaxLength(50)]
    public string CompanyName { get; set; } = null!;
    [MaxLength(50)]
    public string? Position { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
}
