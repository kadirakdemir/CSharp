using FaaliyetRaporu.Data.DataContext;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly FaaliyetRaporuContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public GenericRepository(FaaliyetRaporuContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_context.Entry(entity).State==EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }
            _dbSet.Remove(entity);
        }

        public virtual void Delete(int id)
        {
            var TEntity = GetById(id);
            if (TEntity!=null)
            {
                _dbSet.Remove(TEntity);
            }
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).SingleOrDefault();
        }

        public virtual IQueryable<TEntity> Get()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
