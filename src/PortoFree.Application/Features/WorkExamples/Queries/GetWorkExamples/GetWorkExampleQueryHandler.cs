using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using PortoFree.Application.Common.Pagination;
using PortoFree.Application.Exceptions;
using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;

namespace PortoFree.Application.Features.WorkExamples.Queries.GetWorkExamples;

public class GetWorkExampleQueryHandler 
    : IRequestHandler<GetWorkExamplesQuery, SeekPaginatedResult<WorkExampleDto>>
{
    private readonly IAppLogger<GetWorkExampleQueryHandler> _logger;
    private readonly IWorkExamplesRepository _workExamplesRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    
    public GetWorkExampleQueryHandler(IAppLogger<GetWorkExampleQueryHandler> logger,
        ICurrentUserContext currentUserContext,
        IWorkExamplesRepository workExamplesRepository,
        IMapper mapper,
        UserManager<User> userManager)
    {
        _logger = logger;
        _workExamplesRepository = workExamplesRepository;
        _mapper = mapper;
        _userManager = userManager;
    }
    
    public async Task<SeekPaginatedResult<WorkExampleDto>> Handle(GetWorkExamplesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("try to get work examples with this request : {@request}",request);
        
        _ = await _userManager.FindByIdAsync(request.OwnerId.ToString()) ??
                   throw new NotFoundException(typeof(User), request.OwnerId.ToString());
        var (workExamples, nextCursor) = await _workExamplesRepository
                .GetAllBySeekPagination(request.OwnerId,request.NextCursor, request.Limit);
        
        var workExamplesDto = _mapper.Map<IReadOnlyList<WorkExampleDto>>(workExamples);

        var paginatedResult = new SeekPaginatedResult<WorkExampleDto>(workExamplesDto, nextCursor, nextCursor.HasValue);
        
        return paginatedResult;
    }
}