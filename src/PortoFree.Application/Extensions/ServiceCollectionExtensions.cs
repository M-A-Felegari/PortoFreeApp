using Microsoft.Extensions.DependencyInjection;

namespace PortoFree.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
        services.AddMediatR(conf =>
        {
            conf.RegisterServicesFromAssembly(applicationAssembly);
        });
        
        services.AddAutoMapper(applicationAssembly);
    }
}