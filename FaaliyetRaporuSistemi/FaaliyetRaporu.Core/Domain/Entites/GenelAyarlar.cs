using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"GenelAyarlar")]
    public partial class GenelAyarlar:BaseEntity
    {
        public byte YedeklemeSuresi { get; set; }
        public bool OtomatikGuncelleme { get; set; }
        public string KayitYeri { get; set; }
        public int YedeklemeID { get; set; }
        public int GuncellemeID { get; set; }
        public Yedekleme Yedekleme { get; set; }
        public Guncellemeler Guncellemeler { get; set; }
    }
}
