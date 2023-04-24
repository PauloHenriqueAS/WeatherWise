namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Cidade
    /// </summary>
    public class City
    {
        /// <summary>
        /// Identificação da Cidade
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nome Cidade
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Coordenada da Cidade
        /// </summary>
        public Coordinate coord { get; set; }

        /// <summary>
        /// País de localização da Cidade
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// População
        /// </summary>
        public int population { get; set; }

        /// <summary>
        /// Fuso Horário
        /// </summary>
        public int timezone { get; set; }

        /// <summary>
        /// Nascer do Sol
        /// </summary>
        public int sunrise { get; set; }

        /// <summary>
        /// Pôr do Sol
        /// </summary>
        public int sunset { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre a coordenada da cidade
        /// </summary>
        public int id_coordenate { get; set; }
    }
}
