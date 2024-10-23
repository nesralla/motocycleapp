using System;
using System.Threading.Tasks;

namespace Motocycle.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        bool Commit();

        Task<bool> CommitAsync();
    }


}

