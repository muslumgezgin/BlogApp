using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Interfaces.Repository.Base;
using Blog.Infrastructure.Persistence;
using Blog.Infrastructure.Persistence.Interceptors;
using Blog.Infrastructure.Repositories.Base;
using Blog.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();


        Console.WriteLine(configuration.GetConnectionString("PostgreSql"));
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSql"),
             builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMemoryCache();

        services.AddScoped<ICacheService, CacheService>();

        services.AddTransient<IDateTime, DateTimeService>();

        return services;

    }
}
