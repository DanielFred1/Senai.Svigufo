using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.ViewModels;

namespace Senai.Svigufo.WebApi.Interfaces
{
    interface IUsuarioRepository
    {
        UsuarioDomain BuscarPorEmailESenha(LoginViewModel login);

        void Cadastrar(UsuarioDomain usuario);
    }
}
