using Microsoft.Extensions.Configuration;
using WsRoutine.Code.Model;
using WsRoutine.Helpers;
using System.Reflection;
using System.Text;
using System.Data;
using Npgsql;

namespace WsRoutine.Code.DAL
{
    public class WeatherDAL : DataBase
    {
        public WeatherDAL(IConfiguration configuration) : base(configuration) { }

        #region GETS
        /// <summary>
        /// Consulta de Ids 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<ForecastIDs> GetIdsInfo()
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine(" SELECT                     ");
            selectSql.AppendLine("      ID_MAIN,              ");
            selectSql.AppendLine("      ID_WEATHER,           ");
            selectSql.AppendLine("      ID_CLOUDS,            ");
            selectSql.AppendLine("      ID_WIND,              ");
            selectSql.AppendLine("      ID_RAIN,              ");
            selectSql.AppendLine("      ID_SYS                ");
            selectSql.AppendLine("FROM WS.\"tb_listForecast\" ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(selectSql.ToString(), connection))
                    {
                        var results = new List<ForecastIDs>();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new ForecastIDs
                                {
                                    id_main = reader.GetFieldValue<int>("ID_MAIN"),
                                    id_weather = reader.GetFieldValue<int>("ID_WEATHER"),
                                    id_clouds = reader.GetFieldValue<int>("ID_CLOUDS"),
                                    id_wind = reader.GetFieldValue<int>("ID_WIND"),
                                    id_rain = reader.GetFieldValue<int>("ID_RAIN"),
                                    id_sys = reader.GetFieldValue<int>("ID_SYS"),
                                };

                                results.Add(model);
                            }
                        }

