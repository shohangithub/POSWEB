using Microsoft.Extensions.Configuration;
using Persistence.Repositories;
using Persistence.SeedData;
using Persistence.Tenant;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddContextRepository(configuration)
                .AddMultiTenantServices();


        return services;
    }

    private static IServiceCollection AddContextRepository(this IServiceCollection services, IConfiguration configuration)
    {
        #region register db context provider
        //services.AddScoped<DbContext>();
        services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(configuration.GetConnectionString("ApplicationDbContext")));

        services.AddTransient<ApplicationDbContextInitializer>();
        #endregion

        #region register repositories
        services.AddScoped<IRepository<User, int>, Repository<User, int>>();
        #endregion

        return services;
    }

    private static IServiceCollection AddMultiTenantServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<TenantProvider>();

        return services;
    }

}