using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using WeatherWiseApi.Code.BLL;
using System.Net;
using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Helpers;

namespace WeatherWiseApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Método para Autenticar um Usuário do Sistema
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AuthorizeUser")]
        [SwaggerOperation("AuthorizeUser")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult AuthorizeUser(UserCredentials objUser)
        {
            try
            {
                if (!String.IsNullOrEmpty(objUser.email_user) && !String.IsNullOrEmpty(objUser.password_user))
                {
                    
                    User retorno = new UserBLL(_configuration).AuthorizeUser(objUser);

                    if (retorno is not null)
                        return Ok(new ResponseApi(retorno, true, "Autorização concedida."));
                    else
                        return Unauthorized(new ResponseApi(retorno, false, "Usuário não autorizado!"));
                }
                else
                {
                    return Unauthorized(new ResponseApi(null, false, "Usuário não autorizado!"));
                }

            }
            catch (Exception e)
            {
                return BadRequest(new ResponseApi(null, false, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message));
            }
        }

        [HttpGet]
        [Route("GetUserInfo")]
        [SwaggerOperation("GetUserInfo")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult GetUserInfo(string email_user)
        {
            try
            {
                if (!String.IsNullOrEmpty(email_user))
                {

                    var retorno = new UserBLL(_configuration).GetUserInfo(email_user);

                    if (retorno is not null)
                        return Ok(new ResponseApi(retorno, true, null));
                    else
                        return BadRequest(new ResponseApi(retorno, false, "Não foi possível buscar usuário!"));
                }
                else
                {
                    return BadRequest(new ResponseApi(null, false, "Não foi possível buscar usuário!"));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseApi(null, false, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message));
            }
        }

        [HttpPost]
        [Route("PostUserInfo")]
        [SwaggerOperation("PostUserInfo")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult PostUserInfo(User objUser)
        {
            try
            {
                if (objUser != null)
                {

                    var retorno = new UserBLL(_configuration).PostUser(objUser);

                    if (retorno != false)
                        return Ok(new ResponseApi(retorno, true, "Cadastro realizado com sucesso!."));
                    else
                        return BadRequest(new ResponseApi(retorno, false, "Não foi possível realizar cadastro do usuário"));
                }
                else
                {
                    return BadRequest(new ResponseApi(false, false, "Não foi possível realizar cadastro"));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseApi(null, false, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message));
            }
        }

        [HttpPost]
        [Route("PutUserInfo")]
        [SwaggerOperation("PutUserInfo")]
        [SwaggerResponse((int)HttpStatusCode.OK, Description = "OK")]
        [SwaggerResponse((int)HttpStatusCode.NotFound, Description = "Nenhum resultado encontrado.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Description = "Requisição inválida. Veja a mensagem para mais detalhes.")]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, Description = "Erro interno. Contate o suporte.")]
        public IActionResult PutUserInfo(User objUser)
        {
            try
            {
                if (!String.IsNullOrEmpty(objUser.email_user))
                {

                    var retorno = new UserBLL(_configuration).PutUser(objUser);

                    if (retorno != false)
                        return Ok(new ResponseApi(retorno, true, "Cadastro atualizado com sucesso!."));
                    else
                        return BadRequest(new ResponseApi(retorno, false, "Não foi possível atualizar cadastro do usuário"));
                }
                else
                {
                    return BadRequest(new ResponseApi(false, false, "Não foi possível atualizar cadastro"));
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseApi(null, false, "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message));
            }
        }
    }
}