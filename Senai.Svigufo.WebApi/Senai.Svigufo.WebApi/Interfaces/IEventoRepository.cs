using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Repositories;

namespace Senai.Svigufo.WebApi.Interfaces
{
    public interface IEventoRepository
    {
        /// <summary>
        /// Lista todos os eventos
        /// </summary>
        /// <returns>Retorna uma lista com todos os eventos cadastrados</returns>
        List<EventoDomain> Listar();

        /// <summary>
        /// Cadastra um evento
        /// </summary>
        /// <param name="evento">EventoDomain</param>
        void Cadastrar(EventoDomain evento);

        /// <summary>
        /// Atualiza um evento existente
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="evento">EventoDomain</param>
        void Atualizar(int id, EventoDomain evento);
    }
}
