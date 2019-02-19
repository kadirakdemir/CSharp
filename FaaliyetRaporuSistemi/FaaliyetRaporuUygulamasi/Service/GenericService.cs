using FaaliyetRaporu.Service.KodService;
using FaaliyetRaporu.Service.TalepService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaliyetRaporuUygulamasi.Service
{
    public class GenericService<TService> :IGenericService<TService> where TService : class
    {
        #region

        private readonly TService _tService;
       

        #endregion

        public GenericService(TService tservice)
        {
            _tService = tservice;
        }
    }
}
