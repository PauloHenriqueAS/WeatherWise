using WeatherWiseApi.Api;
using WeatherWiseApi.Code.Model;

namespace WeatherWiseApi.Code.BLL
{
    public class CoordinateBLL
    {
        public CoordinateBLL()
        {
        }

        public Coordinate GetCoordinate(string place)
        {
            return new NominatimApi().GetCoordinates(place).FirstOrDefault();
        }
    }
}