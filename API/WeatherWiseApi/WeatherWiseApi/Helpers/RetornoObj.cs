using System.Security.Cryptography;
using WeatherWiseApi.Code.Model;
using System.Reflection;
using System.Text;

namespace WeatherWiseApi.Helpers
{
    /// <summary>
    /// Objeto para retorno de informações de métodos POST
    /// </summary>
    public class RetornoObj
    {
        /// <summary>
        /// Status do retorno
        /// </summary>
        public bool StatusRetorno { get; set; }

        /// <summary>
        /// Mensagem de erro
        /// </summary>
        public string MensagemErro { get; set; }

        /// <summary>
        /// Obejto genérico de retorno
        /// </summary>
        public object? obj { get; set; }
    }
}
