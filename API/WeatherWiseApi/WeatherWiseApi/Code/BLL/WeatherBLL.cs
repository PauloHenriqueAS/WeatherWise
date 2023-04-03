using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Api;

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
            return new OpenWeatherApi(_configuration).GetCurrentWeather(coordinate);
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
    }
}
