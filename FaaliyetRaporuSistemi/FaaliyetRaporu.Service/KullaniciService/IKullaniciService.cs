using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Service.KullaniciService
{
    public interface IKullaniciService:IService<Kullanici>
    {
        Kullanici OturumAc(string kullaniciIsmi, string kullaniciSifre);

        void ResimEkle();
    }
}
