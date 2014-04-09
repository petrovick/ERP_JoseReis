﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ERP_JOSEREIS.Models
{
    public class Cargo
    {
        [Key]
        public Int16 IdCargo { get; set; }
        public String Descricao { get; set; }
        public ICollection<Funcionario> Funcionario { get; set; }

    }
}