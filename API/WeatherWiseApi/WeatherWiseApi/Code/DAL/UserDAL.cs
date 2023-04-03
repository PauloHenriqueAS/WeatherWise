using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Text;
using System.Data;

namespace WeatherWiseApi.Code.DAL
{
    public class UserDAL : DataBase
    {
        #region SELECTS
        
        /// <summary>
        /// Consultar a senha do usuário salva no banco de dados
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetPasswordUser(User objUser)
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine("SELECT password_user FROM tab_User");
            selectSql.AppendLine("WHERE email_user = :email_user");
            string cmdSql = selectSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                string passwordBD = "";
                this.db.AddInParameter(databaseCommand, ":email_user", DbType.String, objUser.email_user);

                var ret = db.ExecuteScalar(databaseCommand);
                return "";
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Consultar as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public User GetUserInfo(User objUser)
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine("SELECT name_user, type_user, email_user FROM tab_User");
            selectSql.AppendLine("WHERE email_user = :email_user");
            string cmdSql = selectSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                User user = new User();
                this.db.AddInParameter(databaseCommand, ":email_user", DbType.String, objUser.email_user);
                //TO-DO db.ExecuteScalar(databaseCommand);

                return user;
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
            insertSql.AppendLine("INSERT INTO table_name (column1, column2, column3 )");
            insertSql.AppendLine("VALUES(value1, value2, value3 ) ");
            string cmdSql = insertSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":name_user", DbType.String, objUser.name_user);
                this.db.AddInParameter(databaseCommand, ":type_user", DbType.String, objUser.type_user);
                this.db.AddInParameter(databaseCommand, ":password_user", DbType.String, objUser.password_user);
                this.db.AddInParameter(databaseCommand, ":email_user", DbType.String, objUser.email_user);

                return db.ExecuteNonQuery(databaseCommand) > 0;
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
            updateSql.AppendLine("INSERT INTO table_name (column1, column2, column3 )");
            updateSql.AppendLine("VALUES(value1, value2, value3 ) ");
            string cmdSql = updateSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":name_user", DbType.String, objUser.name_user);
                this.db.AddInParameter(databaseCommand, ":type_user", DbType.String, objUser.type_user);
                this.db.AddInParameter(databaseCommand, ":password_user", DbType.String, objUser.password_user);
                this.db.AddInParameter(databaseCommand, ":email_user", DbType.String, objUser.email_user);

                return db.ExecuteNonQuery(databaseCommand) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }
        #endregion
    }
}
