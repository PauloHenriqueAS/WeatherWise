using Microsoft.Extensions.Configuration;
using WsRoutine.Code.Model;
using WsRoutine.Helpers;
using System.Reflection;
using System.Text;
using Npgsql;

namespace WsRoutine.Code.DAL
{
    public class AirPollutionDAL : DataBase
    {
        public AirPollutionDAL(IConfiguration configuration) : base(configuration) { }

        #region INSERTS

        /// <summary>
        /// Salvar Informações da Poluição do Ar 
        /// </summary>
        /// <param name="objAirPollution"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PostAirPollution(AirPollutionDB objAirPollution)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine("INSERT INTO                       ");
            insertSql.AppendLine(" ws.\"tb_airPollution\" (         ");
            insertSql.AppendLine("      \"id_airPollution\",        ");
            insertSql.AppendLine("      ID_COORDENATE,              ");
            insertSql.AppendLine("      ID_MAIN,                    ");
            insertSql.AppendLine("      ID_COMPONENT,               ");
            insertSql.AppendLine("      DT,                         ");
            insertSql.AppendLine("      AQI,                        ");
            insertSql.AppendLine("      DATA_SAVE     )             ");
            insertSql.AppendLine("VALUES              (             ");
            insertSql.AppendLine("      (SELECT COUNT(*)+1 FROM     ");
            insertSql.AppendLine("         ws.\"tb_airPollution\"), ");
            insertSql.AppendLine("      @ID_COODENATE,              ");
            insertSql.AppendLine("      @ID_MAIN,                   ");
            insertSql.AppendLine("      @ID_COMPONENT,              ");
            insertSql.AppendLine("      @DT,                        ");
            insertSql.AppendLine("      @AQI,                       ");
            insertSql.AppendLine("      @DATA_SAVE    )             ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        //command.Parameters.AddWithValue("@ID_AIRPOLLUTION", objAirPollution.id_airPollution);
                        command.Parameters.AddWithValue("@ID_COODENATE", objAirPollution.id_coordenate);
                        command.Parameters.AddWithValue("@ID_MAIN", objAirPollution.id_main);
                        command.Parameters.AddWithValue("@ID_COMPONENT", objAirPollution.id_component);
                        command.Parameters.AddWithValue("@DT", objAirPollution.dt);
                        command.Parameters.AddWithValue("@AQI", objAirPollution.aqi);
                        command.Parameters.AddWithValue("@DATA_SAVE", objAirPollution.data_save);

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
