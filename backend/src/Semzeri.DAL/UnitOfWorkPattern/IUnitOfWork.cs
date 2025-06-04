namespace Semzeri.DAL.UnitOfWorkPattern;

public interface IUnitOfWork 
{
    Task SaveChangesAsync();
}