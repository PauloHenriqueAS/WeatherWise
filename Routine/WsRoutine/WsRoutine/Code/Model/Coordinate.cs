using Newtonsoft.Json;

namespace WsRoutine.Code.Model
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

        /// <summary>
        /// Retorna Lista de coordenadas de pontos importantes de Uberlândia
        /// </summary>
        /// <returns></returns>
        public List<Coordinate> GetListCoordenatesUberlandia()
        {
            var retorno = new List<Coordinate>();
            retorno.Add(new Coordinate
            {
                Lat = -18.8819549052504,
                Lon = -48.2830584744961,
                DisplayName = "Região Norte"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9462623767215,
                Lon = -48.2731021154858,
                DisplayName = "Região Sul"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9210944691962,
                Lon = -48.2351649518704,
                DisplayName = "Região Leste"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9322987155795,
                Lon = -48.3294070423025,
                DisplayName = "Região Oeste"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9103522308577,
                Lon = -48.2757060426256,
                DisplayName = "Centro (Sérgio Pacheco)"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9143553205871,
                Lon = -48.1866706109177,
                DisplayName = "Bairro Morumbi"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9114220830934,
                Lon = -48.2622445321177,
                DisplayName = "Rondon Pacheco + Joao Naves"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9254042727836,
                Lon = -48.2725663561418,
                DisplayName = "Rondon Pacheco + Griff Shopping"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9297203007803,
                Lon = -48.2931336130292,
                DisplayName = "Rondon Pacheco + Uberabinha"
            });
            retorno.Add(new Coordinate
            {
                Lat = -18.9517333321633,
                Lon = -48.2736865997613,
                DisplayName = "Nicomedes + Vinhedos"
            });

            return retorno;
        }
    }
}