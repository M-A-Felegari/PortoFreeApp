using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortoFree.Application.Interfaces.Seeding;

namespace PortoFree.Infrastructure.Seeding;

internal static class SeedingServiceCollectionExtensions
{
    public static void AddSeedingServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OwnerSettings>(configuration.GetSection("OwnerSettings"));
        
        services.AddScoped<IOwnerSeeder,OwnerSeeder>();
    }
}