namespace WeatherWiseApi.Code.Model;

public class Alert
{
    public string email_user { get; set; }
    public double? wind_speed { get; set; } = 0;
    public double? visibility { get; set; } = 0;
    public int? air_pollution_aqi { get; set; } = 0;
    public double? precipitation { get; set; } = 0;
}