using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.GenelAyarlarService
{
    public class GenelAyarlarService : ServiceBase<GenelAyarlar>, IGenelAyarlarService
    {
        public GenelAyarlarService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
