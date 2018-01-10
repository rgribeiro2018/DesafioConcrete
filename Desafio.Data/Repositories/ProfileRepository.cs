using Desafio.Data.DataContext;
using Desafio.Domain;
using Desafio.Domain.Interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Data;

namespace Desafio.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private DesafioDataContext db;

        public ProfileRepository()
        {
            db = new DesafioDataContext();
        }
        public void Dispose()
        {
            db.Dispose();
        }
        public void Create(Profile entity)
        {
            try
            {
                string stringConexao = ConfigurationManager.ConnectionStrings["DesafioConnectionString"].ConnectionString;
                
                
                using (SqlConnection cn = new SqlConnection())
                {
                    var cmd = new SqlCommand();

                 

                    cmd.Parameters.AddWithValue("id", entity.ProfileId);
                    cmd.Parameters.AddWithValue("nome", entity.Nome);
                    cmd.Parameters.AddWithValue("email", entity.Email);
                    cmd.Parameters.AddWithValue("senha", entity.Senha);
                    cmd.Parameters.AddWithValue("token", entity.Token);
                    cmd.Parameters.AddWithValue("datacriacao", entity.DataCriacao);
                    cmd.Parameters.AddWithValue("dataatualizacao", entity.DataAtualizacao);
                    cmd.Parameters.AddWithValue("dataultimologin", entity.DataUltimoLogin);

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO PROFILE(PROFILEID, NOME, EMAIL, SENHA, TOKEN, DATACRIACAO, DATAATUALIZACAO, DATAULTIMOLOGIN) " +
                                      "VALUES(@id,@nome,@email,@senha,@token,@datacriacao,@dataatualizacao,@dataultimologin)";


                    cn.ConnectionString = stringConexao;
                    cmd.Connection = cn;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }

                
                foreach (var Telefone in entity.Telefones)
                {
                    using (var cn = new SqlConnection())
                    {
                        var cmd = new SqlCommand();

                        cmd.Parameters.AddWithValue("numero", Telefone.Numero);
                        cmd.Parameters.AddWithValue("ddd", Telefone.DDD);
                        cmd.Parameters.AddWithValue("profileid", entity.ProfileId);
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "INSERT INTO TELEFONE(NUMERO, DDD, PROFILE_PROFILEID) " +
                                          "VALUES(@numero,@ddd,@profileid)";



                        cn.ConnectionString = stringConexao;
                        cmd.Connection = cn;

                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        


        public Profile Get(Guid Id)
        {
            return db.Profiles.Where(p => p.ProfileId.Equals(Id)).Include(x => x.Telefones).FirstOrDefault();
        }

        public Profile Get(int Id)
        {
            return db.Profiles.Find(Id);
        }
       
        public Profile Get(string  Email)
        {
            return db.Profiles.Where(p => p.Email.Equals(Email)).Include(x => x.Telefones).FirstOrDefault();
        }


        public void Update(Profile entity)
        {
            db.Entry<Profile>(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Profile GetWithTelefone(int Id)
        {
            return db.Profiles.Where(x => x.ProfileId.Equals(Id)).Include(x => x.Telefones).FirstOrDefault();
        }
    }
}
