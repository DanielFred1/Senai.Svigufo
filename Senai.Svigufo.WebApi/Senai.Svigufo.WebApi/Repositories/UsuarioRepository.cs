using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Interfaces;
using System.Data.SqlClient;
using Senai.Svigufo.WebApi.ViewModels;
using System.Data.SqlClient;

namespace Senai.Svigufo.WebApi.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        string stringDeConexao = "Data Source=.\\SqlExpress01; initial catalog=SENAI_SVIGUFO_MANHA; Integrated Security=True";

        public UsuarioDomain BuscarPorEmailESenha(LoginViewModel login)
        {
            string QueryEmailSenha = "SELECT ID, NOME, EMAIL, TIPO_USUARIO FROM USUARIOS WHERE EMAIL = @EMAIL AND SENHA = @SENHA;";

            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                using (SqlCommand cmd = new SqlCommand(QueryEmailSenha, con))
                {
                    cmd.Parameters.AddWithValue("@EMAIL", login.Email);
                    cmd.Parameters.AddWithValue("@SENHA", login.Senha);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        UsuarioDomain usuarioBuscado = new UsuarioDomain();
                        while (rdr.Read())
                        {
                            usuarioBuscado.Id = Int32.Parse(rdr["ID"].ToString());
                            usuarioBuscado.Email = rdr["EMAIL"].ToString();
                            usuarioBuscado.Nome = rdr["NOME"].ToString();
                            usuarioBuscado.TipoUsuario = rdr["TIPO_USUARIO"].ToString();
                        }
                        return usuarioBuscado;
                    }
                }
                return null;
            }
            
        }

        public void Cadastrar(UsuarioDomain usuario)
        {
            string QueryInsert = "INSERT INTO USUARIOS (NOME, EMAIL, SENHA, TIPO_USUARIO) VALUES (@NOME, @EMAIL, @SENHA, @TIPO_USUARIO)";

            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand(QueryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@NOME", usuario.Nome);
                    cmd.Parameters.AddWithValue("@EMAIL", usuario.Email);
                    cmd.Parameters.AddWithValue("@SENHA", usuario.Senha);
                    cmd.Parameters.AddWithValue("@TIPO_USUARIO", usuario.TipoUsuario);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
