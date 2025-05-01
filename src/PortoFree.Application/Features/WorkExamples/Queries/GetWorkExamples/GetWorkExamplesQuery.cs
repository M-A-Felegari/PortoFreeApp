using MediatR;
using PortoFree.Application.Common.Pagination;

namespace PortoFree.Application.Features.WorkExamples.Queries.GetWorkExamples;

public class GetWorkExamplesQuery(int ownerId, int nextCursor, int limit) 
    : IRequest<SeekPaginatedResult<WorkExampleDto>>
{
    public int OwnerId { get; set; } = ownerId;
    public int NextCursor { get; set; } = nextCursor;
    public int Limit { get; set; } = limit;
}