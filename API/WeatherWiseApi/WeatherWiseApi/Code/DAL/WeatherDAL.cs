﻿using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Text;
using System.Data;
using Npgsql;
using System.Globalization;

namespace WeatherWiseApi.Code.DAL
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

        public List<WindStatistics> GetWindAllStatistics()
        {
            var cmdSql = new StringBuilder();

            cmdSql.AppendLine(" SELECT DISTINCT LAT, LON, SPEED, DATE_WEATHER              ");
            cmdSql.AppendLine(" 	FROM WS.\"tb_currentWeather\" WEA             ");
            cmdSql.AppendLine(" 	INNER JOIN WS.TB_WIND AS WIN                  ");
            cmdSql.AppendLine(" 		ON WEA.ID_WIND = WIN.ID_WIND              ");
            cmdSql.AppendLine(" 	INNER JOIN WS.TB_COORDENATES COOR             ");
            cmdSql.AppendLine(" 		ON COOR.ID_COORDENATE = WEA.ID_COORDENATE ");

            using (var connection = new NpgsqlConnection(base.ConnectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(cmdSql.ToString(), connection))
                {
                    var results = new List<WindStatistics>();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(new WindStatistics
                            {
                                Lat = reader.GetFieldValue<double>("LAT"),
                                Lon = reader.GetFieldValue<double>("LON"),
                                Speed = reader.GetFieldValue<double>("SPEED"),
                                DateWeather = reader.GetFieldValue<DateTime>("DATE_WEATHER"),
                            });
                        }
                    }

                    return results;
                }
            }
        }

        

        public List<List<WindStatistics>> GetWindStatisticsByRegion()
        {
            var allStatisticWindData = GetWindAllStatistics();
            var allRegions = GetMockedCoordinates();

            foreach (var windData in allStatisticWindData)
            {
                var selectedRegion = allRegions.Where(x => x.Lat.ToString("N2") == windData.Lat.ToString("N2") && x.Lon.ToString("N2") == windData.Lon.ToString("N2"))
                                            .FirstOrDefault();

                if(selectedRegion is not null)
                {
                    windData.Region = selectedRegion.DisplayName!;
                }
            }

            allStatisticWindData = allStatisticWindData.Where(x => x.Region is not null).ToList();
            var groupedStatistic = (from item in allStatisticWindData
                                    group item by new { item.Region } into Group
                                    select Group.ToList()).ToList();
            List<List<WindStatistics>> returnList = new List<List<WindStatistics>>();

            foreach (var statistic in groupedStatistic)
            {
                returnList.Add(statistic.DistinctBy(x => x.DateWeather).ToList());
            }


            return returnList;
        }

        public List<WindDashboardInformation> GetWindDashboardInformation()
        {
            var windStatistics = GetWindStatisticsByRegion();
            List<WindDashboardInformation> windDashboardInformation = new List<WindDashboardInformation>();

            foreach (var statistics in windStatistics)
            {
                windDashboardInformation.Add(new WindDashboardInformation(statistics));
            }

            return windDashboardInformation;
        } 

        public List<Coordinate> GetMockedCoordinates()
        {
            var retorno = new List<Coordinate>();

            retorno.Add(new Coordinate
            {
                Lat = -18.8819,
                Lon = -48.2830,
                DisplayName = "Região Norte"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9462,
                Lon = -48.2731,
                DisplayName = "Região Sul"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9210,
                Lon = -48.2351,
                DisplayName = "Região Leste"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9322,
                Lon = -48.3294,
                DisplayName = "Região Oeste"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9103,
                Lon = -48.2757,
                DisplayName = "Centro (Sérgio Pacheco)"
            });

            return retorno;
        }

        /// <summary>
        /// Consulta de Ids 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Alert> GetAlertByUser(string user_email)
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine(" SELECT WIND_SPEED, VISIBILITY, AIR_POLLUTION_AQI, PRECIPTATION, \"desactivationDate\"  ");
            selectSql.AppendLine("      FROM WS.TB_ALERT                                           ");
            selectSql.AppendLine("          WHERE EMAIL_USER = @EMAIL_USER                         ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(selectSql.ToString(), connection))
                    {
                        var results = new List<Alert>();
                        command.Parameters.AddWithValue("@EMAIL_USER", user_email);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var model = new Alert
                                {
                                    wind_speed = reader.GetFieldValue<double?>("WIND_SPEED"),
                                    visibility = reader.GetFieldValue<double?>("VISIBILITY"),
                                    air_pollution_aqi = reader.GetFieldValue<int?>("AIR_POLLUTION_AQI"),
                                    preciptation = reader.GetFieldValue<double?>("PRECIPTATION"),
                                    desactivationDate = reader.GetFieldValue<DateTime?>("DESACTIVATIONDATE"),
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

        /// <summary>
        /// Método de Inserir um alerta
        /// </summary>
        /// <param name="alert"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool InsertAlert(Alert alert)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                ");
            insertSql.AppendLine("     WS.TB_ALERT (          ");
            insertSql.AppendLine("         EMAIL_USER,        ");
            insertSql.AppendLine("         WIND_SPEED,        ");
            insertSql.AppendLine("         VISIBILITY,        ");
            insertSql.AppendLine("         PRECIPTATION,      ");
            insertSql.AppendLine("         AIR_POLLUTION_AQI  ");
            insertSql.AppendLine("     )                      ");
            insertSql.AppendLine(" VALUES                     ");
            insertSql.AppendLine("     (                      ");
            insertSql.AppendLine("         @EMAIL_USER,       ");
            insertSql.AppendLine("         @WIND_SPEED,       ");
            insertSql.AppendLine("         @VISIBILITY,       ");
            insertSql.AppendLine("         @PRECIPTATION,     ");
            insertSql.AppendLine("         @AIR_POLLUTION_AQI ");
            insertSql.AppendLine("     )                      ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@EMAIL_USER", alert.email_user);
                        command.Parameters.AddWithValue("@WIND_SPEED", alert.wind_speed);
                        command.Parameters.AddWithValue("@VISIBILITY", alert.visibility);
                        command.Parameters.AddWithValue("@PRECIPTATION", alert.preciptation);
                        command.Parameters.AddWithValue("@AIR_POLLUTION_AQI", alert.air_pollution_aqi);

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
        /// Atualização das Informações do Tempo Atual
        /// </summary>
        /// <param name="objCurrentWeather"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PutCurrentWeather(CurrentWeather objCurrentWeather)
        {
            var updateSql = new StringBuilder();

            updateSql.AppendLine("UPDATE table_name                         ");
            updateSql.AppendLine(" SET column1 = value1, column2 = value2   ");
            updateSql.AppendLine(" WHERE condition                          ");
            string cmdSql = updateSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objCurrentWeather.clouds);
                this.db.AddInParameter(databaseCommand, ":val1", DbType.DateTime, objCurrentWeather.visibility);
                this.db.AddInParameter(databaseCommand, ":val2", DbType.String, objCurrentWeather.name);

                return db.ExecuteNonQuery(databaseCommand) > 0;

            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Atualização das Informações da Previsão do Tempo
        /// </summary>
        /// <param name="objForecast"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool PutForecastWeather(Forecast objForecast)
        {
            var updateSql = new StringBuilder();

            updateSql.AppendLine("UPDATE table_name                         ");
            updateSql.AppendLine(" SET column1 = value1, column2 = value2   ");
            updateSql.AppendLine(" WHERE condition                          ");
            string cmdSql = updateSql.ToString();

            databaseCommand = db.GetSqlStringCommand(cmdSql);
            try
            {
                this.db.AddInParameter(databaseCommand, ":cod1", DbType.String, objForecast.cod);
                this.db.AddInParameter(databaseCommand, ":val1", DbType.DateTime, objForecast.city);
                this.db.AddInParameter(databaseCommand, ":val2", DbType.String, objForecast.message);

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