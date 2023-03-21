using WeatherWiseApi.Code.Model;

namespace WeatherWiseApi.Api;

public class OpenWeatherApi : Api
{
    public OpenWeatherApi(IConfiguration configuration) : base(configuration)
    {
        this.HOST = _configuration.GetSection(this.GetType().Name).Value;
        this.API_KEY = _configuration.GetSection($"{this.GetType().Name}_KEY").Value;
    }

    public CurrentWeather GetCurrentWeather(Coordinate coordinate)
    {
        return base.Get<CurrentWeather>($"{this.HOST}/weather?lat={coordinate.Lat}&lon={coordinate.Long}&units=metric&appid={this.API_KEY}");
    }
}