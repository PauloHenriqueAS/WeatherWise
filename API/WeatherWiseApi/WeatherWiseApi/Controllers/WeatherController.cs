using Swashbuckle.AspNetCore.Annotations;
using WeatherWiseApi.Code.Model;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseApi.Api;
using System.Net;

namespace WeatherWiseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class WeatherController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public WeatherController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("GetCurrentWeather")]
        [SwaggerOperation("GetCurrentWeather")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult GetCurrentWeather(Coordinate coordinate)
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
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }

        [HttpPost]
        [Route("PostWeather")]
        [SwaggerOperation("PostCurrentWeather")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult PostCurrentWeather(Coordinate coordinate)
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
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }


        [HttpPost]
        [Route("GetForecastWeather")]
        [SwaggerOperation("GetForecastWeather")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult GetForecastWeather(Coordinate coordinate)
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
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }

        [HttpPost]
        [Route("PostForecastWeather")]
        [SwaggerOperation("GetForecastWeather")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult PostForecastWeather(Coordinate coordinate)
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
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }
    }
}