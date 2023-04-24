﻿using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Code.DAL;
using WeatherWiseApi.Helpers;

namespace WeatherWiseApi.Code.BLL
{
    /// <summary>
    /// Camada de Regras de Negócio de Usuário
    /// </summary>
    public class UserBLL
    {
        private readonly IConfiguration _configuration;

        public UserBLL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Validar se a senha enviada é igual a senha inserida no banco para realizar a autorização do usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public User AuthorizeUser(UserCredentials objUser)
        {
            objUser.password_user = new Comum().EncriptyUserPassword(objUser.password_user);
            string passwordBD = new UserDAL(_configuration).GetPasswordUser(objUser);
            
            if (passwordBD is null)
                return null;

            if (passwordBD.Equals(objUser.password_user))
                return GetUserInfo(objUser.email_user);
            else
                return null;
        }

        /// <summary>
        /// Consultar as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public User GetUserInfo(string email_user)
        {
            return new UserDAL(_configuration).GetUserInfo(email_user);
        }

        /// <summary>
        /// Inserir as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool PostUser(User objUser)
        {
            objUser.password_user = new Comum().EncriptyUserPassword(objUser.password_user);
            objUser.email_user = objUser.email_user.Trim();
            if (String.IsNullOrEmpty(objUser.profile_image))
                objUser.profile_image = " ";
            return new UserDAL(_configuration).PostUser(objUser);
        }

        /// <summary>
        /// Atualizar as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool PutUser(User objUser)
        {
            objUser.password_user = new Comum().EncriptyUserPassword(objUser.password_user);
            objUser.email_user = objUser.email_user.Trim();
            if (String.IsNullOrEmpty(objUser.profile_image))
                objUser.profile_image = " ";
            return new UserDAL(_configuration).PutUser(objUser);
        }
    }
}
