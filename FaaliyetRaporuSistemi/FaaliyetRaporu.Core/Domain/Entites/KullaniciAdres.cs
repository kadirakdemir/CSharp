using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"KullaniciAdres")]
    public partial class KullaniciAdres:BaseEntity
    {
        [StringLength(500)]
        public string Adres { get; set; }

        [StringLength(50)]
        public string Adi { get; set; }
        public int KullaniciID { get; set; }
        public virtual Kullanici Kullanici { get; set; }
    }
}
