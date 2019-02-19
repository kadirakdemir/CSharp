using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.Repositories;
using FaaliyetRaporu.Data.DataContext;

namespace FaaliyetRaporu.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FaaliyetRaporuContext _context;
        private bool disposed = false;

        public UnitOfWork(FaaliyetRaporuContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return new GenericRepository<TEntity>(_context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
