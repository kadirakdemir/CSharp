using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.KodService
{
    public class KodService : ServiceBase<Kod>,IKodService
    {
        public KodService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
