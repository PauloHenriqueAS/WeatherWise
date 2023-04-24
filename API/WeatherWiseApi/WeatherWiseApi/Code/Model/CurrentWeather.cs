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

    /// <summary>
    /// Model de itens para serem inseridos na tabela de current weather no banco de dados
    /// </summary>
    public class CurrentWeatherDB
    {
        /// <summary>
        /// Id de identificação no Banco de Dados sobre Coordenadas
        /// </summary>
        public int id_coordate { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre Informações do Tempo
        /// </summary>
        public int id_weather { get; set; }

        /// <summary>
        /// Info Base
        /// </summary>
        public string? _base { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre Informações Principais
        /// </summary>
        public int id_main { get; set; }

        /// <summary>
        /// Visibilidade
        /// </summary>
        public int visibility { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre Vento
        /// </summary>
        public int id_wind { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre Nuvens
        /// </summary>
        public int id_clouds { get; set; }

        /// <summary>
        /// Dt
        /// </summary>
        public int dt { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre Sys
        /// </summary>
        public int id_sys { get; set; }

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

        /// <summary>
        /// Data de Inserção do objeto no banco de dados 
        /// </summary>
        public DateTime date_weather { get; set; }
    }
}
