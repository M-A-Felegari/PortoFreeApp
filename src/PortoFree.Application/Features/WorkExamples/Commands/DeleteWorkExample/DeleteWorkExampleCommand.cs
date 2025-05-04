using MediatR;

namespace PortoFree.Application.Features.WorkExamples.Commands.DeleteWorkExample;

public class DeleteWorkExampleCommand(int id) : IRequest
{
    public int Id { get; init; } = id;
}