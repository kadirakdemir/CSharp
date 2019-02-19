using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"KullaniciGirisCikisTarihi")]
    public partial class KullaniciGirisCikisTarihi:BaseEntity
    {
        public DateTime GirisTarihi { get; set; }
        public DateTime CikisTarihi { get; set; }
        public int KullaniciID { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
