using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Svigufo.WebApi.Domains
{
    public class InstituicaoDomain
    {
        public int Id { get; set; }

        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo Razão Social é requerido.")]
        public string RazaoSocial { get; set; }

        public string CNPJ { get; set; }

        public string Logradouro { get; set; }

        public string Cep { get; set; }

        [StringLength(2, MinimumLength = 2, ErrorMessage = "O campo de UF deve conter exatamente dois caracteres.")]
        public string Uf { get; set; }

        public string Cidade { get; set; }
    }
}
