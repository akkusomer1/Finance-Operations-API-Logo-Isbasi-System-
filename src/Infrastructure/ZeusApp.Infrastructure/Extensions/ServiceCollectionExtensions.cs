using ZeusApp.Application.Interfaces.Contexts;
using ZeusApp.Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ZeusApp.Infrastructure.Repositories;
using ZeusApp.Infrastructure.DbContexts;

namespace ZeusApp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient<ILogRepository, LogRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IAyarRepository, AyarRepository>();
    }
}
