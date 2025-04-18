namespace PortoFree.Api.Middlewares;

public static class MiddlewaresServiceCollectionExtensions
{
    public static void AddMiddlewares(this IServiceCollection services)
    {
        services.AddScoped<ExceptionHandlingMiddleware>();
    }
}