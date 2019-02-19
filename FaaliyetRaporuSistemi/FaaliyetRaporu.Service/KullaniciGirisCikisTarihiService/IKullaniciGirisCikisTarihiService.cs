using FaaliyetRaporu.Core.Domain.Entites;
using FaaliyetRaporu.Service.ServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Service.KullaniciGirisCikisTarihiService
{
    public interface IKullaniciGirisCikisTarihiService:IService<KullaniciGirisCikisTarihi>
    {
       KullaniciGirisCikisTarihi SonGirisTarihi(int id);
    }
}
