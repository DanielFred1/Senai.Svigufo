using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, login.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Role, usuario.TipoUsuario),
                };

                //Recebe uma instância da classe SymmetricSecurityKey
                //Armazenando a chave de criptografia usada na criação do token
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("svigufo-chave-autenticacao"));

                //Recebe um objeto do tipo SigningCredentials contendo a chave de 
                //criptografia e o algoritmo de segurança empregados na geração
                //de assinaturas digitais para tokens
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: "Svigufo.WebApi",
                    audience: "Svigufo.WebApi",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    }
                ); 
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}