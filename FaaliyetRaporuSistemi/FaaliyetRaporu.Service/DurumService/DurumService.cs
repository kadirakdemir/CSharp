using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.DurumService
{
    public class DurumService : ServiceBase<Durum>, IDurumService
    {
        public DurumService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
