﻿namespace PortoFree.Application.Services.UserSpace;

public interface IUserSpaceService
{
    Task EnsureUserHasEnoughSpaceAsync(long userId, long uploadedFileSize);
    Task IncreaseUserUsedSpaceAsync(int userId, long uploadedFileSize);
    Task FreeUpUserUsedSpaceAsync(int userId, long uploadedFileSize);
}