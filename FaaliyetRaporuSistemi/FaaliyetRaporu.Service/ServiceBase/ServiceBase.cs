using FaaliyetRaporu.Data.Repositories;
using FaaliyetRaporu.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Service.ServiceBase
{
    public class ServiceBase<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IUnitOfWork _uow;

        public ServiceBase(UnitOfWork uow)
        {
            _repository = uow.GetRepository<TEntity>();
            _uow = uow;
        }
        public TEntity Bul(int id)
        {
            return _repository.GetById(id);
        }

        public TEntity Ekle(TEntity entity)
        {
            var kaydet = _repository.Add(entity);
            _uow.SaveChanges();
            return kaydet;
        }

        public TEntity Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Find(predicate);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _repository.Get(predicate);
        }

        public void Guncelle(TEntity entity)
        {
            _repository.Update(entity);
            _uow.SaveChanges();
        }

        public void Sil(TEntity entity)
        {
            _repository.Delete(entity);
            _uow.SaveChanges();
        }

        public IQueryable<TEntity> TumKayitlar()
        {
            return _repository.Get();
        }
       
    }
}
