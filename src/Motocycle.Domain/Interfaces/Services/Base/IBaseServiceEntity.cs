using System;
using System.Linq;
using System.Threading.Tasks;
using Motocycle.Domain.Core.Models;

namespace Motocycle.Domain.Interfaces.Services.Base
{
    public interface IBaseServiceEntity<TEntity> : IBaseService where TEntity : Entity
    {
        Task<TEntity> RegisterAsync(TEntity entity);
        IQueryable<TEntity> GetAllQuery { get; }
        IQueryable<TEntity> GetAllQueryAsNoTracking { get; }
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
    }
}
