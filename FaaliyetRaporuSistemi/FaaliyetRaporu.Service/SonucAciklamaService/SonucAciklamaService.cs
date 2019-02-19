using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;

namespace FaaliyetRaporu.Service.SonucAciklamaService
{
    public class SonucAciklamaService : ServiceBase<SonucAciklama>, ISonucAciklamaService
    {
        public SonucAciklamaService(UnitOfWork uow) : base(uow)
        {
        }
    }
}
