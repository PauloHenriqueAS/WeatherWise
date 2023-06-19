namespace WsRoutine.Code.Model
{
    /// <summary>
    /// Sys
    /// </summary>
    public class Sys
    {
        /// <summary>
        /// Tipo
        /// </summary>
        public int type { get; set; }

        /// <summary>
        /// Identificação
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// País
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// Nascer do Sol
        /// </summary>
        public int sunrise { get; set; }

        /// <summary>
        /// Pôr do Sol
        /// </summary>
        public int sunset { get; set; }
    }
}
