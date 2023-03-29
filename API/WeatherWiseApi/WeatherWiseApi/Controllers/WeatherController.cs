using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using WeatherWiseApi.Code.BLL;
using WeatherWiseApi.Code.Model;

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
                var retorno = new WeatherBLL(_configuration).GetCurrentWeather(coordinate);
                
                return Ok(retorno);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }
    }
}
