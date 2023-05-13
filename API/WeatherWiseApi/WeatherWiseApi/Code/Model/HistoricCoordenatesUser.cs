namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Historico de Coordenadas salvas do Usuário
    /// </summary>
    public class HistoricCoordenatesUserDB
    {
        public int id_historicCoordenatesUser { get; set; }
        public int id_coordenate { get; set; }
        public int id_user { get; set; }
        public string nom_locate { get; set; }
    }

    /// <summary>
    /// Historico de Coordenadas salvas do Usuário
    /// </summary>
    public class HistoricCoordenatesUser
    {
        public Coordinate? coordenate { get; set; }
        public string? email_user { get; set; }
    }
}
