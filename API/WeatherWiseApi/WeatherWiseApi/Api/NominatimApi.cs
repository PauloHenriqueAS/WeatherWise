using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Extensions;

namespace WeatherWiseApi.Api;

public class NominatimApi : Api
{
    public NominatimApi()
    {
    }

    public IEnumerable<Coordinate> GetCoordinates(string place)
    {
        var apiUrl = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(place)}&format=json&limit=1";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("User-Agent", "Projeto WeatherWiseApi");

        return base.Get<IEnumerable<Coordinate>>(apiUrl, headers);
    }
}