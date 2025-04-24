using Microsoft.AspNetCore.Identity;
using PortoFree.Application.Exceptions;
using PortoFree.Domain.Entities;

namespace PortoFree.Application.Services.UserSpace;

internal class UserSpaceService : IUserSpaceService
{
    private readonly UserManager<User> _userManager;

    public UserSpaceService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task EnsureUserHasEnoughSpaceAsync(long userId, long uploadedFileSize)
    {
        if (uploadedFileSize < 0)
            throw new ArgumentOutOfRangeException(nameof(uploadedFileSize),"Uploaded file size cannot be negative");
        
        var user = await _userManager.FindByIdAsync(userId.ToString())??
                   throw new NotFoundException(typeof(User), userId.ToString());

        if (user.FreeSpace < uploadedFileSize)
            throw new NotEnoughSpaceException();
    }

    public async Task IncreaseUserUsedSpaceAsync(int userId, long uploadedFileSize)
    {
        if (uploadedFileSize < 0)
            throw new ArgumentOutOfRangeException(nameof(uploadedFileSize),"Uploaded file size cannot be negative");
        
        var user = await _userManager.FindByIdAsync(userId.ToString())??
                   throw new NotFoundException(typeof(User), userId.ToString());

        if (user.FreeSpace < uploadedFileSize)
            throw new NotEnoughSpaceException();
        
        user.UsedSpace += uploadedFileSize;
        await _userManager.UpdateAsync(user);
    }

    public async Task FreeUpUserUsedSpaceAsync(int userId, long spaceSizeToFree)
    {
        if (spaceSizeToFree < 0)
            throw new ArgumentOutOfRangeException(nameof(spaceSizeToFree),"space size to free cannot be negative");
        
        var user = await _userManager.FindByIdAsync(userId.ToString())??
                   throw new NotFoundException(typeof(User), userId.ToString());

        if (user.UsedSpace < spaceSizeToFree)
            throw new InvalidOperationException("can't decrease UsedSpace below zero");
        
        user.UsedSpace -= spaceSizeToFree;
        await _userManager.UpdateAsync(user);
    }
}