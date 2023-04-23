namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Infos Principais
    /// </summary>
    public class Main
    {
        /// <summary>
        /// Temperatura
        /// </summary>
        public double temp { get; set; }

        /// <summary>
        /// Sensação Térmica
        /// </summary>
        public double feels_like { get; set; }

        /// <summary>
        /// Temperatura Mínima
        /// </summary>
        public double temp_min { get; set; }

        /// <summary>
        /// Temperatura Máxima
        /// </summary>
        public double temp_max { get; set; }

        /// <summary>
        /// Pressão Atmosférica
        /// </summary>
        public int pressure { get; set; }

        /// <summary>
        /// Umidade do Ar
        /// </summary>
        public int humidity { get; set; }

        /// <summary>
        /// Nível do Mar
        /// </summary>
        public int sea_level { get; set; }

        /// <summary>
        /// Level Grnd
        /// </summary>
        public int grnd_level { get; set; }

        /// <summary>
        /// Temperatura KF
        /// </summary>
        public double temp_kf { get; set; }

        /// <summary>
        /// AQI
        /// </summary>
        public int aqi { get; set; }        
    }
}
