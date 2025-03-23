
using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class EmploymentHistory : BaseEntity
{
    [MaxLength(50)]
    public string CompanyName { get; set; } = default!;
    [MaxLength(50)]
    public string? Position { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
}
