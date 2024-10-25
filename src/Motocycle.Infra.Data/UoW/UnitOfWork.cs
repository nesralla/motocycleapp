using System;
using System.Threading.Tasks;
using Motocycle.Domain.Interfaces.Repositories;
using Motocycle.Infra.Data.Context;

namespace Motocycle.Infra.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            var rowsAffected = _context.SaveChanges();
            return (rowsAffected > 0);
        }

        public async Task<bool> CommitAsync()
        {
            var rowsAffected = await _context.SaveChangesAsync();
            return (rowsAffected > 0);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
