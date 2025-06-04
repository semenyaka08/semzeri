using Semzeri.BusinessLogic.DTOs.URLs;
using Semzeri.DAL.DTOs.Urls;
using Semzeri.DAL.Entities;

namespace Semzeri.BusinessLogic.Mappers;

public static class UrlsMapper
{
    public static UrlInfo ToEntity(this GenerateUrlRequest request, string code, string shortenedUrl, string userEmail)
    {
        return new UrlInfo
        {
            ShortenedUrl = shortenedUrl,
            Code = code,
            OriginalUrl = request.OriginalUrl,
            UserEmail = userEmail
        };
    }

    public static UrlGetResponse ToDto(this UrlInfo urlInfo)
    {
        return new UrlGetResponse
        {
            Id = urlInfo.Id,
            ShortenedUrl = urlInfo.ShortenedUrl,
            Code = urlInfo.Code,
            OriginalUrl = urlInfo.OriginalUrl,
            CreatedAt = urlInfo.CreatedAt,
            UserEmail = urlInfo.UserEmail
        };
    }

    public static UrlsDalGetRequest ToDalDto(this UrlsGetRequest request)
    {
        return new UrlsDalGetRequest
        {
            PageSize = request.PageSize,
            PageNumber = request.PageNumber,
            SortBy = request.SortBy,
            SortDirection = request.SortDirection,
            SearchParam = request.SearchParam
        };
    }
}