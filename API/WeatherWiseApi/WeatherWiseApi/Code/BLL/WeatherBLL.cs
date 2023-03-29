using WeatherWiseApi.Api;
using WeatherWiseApi.Code.Model;

namespace WeatherWiseApi.Code.BLL
{
    public class WeatherBLL
    {
        private readonly IConfiguration _configuration;

        public WeatherBLL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CurrentWeather GetCurrentWeather(Coordinate coordinate)
        {
            return new OpenWeatherApi(_configuration).GetCurrentWeather(coordinate);
        }
    }
}
