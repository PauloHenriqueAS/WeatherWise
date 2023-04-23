using Swashbuckle.AspNetCore.Annotations;
using WeatherWiseApi.Code.Model;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseApi.Code.BLL;
using WeatherWiseApi.Helpers;
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

        #region GET
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
                    var retorno = new WeatherBLL(_configuration).GetCurrentWeather(coordinate);

                    if (retorno is not null)
                        return Ok(new ResponseApi(retorno, true, null));
                    else
                        return BadRequest(new ResponseApi(retorno, false, $"Erro na consulta das informações do tempo para a Latitude: {coordinate.Lat}, e na Longitude {coordinate.Lon}."));
                }
                else
                {
                    return BadRequest(new ResponseApi(null, false, $"Erro na consulta das informações do tempo para a Latitude: {coordinate.Lat}, e na Longitude {coordinate.Lon}."));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseApi(null, false, $"Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message));
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
                    var retorno = new WeatherBLL(_configuration).GetForecastWeather(coordinate);

                    if (retorno != null)
                        return Ok(retorno);
                    else
                        return StatusCode((int)HttpStatusCode.InternalServerError, $"Erro na consulta das informações futuras do tempo para a Latitude: {coordinate.Lat}, e na Longitude {coordinate.Lon}.");
                }
                else
                {
                    return StatusCode((int)HttpStatusCode.BadRequest, $"Erro nas Coordenadas informadas. Latitude: {coordinate.Lat}, e na Longitude {coordinate.Lon}.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }

        #endregion

        #region POST

        [HttpPost]
        [Route("PostWeather")]
        [SwaggerOperation("PostCurrentWeather")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult PostCurrentWeather(CurrentWeather objCurrentWeather)
        {
            try
            {
                if (new Comum().ValidateObjCurrentWeather(objCurrentWeather))
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
        public IActionResult PostForecastWeather(Forecast objForecast)
        {
            try
            {
                if (new Comum().ValidateObjForecastWeather(objForecast))
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
                return StatusCode(500, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
            }
        }
        
        [HttpPost]
        [Route("InsertAlert")]
        [SwaggerOperation("InsertAlert")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult InsertAlert(Alert alert)
        {
            try
            {
                if (alert is not null)
                {
                    var retorno = new WeatherBLL(_configuration).InsertAlert(alert);

                    if (retorno != false)
                        return Ok(new ResponseApi(retorno, true, "Alerta cadastrado com sucesso!."));
                    else
                        return BadRequest(new ResponseApi(retorno, false, "Não foi possível realizar cadastro do alerta"));
                }
                else
                {
                    return BadRequest(new ResponseApi(false, false, "Não foi possível realizar cadastro do alerta!"));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseApi(null, false, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message));
            }
        }
        #endregion
    }
}
