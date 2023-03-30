using Swashbuckle.AspNetCore.Annotations;
using WeatherWiseApi.Code.Model;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseApi.Code.BLL;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Net;

namespace WeatherWiseApi.Controllers
{
    public class AirPollutionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AirPollutionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region GET

        [HttpPost]
        [Route("GetAirPollution")]
        [SwaggerOperation("GetAirPollution")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult GetAirPollution(Coordinate coordinate)
        {
            try
            {
                if (new Comum().ValidateObjCoordenate(coordinate))
                {
                    var retorno = new AirPollutionBLL(_configuration).GetAirPollution(coordinate);

                    if (retorno != null)
                        return Ok(retorno);
                    else
                        return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro na consulta das informações da poluição do ar para a Latitude: {coordinate.Lat}, e na Longitude {coordinate.Long}");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, $"Erro nas Coordenadas informadas. Latitude: {coordinate.Lat}, e na Longitude {coordinate.Long}");
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }
        #endregion

        #region POST

        [HttpPost]
        [Route("PostAirPollution")]
        [SwaggerOperation("PostAirPollution")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult PostAirPollution(AirPollution objAirPollution)
        {
            try
            {
                if (new Comum().ValidateObjAirPollution(objAirPollution))
                {
                    //var retorno = new WeatherBLL(_configuration).GetCurrentWeather(coordinate);

                    //return Ok(retorno);
                    return null;
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, "Erro nas informações do objeto enviado.");
                }
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }

        #endregion
    }
}