using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, TextDifficultyDbContext>();
        
        return services;
    }
}