using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.KullaniciAdresService
{
    public class KullaniciAdresService : ServiceBase<KullaniciAdres>,IKullaniciAdresService
    {
        public KullaniciAdresService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
