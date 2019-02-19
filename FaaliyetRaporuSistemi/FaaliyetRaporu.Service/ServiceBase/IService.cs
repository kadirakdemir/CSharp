using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Service.ServiceBase
{
    public interface IService<TEntity> where TEntity:class
    {
        IQueryable<TEntity> TumKayitlar();

        TEntity Find(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        // id parametresi ile kaydı bul
        TEntity Bul(int id);

        // kayıt ekle
        TEntity Ekle(TEntity entity);

        // kayıt sil
        void Sil(TEntity entity);

        // kayıt düzenle
        void Guncelle(TEntity entity);

       
    }
}
