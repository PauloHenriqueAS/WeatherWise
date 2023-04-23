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
            var result = new OpenWeatherApi(_configuration).GetAirPollution(coordinate);
            result.air_pollution_description = GetAirPollutionSituationDescription(result.list.FirstOrDefault()!.main.aqi);

            return result;
        }

        public string GetAirPollutionSituationDescription(int aqi)
        {
            switch (aqi)
            {
                case 1:
                    return "Bom";
                case 2:
                    return "Normal";
                case 3:
                    return "Moderado";
                case 4:
                    return "Pobre";
                case 5:
                    return "Muito pobre";
                default:
                    return "Inexistente";
            }
        }
    }
}
