using Motocycle.Domain.Interfaces.Services.Base;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Interfaces.Services
{
    public interface IMotocyService : IBaseServiceEntity<Motocy>
    {

        Task<Motocy> GetByPlateAsync(string plate);
        Task<Motocy> GetDefaultMotoAsync();
        Task<Motocy> RemoveMotocycleAsync(Guid id);

    }
}

