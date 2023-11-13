using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Extensions;

namespace WeatherWiseApi.Api;

public class OpenWeatherApi : Api
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Configurações de conexão com a API OpenWeather
    /// </summary>
    /// <param name="configuration"></param>
    public OpenWeatherApi(IConfiguration configuration)
    {
        _configuration = configuration;

        this.HOST = "http://api.openweathermap.org/data/2.5/";
        this.API_KEY = ConnnectionsStrings.GetApiKey(configuration, this.GetType().Name);
    }

    /// <summary>
    /// Consultar informações do tempo atual
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public CurrentWeather GetCurrentWeather(Coordinate coordinate)
    {
        return base.Get<CurrentWeather>($"weather?lat={coordinate.Lat}&lon={coordinate.Lon}&units=metric&appid={this.API_KEY}");
    }

    /// <summary>
    /// Consultar previsão do tempo 
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public Forecast GetForecastWeather(Coordinate coordinate)
    {
        return base.Get<Forecast>($"forecast?lat={coordinate.Lat}&lon={coordinate.Lon}&units=metric&appid={this.API_KEY}");
    }
    
    /// <summary>
    /// Consulta de informações da poluição do ar
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public AirPollution GetAirPollution(Coordinate coordinate)
    {
        return base.Get<AirPollution>($"air_pollution?lat={coordinate.Lat}&lon={coordinate.Lon}&units=metric&appid={this.API_KEY}");
    }
}