namespace PortoFree.Api.Dtos;

public class AddWorkExampleRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public IFormFile? Image { get; set; }
}