using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;

namespace Senai.Svigufo.WebApi.Interfaces
{
    public interface IConviteRepository
    {
        List<ConviteDomain> ListarMeusConvites(int id);

        List<ConviteDomain> Listar();

        void Cadastrar(ConviteDomain convite);

        void Alterar(ConviteDomain convite, int id);
    }
}
