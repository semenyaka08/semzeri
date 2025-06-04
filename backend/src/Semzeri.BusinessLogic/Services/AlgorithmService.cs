using Semzeri.BusinessLogic.DTOs.Algorithm;
using Semzeri.BusinessLogic.Exceptions;
using Semzeri.BusinessLogic.Mappers;
using Semzeri.BusinessLogic.Services.Interfaces;
using Semzeri.DAL.Repositories.Interfaces;

namespace Semzeri.BusinessLogic.Services;

public class AlgorithmService (IAlgorithmRepository algorithmRepository): IAlgorithmService
{
    public async Task UpdateAlgorithm(UpdateAlgorithmRequest request)
    {
        await algorithmRepository.UpdateAlgorithm(request.Title, request.Description);
    }

    public async Task<AlgorithmGetResponse> GetAlgorithm()
    {
        var algorithm = await algorithmRepository.GetAlgorithm();

        if (algorithm == null)
            throw new NotFoundException("Algorithm was not found");
        
        return algorithm.ToDto();
    }
}