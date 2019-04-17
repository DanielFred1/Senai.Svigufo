using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Interfaces;
using Senai.Svigufo.WebApi.Domains;
using System.Data.SqlClient;

namespace Senai.Svigufo.WebApi.Repositories
{
    public class TipoEventoRepository : ITipoEventoRepository
    {
        private string stringDeConexao = "Data Source=localhost;Initial Catalog=SENAI_SVIGUFO;Integrated Security=True";
        private string queryASerExecutada = "SELECT ID, TITULO FROM TIPOS_EVENTOS";

        public List<TipoEventoDomain> Listar()
        {
            //Cria uma nova lista
            List<TipoEventoDomain> listaTiposEventos = new List<TipoEventoDomain>();

            //Garante que os recursos sejam fechados e descartados quando o codigo for encerrado
            //SqlConnection cria uma instancia de objeto
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                //permite que você crie queries e envie para o banco de dados
                using (SqlCommand cmd = new SqlCommand(queryASerExecutada, con))
                {                    
                    con.Open();
                    //Retorna um conjunto de resultados do banco de dados
                    SqlDataReader rdr = cmd.ExecuteReader();
                    //enquanto tiver registros
                    while (rdr.Read())
                    {
                        //Cria um novo objeto e insere na lista
                        TipoEventoDomain tipoEvento = new TipoEventoDomain
                        {
                            Id = Convert.ToInt32(rdr["ID"]),
                            Nome = rdr["TITULO"].ToString()
                        };

                        //Adiciona cada evento na lista
                        listaTiposEventos.Add(tipoEvento);
                    }
                }
            }
            return listaTiposEventos;
        }

        public void Alterar(TipoEventoDomain tipoEvento);

        public TipoEventoDomain BuscarPorId(int id);

        public void Cadastrar(TipoEventoDomain tipoEvento);

        public void Deletar(int id);
    }
}
