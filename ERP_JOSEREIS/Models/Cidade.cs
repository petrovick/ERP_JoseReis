using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP_JOSEREIS.Models
{
    public class Cidade
    {
        [Key]
        public int IdCidade { get; set; }
        [Required(ErrorMessage="Nome da cidade é obrigatório.")]
        [StringLength(60)]
        public string Nome { get; set; }

        public int IdEstado { get; set; }
        [ForeignKey("IdEstado")]   //pega o atributo IdEstado e fala que ele é o ForeignKey, serve para mapear correto.
        public Estado Estado { get; set; }
    }
}