using Semzeri.BusinessLogic.DTOs.Algorithm;
using Semzeri.DAL.Entities;

namespace Semzeri.BusinessLogic.Mappers;

public static class AlgorithmMapper
{
    public static AlgorithmGetResponse ToDto(this Algorithm algorithm)
    {
        return new AlgorithmGetResponse
        {
            Title = algorithm.Title,
            Description = algorithm.Description
        };
    }
}