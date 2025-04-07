using Microsoft.Extensions.DependencyInjection;
using PortoFree.Application.Interfaces.StorageSavers;

namespace PortoFree.Infrastructure.StorageSavers;

public static class StorageServiceCollectionExtensions
{
    public static void AddStorageSavers(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageService,FileStorageService>();
        services.AddScoped<IFileNamingService, FileNamingService>();
    }
}