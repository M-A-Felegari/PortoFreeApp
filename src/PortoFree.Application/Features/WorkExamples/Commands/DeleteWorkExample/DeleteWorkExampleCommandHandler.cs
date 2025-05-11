using MediatR;
using PortoFree.Application.Common.Events;
using PortoFree.Application.Exceptions;
using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Application.Interfaces.Messaging;
using PortoFree.Application.Services.Auth;
using PortoFree.Application.Services.UserSpace;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;

namespace PortoFree.Application.Features.WorkExamples.Commands.DeleteWorkExample;

public class DeleteWorkExampleCommandHandler : IRequestHandler<DeleteWorkExampleCommand>
{
    private readonly IAppLogger<DeleteWorkExampleCommandHandler> _logger;
    private readonly IWorkExamplesRepository _workExamplesRepository;
    private readonly ICurrentUserContext _currentUserContext;
    private readonly IResourceAuthorizationService _resourceAuthorizationService;
    private readonly IEventPublisher _eventPublisher;
    private readonly IUserSpaceService _userSpaceService;

    public DeleteWorkExampleCommandHandler(IAppLogger<DeleteWorkExampleCommandHandler> logger,
        IWorkExamplesRepository workExamplesRepository,
        ICurrentUserContext currentUserContext,
        IResourceAuthorizationService resourceAuthorizationService,
        IEventPublisher eventPublisher,
        IUserSpaceService userSpaceService)
    {
        _logger = logger;
        _workExamplesRepository = workExamplesRepository;
        _currentUserContext = currentUserContext;
        _resourceAuthorizationService = resourceAuthorizationService;
        _eventPublisher = eventPublisher;
        _userSpaceService = userSpaceService;
    }

    public async Task Handle(DeleteWorkExampleCommand request, CancellationToken cancellationToken)
    {
        var user = _currentUserContext.GetCurrentUser() ??
                   throw new UnauthenticatedException();

        _logger.LogInformation("try to delete work example with id: {id} by {@user}", request.Id, user);

        var workExample = await _workExamplesRepository.GetAsync(request.Id) ??
                          throw new NotFoundException(typeof(WorkExample), request.Id.ToString());

        _resourceAuthorizationService.EnsureUserCanDeleteWorkExample(user, workExample);

        await _workExamplesRepository.DeleteAsync(workExample);

        if (workExample.ImagePath is not null)
        {
            await _eventPublisher.PublishAsync(new DeleteImageEvent(workExample.ImagePath, user.Id), cancellationToken);
            // _userSpaceService.FreeUpUserUsedSpaceAsync() //todo: make sure image is deleted
        }
    }
}