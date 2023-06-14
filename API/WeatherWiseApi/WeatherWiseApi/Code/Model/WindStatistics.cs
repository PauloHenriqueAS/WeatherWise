namespace WeatherWiseApi.Code.Model
{
    public class WindStatistics
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
        public double Speed { get; set; }
        public DateTime DateWeather { get; set; }
        public string Region { get; set; }
    }
}
