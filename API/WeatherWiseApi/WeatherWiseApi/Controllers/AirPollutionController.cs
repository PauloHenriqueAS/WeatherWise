using Swashbuckle.AspNetCore.Annotations;
using WeatherWiseApi.Code.Model;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseApi.Code.BLL;
using WeatherWiseApi.Helpers;
using System.Reflection;
using System.Net;

namespace WeatherWiseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IActionResult GetAirPollution([FromBody]Coordinate coordinate)
        {
            try
            {
                if (new Comum().ValidateObjCoordenate(coordinate))
                {
                    var retorno = new AirPollutionBLL(_configuration).GetAirPollution(coordinate);

                    if (retorno is not null)
                        return Ok(new ResponseApi(retorno, true, null));
                    else
                        return BadRequest(new ResponseApi(retorno, false, $"Erro na consulta das informações da poluição do ar para a Latitude: {coordinate.Lat}, e na Longitude {coordinate.Lon}."));
                }
                else
                {
                    return BadRequest(new ResponseApi(null, false, $"Erro nas Coordenadas informadas. Latitude: {coordinate.Lat}, e na Longitude {coordinate.Lon}."));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseApi(null, false, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message));
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
        public IActionResult PostAirPollution([FromBody]AirPollution objAirPollution)
        {
            try
            {
                if (new Comum().ValidateObjAirPollution(objAirPollution))
                {
                    var retorno = new AirPollutionBLL(_configuration).PostAirPollution(objAirPollution);

                    if (retorno != null && retorno.StatusRetorno == true)
                        return Ok(retorno);
                    else
                        return BadRequest(retorno);
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