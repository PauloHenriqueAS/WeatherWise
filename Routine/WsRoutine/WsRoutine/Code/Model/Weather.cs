namespace WsRoutine.Code.Model
{
    public class Weather
    {
        /// <summary>
        ///  Identificação do tempo
        /// </summary>
        public int id_Weather { get; set; }

        /// <summary>
        ///  Principal
        /// </summary>
        public string main { get; set; }

        /// <summary>
        /// Descrição do Tempo
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Icone do tempo
        /// </summary>
        public string icon { get; set; }
    }
}
