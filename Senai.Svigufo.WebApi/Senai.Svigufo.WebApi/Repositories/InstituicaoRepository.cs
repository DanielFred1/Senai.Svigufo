using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senai.Svigufo.WebApi.Domains;
using Senai.Svigufo.WebApi.Interfaces;
using System.Data.SqlClient;

namespace Senai.Svigufo.WebApi.Repositories
{
    public class InstituicaoRepository : IInstituicaoRepository
    {
        string stringDeConexao = "Data Source=.\\SqlExpress; initial catalog=SENAI_SVIGUFO_MANHA; User Id=sa; Pwd=132";

        public void Cadastrar(InstituicaoDomain instituicao)
        {
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                string comandoSQL = "INSERT INTO INSTITUICOES (RAZAO_SOCIAL, NOME_FANTASIA, CNPJ, LOGRADOURO, CEP, UF, CIDADE) VALUES (@RAZAO_SOCIAL, @NOME_FANTASIA, @CNPJ, @LOGRADOURO, @CEP, @UF, @CIDADE)";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
                cmd.Parameters.AddWithValue("@NOME_FANTASIA", instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@CNPJ", instituicao.CNPJ);
                cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP", instituicao.Cep);
                cmd.Parameters.AddWithValue("@UF", instituicao.Uf);
                cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Alterar(InstituicaoDomain instituicao)
        {
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                string comandoSQL = "UPDATE INSTITUICOES SET RAZAO_SOCIAL = @RAZAO_SOCIAL" +
                    ", NOME_FANTASIA = @NOME_FANTASIA" +
                    ", CNPJ = @CNPJ" +
                    ", LOGRADOURO = @LOGRADOURO" +
                    ", CEP = @CEP" +
                    ", UF = @UF" +
                    ", CIDADE = @CIDADE" +
                    " WHERE ID = @ID";

                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@ID", instituicao.Id);
                cmd.Parameters.AddWithValue("@RAZAO_SOCIAL", instituicao.RazaoSocial);
                cmd.Parameters.AddWithValue("@NOME_FANTASIA", instituicao.NomeFantasia);
                cmd.Parameters.AddWithValue("@CPNJ", instituicao.CNPJ);
                cmd.Parameters.AddWithValue("@LOGRADOURO", instituicao.Logradouro);
                cmd.Parameters.AddWithValue("@CEP", instituicao.Cep);
                cmd.Parameters.AddWithValue("@UF", instituicao.Uf);
                cmd.Parameters.AddWithValue("@CIDADE", instituicao.Cidade);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public InstituicaoDomain BuscarPorId(int id)
        {
            InstituicaoDomain instituicao = new InstituicaoDomain();

            //Cria a conexão com o banco de dados
            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                //Query a ser executada no Banco de Dados
                string comandoSQL = "SELECT * FROM INSTITUICOES WHERE ID = @ID ORDER BY ID ASC";
                SqlCommand cmd = new SqlCommand(comandoSQL, con);
                cmd.Parameters.AddWithValue("@ID", id);
                con.Open();
                //Se o id buscado for igual ao valor passado, faz a consulta
                SqlDataReader lerOsRegistros = cmd.ExecuteReader();
                //Verifica se possui os campos
                if (lerOsRegistros.HasRows)
                {
                    while (lerOsRegistros.Read())
                    {
                        instituicao.Id = Int32.Parse(lerOsRegistros["ID"].ToString());
                        instituicao.RazaoSocial = lerOsRegistros["RAZAO_SOCIAL"].ToString();
                        instituicao.NomeFantasia = lerOsRegistros["NOME_FANTASIA"].ToString();
                        instituicao.CNPJ = lerOsRegistros["CNPJ"].ToString();
                        instituicao.Cep = lerOsRegistros["CEP"].ToString();
                        instituicao.Logradouro = lerOsRegistros["LOGRADOURO"].ToString();
                        instituicao.Uf = lerOsRegistros["UF"].ToString();
                        instituicao.Cidade = lerOsRegistros["CIDADE"].ToString();
                    }
                    //Retorna a instituição 
                    return instituicao;
                }
            }
            //Se não possuir instituição com o Id passado, retorna nulo
            return null;
        }

        public List<InstituicaoDomain> Listar()
        {            
            string queryASerExecutada = "SELECT ID, NOME_FANTASIA, RAZAO_SOCIAL, CNPJ, CEP, LOGRADOURO, UF, CIDADE FROM INSTITUICOES";

            //Cria uma nova lista
            List<InstituicaoDomain> listaInstituicoes = new List<InstituicaoDomain>();

            using (SqlConnection con = new SqlConnection(stringDeConexao))
            {
                //Permite que você crie queries e envie para o banco de dados
                using (SqlCommand cmd = new SqlCommand(queryASerExecutada, con))
                {
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //Cria um novo objeto e insere na lista
                        InstituicaoDomain instituicao = new InstituicaoDomain
                        {
                            Id = Convert.ToInt32(rdr["ID"]),
                            NomeFantasia = rdr["NOME_FANTASIA"].ToString(),
                            RazaoSocial = rdr["RAZAO_SOCIAL"].ToString(),
                            CNPJ = rdr["CNPJ"].ToString(),
                            Cep = rdr["CEP"].ToString(),
                            Logradouro = rdr["LOGRADOURO"].ToString(),
                            Uf = rdr["UF"].ToString(),
                            Cidade = rdr["CIDADE"].ToString(),
                        };
                        //Adiciona cada evento na lista
                        listaInstituicoes.Add(instituicao);
                    }
                }
            }
            //Retorna a propria lista
            return listaInstituicoes;
        }
    }
}
