using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Motocycle.Domain.Models;
using Motocycle.Infra.Data.Context;
using Motocycle.Infra.Data.Repositories.Base;

namespace Motocycle.Infra.Data.Repositories
{
    public class DeliverymanRepository : BaseRepository<Deliveryman>, IDeliverymanRepository
    {
        public DeliverymanRepository(ApplicationDbContext context) : base(context)
        {
        }
        public async Task<List<Deliveryman>> AddRangeAsync(List<Deliveryman> entities)
        {
            await DbSet.AddRangeAsync(entities);
            return entities;
        }
        public async Task<Deliveryman> AddAsync(Deliveryman entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }
        public async Task<Deliveryman> UpdateAsync(Deliveryman entity)
        {
            DbSet.Update(entity);
            return entity;
        }

        public async Task DeleteAll()
        {
            var list = await DbSet.ToListAsync();
            DbSet.RemoveRange(list);
        }
        public async Task Delete(Guid Id)
        {
            var deliveryman = await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id);
            if (deliveryman?.Id == null)
            {
                throw new InvalidOperationException("Dados Invalidos");
            }
            DbSet.Remove(deliveryman);
        }
        public async Task<Deliveryman> GetByNationalIDAsync(string nationalid)
        {
            var deliveryman = await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.NationalID == nationalid);
            if (deliveryman?.Id == null)
            {
                throw new InvalidOperationException("Dados Invalidos");
            }
            return deliveryman;
        }

        public async Task<Deliveryman> GetDefaultAsync()
        {
            var deliveryman = await DbSet.AsNoTracking().FirstOrDefaultAsync();
            if (deliveryman == null)
            {
                throw new InvalidOperationException("Dados Invalidos");
            }
            return deliveryman;
        }

    }
}
