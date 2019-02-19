using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Kod")]
    public partial class Kod:BaseEntity
    {
        [StringLength(10)]
        public string KodAdi { get; set; }
    }
}
