namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Vento
    /// </summary>
    public class Wind
    {
        /// <summary>
        /// Velocidade do vento
        /// </summary>
        public double speed { get; set; }

        /// <summary>
        /// Temperatua do vento
        /// </summary>
        public int deg { get; set; }

        /// <summary>
        /// Gust
        /// </summary>
        public double gust { get; set; }
    }
}
