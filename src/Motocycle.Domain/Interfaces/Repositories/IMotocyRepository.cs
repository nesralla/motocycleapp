using System.Collections.Generic;
using System.Threading.Tasks;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Entities
{
    public interface IMotocyRepository : IBaseRepository<Motocy>
    {
        Task<List<Motocy>> AddRangeAsync(List<Motocy> entities);
        Task DeleteAll();
        Task<Motocy> GetByPlateAsync(string plate);
        Task<Motocy> GetDefaultAsync();
    }
}
