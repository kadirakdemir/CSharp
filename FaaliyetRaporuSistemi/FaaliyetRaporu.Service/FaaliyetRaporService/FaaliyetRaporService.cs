using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaaliyetRaporu.Data.UnitOfWork;
using FaaliyetRaporu.Data.Repositories;

namespace FaaliyetRaporu.Service.FaaliyetRaporService
{
    public class FaaliyetRaporService : ServiceBase<FaaliyetRapor>,IFaaliyetRaporService
    {
        private readonly IGenericRepository<FaaliyetRapor> _repository;
        public FaaliyetRaporService(UnitOfWork uow) : base(uow)
        {
            _repository = uow.GetRepository<FaaliyetRapor>();
        }

        public IQueryable<FaaliyetRapor> kayitGetir(string kod)
        {

            return _repository.Get(x => x.Kod == kod);
        }
    }
}
