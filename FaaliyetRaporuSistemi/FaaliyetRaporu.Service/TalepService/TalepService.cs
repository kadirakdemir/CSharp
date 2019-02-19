using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.TalepService
{
    public class TalepService : ServiceBase<Talep>, ITalepService
    {
        public TalepService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
