using Semzeri.BusinessLogic.DTOs.Algorithm;

namespace Semzeri.BusinessLogic.Services.Interfaces;

public interface IAlgorithmService
{
    Task UpdateAlgorithm(UpdateAlgorithmRequest request);

    Task<AlgorithmGetResponse> GetAlgorithm();
}