using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;
using FaaliyetRaporu.Data.Repositories;

namespace FaaliyetRaporu.Service.KullaniciService
{
    public class KullaniciService : ServiceBase<Kullanici>, IKullaniciService
    {
        private readonly IGenericRepository<Kullanici> _repository;
        public KullaniciService(UnitOfWork uow) : base(uow)
        {
            _repository = uow.GetRepository<Kullanici>();
        }
      
        public Kullanici OturumAc(string kullaniciIsmi, string kullaniciSifre)
        {
            return _repository.Find(x => x.Adi == kullaniciIsmi && x.Sifre == kullaniciSifre);
        }

        public void ResimEkle()
        {
           
        }
    }
}
