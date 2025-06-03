namespace Semzeri.DAL.DTOs.Urls;

public record UrlsGetRequest(int PageSize = 12, int PageNumber = 1, string? SortBy = null, string? SortDirection = null, string? SearchParam = null);