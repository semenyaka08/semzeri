using Semzeri.DAL.Entities;
using Semzeri.DAL.Repositories.Interfaces;

namespace Semzeri.DAL.Repositories;

public class AlgorithmRepository : IAlgorithmRepository
{
    public Task UpdateAlgorithm(string title, string description)
    {
        throw new NotImplementedException();
    }

    public Task<Algorithm?> GetAlgorithm()
    {
        throw new NotImplementedException();
    }
}