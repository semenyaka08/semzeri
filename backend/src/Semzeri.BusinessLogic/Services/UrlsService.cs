using Semzeri.BusinessLogic.Common;
using Semzeri.BusinessLogic.DTOs.Admin;
using Semzeri.BusinessLogic.DTOs.URLs;
using Semzeri.BusinessLogic.Exceptions;
using Semzeri.BusinessLogic.Mappers;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL.Entities;
using Semzeri.DAL.Repositories.Interfaces;

namespace Semzeri.BusinessLogic.Services;

public class UrlsService (IUrlsRepository urlsRepository, IUrlShortenerService urlShortenerService) : IUrlsService
{
    public async Task<UrlGetResponse> GenerateUrlAsync(GenerateUrlRequest addRequest, string schema, string host, string userEmail)
    {
        var isAlreadyExist = await urlsRepository.GetUrlByOriginalUrlAsync(addRequest.OriginalUrl);

        if (isAlreadyExist != null) throw new UrlAlreadyShortenedException();

        var code = await urlShortenerService.GenerateUniqueCode();

        var shortenedUrl = $"{schema}://{host}/{code}";
        
        var entity = addRequest.ToEntity(code, shortenedUrl, userEmail);

        var generatedUrl = await urlsRepository.AddUrlAsync(entity);

        return generatedUrl.ToDto();
    }

    public async Task<string> GetUrlByCodeAsync(string code)
    {
        var urlInfo = await urlsRepository.GetUrlByCodeAsync(code);

        if (urlInfo is null) throw new NotFoundException($"Resource type: {nameof(UrlInfo)} with code: {code} does not exist");
        
        return urlInfo.OriginalUrl;
    }

    public async Task<UrlGetResponse> GetUrlByIdAsync(Guid id, string userEmail, bool isAdmin)
    {
        var url = await urlsRepository.GetUrlByIdAsync(id);

        if (url is null)
            throw new NotFoundException($"Resource type: {nameof(UrlInfo)} with id: {id} does not exist");

        if (!isAdmin && url.UserEmail != userEmail)
            throw new ForbiddenException("This operation is forbidden for you!");
        
        return url.ToDto();
    }

    public async Task<PageResult<UrlGetResponse>> GetUrlsAsync(UrlsGetRequest request, string userEmail)
    {
        var (urls, totalCount) = await urlsRepository.GetUrlsAsync(request.ToDalDto(), userEmail);

        var mappedUrls = urls.Select(x=>x.ToDto());
        
        return new PageResult<UrlGetResponse>(mappedUrls, totalCount, request.PageSize, request.PageNumber);
    }

    public async Task DeleteUrlAsync(Guid id, string userEmail, bool isAdmin)
    {
        var url = await urlsRepository.GetUrlByIdAsync(id);

        if (url is null)
            throw new NotFoundException($"Resource type: {nameof(UrlInfo)} with id: {id} does not exist");

        if (!isAdmin && url.UserEmail != userEmail)
            throw new ForbiddenException("This operation is forbidden for you!");
        
        await urlsRepository.DeleteUrlAsync(url);
    }

    public async Task<PageResult<UrlGetResponse>> GetAllUrlsAsync(AdminUrlsGetRequest request)
    {
        var (urls, totalCount) = await urlsRepository.GetAllUrlsAsync(request.ToDalDto());

        var mappedUrls = urls.Select(x=>x.ToDto());

        return new PageResult<UrlGetResponse>(mappedUrls, totalCount, request.PageSize, request.PageNumber);
    }
}
