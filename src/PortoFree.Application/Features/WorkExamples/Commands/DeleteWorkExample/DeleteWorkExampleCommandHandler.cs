using MediatR;
using PortoFree.Application.Exceptions;
using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Application.Services.Auth;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;

namespace PortoFree.Application.Features.WorkExamples.Commands.DeleteWorkExample;

public class DeleteWorkExampleCommandHandler : IRequestHandler<DeleteWorkExampleCommand>
{
    private readonly IAppLogger<DeleteWorkExampleCommandHandler> _logger;
    private readonly IWorkExamplesRepository _workExamplesRepository;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;

    public DeleteWorkExampleCommandHandler(IAppLogger<DeleteWorkExampleCommandHandler> logger,
        IWorkExamplesRepository workExamplesRepository,
        ICurrentUserContext currentUserContext,
        IResourceAuthorizationService resourceAuthorizationService)
    {
        _logger = logger;
        _workExamplesRepository = workExamplesRepository;
        _currentUserContext = currentUserContext;
        _resourceAuthorizationService = resourceAuthorizationService;
    }

    public async Task Handle(DeleteWorkExampleCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUserContext.GetCurrentUser() ??
                   throw new UnauthenticatedException();
        
        _logger.LogInformation("try to delete work example with id: {id} by {@user}",request.Id, user);
        
        var workExample = await _workExamplesRepository.GetAsync(request.Id) ??
                          throw new NotFoundException(typeof(WorkExample), request.Id.ToString());
        
        _resourceAuthorizationService.EnsureUserCanDeleteWorkExample(user, workExample);
        
        await _workExamplesRepository.DeleteAsync(workExample);
        
        //todo: remove remained image in storage if exist via an Event
    }
}