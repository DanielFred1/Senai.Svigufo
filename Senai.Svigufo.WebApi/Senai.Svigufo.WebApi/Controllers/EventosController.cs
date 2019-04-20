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
    public class EventosController : ControllerBase
    {
        public IEventoRepository EventoRepositorio { get; set; }

        public EventosController()
        {
            EventoRepositorio = new EventoRepository();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, EventoDomain evento)
        {
            try
            {
                EventoRepositorio.Atualizar(id, evento);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}