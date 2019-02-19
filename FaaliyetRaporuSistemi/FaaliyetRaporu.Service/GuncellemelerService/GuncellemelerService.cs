using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.GuncellemelerService
{
    public class GuncellemelerService : ServiceBase<Guncellemeler>, IGuncellemelerService
    {
        public GuncellemelerService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
