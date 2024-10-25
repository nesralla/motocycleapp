using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Motocycle.Domain.Models;
using Motocycle.Infra.Data.Context;
using Motocycle.Infra.Data.Repositories.Base;

namespace Motocycle.Infra.Data.Repositories
{
    public class MotocyRepository : BaseRepository<Motocy>
    {
        public MotocyRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Motocy> GetByPlateAsync(string plate)
        {
            var motocy = await DbSet.AsNoTracking().FirstOrDefaultAsync(x => x.LicensePlate == plate);
            if (motocy == null)
            {
                throw new InvalidOperationException("Dados Invalidos");
            }
            return motocy;
        }

        public async Task<Motocy> GetDefaultAsync()
        {
            var motocy = await DbSet.AsNoTracking().FirstOrDefaultAsync();
            if (motocy == null)
            {
                throw new InvalidOperationException("Dados Invalidos");
            }
            return motocy;
        }

    }
}
