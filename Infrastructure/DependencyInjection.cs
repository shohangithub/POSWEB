using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;
using Infrastructure.BackgroundServices;
using Infrastructure.Services;
//using Infrastructure.Security;
//using Infrastructure.Security.PolicyEnforcer;
//using Infrastructure.Security.CurrentUserProvider;
//using Infrastructure.Security.TokenGenerator;
//using Infrastructure.Security.TokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Infrastructure.Services.Common;
using Infrastructure.Authentication.OptionSetup;
using Infrastructure.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Authentication.TokenGenerator;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHttpContextAccessor()
            .AddServices()
            .AddBackgroundServices(configuration)
            .AddAuthorizationServices()
            .AddAuthentication(configuration);

        return services;
    }



    private static IServiceCollection AddBackgroundServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEmailNotifications(configuration);

        return services;
    }

    private static IServiceCollection AddEmailNotifications(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        EmailSettings emailSettings = new();
        configuration.Bind(EmailSettings.Section, emailSettings);

        if (!emailSettings.EnableEmailNotifications)
        {
            return services;
        }

        services.AddHostedService<EmailBackgroundService>();

        services
            .AddFluentEmail(emailSettings.DefaultFromEmail)
            .AddSmtpSender(new SmtpClient(emailSettings.SmtpSettings.Server)
            {
                Port = emailSettings.SmtpSettings.Port,
                Credentials = new NetworkCredential(
                    emailSettings.SmtpSettings.Username,
                    emailSettings.SmtpSettings.Password),
            });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        services.AddScoped<IUserService<int>, UserService>();

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

        //services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.Section));

        //services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        //services
        //    .ConfigureOptions<JwtBearerTokenValidationConfiguration>()
        //    .AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        //    .AddJwtBearer();

        return services;
    }
}
