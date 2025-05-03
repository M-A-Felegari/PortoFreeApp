using AutoMapper;
using MediatR;
using PortoFree.Application.Exceptions;
using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Application.Services.Auth;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;

namespace PortoFree.Application.Features.WorkExamples.Commands.UpdateWorkExample;

public class UpdateWorkExampleCommandHandler : IRequestHandler<UpdateWorkExampleCommand>
{
    private readonly IWorkExamplesRepository _workExamplesRepository;
    private readonly IAppLogger<UpdateWorkExampleCommandHandler> _logger;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly IMapper _mapper;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public UpdateWorkExampleCommandHandler(IWorkExamplesRepository workExamplesRepository,
        IAppLogger<UpdateWorkExampleCommandHandler> logger,
        ICurrentUserContext currentUserContext,
        IMapper mapper,
        IResourceAuthorizationService resourceAuthorizationService)
    {
        _workExamplesRepository = workExamplesRepository;
        _logger = logger;
        _currentUserContext = currentUserContext;
        _mapper = mapper;
        _resourceAuthorizationService = resourceAuthorizationService;
    }

    public async Task Handle(UpdateWorkExampleCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUserContext.GetCurrentUser() ??
                   throw new UnauthenticatedException();
        
        _logger.LogInformation("try to update work example with data: {@request} by {@user}",request,user);
        
        var workExample = await _workExamplesRepository.GetAsync(request.Id) ??
                          throw new NotFoundException(typeof(WorkExample), request.Id.ToString());
        
        workExample = _mapper.Map(request, workExample);
        
        _resourceAuthorizationService.EnsureUserCanEditWorkExample(user, workExample);
        
        await _workExamplesRepository.UpdateAsync(workExample);
    }
}