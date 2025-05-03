using MediatR;

namespace PortoFree.Application.Features.WorkExamples.Commands.UpdateWorkExample;

public class UpdateWorkExampleCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? FinishDate { get; set; }
}