namespace Semzeri.DAL.UnitOfWork;

public class UnitOfWork (ApplicationDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}