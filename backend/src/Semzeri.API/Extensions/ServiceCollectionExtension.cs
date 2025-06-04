using Semzeri.API.Middlewares;

namespace Semzeri.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddScoped<ExceptionHandlingMiddleware>();
        
        return services;
    }
}