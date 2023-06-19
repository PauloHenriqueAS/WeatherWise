namespace WsRoutine.Code.Model
{
    /// <summary>
    /// Poluição do Ar
    /// </summary>
    public class AirPollution
    {
        /// <summary>
        /// Description air pollution
        /// </summary>
        public string air_pollution_description { get; set; }

        /// <summary>
        /// Coordenadas
        /// </summary>
        public Coordinate coord { get; set; }

        /// <summary>
        /// Lista de Indices de Poluição do Ar
        /// </summary>
        public List<ListIdxAirPollution> list { get; set; }

        /// <summary>
        /// Dt
        /// </summary>
        public int dt { get; set; }

        /// <summary>
        /// AQI
        /// </summary>
        public int aqi { get; set; }

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

    /// <summary>
    /// Model de itens para serem inseridos na tabela de poluição do ar no banco de dados
    /// </summary>
    public class AirPollutionDB
    {
        /// <summary>
        /// Id de identificação no Banco de Dados sobre a poluição do ar
        /// </summary>
        public int id_airPollution { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre a coordenada
        /// </summary>
        public int id_coordenate { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre a main
        /// </summary>
        public int id_main { get; set; }

        /// <summary>
        /// Id de identificação no Banco de Dados sobre componente
        /// </summary>
        public int id_component { get; set; }

        /// <summary>
        /// Dt
        /// </summary>
        public int dt { get; set; }

        /// <summary>
        /// AQI
        /// </summary>
        public int aqi { get; set; }

        /// <summary>
        /// Data se inserção no Banco
        /// </summary>
        public DateTime data_save { get; set; }
    }
}
