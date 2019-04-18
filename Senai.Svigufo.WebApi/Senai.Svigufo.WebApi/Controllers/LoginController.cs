using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Interfaces;
using Senai.Svigufo.WebApi.Repositories;
using Senai.Svigufo.WebApi.ViewModels;

namespace Senai.Svigufo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository UsuarioRepository { get; set; }

        public LoginController()
        {
            UsuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Busca um usuario por email e senha
        /// </summary>
        /// <param name="login"></param>
        /// <returns>Retorna uma autenticação para o usuario</returns>
        [HttpPost]
        public IActionResult Post(LoginViewModel login)
        {
            try
            {
                UsuarioDomain usuario = UsuarioRepository.BuscarPorEmailESenha(login);
                if (usuario == null)
                {
                    return NotFound(
                        new
                        {
                            mensagem = "Usuário não foi encontrado."
                        }
                    );
                }
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}