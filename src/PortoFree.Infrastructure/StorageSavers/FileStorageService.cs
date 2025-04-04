using PortoFree.Application.Interfaces.StorageSavers;

namespace PortoFree.Infrastructure.StorageSavers;

public class FileStorageService : IFileStorageService
{
    public async Task<string> SaveFileAsync(Stream stream, string fileName)
    {
        ArgumentNullException.ThrowIfNull(stream);

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException($"{nameof(fileName)} cannot be null or empty.");
        
        var uploadsBasePath = Path.Combine("wwwroot");
        
        if (!Directory.Exists(uploadsBasePath))
            Directory.CreateDirectory(uploadsBasePath);
        
        var saveFullPath = Path.Combine(uploadsBasePath, fileName);
        
        await using var fileStream = new FileStream(saveFullPath, FileMode.Create);
        await stream.CopyToAsync(fileStream);
        
        //todo: save into another layer of wwwroot and return path without wwwroot
        return saveFullPath;
    }
}