using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;
using FaaliyetRaporu.Data.Repositories;

namespace FaaliyetRaporu.Service.KullaniciGirisCikisTarihiService
{
    public class KullaniciGirisCikisTarihiService : ServiceBase<KullaniciGirisCikisTarihi>, IKullaniciGirisCikisTarihiService
    {
        private readonly IGenericRepository<KullaniciGirisCikisTarihi> _repository;
        public KullaniciGirisCikisTarihiService(UnitOfWork uow) : base(uow)
        {
            _repository = uow.GetRepository<KullaniciGirisCikisTarihi>();
        }

        public KullaniciGirisCikisTarihi SonGirisTarihi(int id)
        {
            return _repository.Get(x => x.KullaniciID == id).OrderBy(k => k.GirisTarihi).ToList().Last();
        }
    }
}
