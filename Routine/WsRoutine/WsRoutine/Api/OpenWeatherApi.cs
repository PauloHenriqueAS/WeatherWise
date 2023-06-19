using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using WsRoutine.Code.Model;
using WsRoutine.Extensions;

namespace WsRoutine.Api;

public class OpenWeatherApi : Api
{
    /// <summary>
    /// Configurações de conexão com a API OpenWeather
    /// </summary>
    /// <param name="configuration"></param>
    public OpenWeatherApi()
    {
        IConfiguration configDatabase = new ConfigurationBuilder()
                               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .Build();

        this.HOST = ConnnectionsStrings.GetRootCOnfigProperties(this.GetType().Name);
        this.API_KEY = ConnnectionsStrings.GetApiKey(this.GetType().Name);
    }

    /// <summary>
    /// Consultar informações do tempo atual
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public CurrentWeather GetCurrentWeather(Coordinate coordinate)
    {
        return base.Get<CurrentWeather>($"http://api.openweathermap.org/data/2.5/weather?lat={coordinate.Lat}&lon={coordinate.Lon}&units=metric&appid={this.API_KEY}");
    }

    /// <summary>
    /// Consultar previsão do tempo 
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public Forecast GetForecastWeather(Coordinate coordinate)
    {
        return base.Get<Forecast>($"http://api.openweathermap.org/data/2.5/forecast?lat={coordinate.Lat}&lon={coordinate.Lon}&units=metric&appid={this.API_KEY}");
    }

    /// <summary>
    /// Consulta de informações da poluição do ar
    /// </summary>
    /// <param name="coordinate"></param>
    /// <returns></returns>
    public AirPollution GetAirPollution(Coordinate coordinate)
    {
        return base.Get<AirPollution>($"http://api.openweathermap.org/data/2.5/air_pollution?lat={coordinate.Lat}&lon={coordinate.Lon}&units=metric&appid={this.API_KEY}");
    }
}