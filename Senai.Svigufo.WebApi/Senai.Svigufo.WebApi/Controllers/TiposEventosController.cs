using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Repositories;
using Senai.Svigufo.WebApi.Interfaces;

namespace Senai.Svigufo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposEventosController : ControllerBase
    {
        private ITipoEventoRepository TipoEventoRepositorio { get; set; }

        public TiposEventosController()
        {
            TipoEventoRepositorio = new TipoEventoRepository();
        }

        //List<TipoEventoDomain> eventos = new List<TipoEventoDomain>()
        //{
        //    new TipoEventoDomain { Id = 1, Nome = "Tipo Evento A"}
        //    ,new TipoEventoDomain { Id = 2, Nome = "Tipo Evento B"}
        //    ,new TipoEventoDomain { Id = 3, Nome = "Tipo Evento C"}
        //    ,new TipoEventoDomain { Id = 4, Nome = "Tipo Evento D"}
        //};

        [Produces("application/json")]
        [HttpGet]
        public IEnumerable<TipoEventoDomain> Get()
        {
            return TipoEventoRepositorio.Listar();
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TipoEventoDomain evento = TipoEventoRepositorio.BuscarPorId(id);
            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);

        }

        [HttpPost]
        public IActionResult Post(TipoEventoDomain tipoEvento)
        {
            //eventos.Add(new TipoEventoDomain() { Id = eventos.Count + 1, Nome = tipoEvento.Nome });
            //return Ok(eventos);
            //tipoEvento.Id = eventos.Count + 1;
            //return Ok(tipoEvento);
            try
            {
                TipoEventoRepositorio.Cadastrar(tipoEvento);
                return Ok();
            }
            catch 
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(TipoEventoDomain tipoEvento)
        {
            //var eventoEncontrado = eventos.Find(x => x.Id == tipoEvento.Id);
            //eventoEncontrado.Nome = tipoEvento.Nome;

            //return Ok(eventos);
            TipoEventoDomain eventoASerAtualizado = TipoEventoRepositorio.BuscarPorId(tipoEvento.Id);
            if (eventoASerAtualizado == null)
            {
                return NotFound();
            }

            try
            {
                TipoEventoRepositorio.Alterar(tipoEvento);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            eventos.Remove(eventos.Find(x => x.Id == id));
            return Ok(eventos);
        }
    }
}