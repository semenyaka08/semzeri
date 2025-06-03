namespace Semzeri.BusinessLogic.Services.Interfaces;

public interface IUrlShortenerService
{
    public Task<string> GenerateUniqueCode();
}