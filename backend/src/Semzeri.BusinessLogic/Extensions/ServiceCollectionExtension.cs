using Microsoft.Extensions.DependencyInjection;
using Semzeri.BusinessLogic.Services;
using Semzeri.BusinessLogic.Services.Interfaces;

namespace Semzeri.BusinessLogic.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IUrlsService, UrlsService>();
        services.AddScoped<IUrlShortenerService, UrlShortenerService>();
        services.AddScoped<IAlgorithmService, AlgorithmService>();
        services.AddSingleton<IUniqueCodeCacheService, UniqueCodeCacheService>();
    }
}