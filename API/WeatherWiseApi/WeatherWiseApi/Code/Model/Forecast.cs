namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Previsão do Tempo
    /// </summary>
    public class Forecast
    {
        /// <summary>
        /// Código do Forecast
        /// </summary>
        public string cod { get; set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        public int message { get; set; }

        /// <summary>
        /// CNT
        /// </summary>
        public int cnt { get; set; }

        /// <summary>
        /// Lista de Previões do Tempo
        /// </summary>
        public List<ListForecast> list { get; set; }

        /// <summary>
        /// Cidade da Previsão
        /// </summary>
        public City city { get; set; }
    }

    /// <summary>
    /// Lista de Previsões do Tempo
    /// </summary>
    public class ListForecast
    {
        /// <summary>
        /// Dt
        /// </summary>
        public int dt { get; set; }

        /// <summary>
        /// Main
        /// </summary>
        public Main main { get; set; }

        /// <summary>
        /// Lista do Tempo
        /// </summary>
        public List<Weather> weather { get; set; }

        //Nuvens
        public Clouds clouds { get; set; }

        /// <summary>
        /// Vento
        /// </summary>
        public Wind wind { get; set; }

        /// <summary>
        /// Visibilidade
        /// </summary>
        public int visibility { get; set; }

        /// <summary>
        /// Pop
        /// </summary>
        public double pop { get; set; }

        /// <summary>
        /// Chuva
        /// </summary>
        public Rain rain { get; set; }

        /// <summary>
        /// Sys
        /// </summary>
        public Sys sys { get; set; }

        /// <summary>
        /// Texto Dt
        /// </summary>
        public string dt_txt { get; set; }
    }


    /// <summary>
    /// Chuva
    /// </summary>
    public class Rain
    {
        /// <summary>
        /// Previsão 3h
        /// </summary>
        public double _3h { get; set; }
    }

}
