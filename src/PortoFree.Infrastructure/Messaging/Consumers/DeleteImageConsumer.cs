using MassTransit;
using PortoFree.Application.Common.Events;
using PortoFree.Application.Exceptions;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Application.Interfaces.StorageSavers;
using PortoFree.Application.Services.UserSpace;

namespace PortoFree.Infrastructure.Messaging.Consumers;

public class DeleteImageConsumer : IConsumer<DeleteImageEvent>
{
    private readonly IAppLogger<DeleteImageConsumer> _logger;
    private readonly IFileStorageService _fileStorageService;
    private readonly IUserSpaceService _userSpaceService;

    public DeleteImageConsumer(IAppLogger<DeleteImageConsumer> logger,
        IFileStorageService fileStorageService,
        IUserSpaceService userSpaceService)
    {
        _logger = logger;
        _fileStorageService = fileStorageService;
        _userSpaceService = userSpaceService;
    }

    public async Task Consume(ConsumeContext<DeleteImageEvent> context)
    {
        try
        {
            _logger.LogInformation("try to delete image at path: {path}", context.Message.Path);
            
            var fileSize = _fileStorageService.GetFileSize(context.Message.Path);
            _fileStorageService.DeleteFile(context.Message.Path);
            await _userSpaceService.FreeUpUserUsedSpaceAsync(context.Message.OwnerId, fileSize);
        }
        catch (FileNotFoundException)
        {
            _logger.LogWarning("tried to delete file at path: {path} but file not found", context.Message.Path);
        }
        catch (Exception ex)
        {
            _logger.LogError("error occured while deleting file at path: {path} with exception: {@ex}",
                context.Message.Path, ex);
            throw;
        }
    }
}