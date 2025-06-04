using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Semzeri.DAL.Entities;

namespace Semzeri.DAL;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<AppUser> AppUsers { get; set; }
    
    public DbSet<UrlInfo> UrlInfos { get; set; }
    
    public DbSet<Algorithm> Algorithms  { get; set; }
}