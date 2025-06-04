using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Semzeri.BusinessLogic.Services;
using Semzeri.DAL.Repositories.Interfaces;

namespace Semzeri.BusinessLogic.Tests;

public class UniqueCodeCacheServiceTests
{
    private readonly IUrlsRepository _urlsRepository;
    private readonly UniqueCodeCacheService _service;
    
    public UniqueCodeCacheServiceTests()
    {
        var scopeFactory = Substitute.For<IServiceScopeFactory>();
        var scope = Substitute.For<IServiceScope>();
        _urlsRepository = Substitute.For<IUrlsRepository>();

        var serviceProvider = Substitute.For<IServiceProvider>();
        serviceProvider.GetService(typeof(IUrlsRepository)).Returns(_urlsRepository);
        scope.ServiceProvider.Returns(serviceProvider);
        scopeFactory.CreateScope().Returns(scope);

        _service = new UniqueCodeCacheService(scopeFactory);
    }
    
    [Fact]
    public async Task InitializeCacheAsync_Should_LoadCodesIntoCache()
    {
        // Arrange
        var codes = new List<string> { "Code1", "Code2", "Code3" };
        _urlsRepository.GetAllCodesAsync().Returns(codes);

        // Act
        await _service.InitializeCacheAsync();

        // Assert
        codes.All(code => _service.IsCodeUnique(code) == false).Should().BeTrue();
    }
    
    [Fact]
    public void IsCodeUnique_Should_ReturnTrueIfCacheNotInitialized()
    {
        // Act
        var result = _service.IsCodeUnique("Code1");

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public async Task IsCodeUnique_Should_ReturnFalseIfCodeExists()
    {
        // Arrange
        var codes = new List<string> { "Code1" };
        _urlsRepository.GetAllCodesAsync().Returns(codes);
        await _service.InitializeCacheAsync();

        // Act
        var result = _service.IsCodeUnique("Code1");

        // Assert
        result.Should().BeFalse();
    }
}