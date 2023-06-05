using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Api;
using WeatherWiseApi.Helpers;
using WeatherWiseApi.Code.DAL;
using System.Transactions;

namespace WeatherWiseApi.Code.BLL
{
    /// <summary>
    /// Camada de Regras de Negócio do Tempo
    /// </summary>
    public class WeatherBLL
    {
        private readonly IConfiguration _configuration;

        public WeatherBLL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Consultar informações do tempo atual
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public CurrentWeather GetCurrentWeather(Coordinate coordinate)
        {
            var result = new OpenWeatherApi(_configuration).GetCurrentWeather(coordinate);
            return result;
        }
        public List<Alert> GetAlertByUser(string email_user)
        {
            return new WeatherDAL(_configuration).GetAlertByUser(email_user);
        }

        /// <summary>
        /// Inserir as alertas
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool InsertAlert(Alert alert)
        {
            return new WeatherDAL(_configuration).InsertAlert(alert);
        }

        /// <summary>
        /// Consultar previsão do tempo
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public Forecast GetForecastWeather(Coordinate coordinate)
        {
            return new OpenWeatherApi(_configuration).GetForecastWeather(coordinate);
        }

        /// <summary>
        /// Inserir as Informações do tempo atual no Banco de Dados
        /// </summary>
        /// <param name="currentWeather"></param>
        /// <returns></returns>
        public RetornoObj PostCurrentWeather(CurrentWeather currentWeather)
        {
            RetornoObj retornoObj = new RetornoObj();
            try
            {
                WeatherDAL weatherDAL = new WeatherDAL(_configuration);
                CurrentWeatherDB weatherDB = new CurrentWeatherDB();
                ComumDAL comumDal = new ComumDAL(_configuration);

                retornoObj.obj = currentWeather;
                weatherDB._base = (!String.IsNullOrEmpty(currentWeather._base)) ? currentWeather._base : " ";
                weatherDB.visibility = currentWeather.visibility;
                weatherDB.dt = currentWeather.dt;
                weatherDB.timezone = currentWeather.timezone;
                weatherDB.id = currentWeather.id;
                weatherDB.name = currentWeather.name;
                weatherDB.cod = currentWeather.cod;
                weatherDB.date_weather = DateTime.Now;

                using (TransactionScope scope = new TransactionScope())
                {
                    int idCord = comumDal.GetCoordenateInfo(currentWeather.coord);

                    if (idCord == 0)
                        weatherDB.id_coordate = comumDal.PostCoordenate(currentWeather.coord);
                    else
                        weatherDB.id_coordate = idCord;

                    weatherDB.id_main = comumDal.PostMain(currentWeather.main);
                    weatherDB.id_wind = comumDal.PostWind(currentWeather.wind);
                    weatherDB.id_clouds = comumDal.PostClouds(currentWeather.clouds);
                    weatherDB.id_sys = comumDal.PostSys(currentWeather.sys);

                    foreach (var item in currentWeather.weather)
                    {
                        weatherDB.id_weather = comumDal.PostWeather(item);
                        weatherDAL.PostCurrentWeather(weatherDB);
                    }

                    retornoObj.StatusRetorno = true;
                    retornoObj.MensagemErro = "Objeto inserido com sucesso.";
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                retornoObj.StatusRetorno = false;
                retornoObj.MensagemErro = ex.Message;
            }

            return retornoObj;
        }

        public RetornoObj PostForecast(Forecast objForecast)
        {
            RetornoObj retornoObj = new RetornoObj();
            try
            {
                WeatherDAL weatherDAL = new WeatherDAL(_configuration);
                ComumDAL comumDal = new ComumDAL(_configuration);

                retornoObj.obj = objForecast;

                var listIds = weatherDAL.GetIdsInfo();
                if (listIds != null && listIds.Count > 0)
                {
                    using (TransactionScope scope2 = new TransactionScope())
                    {
                        weatherDAL.DeleteForecastWeather();
                        weatherDAL.DeleteListForecast();

                        foreach (var item in listIds)
                        {
                            comumDal.DeleteMain(item.id_main);
                            comumDal.DeleteClouds(item.id_clouds);
                            comumDal.DeleteRain(item.id_rain);
                            comumDal.DeleteSys(item.id_sys);
                            comumDal.DeleteWeather(item.id_weather);
                            comumDal.DeleteWind(item.id_wind);
                        }
                        scope2.Complete();
                    }
                }

                using (TransactionScope scope = new TransactionScope())
                {
                    var idCoordenateCity = comumDal.GetCityInfo(objForecast.city);
                    if (idCoordenateCity > 0)
                        objForecast.id_city = idCoordenateCity;
                    else
                    {
                        int idCord = comumDal.GetCoordenateInfo(objForecast.city.coord);

                        if (idCord == 0)
                            objForecast.city.id_coordenate = comumDal.PostCoordenate(objForecast.city.coord);
                        else
                            objForecast.city.id_coordenate = idCord;


                        objForecast.id_city = comumDal.PostCity(objForecast.city);
                    }

                    foreach (var item in objForecast.list)
                    {
                        ForecastDB forecastDB = new ForecastDB();
                        forecastDB.dt = item.dt;
                        forecastDB.pop = item.pop;
                        forecastDB.dt_txt = item.dt_txt;
                        forecastDB.visibility = item.visibility;
                        forecastDB.id_main = comumDal.PostMain(item.main);
                        forecastDB.id_clouds = comumDal.PostClouds(item.clouds);
                        forecastDB.id_wind = comumDal.PostWind(item.wind);
                        forecastDB.id_sys = comumDal.PostSys(item.sys);
                        forecastDB.id_rain = comumDal.PostRain(item.rain);

                        foreach (var itemWeather in item.weather)
                        {
                            forecastDB.id_weather = comumDal.PostWeather(itemWeather);
                            objForecast.id_listForecast = weatherDAL.PostListForecast(forecastDB);
                            weatherDAL.PostForecastWeather(objForecast);
                        }
                    }

                    retornoObj.StatusRetorno = true;
                    retornoObj.MensagemErro = "Objeto inserido com sucesso.";
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                retornoObj.StatusRetorno = false;
                retornoObj.MensagemErro = ex.Message;
            }

            return retornoObj;
        }
    }
}
