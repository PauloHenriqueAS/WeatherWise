namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Tempo no Momento
    /// </summary>
    public class CurrentWeather
    {
        /// <summary>
        /// Coordenadas
        /// </summary>
        public Coordinate coord { get; set; }

        /// <summary>
        /// Lista de Informações do Tempo
        /// </summary>
        public List<Weather> weather { get; set; }

        /// <summary>
        /// Info Base
        /// </summary>
        public string? _base { get; set; }

        /// <summary>
        /// Informações Principais
        /// </summary>
        public Main main { get; set; }

        /// <summary>
        /// Visibilidade
        /// </summary>
        public int visibility { get; set; }

        /// <summary>
        /// Vento
        /// </summary>
        public Wind wind { get; set; }

        /// <summary>
        /// Nuvens
        /// </summary>
        public Clouds clouds { get; set; }

        /// <summary>
        /// Dt
        /// </summary>
        public int dt { get; set; }

        /// <summary>
        /// Sys
        /// </summary>
        public Sys sys { get; set; }

        /// <summary>
        /// Fuso Horário
        /// </summary>
        public int timezone { get; set; }

        /// <summary>
        /// Identificação
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        public string name { get; set; }        

        /// <summary>
        /// Código
        /// </summary>
        public int cod { get; set; }
    }
}
