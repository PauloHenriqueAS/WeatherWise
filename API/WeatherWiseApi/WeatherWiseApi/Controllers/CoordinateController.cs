using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseApi.Code.BLL;
using System.Net;

namespace WeatherWiseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoordinateController : ControllerBase
    {
        private readonly ILogger<CoordinateController> _logger;

        public CoordinateController(ILogger<CoordinateController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Método para consultar coordenadas (Latitude e Longitude) com base em um local
        /// </summary>
        /// <param name="place">Local para consulta de coordenada</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCoordinate")]
        [SwaggerOperation("GetCoordinate")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult GetCoordinate(string place = "Rondon Pacheco, Uberlandia")
        {
            try
            {
                if (!String.IsNullOrEmpty(place))
                {
                    var retorno = new CoordinateBLL().GetCoordinate(place);

                    if (retorno != null)
                        return Ok(retorno);
                    else
                        return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro na consulta da Latitude e Longitude para a localidade {place} informada.");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Informe uma Localidade Válida.");
                }
               
            }
            catch (Exception e)
            {
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }

    }
}