using FaaliyetRaporu.Core.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Data.DataContext
{
    public partial class FaaliyetRaporuContext:DbContext
    {
        public FaaliyetRaporuContext():base("name=FaaliyetRaporuContext")
        {
            Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<FaaliyetRapor> FaaliyetRapor { get; set; }
        public DbSet<Kod> Kod { get; set; }
        public DbSet<Konu> Konu { get; set; }
        public DbSet<FaaliyetTuru> FaaliyetTuru { get; set; }
        public DbSet<Kullanici> Kullanici { get; set; }
        public DbSet<KullaniciAdres> KullaniciAdres { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<SonucAciklama> SonucAciklama { get; set; }
        public DbSet<Talep> Talep { get; set; }
        public DbSet<Yonlendirme> Yonlendirme { get; set; }
        public DbSet<Durum> Durum { get; set; }
        public DbSet<IslemSonucu> IslemSonucu { get; set; }
        public DbSet<KullaniciGirisCikisTarihi> KullaniciGirisCikisTarihi { get; set; }
        public DbSet<Aciklamalar> Aciklamalar { get; set; }
        public DbSet<Yedekleme> Yedekleme { get; set; }
        public DbSet<GenelAyarlar> GenelAyarlar { get; set; }
        public DbSet<Guncellemeler> Guncellemeler { get; set; }
    }
}
