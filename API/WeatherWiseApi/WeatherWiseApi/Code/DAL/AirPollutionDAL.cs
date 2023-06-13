using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Text;
using System.Data;
using Npgsql;

namespace WeatherWiseApi.Code.DAL
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


        public DashAirPollutionDB GetDataAirPollutionDashBoard(Coordinate coordinate, DateTime dataDia)
        {
            var selectSql = new StringBuilder();

            selectSql.AppendLine("select round(COMP.\"CO\") as \"CO\"       ");
            selectSql.AppendLine("     , round(COMP.\"NO\") as \"NO\"       ");
            selectSql.AppendLine("     , round(COMP.\"NO2\") as \"NO2\"     ");
            selectSql.AppendLine("     , round(COMP.\"O3\") as \"O3\"       ");
            selectSql.AppendLine("     , round(COMP.\"SO2\") as \"SO2\"     ");
            selectSql.AppendLine("     , round(COMP.\"PM2_5\") as \"PM2_5\" ");
            selectSql.AppendLine("     , round(COMP.\"PM10\") as \"PM10\"   ");
            selectSql.AppendLine("     , round(COMP.\"NH3\") as \"NH3\"     ");
            //selectSql.AppendLine("     , round(SUM(COMP.\"CO\" + COMP.\"NO\" + COMP.\"NO2\" + COMP.\"O3\" + COMP.\"SO2\" + COMP.\"PM2_5\" + COMP.\"PM10\" + COMP.\"NH3\")) as Total ");
            selectSql.AppendLine(" from ws.\"tb_airPollution\" AIR                                               ");
            selectSql.AppendLine(" inner join ws.tb_coordenates coord ON coord.id_coordenate = air.id_coordenate ");
            selectSql.AppendLine(" inner join ws.tb_components COMP ON COMP.id_components = AIR.id_component     ");
            selectSql.AppendLine(" where 1 = 1                                                                   ");
            selectSql.AppendLine(" and AIR.data_save = @date_query                                               ");
            selectSql.AppendLine(" and coord.lat = @lat                                                          ");
            selectSql.AppendLine(" and coord.lon = @lon                                                          ");
            selectSql.AppendLine(" group by COMP.\"CO\"                                                          ");
            selectSql.AppendLine("     , COMP.\"NO\"                                                             ");
            selectSql.AppendLine("     , COMP.\"NO2\"                                                            ");
            selectSql.AppendLine("     , COMP.\"O3\"                                                             ");
            selectSql.AppendLine("     , COMP.\"SO2\"                                                            ");
            selectSql.AppendLine("     , COMP.\"PM2_5\"                                                          ");
            selectSql.AppendLine("     , COMP.\"PM10\"                                                           ");
            selectSql.AppendLine("     , COMP.\"NH3\"                                                            ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(selectSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@date_query", dataDia);
                        command.Parameters.AddWithValue("@lat", coordinate.Lat);
                        command.Parameters.AddWithValue("@lon", coordinate.Lon);

                        var results = new List<DashAirPollutionDB>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new DashAirPollutionDB
                                {
                                    co = reader.GetFieldValue<double>("CO"),
                                    no = reader.GetFieldValue<double>("NO"),
                                    no2 = reader.GetFieldValue<double>("NO2"),
                                    o3 = reader.GetFieldValue<double>("O3"),
                                    so2 = reader.GetFieldValue<double>("SO2"),
                                    pm2_5 = reader.GetFieldValue<double>("PM2_5"),
                                    pm10 = reader.GetFieldValue<double>("PM10"),
                                    nh3 = reader.GetFieldValue<double>("NH3"),
                                };

                                results.Add(model);
                            }
                        }
                        return results.FirstOrDefault()!;
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
