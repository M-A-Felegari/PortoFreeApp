using Microsoft.AspNetCore.Hosting;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Application.Interfaces.StorageSavers;

namespace PortoFree.Infrastructure.StorageSavers;

public class FileStorageService : IFileStorageService
{
    private readonly string _webRootPath;
    private readonly IAppLogger<FileStorageService> _logger;
    public FileStorageService(IWebHostEnvironment webHostEnvironment, IAppLogger<FileStorageService> logger)
    {
        _logger = logger;
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
        stream.Seek(0, SeekOrigin.Begin);
        await stream.CopyToAsync(fileStream);
        
        _logger.LogInformation("File successfully saved at path: {path}",saveFullPath);
        
        return GetWebNormalizedPath(saveFullPath);
    }

    public void DeleteFile(string webNormalizedPath)
    {
        var physicalPath = GetPhysicalPath(webNormalizedPath);

        if (!File.Exists(physicalPath))
        {
            _logger.LogWarning("try to delete file but File does not exist at {path}", physicalPath);
            throw new FileNotFoundException($"File {physicalPath} not found.");
        }
        
        File.Delete(physicalPath);
        
        _logger.LogInformation("File at path: {path} successfully deleted", physicalPath);
    }

    public long GetFileSize(string webNormalizedPath)
    {
        var physicalPath = GetPhysicalPath(webNormalizedPath);

        if (!File.Exists(physicalPath))
        {
            _logger.LogWarning("try to get file size file but File does not exist at {path}", physicalPath);
            throw new FileNotFoundException($"File {physicalPath} not found.");
        }
        
        var file = new FileInfo(physicalPath);
        return file.Length;
    }
}