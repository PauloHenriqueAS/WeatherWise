using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Text;
using System.Data;
using Npgsql;

namespace WeatherWiseApi.Code.DAL
{
    public class ComumDAL : DataBase
    {
        public ComumDAL(IConfiguration configuration) : base(configuration) { }

        #region GET
        /// <summary>
        /// Consulta Identificação de coordenada caso exista já no banco de dados
        /// </summary>
        /// <param name="objCoordenate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int GetCoordenateInfo(Coordinate objCoordenate)
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine(" SELECT                  ");
            selectSql.AppendLine("     ID_COORDENATE       ");
            selectSql.AppendLine("  FROM WS.TB_COORDENATES ");
            selectSql.AppendLine(" WHERE                   ");
            selectSql.AppendLine("     LAT = @LAT          ");
            selectSql.AppendLine(" AND                     ");
            selectSql.AppendLine("     LON = @LON          ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(selectSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@LAT", objCoordenate.Lat);
                        command.Parameters.AddWithValue("@LON", objCoordenate.Lon);

                        int result = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = reader.GetFieldValue<int>("ID_COORDENATE");
                            }
                        }

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Consulta Identificação de cidade caso exista já no banco de dados
        /// </summary>
        /// <param name="objCity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int GetCityInfo(City objCity)
        {
            var selectSql = new StringBuilder();
            selectSql.AppendLine(" SELECT                             ");
            selectSql.AppendLine("     ID_CITY                        ");
            selectSql.AppendLine("  FROM WS.TB_CITY                   ");
            selectSql.AppendLine(" WHERE                              ");
            selectSql.AppendLine("     NAME = @NAME                   ");
            selectSql.AppendLine(" AND                                ");
            selectSql.AppendLine("     ID_COORDENATE = @ID_COORDENATE ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(selectSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@NAME", objCity.name);
                        command.Parameters.AddWithValue("@ID_COORDENATE", objCity.id_coordenate);

                        int result = 0;
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = reader.GetFieldValue<int>("ID_CITY");
                            }
                        }

                        return result;
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
        /// Inserção das Coordenadas no banco de dados
        /// </summary>
        /// <param name="objCoordenate"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostCoordenate(Coordinate objCoordenate)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                         ");
            insertSql.AppendLine("     WS.TB_COORDENATES (             ");
            insertSql.AppendLine("         ID_COORDENATE,              ");
            insertSql.AppendLine("         LAT,                        ");
            insertSql.AppendLine("         LON                         ");
            insertSql.AppendLine("     )                               ");
            insertSql.AppendLine(" VALUES                              ");
            insertSql.AppendLine("     (                               ");
            insertSql.AppendLine("         (SELECT COUNT(*)+1          ");
            insertSql.AppendLine("            FROM WS.TB_COORDENATES), ");
            insertSql.AppendLine("         @LAT,                       ");
            insertSql.AppendLine("         @LON                        ");
            insertSql.AppendLine("     )                               ");
            insertSql.AppendLine(" RETURNING  ID_COORDENATE            ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@LAT", objCoordenate.Lat);
                        command.Parameters.AddWithValue("@LON", objCoordenate.Lon);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de objeto main no banco de dados
        /// </summary>
        /// <param name="objMain"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostMain(Main objMain)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                  ");
            insertSql.AppendLine("     WS.TB_MAIN (             ");
            insertSql.AppendLine("          ID_MAIN,            ");
            insertSql.AppendLine("          TEMP,               ");
            insertSql.AppendLine("          FEELS_LIKE,         ");
            insertSql.AppendLine("          TEMP_MIN,           ");
            insertSql.AppendLine("          TEMP_MAX,           ");
            insertSql.AppendLine("          PRESSURE,           ");
            insertSql.AppendLine("          HUMIDITY,           ");
            insertSql.AppendLine("          SEA_LEVEL,          ");
            insertSql.AppendLine("          GRND_LEVEL,         ");
            insertSql.AppendLine("          TEMP_KF,            ");
            insertSql.AppendLine("          AQI                 ");
            insertSql.AppendLine("     )                        ");
            insertSql.AppendLine(" VALUES                       ");
            insertSql.AppendLine("     (                        ");
            insertSql.AppendLine("         (SELECT COUNT(*)+1   ");
            insertSql.AppendLine("            FROM WS.TB_MAIN), ");
            insertSql.AppendLine("          @TEMP,              ");
            insertSql.AppendLine("          @FEELS_LIKE,        ");
            insertSql.AppendLine("          @TEMP_MIN,          ");
            insertSql.AppendLine("          @TEMP_MAX,          ");
            insertSql.AppendLine("          @PRESSURE,          ");
            insertSql.AppendLine("          @HUMIDITY,          ");
            insertSql.AppendLine("          @SEA_LEVEL,         ");
            insertSql.AppendLine("          @GRND_LEVEL,        ");
            insertSql.AppendLine("          @TEMP_KF,           ");
            insertSql.AppendLine("          @AQI                ");
            insertSql.AppendLine("     )                        ");
            insertSql.AppendLine(" RETURNING  ID_MAIN           ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@TEMP", objMain.temp);
                        command.Parameters.AddWithValue("@FEELS_LIKE", objMain.feels_like);
                        command.Parameters.AddWithValue("@TEMP_MIN", objMain.temp_min);
                        command.Parameters.AddWithValue("@TEMP_MAX", objMain.temp_max);
                        command.Parameters.AddWithValue("@PRESSURE", objMain.pressure);
                        command.Parameters.AddWithValue("@HUMIDITY", objMain.humidity);
                        command.Parameters.AddWithValue("@SEA_LEVEL", objMain.sea_level);
                        command.Parameters.AddWithValue("@GRND_LEVEL", objMain.grnd_level);
                        command.Parameters.AddWithValue("@TEMP_KF", objMain.temp_kf);
                        command.Parameters.AddWithValue("@AQI", objMain.aqi);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de componentes no banco de dados
        /// </summary>
        /// <param name="obComponents"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostComponent(Components obComponents)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                     ");
            insertSql.AppendLine("     WS.TB_COMPONENTS (          ");
            insertSql.AppendLine("          ID_COMPONENTS,         ");
            insertSql.AppendLine("          \"CO\",                ");
            insertSql.AppendLine("          \"NO\",                ");
            insertSql.AppendLine("          \"NO2\",               ");
            insertSql.AppendLine("          \"O3\",                ");
            insertSql.AppendLine("          \"SO2\",               ");
            insertSql.AppendLine("          \"PM2_5\",             ");
            insertSql.AppendLine("          \"PM10\",              ");
            insertSql.AppendLine("          \"NH3\"                ");
            insertSql.AppendLine("     )                           ");
            insertSql.AppendLine(" VALUES                          ");
            insertSql.AppendLine("     (                           ");
            insertSql.AppendLine("      (SELECT COUNT(*)+1         ");
            insertSql.AppendLine("         FROM WS.TB_COMPONENTS), ");
            insertSql.AppendLine("          @CO,                   ");
            insertSql.AppendLine("          @NO,                   ");
            insertSql.AppendLine("          @NO2,                  ");
            insertSql.AppendLine("          @O3,                   ");
            insertSql.AppendLine("          @SO2,                  ");
            insertSql.AppendLine("          @PM2_5,                ");
            insertSql.AppendLine("          @PM10,                 ");
            insertSql.AppendLine("          @NH3                   ");
            insertSql.AppendLine("     )                           ");
            insertSql.AppendLine(" RETURNING  ID_COMPONENTS        ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@CO", obComponents.co);
                        command.Parameters.AddWithValue("@NO", obComponents.no);
                        command.Parameters.AddWithValue("@NO2", obComponents.no2);
                        command.Parameters.AddWithValue("@O3", obComponents.o3);
                        command.Parameters.AddWithValue("@SO2", obComponents.so2);
                        command.Parameters.AddWithValue("@PM2_5", obComponents.pm2_5);
                        command.Parameters.AddWithValue("@PM10", obComponents.pm10);
                        command.Parameters.AddWithValue("@NH3", obComponents.nh3);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de weather no banco de dados
        /// </summary>
        /// <param name="objWeather"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostWeather(Weather objWeather)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                     ");
            insertSql.AppendLine("     WS.TB_WEATHER (             ");
            insertSql.AppendLine("         ID_WEATHER,             ");
            insertSql.AppendLine("         MAIN,                   ");
            insertSql.AppendLine("         DESCRIPTION,            ");
            insertSql.AppendLine("         ICON                    ");
            insertSql.AppendLine("     )                           ");
            insertSql.AppendLine(" VALUES                          ");
            insertSql.AppendLine("     (                           ");
            insertSql.AppendLine("         (SELECT COUNT(*)+1      ");
            insertSql.AppendLine("            FROM WS.TB_WEATHER), ");
            insertSql.AppendLine("         @MAIN,                  ");
            insertSql.AppendLine("         @DESCRIPTION,           ");
            insertSql.AppendLine("         @ICON                   ");
            insertSql.AppendLine("     )                           ");
            insertSql.AppendLine(" RETURNING  ID_WEATHER           ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@MAIN", objWeather.main);
                        command.Parameters.AddWithValue("@DESCRIPTION", objWeather.description);
                        command.Parameters.AddWithValue("@ICON", objWeather.icon);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de vento no banco de dados
        /// </summary>
        /// <param name="objWind"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostWind(Wind objWind)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO               ");
            insertSql.AppendLine("     WS.TB_WIND (          ");
            insertSql.AppendLine("          ID_WIND,         ");
            insertSql.AppendLine("          SPEED,           ");
            insertSql.AppendLine("          DEG,             ");
            insertSql.AppendLine("          GUST             ");
            insertSql.AppendLine("     )                     ");
            insertSql.AppendLine(" VALUES                    ");
            insertSql.AppendLine("     (                     ");
            insertSql.AppendLine("      (SELECT COUNT(*)+1   ");
            insertSql.AppendLine("         FROM WS.TB_WIND), ");
            insertSql.AppendLine("         @SPEED,           ");
            insertSql.AppendLine("         @DEG,             ");
            insertSql.AppendLine("         @GUST             ");
            insertSql.AppendLine("     )                     ");
            insertSql.AppendLine(" RETURNING  ID_WIND        ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@SPEED", objWind.speed);
                        command.Parameters.AddWithValue("@DEG", objWind.deg);
                        command.Parameters.AddWithValue("@GUST", objWind.gust);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de nuvens no banco de dados
        /// </summary>
        /// <param name="objClouds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostClouds(Clouds objClouds)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                    ");
            insertSql.AppendLine("     WS.TB_CLOUDS (             ");
            insertSql.AppendLine("        ID_CLOUDS,              ");
            insertSql.AppendLine("        \"all\"                 ");
            insertSql.AppendLine("     )                          ");
            insertSql.AppendLine(" VALUES                         ");
            insertSql.AppendLine("     (                          ");
            insertSql.AppendLine("         (SELECT COUNT(*)+1     ");
            insertSql.AppendLine("            FROM WS.TB_CLOUDS), ");
            insertSql.AppendLine("         @ALL                   ");
            insertSql.AppendLine("     )                          ");
            insertSql.AppendLine(" RETURNING  ID_CLOUDS           ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ALL", objClouds.all);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de Sys no banco de dados
        /// </summary>
        /// <param name="objSys"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostSys(Sys objSys)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                 ");
            insertSql.AppendLine("     WS.TB_SYS (             ");
            insertSql.AppendLine("         ID_SYS,             ");
            insertSql.AppendLine("         TYPE,               ");
            insertSql.AppendLine("         ID,                 ");
            insertSql.AppendLine("         COUNTRY,            ");
            insertSql.AppendLine("         SUNRISE,            ");
            insertSql.AppendLine("         SUNSET              ");
            insertSql.AppendLine("     )                       ");
            insertSql.AppendLine(" VALUES                      ");
            insertSql.AppendLine("     (                       ");
            insertSql.AppendLine("         (SELECT COUNT(*)+1  ");
            insertSql.AppendLine("            FROM WS.TB_SYS), ");
            insertSql.AppendLine("         @TYPE,              ");
            insertSql.AppendLine("         @ID,                ");
            insertSql.AppendLine("         @COUNTRY,           ");
            insertSql.AppendLine("         @SUNRISE,           ");
            insertSql.AppendLine("         @SUNSET             ");
            insertSql.AppendLine("     )                       ");
            insertSql.AppendLine(" RETURNING  ID_SYS           ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@TYPE", objSys.type);
                        command.Parameters.AddWithValue("@ID", objSys.id);
                        command.Parameters.AddWithValue("@COUNTRY", objSys.country);
                        command.Parameters.AddWithValue("@SUNRISE", objSys.sunrise);
                        command.Parameters.AddWithValue("@SUNSET", objSys.sunset);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de City no banco de dados
        /// </summary>
        /// <param name="objCity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostCity(City objCity)
        {
            objCity.name = objCity.name.Trim();

            var insertSql = new StringBuilder();
            insertSql.AppendLine(" INSERT INTO                ");
            insertSql.AppendLine("     WS.TB_CITY (           ");
            insertSql.AppendLine("      ID_CITY,              ");
            insertSql.AppendLine("      ID,                   ");
            insertSql.AppendLine("      NAME,                 ");
            insertSql.AppendLine("      ID_COORDENATE,        ");
            insertSql.AppendLine("      COUNTRY,              ");
            insertSql.AppendLine("      POPULATION,           ");
            insertSql.AppendLine("      TIMEZONE,             ");
            insertSql.AppendLine("      SUNRISE,              ");
            insertSql.AppendLine("      SUNSET                ");
            insertSql.AppendLine("     )                      ");
            insertSql.AppendLine(" SELECT                     ");
            insertSql.AppendLine("     (SELECT COUNT(*)+1     ");
            insertSql.AppendLine("         FROM WS.TB_CITY),  ");
            insertSql.AppendLine("      @ID,                  ");
            insertSql.AppendLine("      @NAME,                ");
            insertSql.AppendLine("      @ID_COORDENATE,       ");
            insertSql.AppendLine("      @COUNTRY,             ");
            insertSql.AppendLine("      @POPULATION,          ");
            insertSql.AppendLine("      @TIMEZONE,            ");
            insertSql.AppendLine("      @SUNRISE,             ");
            insertSql.AppendLine("      @SUNSET               ");
            insertSql.AppendLine(" WHERE NOT EXISTS           "); 
            insertSql.AppendLine("  (SELECT * FROM WS.TB_CITY ");
            insertSql.AppendLine("    WHERE NAME = @NAME )    ");                 
            insertSql.AppendLine(" RETURNING  ID_CITY         ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID", objCity.id);
                        command.Parameters.AddWithValue("@NAME", objCity.name);
                        command.Parameters.AddWithValue("@ID_COORDENATE", objCity.id_coordenate);
                        command.Parameters.AddWithValue("@COUNTRY", objCity.country);
                        command.Parameters.AddWithValue("@POPULATION", objCity.population);
                        command.Parameters.AddWithValue("@TIMEZONE", objCity.timezone);
                        command.Parameters.AddWithValue("@SUNRISE", objCity.sunrise);
                        command.Parameters.AddWithValue("@SUNSET", objCity.sunset);

                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao executar método {MethodBase.GetCurrentMethod()} em {this.GetType().Name}. 4o: " + ex.Message);
            }
        }

        /// <summary>
        /// Inserção de Rain no banco de dados
        /// </summary>
        /// <param name="objRain"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int PostRain(Rain objRain)
        {
            var insertSql = new StringBuilder();

            insertSql.AppendLine(" INSERT INTO                ");
            insertSql.AppendLine("     WS.TB_RAIN (           ");
            insertSql.AppendLine("         ID_RAIN,           ");
            insertSql.AppendLine("         _3H                ");
            insertSql.AppendLine("     )                      ");
            insertSql.AppendLine(" VALUES                     ");
            insertSql.AppendLine("     (                      ");
            insertSql.AppendLine("       (SELECT COUNT(*)+1   ");
            insertSql.AppendLine("          FROM WS.TB_RAIN), ");
            insertSql.AppendLine("         @3H                ");
            insertSql.AppendLine(" RETURNING  ID_RAIN         ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(insertSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@3H", objRain._3h);

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
        /// Deletar as informações de main
        /// </summary>
        /// <param name="id_main"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteMain(int id_main)
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine(" DELETE FROM WS.TB_MAIN   ");
            deletSql.AppendLine(" WHERE ID_MAIN = @ID_MAIN ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_MAIN", id_main);

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
        /// Deletar as informações de weather
        /// </summary>
        /// <param name="id_weather"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteWeather(int id_weather)
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine(" DELETE FROM WS.TB_WEATHER   ");
            deletSql.AppendLine(" WHERE ID_WEATHER = @ID_WEATHER ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_WEATHER", id_weather);

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
        /// Deletar as informações de clouds
        /// </summary>
        /// <param name="id_clouds"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteClouds(int id_clouds)
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine(" DELETE FROM WS.TB_CLOUDS   ");
            deletSql.AppendLine(" WHERE ID_CLOUDS = @ID_CLOUDS ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_CLOUDS", id_clouds);

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
        /// Deletar as informações de wind
        /// </summary>
        /// <param name="id_wind"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteWind(int id_wind)
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine(" DELETE FROM WS.TB_WIND   ");
            deletSql.AppendLine(" WHERE ID_WIND = @ID_WIND ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_WIND", id_wind);

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
        /// Deletar as informações de rain
        /// </summary>
        /// <param name="id_rain"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteRain(int id_rain)
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine(" DELETE FROM WS.TB_RAIN   ");
            deletSql.AppendLine(" WHERE ID_RAIN = @ID_RAIN ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_RAIN", id_rain);

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
        /// Deletar as informações de sys
        /// </summary>
        /// <param name="id_sys"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool DeleteSys(int id_sys)
        {
            var deletSql = new StringBuilder();

            deletSql.AppendLine(" DELETE FROM WS.TB_SYS   ");
            deletSql.AppendLine(" WHERE ID_SYS = @ID_SYS ");

            try
            {
                using (var connection = new NpgsqlConnection(base.ConnectionString))
                {
                    connection.Open();

                    using (var command = new NpgsqlCommand(deletSql.ToString(), connection))
                    {
                        command.Parameters.AddWithValue("@ID_SYS", id_sys);

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
