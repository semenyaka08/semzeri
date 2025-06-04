using FluentAssertions;
using NSubstitute;
using Semzeri.BusinessLogic.DTOs.Algorithm;
using Semzeri.BusinessLogic.Exceptions;
using Semzeri.BusinessLogic.Services;
using Semzeri.DAL.Entities;
using Semzeri.DAL.Repositories.Interfaces;

namespace Semzeri.BusinessLogic.Tests;

public class AlgorithmServiceTests
{
    private readonly IAlgorithmRepository _algorithmRepository;
    private readonly AlgorithmService _service;
    
    public AlgorithmServiceTests()
    {
        _algorithmRepository = Substitute.For<IAlgorithmRepository>();
        _service = new AlgorithmService(_algorithmRepository);
    }
    
    [Fact]
    public async Task UpdateAlgorithm_Should_CallRepositoryWithCorrectParameters()
    {
        // Arrange
        var request = new UpdateAlgorithmRequest
        (
            "New Title",
            "New Description"
        );

        // Act
        await _service.UpdateAlgorithm(request);

        // Assert
        await _algorithmRepository.Received(1).UpdateAlgorithm(request.Title, request.Description);
    }
    
    [Fact]
    public async Task GetAlgorithm_Should_ReturnAlgorithmGetResponse_WhenAlgorithmExists()
    {
        // Arrange
        var algorithm = new Algorithm
        {
            Title = "Algorithm Title",
            Description = "Algorithm Description"
        };

        _algorithmRepository.GetAlgorithm().Returns(algorithm);

        // Act
        var result = await _service.GetAlgorithm();

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(algorithm.Title);
        result.Description.Should().Be(algorithm.Description);
    }
    
    [Fact]
    public async Task GetAlgorithm_Should_ThrowNotFoundException_WhenAlgorithmDoesNotExist()
    {
        // Arrange
        _algorithmRepository.GetAlgorithm().Returns((Algorithm)null);

        // Act
        var act = async () => await _service.GetAlgorithm();

        // Assert
        await act.Should().ThrowAsync<NotFoundException>().WithMessage("Algorithm was not found");
    }
}