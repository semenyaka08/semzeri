namespace Semzeri.DAL.UnitOfWork;

public interface IUnitOfWork 
{
    Task SaveChangesAsync();
}