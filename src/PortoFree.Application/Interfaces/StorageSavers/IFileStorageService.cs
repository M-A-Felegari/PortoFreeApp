namespace PortoFree.Application.Interfaces.StorageSavers;

public interface IFileStorageService
{
    public string GetWebNormalizedPath(string physicalPath);
    public string GetPhysicalPath(string webNormalizedPath);
    public Task<string> SaveFileAsync(Stream stream, string fileName);
    public void DeleteFile(string webNormalizedPath);
    public long GetFileSize(string webNormalizedPath);
}