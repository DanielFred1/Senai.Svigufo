using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Interfaces;
using Senai.Svigufo.WebApi.Repositories;
using System.IdentityModel.Tokens.Jwt;

namespace Senai.Svigufo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConviteController : ControllerBase
    {
        public IConviteRepository ConviteRepositorio { get; set; }

        public IEventoRepository EventoRepositorio { get; set; }

        public ConviteController()
        {
            ConviteRepositorio = new ConviteRepository();
            EventoRepositorio = new EventoRepository();
        }


        [HttpPost]
        [Authorize]
        [Route("convidar")]
        public IActionResult Convite(ConviteDomain convite)
        {
            try
            {
                EventoDomain evento = EventoRepositorio.BuscarPorId(convite.EventoId);

                if (evento != null)
                {
                    return NotFound();
                }

                convite.Situacao = (evento.AcessoLivre ? EnSituacaoConvite.APROVADO : EnSituacaoConvite.AGUARDANDO);

                ConviteRepositorio.Cadastrar(convite);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost("entrar/{eventoid}")]
        public IActionResult Inscricao(int eventoid)
        {
            try
            {
                int usuarioid = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);

                EventoDomain evento = EventoRepositorio.BuscarPorId(eventoid);

                if (evento != null)
                {
                    return NotFound();
                }

                ConviteDomain convite = new ConviteDomain
                {
                    EventoId = eventoid,
                    UsuarioId = usuarioid,
                    Situacao = (evento.AcessoLivre ? EnSituacaoConvite.APROVADO : EnSituacaoConvite.AGUARDANDO)
                };

                ConviteRepositorio.Cadastrar(convite);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize("ADMINISTRADOR")]
        [HttpPut("{id}")]
        public IActionResult Put(ConviteDomain convite, int id)
        {
            try
            {
                ConviteRepositorio.Alterar(convite, id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpGet]
        [Route("listar")]
        public IActionResult ListarTodos()
        {
            return Ok(ConviteRepositorio.Listar());
        }

        [Authorize]
        [HttpGet]
        public IActionResult MeusConvites()
        {
            int usuarioid = Convert.ToInt32(HttpContext.User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value);
            return Ok(ConviteRepositorio.ListarMeusConvites(usuarioid));
        }
    }
}