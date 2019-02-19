using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Data.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity:class
    {
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        TEntity GetById(int id);
        TEntity Find(Expression<Func<TEntity, bool>> predicate);

        TEntity Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
        void Save();
    }
}
