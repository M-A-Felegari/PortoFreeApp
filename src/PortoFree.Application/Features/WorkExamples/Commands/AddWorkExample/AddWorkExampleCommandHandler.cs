using AutoMapper;
using MediatR;
using PortoFree.Application.Exceptions;
using PortoFree.Application.Features.Users.UserContext;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Application.Interfaces.StorageSavers;
using PortoFree.Domain.Entities;
using PortoFree.Domain.Repositories;

namespace PortoFree.Application.Features.WorkExamples.Commands.AddWorkExample;

public class AddWorkExampleCommandHandler : IRequestHandler<AddWorkExampleCommand, int>
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IFileNamingService _fileNamingService;
    private readonly IMapper _mapper;
    private readonly IWorkExamplesRepository _workExamplesRepository;
    private readonly IAppLogger<AddWorkExampleCommandHandler> _logger;
    private readonly ICurrentUserContext _currentUserContext;

    public AddWorkExampleCommandHandler(IFileStorageService fileStorageService,
        IWorkExamplesRepository workExamplesRepository,
        IAppLogger<AddWorkExampleCommandHandler> logger,
        ICurrentUserContext currentUserContext, IMapper mapper, IFileNamingService fileNamingService)
    {
        _fileStorageService = fileStorageService;
        _workExamplesRepository = workExamplesRepository;
        _logger = logger;
        _currentUserContext = currentUserContext;
        _mapper = mapper;
        _fileNamingService = fileNamingService;
    }

    public async Task<int> Handle(AddWorkExampleCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("try to add new work example with data: {@request} by {user}",request);

        var user = _currentUserContext.GetCurrentUser() ??
                   throw new UnauthenticatedException();
        
        _logger.LogInformation("user: {@user}", user);

        string? imagePath = null;
        if (request.ImageFileStream is not null)
        {
            var saveFileName = _fileNamingService.GenerateUniqueFileName(request.ImageFileName!);
            imagePath = await _fileStorageService.SaveFileAsync(request.ImageFileStream,saveFileName);
        }
        
        var newWorkExample = _mapper.Map<AddWorkExampleCommand, WorkExample>(request);
        newWorkExample.ImagePath = imagePath;
        
        var createdId = await _workExamplesRepository.AddAsync(newWorkExample);
        
        return createdId;
    }
}