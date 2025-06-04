using FluentAssertions;
using NSubstitute;
using Semzeri.BusinessLogic.Services;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL;

namespace Semzeri.BusinessLogic.Tests;

public class UrlShortenerServiceTests
{
    private readonly IUniqueCodeCacheService _codeCacheService;
    private readonly UrlShortenerService _sut;
    
    public UrlShortenerServiceTests()
    {
        _codeCacheService = Substitute.For<IUniqueCodeCacheService>();
        _sut = new UrlShortenerService(_codeCacheService);
    }
    
    [Fact]
    public async Task GenerateUniqueCode_WhenCodeIsUnique_ReturnsCodeAndAddsItToCache()
    {
        // Arrange
        _codeCacheService.IsCodeUnique(Arg.Any<string>()).Returns(true);

        // Act
        var result = await _sut.GenerateUniqueCode();

        // Assert
        result.Should().NotBeNullOrEmpty();

        _codeCacheService.Received(1).AddCode(Arg.Any<string>());
    }
    
    [Fact]
    public async Task GenerateUniqueCode_WhenCodeIsNotUnique_RegeneratesCodeAndReturnsUniqueCode()
    {
        // Arrange
        _codeCacheService.IsCodeUnique(Arg.Any<string>())
            .Returns(false, false, true);
        
        // Act
        var result = await _sut.GenerateUniqueCode();
        
        // Assert
        result.Should().NotBeNullOrEmpty();
        _codeCacheService.Received(1).AddCode(Arg.Any<string>());
    }
    
    [Fact]
    public async Task GenerateUniqueCode_GeneratesCodeWithCorrectLength()
    {
        // Arrange
        _codeCacheService.IsCodeUnique(Arg.Any<string>()).Returns(true);

        // Act
        var result = await _sut.GenerateUniqueCode();

        // Assert
        result.Should().HaveLength(UrlShorteningConfig.NumberOfCharsInShortenedLink);
    }
}