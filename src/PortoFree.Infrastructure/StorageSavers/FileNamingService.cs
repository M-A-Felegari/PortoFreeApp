using PortoFree.Application.Interfaces.StorageSavers;

namespace PortoFree.Infrastructure.StorageSavers;

internal class FileNamingService : IFileNamingService
{
    public string GenerateUniqueFileName(string originalFileName)
    {
        var extension = Path.GetExtension(originalFileName);
        return $"{Guid.NewGuid()}{extension}";
    }
}