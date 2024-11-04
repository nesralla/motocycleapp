using System.Collections.Generic;
using System.Threading.Tasks;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Models
{
    public interface IDeliverymanRepository : IBaseRepository<Deliveryman>
    {
        Task<List<Deliveryman>> AddRangeAsync(List<Deliveryman> entities);
        Task<Deliveryman> AddAsync(Deliveryman entity);

        Task<Deliveryman> UpdateAsync(Deliveryman entity);
        Task DeleteAll();
        Task Delete(Guid Id);
        Task<Deliveryman> GetByNationalIDAsync(string nationalid);
        Task<Deliveryman> GetDefaultAsync();
    }
}
