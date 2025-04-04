namespace PortoFree.Application.Interfaces.StorageSavers;

public interface IFileStorageService
{
    public Task<string> SaveFileAsync(Stream stream, string fileName);
}