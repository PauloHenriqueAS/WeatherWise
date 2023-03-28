namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Poluição do Ar
    /// </summary>
    public class AirPollution
    {
        /// <summary>
        /// Coordenadas
        /// </summary>
        public Coordinate coord { get; set; }

        /// <summary>
        /// Lista de Indices de Poluição do Ar
        /// </summary>
        public List<ListIdxAirPollution> list { get; set; }
    }

    /// <summary>
    /// Lista de Indices de Poluição do Ar
    /// </summary>
    public class ListIdxAirPollution
    {
        /// <summary>
        /// Main
        /// </summary>
        public Main main { get; set; }

        /// <summary>
        /// Componentes
        /// </summary>
        public Components components { get; set; }

        /// <summary>
        /// Dt
        /// </summary>
        public int dt { get; set; }
    }
}
