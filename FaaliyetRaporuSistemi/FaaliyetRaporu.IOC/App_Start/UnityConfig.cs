using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Data.Repositories;
using FaaliyetRaporu.Data.UnitOfWork;
using FaaliyetRaporu.Service.AciklamaService;
using FaaliyetRaporu.Service.DurumService;
using FaaliyetRaporu.Service.FaaliyetRaporService;
using FaaliyetRaporu.Service.FaaliyetTuruService;
using FaaliyetRaporu.Service.GenelAyarlarService;
using FaaliyetRaporu.Service.GuncellemelerService;
using FaaliyetRaporu.Service.IslemSonucuService;
using FaaliyetRaporu.Service.KodService;
using FaaliyetRaporu.Service.KonuService;
using FaaliyetRaporu.Service.KullaniciAdresService;
using FaaliyetRaporu.Service.KullaniciGirisCikisTarihiService;
using FaaliyetRaporu.Service.KullaniciService;
using FaaliyetRaporu.Service.RolService;
using FaaliyetRaporu.Service.SonucAciklamaService;
using FaaliyetRaporu.Service.TalepService;
using FaaliyetRaporu.Service.YedeklemeService;
using FaaliyetRaporu.Service.YonlendirmeService;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.IOC.App_Start
{
    public static class UnityConfig
    {


        public static IUnityContainer RegisterComponents()
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        private static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IGenericRepository<Kod>, GenericRepository<Kod>>();
            container.RegisterType<IGenericRepository<Rol>, GenericRepository<Rol>>();
            container.RegisterType<IGenericRepository<Konu>, GenericRepository<Konu>>();
            container.RegisterType<IGenericRepository<Durum>, GenericRepository<Durum>>();
            container.RegisterType<IGenericRepository<Talep>, GenericRepository<Talep>>();
            container.RegisterType<IGenericRepository<Yedekleme>, GenericRepository<Yedekleme>>();
            container.RegisterType<IGenericRepository<Kullanici>, GenericRepository<Kullanici>>();
            container.RegisterType<IGenericRepository<Aciklamalar>, GenericRepository<Aciklamalar>>();
            container.RegisterType<IGenericRepository<IslemSonucu>, GenericRepository<IslemSonucu>>();
            container.RegisterType<IGenericRepository<Yonlendirme>, GenericRepository<Yonlendirme>>();            
            container.RegisterType<IGenericRepository<FaaliyetTuru>, GenericRepository<FaaliyetTuru>>();
            container.RegisterType<IGenericRepository<GenelAyarlar>, GenericRepository<GenelAyarlar>>();            
            container.RegisterType<IGenericRepository<Guncellemeler>, GenericRepository<Guncellemeler>>();
            container.RegisterType<IGenericRepository<FaaliyetRapor>, GenericRepository<FaaliyetRapor>>();
            container.RegisterType<IGenericRepository<SonucAciklama>, GenericRepository<SonucAciklama>>();            
            container.RegisterType<IGenericRepository<KullaniciAdres>, GenericRepository<KullaniciAdres>>();
            container.RegisterType<IGenericRepository<KullaniciGirisCikisTarihi>, GenericRepository<KullaniciGirisCikisTarihi>>();

            container.RegisterType<IKodService, KodService>();
            container.RegisterType<IRolService, RolService>();
            container.RegisterType<IUnitOfWork, IUnitOfWork>();
            container.RegisterType<IKonuService, KonuService>();
            container.RegisterType<IDurumService, DurumService>();
            container.RegisterType<ITalepService, TalepService>();
            container.RegisterType<IYedeklemeService, YedeklemeService>();
            container.RegisterType<IKullaniciService, KullaniciService>();
            container.RegisterType<IAciklamaService, AciklamaService>();
            container.RegisterType<IIslemSonucuService, IslemSonucuService>();
            container.RegisterType<IYonlendirmeService, YonlendirmeService>();
            container.RegisterType<IFaaliyetTuruService, FaaliyetTuruService>();
            container.RegisterType<IGenelAyarlarService, GenelAyarlarService>();
            container.RegisterType<IGuncellemelerService, GuncellemelerService>();
            container.RegisterType<IFaaliyetRaporService, FaaliyetRaporService>();
            container.RegisterType<ISonucAciklamaService, SonucAciklamaService>();
            container.RegisterType<IKullaniciAdresService, KullaniciAdresService>();
            container.RegisterType<IKullaniciGirisCikisTarihiService, KullaniciGirisCikisTarihiService>();

            return container;
        }
    }

    public static class IOCExtensions
    {
        public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
        public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
        {
            container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
        }
    }
}
