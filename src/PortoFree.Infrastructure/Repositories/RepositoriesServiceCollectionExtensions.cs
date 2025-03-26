using Microsoft.Extensions.DependencyInjection;
using PortoFree.Domain.Repositories;

namespace PortoFree.Infrastructure.Repositories;

internal static class RepositoriesServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICommentsRepository, CommentsRepository>();
        services.AddScoped<IEmploymentHistoriesRepository, EmploymentHistoriesRepository>();
        services.AddScoped<ISkillsRepository, SkillsRepository>();
        services.AddScoped<IUserSkillsRepository, UserSkillsRepository>();
        services.AddScoped<IWorkExamplesRepository, WorkExamplesRepository>();
    }
}
