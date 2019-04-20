using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Repositories;

namespace Senai.Svigufo.WebApi.Domains
{
    public class ConviteDomain
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public EventoDomain Evento { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioDomain Usuario { get; set; }

        public EnSituacaoConvite Situacao { get; set; }
    }

    public enum EnSituacaoConvite
    {
        AGUARDANDO = 1,
        APROVADO = 2,
        REPROVADO = 3,
    }
}
