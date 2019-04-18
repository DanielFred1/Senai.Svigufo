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
        string stringDeConexao = "Data Source=.\\SqlExpress01; initial catalog=SENAI_SVIGUFO_MANHA; Integrated Security=True";
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

        public void Alterar(TipoEventoDomain tipoEvento)
        {
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                string queryASerExecutada = "UPDATE TIPOS_EVENTOS SET TITULO = @TITULO WHERE ID = @ID;";
                SqlCommand cmd = new SqlCommand(queryASerExecutada, con);
                cmd.Parameters.AddWithValue("@TITULO", tipoEvento.Nome);
                cmd.Parameters.AddWithValue("@ID", tipoEvento.Id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public TipoEventoDomain BuscarPorId(int id)
        {
            string queryASerExecutada = "SELECT ID, TITULO FROM TIPOS_EVENTOS WHERE ID = @ID";

            //Cria um novo evento
            TipoEventoDomain tipoEvento = new TipoEventoDomain();
            //Abre uma nova instancia na conexão
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                //Novo comando que será executado na conexão
                using (SqlCommand cmd = new SqlCommand(queryASerExecutada, con))
                {
                    //Passa o parâmetro recebido para a query a ser executada
                    cmd.Parameters.AddWithValue("@ID", id);
                    //Abre a conexão
                    con.Open();
                    //Lê os registros
                    SqlDataReader lerOsRegistros = cmd.ExecuteReader();
                    //Agora pode verificar se há linhas
                    if (lerOsRegistros.HasRows)
                    {
                        //enquanto tiver registros, no caso, somente um, coloca os dados no objeto criado
                        while (lerOsRegistros.Read())
                        {
                            tipoEvento.Id = Convert.ToInt32(lerOsRegistros["ID"].ToString());
                            tipoEvento.Nome = lerOsRegistros["TITULO"].ToString();
                        }
                        return tipoEvento;
                    }
                }
            }
            //Caso não tenha registros, retorna nulo
            return null;
        }

        public void Cadastrar(TipoEventoDomain tipoEvento)
        {
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                string queryASerExecutada = "INSERT INTO TIPOS_EVENTOS (TITULO) VALUES (@TITULO)";

                SqlCommand cmd = new SqlCommand(queryASerExecutada, con);
                cmd.Parameters.AddWithValue("@TITULO", tipoEvento.Nome);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Deletar(int id)
        {
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                string comandoSQL = "DELETE FROM TIPOS_EVENTOS WHERE ID = @ID;";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
