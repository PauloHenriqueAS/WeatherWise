using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Text;
using System.Data;
using Npgsql;

namespace WeatherWiseApi.Code.DAL
{
    public class UserDAL : DataBase
    {
        public UserDAL(IConfiguration configuration) : base(configuration)
        {

        }

        #region SELECTS

        /// <summary>
        /// Consultar a senha do usuário salva no banco de dados
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetPasswordUser(UserCredentials objUser)
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine("SELECT PASSWORD_USER FROM WS.TB_USER");
            selectSql.AppendLine("WHERE EMAIL_USER = @EMAIL_USER");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(selectSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@EMAIL_USER", objUser.email_user);

                        var results = new List<string>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {                                
                                results.Add(reader.GetFieldValue<string>("PASSWORD_USER"));
                            }
                        }

                        return results.FirstOrDefault()!;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message)!;
            }
        }

        /// <summary>
        /// Consultar as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public User GetUserInfo(string email_user)
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine("SELECT NAME_USER, TYPE_USER, EMAIL_USER, PROFILE_IMAGE FROM WS.TB_USER");
            selectSql.AppendLine("WHERE EMAIL_USER = @EMAIL_USER");
            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(selectSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@EMAIL_USER", email_user);

                        var results = new List<User>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new User
                                {
                                    name_user = reader.GetFieldValue<string>("NAME_USER"),
                                    //type_user = reader.GetFieldValue<string>("TYPE_USER"),
                                    email_user = reader.GetFieldValue<string>("EMAIL_USER"),
                                    profile_image = reader.GetFieldValue<string>("PROFILE_IMAGE")
                                };

                                results.Add(model);
                            }
                        }

                        return results.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }
        #endregion

        #region INSERTS
        /// <summary>
        /// Método de Inserir um Usuário no Banco de Dados
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PostUser(User objUser)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO ");
            insertSql.AppendLine("     WS.TB_USER ( ");
            insertSql.AppendLine("         ID_USER, ");
            insertSql.AppendLine("         NAME_USER, ");
            insertSql.AppendLine("         TYPE_USER, ");
            insertSql.AppendLine("         PASSWORD_USER, ");
            insertSql.AppendLine("         EMAIL_USER ");
            insertSql.AppendLine("     ) ");
            insertSql.AppendLine(" VALUES ");
            insertSql.AppendLine("     ( ");
            insertSql.AppendLine("         @ID_USER, ");
            insertSql.AppendLine("         @NAME_USER, ");
            insertSql.AppendLine("         @TYPE_USER, ");
            insertSql.AppendLine("         @PASSWORD_USER, ");
            insertSql.AppendLine("         @EMAIL_USER ");
            insertSql.AppendLine("     ) ");

            try
            {

                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_USER", objUser.id_user);
                        command.Parameters.AddWithValue("@NAME_USER", objUser.name_user);
                        command.Parameters.AddWithValue("@TYPE_USER", TypesUser.Admin.ToString());
                        command.Parameters.AddWithValue("@PASSWORD_USER", objUser.password_user);
                        command.Parameters.AddWithValue("@EMAIL_USER", objUser.email_user);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }
        #endregion

        #region UPDATES
        /// <summary>
        /// Método para atualizar os dados de um Usuário no Banco de Dados
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PutUser(User objUser)
        {
            var updateSql = new StringBuilder();

            updateSql.AppendLine(" UPDATE ");
            updateSql.AppendLine("     WS.TB_USER ");
            updateSql.AppendLine(" SET ");
            updateSql.AppendLine("     NAME_USER = @NAME_USER, ");
            updateSql.AppendLine("     PROFILE_IMAGE = @PROFILE_IMAGE, ");
            updateSql.AppendLine("     PASSWORD_USER = @PASSWORD_USER ");
            updateSql.AppendLine(" WHERE  ");
            updateSql.AppendLine("     EMAIL_USER = @EMAIL_USER ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(updateSql.ToString(), connection))
                    {   
                        command.Parameters.AddWithValue("@NAME_USER", objUser.name_user);
                        command.Parameters.AddWithValue("@PROFILE_IMAGE", objUser.profile_image);
                        command.Parameters.AddWithValue("@PASSWORD_USER", objUser.password_user);
                        command.Parameters.AddWithValue("@EMAIL_USER", objUser.email_user);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }
        #endregion
    }
}
