using Newtonsoft.Json;

namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Coordenadas do Lugar
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Nome correto da localização retornada pela API Nominatim
        /// </summary>
        [JsonProperty("display_name")]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Latitude
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Longitude
        /// </summary>
        public double Lon { get; set; }

        public Coordinate(double latitude, double longitude)
        {
            this.Lat = latitude;
            this.Lon = longitude;
        }

        public Coordinate() { }
    }
}