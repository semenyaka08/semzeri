using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Semzeri.DAL.Repositories;
using Semzeri.DAL.Repositories.Interfaces;
using Semzeri.DAL.UnitOfWorkPattern;

namespace Semzeri.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAlgorithmRepository, AlgorithmRepository>();
        services.AddScoped<IUrlsRepository, UrlsRepository>();
        
        return services;
    }
}