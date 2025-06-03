using Semzeri.BusinessLogic.Common;
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

        if (isAlreadyExist != null)
        {
            throw new UrlAlreadyShortenedException();
        }

        var code = await urlShortenerService.GenerateUniqueCode();

        var shortenedUrl = $"{schema}://{host}/{code}";
        
        var entity = addRequest.ToEntity(code, shortenedUrl, userEmail);

        var generatedUrl = await urlsRepository.AddUrlAsync(entity);

        return generatedUrl.ToDto();
    }

    public async Task<string> GetUrlByCodeAsync(string code)
    {
        var urlInfo = urlsRepository.GetUrlByCodeAsync(code);

        if (urlInfo is null) throw new NotFoundException($"Resource type: {nameof(UrlInfo)} with code: {code} does not exist");
        
        return urlInfo.ToString();
    }

    public async Task<UrlGetResponse> GetUrlByIdAsync(Guid id, string userEmail)
    {
        var url = await urlsRepository.GetUrlByIdAsync(id);

        if (url is null)
            throw new NotFoundException($"Resource type: {nameof(UrlInfo)} with id: {id} does not exist");

        if (url.UserEmail != userEmail)
            throw new ForbiddenException("This operation is forbidden for you!");
        
        return url.ToDto();
    }

    public async Task<PageResult<UrlGetResponse>> GetUrlsAsync(UrlsGetRequest request, string userEmail)
    {
        var (urls, totalCount) = await urlsRepository.GetUrlsAsync(request.ToDalDto(), userEmail);

        var mappedUrls = urls.Select(x=>x.ToDto());
        
        return new PageResult<UrlGetResponse>(mappedUrls, totalCount, request.PageSize, request.PageNumber);
    }

    public async Task DeleteUrlAsync(Guid id, string userEmail)
    {
        var url = await urlsRepository.GetUrlByIdAsync(id);

        if (url is null)
            throw new NotFoundException($"Resource type: {nameof(UrlInfo)} with id: {id} does not exist");

        if (url.UserEmail != userEmail)
            throw new ForbiddenException("This operation is forbidden for you!");
        
        await urlsRepository.DeleteUrlAsync(url);
    }
}