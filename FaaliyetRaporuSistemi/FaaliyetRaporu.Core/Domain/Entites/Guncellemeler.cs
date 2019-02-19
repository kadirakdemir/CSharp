using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Guncellemeler")]
    public partial class Guncellemeler:BaseEntity
    {
        public string Version { get; set; }
        public DateTime GuncellemeTarihi { get; set; }
        public DateTime DenetlemeTarihi { get; set; }
    }
}
