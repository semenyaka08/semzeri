using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Semzeri.DAL.Entities;

namespace Semzeri.DAL.Seeders;

public class DataSeeder(ApplicationDbContext context, UserManager<AppUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager) : IDataSeeder
{
    public async Task SeedAsync()
    {
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }

        if (await context.Database.CanConnectAsync())
        {
            string[] roles = [ApplicationRoles.Admin, ApplicationRoles.User];

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            if (!userManager.Users.Any(z => z.Email == configuration["AdminData:Email"]))
            {
                var (adminUser, password) = GetAdminUser();
                await userManager.CreateAsync(adminUser, password);
                await userManager.AddToRoleAsync(adminUser, ApplicationRoles.Admin);
            }
            
            var algorithm = GetAlgorithm();
            
            if (!await context.Algorithms.AnyAsync())
            {
                await context.Algorithms.AddAsync(algorithm);
            }
            
            await context.SaveChangesAsync();
        }
    }
    
    private (AppUser, string) GetAdminUser()
    {
        var adminUser = new AppUser
        {
            UserName = configuration["AdminData:Email"],
            Email = configuration["AdminData:Email"],
        };
        
        string password = configuration["AdminData:Password"]!;
        
        return (adminUser, password);
    }

    private Algorithm GetAlgorithm()
    {
        var algorithm = new Algorithm
        {
            Title = "Semzeri Algorithm",
            Description = "This is a collision-resistant random code generator for a URL shortening service (similar to bit.ly or tinyurl). The algorithm generates short, unique alphanumeric codes that serve as identifiers for shortened URLs."
        };
        
        return algorithm;
    }
}