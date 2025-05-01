namespace PortoFree.Application.Features.WorkExamples;

public class WorkExampleDto
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public string? ImagePath { get; set; }
}