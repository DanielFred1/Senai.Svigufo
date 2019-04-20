using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Senai.Svigufo.WebApi.Domains
{
    /// <summary>
    /// Dominio do evento
    /// </summary>
    public class EventoDomain
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime DataEvento { get; set; }

        public bool AcessoLivre { get; set; }

        public int InstituicaoId { get; set; }

        public int TipoEventoId { get; set; }

        public TipoEventoDomain TipoEvento { get; set; }

        public InstituicaoDomain Instituicao { get; set; }
    }
}
