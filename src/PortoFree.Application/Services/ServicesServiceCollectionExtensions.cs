using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PortoFree.Application.Services.FileValidation;
using PortoFree.Application.Services.UserSpace;

namespace PortoFree.Application.Services;

public static class ServicesServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileValidator, ImageValidator>();
        services.AddScoped<IUserSpaceService, UserSpaceService>();
    }
}