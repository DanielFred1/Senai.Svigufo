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
        public void Cadastrar(InstituicaoDomain instituicao)
        {
            throw new NotImplementedException();
        }

        public void Alterar(InstituicaoDomain instituicao)
        {
            throw new NotImplementedException();
        }

        public InstituicaoDomain BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<InstituicaoDomain> Listar()
        {
            string stringDeConexao = "Data Source=localhost;Initial Catalog=SENAI_SVIGUFO;Integrated Security=True";
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
