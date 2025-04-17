using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PortoFree.Application.Services.FileValidation;

namespace PortoFree.Application.Services;

public static class ServicesServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileValidator, ImageValidator>();
    }
}