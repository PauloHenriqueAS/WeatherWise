using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Api;

namespace WeatherWiseApi.Code.BLL
{
    public class CoordinateBLL
    {
        /// <summary>
        /// Consultar Coordenadas de um lugar pelo
        /// </summary>
        /// <param name="place"></param>
        /// <returns></returns>
        public Coordinate GetCoordinate(string place)
        {
            return new NominatimApi().GetCoordinates(place).FirstOrDefault();
        }
    }
}