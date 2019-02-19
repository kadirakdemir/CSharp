using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Rol")]
    public partial class Rol:BaseEntity
    {
        public Rol()
        {
            kullanici = new HashSet<Kullanici>();
        }

        [StringLength(50)]
        public string RolAdi { get; set; }
        public virtual ICollection<Kullanici> kullanici { get; set; }

    }
}