                        return results;
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
        /// Salvar Informações do Tempo Atual
        /// </summary>
        /// <param name="objCurrentWeather"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PostCurrentWeather(CurrentWeatherDB objCurrentWeather)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                                ");
            insertSql.AppendLine("   ws.\"tb_currentWeather\" (               ");
            insertSql.AppendLine("      \"id_currentWeather\",                ");
            insertSql.AppendLine("      ID_COORDENATE,                        ");
            insertSql.AppendLine("      ID_WEATHER,                           ");
            insertSql.AppendLine("      _BASE,                                ");
            insertSql.AppendLine("      ID_MAIN,                              ");
            insertSql.AppendLine("      VISIBILITY,                           ");
            insertSql.AppendLine("      ID_WIND,                              ");
            insertSql.AppendLine("      ID_CLOUDS,                            ");
            insertSql.AppendLine("      DT,                                   ");
            insertSql.AppendLine("      ID_SYS,                               ");
            insertSql.AppendLine("      TIMEZONE,                             ");
            insertSql.AppendLine("      ID,                                   ");
            insertSql.AppendLine("      NAME,                                 ");
            insertSql.AppendLine("      COD,                                  ");
            insertSql.AppendLine("      DATE_WEATHER                          ");
            insertSql.AppendLine("                          )                 ");
            insertSql.AppendLine(" VALUES (                                   ");
            insertSql.AppendLine("         (SELECT COUNT(*) + 1               ");
            insertSql.AppendLine("            FROM WS.\"tb_currentWeather\"), ");
            insertSql.AppendLine("      @ID_COORDENATE,                       ");
            insertSql.AppendLine("      @ID_WEATHER,                          ");
            insertSql.AppendLine("      @BASE,                                ");
            insertSql.AppendLine("      @ID_MAIN,                             ");
            insertSql.AppendLine("      @VISIBILITY,                          ");
            insertSql.AppendLine("      @ID_WIND,                             ");
            insertSql.AppendLine("      @ID_CLOUDS,                           ");
            insertSql.AppendLine("      @DT,                                  ");
            insertSql.AppendLine("      @ID_SYS,                              ");
            insertSql.AppendLine("      @TIMEZONE,                            ");
            insertSql.AppendLine("      @ID,                                  ");
            insertSql.AppendLine("      @NAME,                                ");
            insertSql.AppendLine("      @COD,                                 ");
            insertSql.AppendLine("      @DATE_WEATHER         )               ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_COORDENATE", objCurrentWeather.id_coordate);
                        command.Parameters.AddWithValue("@ID_WEATHER", objCurrentWeather.id_weather);
                        command.Parameters.AddWithValue("@BASE", objCurrentWeather._base);
                        command.Parameters.AddWithValue("@ID_MAIN", objCurrentWeather.id_main);
                        command.Parameters.AddWithValue("@VISIBILITY", objCurrentWeather.visibility);
                        command.Parameters.AddWithValue("@ID_WIND", objCurrentWeather.id_wind);
                        command.Parameters.AddWithValue("@ID_CLOUDS", objCurrentWeather.id_clouds);
                        command.Parameters.AddWithValue("@DT", objCurrentWeather.dt);
                        command.Parameters.AddWithValue("@ID_SYS", objCurrentWeather.id_sys);
                        command.Parameters.AddWithValue("@TIMEZONE", objCurrentWeather.timezone);
                        command.Parameters.AddWithValue("@ID", objCurrentWeather.id);
                        command.Parameters.AddWithValue("@NAME", objCurrentWeather.name);
                        command.Parameters.AddWithValue("@COD", objCurrentWeather.cod);
                        command.Parameters.AddWithValue("@DATE_WEATHER", objCurrentWeather.date_weather);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Salvar Informações da Previsão do Tempo
        /// </summary>
        /// <param name="objForecast"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PostForecastWeather(Forecast objForecast)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine("INSERT INTO                  ");
            insertSql.AppendLine("  WS.TB_FORECAST (           ");
            insertSql.AppendLine("     ID_FORECAST,            ");
            insertSql.AppendLine("     COD,                    ");
            insertSql.AppendLine("     MESSAGE,                ");
            insertSql.AppendLine("     CNT,                    ");
            insertSql.AppendLine("     \"id_listForecast\",    ");
            insertSql.AppendLine("     ID_CITY  )              ");
            insertSql.AppendLine("VALUES (                     ");
            insertSql.AppendLine("     (SELECT COUNT(*) + 1    ");
            insertSql.AppendLine("       FROM WS.TB_FORECAST), ");
            insertSql.AppendLine("     @COD,                   ");
            insertSql.AppendLine("     @MESSAGE,               ");
            insertSql.AppendLine("     @CNT,                   ");
            insertSql.AppendLine("     @id_listForecast,       ");
            insertSql.AppendLine("     @ID_CITY              ) ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@COD", objForecast.cod);
                        command.Parameters.AddWithValue("@MESSAGE", objForecast.message);
                        command.Parameters.AddWithValue("@CNT", objForecast.cnt);
                        command.Parameters.AddWithValue("@id_listForecast", objForecast.id_listForecast);
                        command.Parameters.AddWithValue("@ID_CITY", objForecast.id_city);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Salvar Informações de lista de previsão do tempo
        /// </summary>
        /// <param name="objListForecast"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostListForecast(ForecastDB objListForecast)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                         ");
            insertSql.AppendLine("  WS.\"tb_listForecast\" (           ");
            insertSql.AppendLine("    \"id_listForecast\",             ");
            insertSql.AppendLine("     DT,                             ");
            insertSql.AppendLine("     ID_MAIN,                        ");
            insertSql.AppendLine("     ID_WEATHER,                     ");
            insertSql.AppendLine("     ID_CLOUDS,                      ");
            insertSql.AppendLine("     ID_WIND,                        ");
            insertSql.AppendLine("     VISIBILITY,                     ");
            insertSql.AppendLine("     POP,                            ");
            insertSql.AppendLine("     ID_RAIN,                        ");
            insertSql.AppendLine("     ID_SYS,                         ");
            insertSql.AppendLine("     DT_TXT   )                      ");
            insertSql.AppendLine(" VALUES (                            ");
            insertSql.AppendLine("     (SELECT COUNT(*) + 1            ");
            insertSql.AppendLine("       FROM WS.\"tb_listForecast\"), ");
            insertSql.AppendLine("     @DT,                            ");
            insertSql.AppendLine("     @ID_MAIN,                       ");
            insertSql.AppendLine("     @ID_WEATHER,                    ");
            insertSql.AppendLine("     @ID_CLOUDS,                     ");
            insertSql.AppendLine("     @ID_WIND,                       ");
            insertSql.AppendLine("     @VISIBILITY,                    ");
            insertSql.AppendLine("     @POP,                           ");
            insertSql.AppendLine("     @ID_RAIN,                       ");
            insertSql.AppendLine("     @ID_SYS,                        ");
            insertSql.AppendLine("     @DT_TXT   )                     ");
            insertSql.AppendLine(" RETURNING \"id_listForecast\"       ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@DT", objListForecast.dt);
                        command.Parameters.AddWithValue("@ID_MAIN", objListForecast.id_main);
                        command.Parameters.AddWithValue("@ID_WEATHER", objListForecast.id_weather);
                        command.Parameters.AddWithValue("@ID_CLOUDS", objListForecast.id_clouds);
                        command.Parameters.AddWithValue("@ID_WIND", objListForecast.id_wind);
                        command.Parameters.AddWithValue("@VISIBILITY", objListForecast.visibility);
                        command.Parameters.AddWithValue("@POP", objListForecast.pop);
                        command.Parameters.AddWithValue("@ID_RAIN", objListForecast.id_rain);
                        command.Parameters.AddWithValue("@ID_SYS", objListForecast.id_sys);
                        command.Parameters.AddWithValue("@DT_TXT", objListForecast.dt_txt);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }
        #endregion

        #region DELETES

        /// <summary>
        /// Deletar as informações de Forecast
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteForecastWeather()
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine("DELETE FROM WS.TB_FORECAST");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Deletar as informações da lista de forecast
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteListForecast()
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine("DELETE FROM WS.\"tb_listForecast\"");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();
                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
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
