using Semzeri.DAL.Entities;

namespace Semzeri.DAL.Repositories.Interfaces;

public interface IAlgorithmRepository
{
    Task UpdateAlgorithm(string title, string description);

    Task<Algorithm?> GetAlgorithm();
}