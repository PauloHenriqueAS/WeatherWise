using Swashbuckle.AspNetCore.Annotations;
using WeatherWiseApi.Code.Model;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseApi.Api;
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
                    //var retorno = new WeatherBLL(_configuration).GetCurrentWeather(coordinate);

                    //return Ok(retorno);
                    return null;
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

        [HttpPost]
        [Route("PostAirPollution")]
        [SwaggerOperation("PostAirPollution")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult PostAirPollution(Coordinate coordinate)
        {
            try
            {

                if (new Comum().ValidateObjCoordenate(coordinate))
                {
                    //var retorno = new WeatherBLL(_configuration).GetCurrentWeather(coordinate);

                    //return Ok(retorno);
                    return null;
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

    }
}