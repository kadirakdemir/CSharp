using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Aciklama")]
    public partial class Aciklamalar:BaseEntity
    {
        [StringLength(250)]
        public string Aciklama { get; set; }
    }
}
