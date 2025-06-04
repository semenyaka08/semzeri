using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL;

namespace Semzeri.BusinessLogic.Services;

public class UrlShortenerService (IUniqueCodeCacheService codeCacheService) : IUrlShortenerService
{
    private readonly Random _random = new();
    
    public async Task<string> GenerateUniqueCode()
    {
        await codeCacheService.InitializeCacheAsync();
    
        var codeChars = new char[UrlShorteningConfig.NumberOfCharsInShortenedLink];
    
        while (true)
        {
            for (int i = 0;i<UrlShorteningConfig.NumberOfCharsInShortenedLink;i++)
            {
                var randomIndex = _random.Next(UrlShorteningConfig.Alphabet.Length - 1);
    
                codeChars[i] = UrlShorteningConfig.Alphabet[randomIndex];
            }
    
            var code = new string(codeChars);
    
            if (codeCacheService.IsCodeUnique(code))
            {
                codeCacheService.AddCode(code);
                return code;
            }
        }
    }
}