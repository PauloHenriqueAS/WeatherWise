﻿using Swashbuckle.AspNetCore.Annotations;
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
                    
                    bool retorno = new UserBLL(_configuration).AuthorizeUser(objUser);

                    if (retorno != false)
                        return Ok(new { success = true, message = "Autorização concedida." });
                    else
                        return Unauthorized(new { success = false, message = "Autorização negada." });
                }
                else
                {
                    return Unauthorized(new { success = false, message = "Autorização negada." });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new { success = false, message = "Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message });
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

                    return Ok(retorno);
                }
                else
                {
                    return BadRequest("Informe uma Localidade Válida.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Um erro ocorreu. Erro:" + e.Message + " Inner:" + e.InnerException?.Message);
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
                        return Ok(retorno);
                    else
                        return StatusCode((int)HttpStatusCode.InternalServerError, $"Não foi possível salvar as informações do usuário informado.");
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
                        return Ok(retorno);
                    else
                        return StatusCode((int)HttpStatusCode.InternalServerError, $"Não foi possível atualizar as informações do usuário informado.");
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