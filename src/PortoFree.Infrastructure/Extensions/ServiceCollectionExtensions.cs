using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortoFree.Domain.Entities;
using PortoFree.Infrastructure.Persistence;
using PortoFree.Infrastructure.Repositories;

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

        services.AddRepositories();

        services.AddIdentityApiEndpoints<User>()
            .AddRoles<IdentityRole<int>>()
            .AddEntityFrameworkStores<DataContext>();

    }
}
