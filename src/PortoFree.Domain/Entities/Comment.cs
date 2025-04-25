using System.ComponentModel.DataAnnotations;

namespace PortoFree.Domain.Entities;

public class Comment : BaseEntity
{
    public virtual required User Owner { get; set; }
    public int OwnerId { get; set; }
    [MaxLength(50)]
    public string SenderName { get; set; } = null!;
    [MaxLength(50)]
    public string? SenderCompany { get; set; }
    [MaxLength(50)]
    public string? SenderPosition { get; set; }
    [MaxLength(500)]
    public string Text { get; set; } = null!;
    public DateTime? Date { get; set; }
    [MaxLength(100)]
    public string? CommentUri { get; set; }
}
