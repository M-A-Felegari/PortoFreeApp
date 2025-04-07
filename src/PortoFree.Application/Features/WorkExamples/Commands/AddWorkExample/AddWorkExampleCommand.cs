using MediatR;

namespace PortoFree.Application.Features.WorkExamples.Commands.AddWorkExample;

public class AddWorkExampleCommand : IRequest<int>
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
    public Stream? ImageFileStream { get; set; }
    public string? ImageFileName { get; set; }
}