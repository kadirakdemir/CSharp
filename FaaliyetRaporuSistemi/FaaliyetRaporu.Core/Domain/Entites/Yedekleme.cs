using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    [Table(name:"Yedekleme")]
    public partial class Yedekleme:BaseEntity
    {
        public DateTime YedeklemeTarihi { get; set; }
    }
}
