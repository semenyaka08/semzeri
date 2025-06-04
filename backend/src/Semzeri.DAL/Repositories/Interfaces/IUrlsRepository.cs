using Semzeri.DAL.DTOs.Urls;
using Semzeri.DAL.Entities;

namespace Semzeri.DAL.Repositories.Interfaces;

public interface IUrlsRepository
{
    Task<UrlInfo> AddUrlAsync(UrlInfo urlInfo);

    Task<UrlInfo?> GetUrlByIdAsync(Guid id);

    Task<UrlInfo?> GetUrlByOriginalUrlAsync(string originalUrl);

    Task<bool> IsCodeAlreadyExist(string code);

    Task<UrlInfo?> GetUrlByCodeAsync(string code);

    Task<List<string>> GetAllCodesAsync();
    
    Task<(IEnumerable<UrlInfo>, int)> GetUrlsAsync(UrlsDalGetRequest request, string userEmail);

    Task DeleteUrlAsync(UrlInfo urlInfo);
}