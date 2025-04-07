namespace PortoFree.Application.Interfaces.StorageSavers;

public interface IFileNamingService
{
    public string GenerateUniqueFileName(string originalFileName);
}