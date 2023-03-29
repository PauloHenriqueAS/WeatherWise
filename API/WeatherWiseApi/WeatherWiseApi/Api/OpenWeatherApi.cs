using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Extensions;

namespace WeatherWiseApi.Api;

public class OpenWeatherApi : Api
{
    private readonly IConfiguration _configuration;
    public OpenWeatherApi(IConfiguration configuration)
    {
        _configuration = configuration;

        this.HOST = _configuration.GetSection(this.GetType().Name).Value;
        this.API_KEY = ConnnectionsStrings.GetApiKey(configuration, this.GetType().Name);
    }

    public CurrentWeather GetCurrentWeather(Coordinate coordinate)
    {
        return base.Get<CurrentWeather>($"weather?lat={coordinate.Lat}&lon={coordinate.Long}&units=metric&appid={this.API_KEY}");
    }
}