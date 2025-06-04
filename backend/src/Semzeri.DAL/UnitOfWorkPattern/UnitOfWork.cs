namespace Semzeri.DAL.UnitOfWorkPattern;

public class UnitOfWork (ApplicationDbContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}