using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Motocycle.Domain.Core.Models;
using Motocycle.Domain.Repositories.Base;
using Motocycle.Infra.Data.Context;
using PXBank.CiotProviderService.Infra.Data.Context;

namespace Motocycle.Infra.Data.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity
    {
        protected DbSet<TEntity> DbSet { get; }
        protected ApplicationDbContext Db { get; }

        public BaseRepository(ApplicationDbContext context)
        {
            Db = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> GetAllQuery
        {
            get
            {
                return DbSet;
            }
        }

        public virtual IQueryable<TEntity> GetAllQueryNoTracking
        {
            get
            {
                return DbSet.AsNoTracking();
            }
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public bool Exists(Guid id)
        {
            return DbSet.Any(x => x.Id == id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await DbSet.AnyAsync(x => x.Id == id);
        }

        public TEntity GetById(Guid id)
            => DbSet.FirstOrDefault(x => x.Id == id);

        public async Task<TEntity> GetByIdAsync(Guid id)
            => await DbSet.FirstOrDefaultAsync(x => x.Id == id);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            Db.Dispose();
        }
    }
}
