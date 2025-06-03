using Semzeri.BusinessLogic.Common;
using Semzeri.BusinessLogic.DTOs.URLs;

namespace Semzeri.BusinessLogic.Services.Interfaces;

public interface IUrlsService
{
    Task<UrlGetResponse> GenerateUrlAsync(GenerateUrlRequest addRequest, string schema, string host, string userEmail);

    Task<string> GetUrlByCodeAsync(string code);

    Task<UrlGetResponse> GetUrlByIdAsync(Guid id, string userEmail, bool isAdmin);

    Task<PageResult<UrlGetResponse>> GetUrlsAsync(UrlsGetRequest request, string userEmail);

    Task DeleteUrlAsync(Guid id, string userEmail, bool isAdmin);
}