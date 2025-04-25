using Microsoft.AspNetCore.Hosting;
using PortoFree.Application.Interfaces.StorageSavers;

namespace PortoFree.Infrastructure.StorageSavers;

public class FileStorageService : IFileStorageService
{
    private readonly string _webRootPath;
    public FileStorageService(IWebHostEnvironment webHostEnvironment)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
    }

    public string GetWebNormalizedPath(string physicalPath)
    {
        var webNormalizedPath = physicalPath
            .Replace(_webRootPath + Path.DirectorySeparatorChar, string.Empty)
            .Replace(Path.DirectorySeparatorChar, '/');
        
        return webNormalizedPath;
    }

    public string GetPhysicalPath(string webNormalizedPath)
    {
        var physicalPath = Path
            .Combine(_webRootPath, webNormalizedPath)
            .Replace('/', Path.DirectorySeparatorChar);
        
        return physicalPath;
    }
    
    public async Task<string> SaveFileAsync(Stream stream, string fileName)
    {
        ArgumentNullException.ThrowIfNull(stream);

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException($"{nameof(fileName)} cannot be null or empty.");
        
        var uploadsBasePath = Path.Combine(_webRootPath, "uploads");
        
        if (!Directory.Exists(uploadsBasePath))
            Directory.CreateDirectory(uploadsBasePath);
        
        var saveFullPath = Path.Combine(uploadsBasePath, fileName);
        
        await using var fileStream = new FileStream(saveFullPath, FileMode.Create);
        await stream.CopyToAsync(fileStream);
        
        return GetWebNormalizedPath(saveFullPath);
    }
}