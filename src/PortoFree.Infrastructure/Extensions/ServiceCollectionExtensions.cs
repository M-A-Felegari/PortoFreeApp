using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortoFree.Application.Interfaces.Logging;
using PortoFree.Domain.Entities;
using PortoFree.Infrastructure.Logging;
using PortoFree.Infrastructure.Persistence;
using PortoFree.Infrastructure.Repositories;
using PortoFree.Infrastructure.Seeders;
using PortoFree.Infrastructure.Users.UserContext;

namespace PortoFree.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.AddSeedingServices(configuration);

        services.AddRepositories();
        
        services.AddUserContextServices();

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<DataContext>();
        
        services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));

    }
}
