using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Code.BLL;
using Swashbuckle.AspNetCore.Annotations;

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
        public IActionResult GetCoordinate(string place = "Rondon Pacheco, Uberlandia")
        {
            try
            {
                var retorno = new CoordinateBLL().GetCoordinate(place);

                return Ok(retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }

    }
}