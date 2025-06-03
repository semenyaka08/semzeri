using Microsoft.Extensions.DependencyInjection;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL.Repositories.Interfaces;

namespace Semzeri.BusinessLogic.Services;

public class UniqueCodeCacheService (IServiceScopeFactory scopeFactory) : IUniqueCodeCacheService
{
    private readonly HashSet<string> _cachedCodes = [];
    public bool IsInitialized;
    
    public async Task InitializeCacheAsync()
    {
        if (IsInitialized) return;
        

        using var scope = scopeFactory.CreateScope();
        var urlsRepository = scope.ServiceProvider.GetRequiredService<IUrlsRepository>();
        
        var existingCodes = await urlsRepository.GetAllCodesAsync();

        foreach (var code in existingCodes)
        {
            _cachedCodes.Add(code);
        }

        IsInitialized = true;
    }

    public bool IsCodeUnique(string code)
    {
        if (!IsInitialized) return true;
        
        return !_cachedCodes.Contains(code);
    }

    public void AddCode(string code)
    {
        if (!IsInitialized) return;

        _cachedCodes.Add(code);
    }
}