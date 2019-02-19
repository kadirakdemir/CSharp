﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaaliyetRaporu.Core.Domain.Entites
{
    public partial class BaseEntity
    {
        [Key]
        public int ID { get; set; }
        public bool IsActive { get; set; }
    }
}
