using System.Collections.Generic;
using System.Threading.Tasks;
using Motocycle.Domain.Interfaces.Repositories.Base;
using Motocycle.Domain.Models;

namespace Motocycle.Domain.Models
{
    public interface IMotocyRepository : IBaseRepository<Motocy>
    {
        Task<List<Motocy>> AddRangeAsync(List<Motocy> entities);
        Task<Motocy> AddAsync(Motocy entity);

        Task<Motocy> UpdateAsync(Motocy entity);
        Task DeleteAll();
        Task Delete(Guid Id);
        Task<Motocy> GetByPlateAsync(string plate);
        Task<Motocy> GetDefaultAsync();
    }
}
