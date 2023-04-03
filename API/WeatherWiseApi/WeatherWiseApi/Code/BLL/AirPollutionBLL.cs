using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Api;

namespace WeatherWiseApi.Code.BLL
{
    /// <summary>
    /// Camada de Regras de Negócio da Poluição do Ar
    /// </summary>
    public class AirPollutionBLL
    {
        private readonly IConfiguration _configuration;

        public AirPollutionBLL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Consultar informações da poluição do ar
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public AirPollution GetAirPollution(Coordinate coordinate)
        {
            return new OpenWeatherApi(_configuration).GetAirPollution(coordinate);
        }
    }
}
