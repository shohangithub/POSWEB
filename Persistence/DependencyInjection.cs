using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Authentication;
using Persistence.Authentication.CurrentUserContext;
using Persistence.Authentication.OptionSetup;
using Persistence.Authentication.TokenGenerator;
using Persistence.Repositories;
using Persistence.SeedData;
using Persistence.Tenant;
using System.Text;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddContextRepository(configuration)
                .AddAuthorizationServices()
                .AddAuthentication(configuration)
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

        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        #region register repositories
        services.AddScoped<IRepository<User, int>, Repository<User, int>>();
        services.AddScoped<IRepository<ProductCategory, short>, Repository<ProductCategory, short>>();
        #endregion

        return services;
    }

    private static IServiceCollection AddMultiTenantServices(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<TenantProvider>();

        return services;
    }

    private static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {


        #region JWT configuration
        services.ConfigureOptions<ConfigureJwtOptions>();

        //Jwt configuration starts here
        var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
        var jwtKey = configuration.GetSection("Jwt:SecretKey").Get<string>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters()
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidateIssuerSigningKey = true,
                 ValidIssuer = jwtIssuer,
                 ValidAudience = jwtIssuer,
                 RequireExpirationTime = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
             };
         });



        services.AddTransient<IJwtProvider, JwtProvider>();


        #endregion
        return services;
    }
    private static IServiceCollection AddAuthorizationServices(this IServiceCollection services)
    {
        //services.AddScoped<Application.Contractors.Common.IAuthorizationService, AuthorizationService>();
        //services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        //services.AddSingleton<IPolicyEnforcer, PolicyEnforcer>();

        //register authorization handler
        services.AddAuthorization();
        services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();


        return services;
    }

}