using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"FaaliyetRapor")]
    public partial class FaaliyetRapor:BaseEntity
    {
        public string Talep { get; set; }
        public string FaaliyetTuru { get; set; }
        public string Konu { get; set; }
        public string Yonlendirme { get; set; }
        public string Kod { get; set; }    
        public string SonucAciklama { get; set; }
        public  DateTime IslemBaslangisTarihi { get; set; }
        public DateTime IslemBitisTarihi { get; set; }
        public int DegigistirenId { get; set; }
        public int DurumID { get; set; }
        public int IslemSonucuID { get; set; }
        public int KullaniciId { get; set; }
        public int AciklamaID { get; set; }
        public virtual IslemSonucu IslemSonucu { get; set; }
        public virtual Durum Durum { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Aciklamalar Aciklama { get; set; }
    }
}
