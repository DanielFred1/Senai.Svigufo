using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Interfaces;
using Senai.Svigufo.WebApi.Repositories;

namespace Senai.Svigufo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicoesController : ControllerBase
    {
        private IInstituicaoRepository InstituicaoRepository { get; set; }

        public InstituicoesController()
        {
            InstituicaoRepository = new InstituicaoRepository();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(InstituicaoRepository.Listar());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            InstituicaoDomain instituicao = InstituicaoRepository.BuscarPorId(id);
            if (instituicao == null)
            {
                return NotFound();
            }
            return Ok(instituicao);
        }

        [HttpPost]
        public IActionResult Post(InstituicaoDomain instituicao)
        {
            try
            {
                InstituicaoRepository.Cadastrar(instituicao);
                return Ok(instituicao);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public IActionResult Put(int id, InstituicaoDomain instituicao)
        {
            InstituicaoDomain buscada = InstituicaoRepository.BuscarPorId(id);
            if (buscada == null)
            {
                return NotFound();
            }
            try
            {
                instituicao.Id = id;
                InstituicaoRepository.Cadastrar(instituicao);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}