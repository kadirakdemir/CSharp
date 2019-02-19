using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.KonuService
{
    public class KonuService : ServiceBase<Konu>,IKonuService
    {
        public KonuService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
