using Microsoft.EntityFrameworkCore;
using Semzeri.DAL.Entities;
using Semzeri.DAL.Repositories.Interfaces;
using Semzeri.DAL.UnitOfWork;

namespace Semzeri.DAL.Repositories;

public class AlgorithmRepository (ApplicationDbContext context, IUnitOfWork unitOfWork) : IAlgorithmRepository
{
    public async Task UpdateAlgorithm(string title, string description)
    {
        var algo = await context.Algorithms.FirstOrDefaultAsync();

        if (algo is null)
        {
            algo = new Algorithm
            {
                Title = title,
                Description = description
            };
            await context.Algorithms.AddAsync(algo);
            await unitOfWork.SaveChangesAsync();
            return;
        }

        algo.Description = description;
        algo.Title = title;
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<Algorithm?> GetAlgorithm() => await context.Algorithms.FirstOrDefaultAsync();
}