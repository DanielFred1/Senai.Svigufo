using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Repositories;

namespace Senai.Svigufo.WebApi.Interfaces
{
    interface IInstituicaoRepository
    {
        void Cadastrar(InstituicaoDomain instituicao);
        void Alterar(InstituicaoDomain instituicao);
        InstituicaoDomain BuscarPorId(int id);
        List<InstituicaoDomain> Listar();
    }
}
