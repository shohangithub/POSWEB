using POSWEB.Server.Middlewares;
using System.Text.Json;

namespace POSWEB.Server;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {

        services.AddTransient<AuthenticationErrorHandler>();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddControllers()
        .AddJsonOptions(option =>
        {
            option.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            option.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        #region register exception service

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        #endregion


        return services;
    }
}
