using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"SonucAciklama")]
    public partial class SonucAciklama:BaseEntity
    {
        [StringLength(50)]
        public string SonucAdi { get; set; }
    }
}
