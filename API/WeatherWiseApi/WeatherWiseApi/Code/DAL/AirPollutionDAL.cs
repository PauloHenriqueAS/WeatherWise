using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Text;
using System.Data;

namespace WeatherWiseApi.Code.DAL
{
    public class AirPollutionDAL : DataBase
    {
        #region INSERTS
        
        /// <summary>
        /// Salvar Informações da Poluição do Ar 
        /// </summary>
        /// <param name="objAirPollution"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PostAirPollution(AirPollution objAirPollution)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine("INSERT INTO table_name (column1, column2, column3, )");
            insertSql.AppendLine("VALUES(value1, value2, value3, ) ");
            string cmdSql = insertSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objAirPollution.coord);

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
        /// Atualizar Informações da Poluição do Ar 
        /// </summary>
        /// <param name="objAirPollution"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PutAirPollution(AirPollution objAirPollution)
        {
            var updateSql = new StringBuilder();

            updateSql.AppendLine("INSERT INTO table_name (column1, column2, column3, )");
            updateSql.AppendLine("VALUES(value1, value2, value3, ) ");
            string cmdSql = updateSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objAirPollution.coord);

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
