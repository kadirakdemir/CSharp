using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Konu")]
    public partial class Konu:BaseEntity
    {
        [StringLength(150)]
        public string KonuAdi { get; set; }
    }
}
