using ERP_JOSEREIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_JOSEREIS.ViewModels
{
    public class FornecedorViewModel
    {
        public PessoaFisica pessoaFisica { get; set; }
        public PessoaJuridica pessoaJuridica { get; set; }
        public Fornecedor fornecedor { get; set; }
        public FornecedorViewModel(Fornecedor fornecedor, PessoaFisica pf)
        {
            this.fornecedor = fornecedor;
            this.pessoaFisica = pf;
        }

        public FornecedorViewModel(Fornecedor fornecedor, PessoaJuridica pj)
        {
            this.fornecedor = fornecedor;
            this.pessoaJuridica = pj;
        }

    }
}