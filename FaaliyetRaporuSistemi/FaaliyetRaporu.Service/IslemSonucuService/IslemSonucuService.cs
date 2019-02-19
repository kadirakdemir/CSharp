using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.IslemSonucuService
{
    public class IslemSonucuService : ServiceBase<IslemSonucu>, IIslemSonucuService
    {
        public IslemSonucuService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
