using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Data.Repositories;
using FaaliyetRaporu.Data.UnitOfWork;
using FaaliyetRaporu.IOC.App_Start;
using FaaliyetRaporu.Service.KullaniciAdresService;
using FaaliyetRaporu.Service.KullaniciService;
using FaaliyetRaporu.Service.RolService;
using FaliyetRaporuUygulamasi;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaaliyetRaporuWinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// </summary>
        /// 
       
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var container = UnityConfig.RegisterComponents();         
            Application.Run(container.Resolve<Giris>());
        }
    }
}
