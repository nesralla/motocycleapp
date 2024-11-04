using Motocycle.Domain.Interfaces.Services.Base;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Interfaces.Services
{
    public interface IDeliverymanService : IBaseServiceEntity<Deliveryman>
    {

        Task<Deliveryman> GetByNationalIDAsync(string nationalid);
        Task<Deliveryman> GetDefaultDeliverymanAsync();
        Task<Deliveryman> RemoveDeliverymanAsync(Guid id);

    }
}

