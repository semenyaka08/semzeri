using Microsoft.EntityFrameworkCore;
using Semzeri.DAL.Entities;

namespace Semzeri.DAL;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<UrlInfo> UrlInfos { get; set; }
    
    public DbSet<Algorithm> Algorithms  { get; set; }
}