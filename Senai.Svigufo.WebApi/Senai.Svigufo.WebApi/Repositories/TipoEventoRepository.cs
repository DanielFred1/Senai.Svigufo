using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Interfaces;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Repositories;


namespace Senai.Svigufo.WebApi.Repositories
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        public List<TipoEventoDomain> Listar()

        public void Alterar(TipoEventoDomain tipoEvento)

        public TipoEventoDomain BuscarPorId(int id)

        public void Cadastrar(TipoEventoDomain tipoEvento)

        public void Deletar(int id)
    }
}
