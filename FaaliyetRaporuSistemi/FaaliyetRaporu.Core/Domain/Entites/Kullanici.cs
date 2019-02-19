using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Kullanici")]
    public partial class Kullanici:BaseEntity
    {
        public Kullanici()
        {      
            KullaniciAdresler = new HashSet<KullaniciAdres>();
            KullaniciGirisCikisTarihleri = new HashSet<KullaniciGirisCikisTarihi>();
        }

        //[Required]
        ////public Guid GuidID { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        public string Sifre { get; set; }

        [StringLength(256)]
        public string GizliSoru { get; set; }

        [StringLength(256)]
        public string GizliSoruCevap { get; set; }

        [StringLength(16)]
        public string CepTelefonNo { get; set; }
        public bool OnaylandiMi { get; set; }
        public bool KilitliMi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime SifreDegistirmeTarihi { get; set; }
        public int RolID { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<KullaniciAdres> KullaniciAdresler { get; set; }
        public virtual ICollection<KullaniciGirisCikisTarihi> KullaniciGirisCikisTarihleri { get; set; }
    }
}
