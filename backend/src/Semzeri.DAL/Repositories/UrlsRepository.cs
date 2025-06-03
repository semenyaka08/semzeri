using Semzeri.DAL.DTOs.Urls;
using Semzeri.DAL.Entities;
using Semzeri.DAL.Repositories.Interfaces;

namespace Semzeri.DAL.Repositories;

public class UrlsRepository : IUrlsRepository
{
    public Task<UrlInfo> AddUrlAsync(UrlInfo urlInfo)
    {
        throw new NotImplementedException();
    }

    public Task<UrlInfo?> GetUrlByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UrlInfo?> GetUrlByOriginalUrlAsync(string originalUrl)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsCodeAlreadyExist(string code)
    {
        throw new NotImplementedException();
    }

    public Task<UrlInfo?> GetUrlByCodeAsync(string code)
    {
        throw new NotImplementedException();
    }

    public Task<List<string>> GetAllCodesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<UrlInfo>, int)> GetUrlsAsync(UrlsDalGetRequest request, string userEmail)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUrlAsync(UrlInfo urlInfo)
    {
        throw new NotImplementedException();
    }
}