using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;

namespace Senai.Svigufo.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsavel pelo Tipo de Evento Repository
    /// </summary>
    interface ITipoEventoRepository
    {
        void Cadastrar(TipoEventoDomain tipoEvento);
        void Alterar(TipoEventoDomain tipoEvento);
        TipoEventoDomain BuscarPorId(int Id);
        List<TipoEventoDomain> Listar();
        void Deletar(int Id);
    }
}
