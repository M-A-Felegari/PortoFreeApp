using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class Comment : BaseEntity
{
    [MaxLength(50)]
    public string SenderName { get; set; } = default!;
    [MaxLength(50)]
    public string? SenderCompany { get; set; }
    [MaxLength(50)]
    public string? SenderPositon { get; set; }
    [MaxLength(500)]
    public string Text { get; set; } = default!;
    public DateTime? Date { get; set; }
    [MaxLength(100)]
    public string? CommentUri { get; set; }
}
