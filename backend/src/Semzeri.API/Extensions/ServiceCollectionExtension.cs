using Microsoft.AspNetCore.Identity;
using Semzeri.API.Middlewares;
using Semzeri.DAL;
using Semzeri.DAL.Entities;

namespace Semzeri.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddScoped<ExceptionHandlingMiddleware>();
        
        services.AddAuthorization();
        services.AddIdentityApiEndpoints<AppUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always; 
            options.Cookie.SameSite = SameSiteMode.None; 
            options.Cookie.HttpOnly = true; 
        });
        
        return services;
    }
}