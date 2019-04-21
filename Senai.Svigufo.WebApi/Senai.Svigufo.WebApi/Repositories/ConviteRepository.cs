using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Interfaces;
using System.Data.SqlClient;

namespace Senai.Svigufo.WebApi.Repositories
{
    public class ConviteRepository : IConviteRepository
    {
        string stringDeConexao = "Data Source=.\\SqlExpress01; initial catalog=SENAI_SVIGUFO_MANHA; Integrated Security=True";

        public void Cadastrar(ConviteDomain convite)
        {
            string QueryInsert = @"INSERT INTO CONVITES(ID_EVENTO, ID_USUARIO, SITUACAO) 
                                    VALUES (@ID_EVENTO, @ID_USUARIO, @SITUACAO)";

            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(QueryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@ID_EVENTO", convite.EventoId);
                    cmd.Parameters.AddWithValue("@ID_USUARIO", convite.UsuarioId);
                    cmd.Parameters.AddWithValue("@SITUACAO", convite.Situacao);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Alterar(ConviteDomain convite, int id)
        {
            string QueryUpdate = @"UPDATE CONVITES SET ID_EVENTO = @ID_EVENTO, ID_USUARIO = @ID_USUARIO, SITUACAO = 
                @SITUACAO WHERE ID = @ID";

            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(QueryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@ID", convite.Id);
                    cmd.Parameters.AddWithValue("@ID_EVENTO", convite.EventoId);
                    cmd.Parameters.AddWithValue("@ID_USUARIO", convite.UsuarioId);
                    cmd.Parameters.AddWithValue("@SITUACAO", convite.Situacao);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<ConviteDomain> ListarMeusConvites(int id)
        {
            List<ConviteDomain> convites = new List<ConviteDomain>();

            string QuerySelect = @"SELECT C.ID AS ID_CONVITE, C.SITUACAO, E.ID AS ID_EVENTO, E.TITULO AS TITULO_EVENTO, E.DATA_EVENTO, E.ACESSO_LIVRE, TE.TITULO AS TIPO_EVENTO
                                    FROM CONVITES C INNER JOIN EVENTOS  E ON C.ID_EVENTO = E.ID INNER JOIN TIPOS_EVENTOS TE ON E._ID_TIPO_EVENTO = TE.ID WHERE C.ID_USUARIO = @ID;";

            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ConviteDomain convite = new ConviteDomain
                        {
                            Id = Convert.ToInt32(rdr["@ID_CONVITE"]),
                            Situacao = (EnSituacaoConvite)Convert.ToInt32(rdr["SITUACAO"]),
                            Evento = new EventoDomain
                            {
                                Titulo = rdr["TITULO_EVENTO"].ToString(),
                                DataEvento = Convert.ToDateTime(rdr["DATA_EVENTO"]),
                                TipoEvento = new TipoEventoDomain
                                {
                                    Nome = rdr["TIPO_EVENTO"].ToString()
                                }
                            }
                        };

                        convites.Add(convite);
                    }
                }
            }

            return convites;
        }

        public List<ConviteDomain> Listar()
        {
            List<ConviteDomain> convites = new List<ConviteDomain>();
            string QuerySelect = @"SELECT C.ID AS ID_CONVITE, C.SITUACAO, E.ID AS ID_EVENTO, E.TITULO AS TITULO_EVENTO, E.DATA_EVENTO, E.ACESSO_LIVRE, TE.TITULO AS TIPO_EVENTO
                                    , U.ID AS ID_USUARIO, U.Email, U.Nome FROM CONVITES C INNER JOIN USUARIOS U ON C.ID_USUARIO = U.ID INNER JOIN EVENTOS E ON C.ID_EVENTO = E.ID
                                    INNER JOIN TIPOS_EVENTOS TE ON E.ID_TIPO_EVENTO = TE.ID";

            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(QuerySelect, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        ConviteDomain convite = new ConviteDomain
                        {
                            Id = Convert.ToInt32(rdr["ID_CONVITE"]),
                            Situacao = (EnSituacaoConvite)Convert.ToInt32(rdr["SITUACAO"]),
                            Evento = new EventoDomain
                            {
                                Titulo = rdr["TITULO_EVENTO"].ToString(),
                                DataEvento = Convert.ToDateTime(rdr["DATA_EVENTO"]),
                                AcessoLivre = Convert.ToBoolean(rdr["ACESSO_LIVRE"]),
                                TipoEvento = new TipoEventoDomain
                                {
                                    Nome = rdr["TIPO_EVENTO"].ToString()
                                }
                            },
                            Usuario = new UsuarioDomain
                            {
                                Id = Convert.ToInt32(rdr["ID_USUARIO"]),
                                Nome = rdr["NOME"].ToString(),
                                Email = rdr["EMAIL"].ToString()
                            }
                        };
                        convites.Add(convite);
                    }
                }
            }

            return convites;
        }
    }
}
