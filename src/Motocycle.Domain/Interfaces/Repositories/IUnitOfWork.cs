using System;
using System.Threading.Tasks;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();

        Task<bool> CommitAsync();
    }
}

