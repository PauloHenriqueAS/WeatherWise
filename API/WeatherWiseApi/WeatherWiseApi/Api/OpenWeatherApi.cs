namespace WeatherWiseApi.Api;

public class OpenWeatherApi : Api
{
    public OpenWeatherApi(IConfiguration configuration) : base(configuration)
    {
        this.HOST = "http://api.openweathermap.org/data/2.5/";
    }

    public CurrentWeather GetCurrentWeather(Coordinates coordinates)
    {
        return base.Post<CurrentWeather>($"{this.HOST}/weather?lat={coordinates.Lat}&lon={coordinates.Long}&units=metric");
    }
}