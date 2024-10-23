using System;
using System.Linq;
using System.Threading.Tasks;
using Motocycle.Domain.Core.Models;

namespace Motocycle.Domain.Repositories.Base
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> GetByIdAsync(Guid id);

        IQueryable<TEntity> GetAllQuery { get; }
        IQueryable<TEntity> GetAllQueryNoTracking { get; }

        Task<bool> ExistsAsync(Guid id);
    }
}
