using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Yonlendirme")]
    public partial class Yonlendirme:BaseEntity
    {
        [StringLength(50)]
        public string YonlendirmeAdi { get; set; }
    }
}
