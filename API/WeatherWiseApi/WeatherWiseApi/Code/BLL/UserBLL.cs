using WeatherWiseApi.Code.Model;
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
        public bool AuthorizeUser(User objUser)
        {
            objUser.password_user = new Comum().EncriptyUserPassword(objUser.password_user);
            string passwordBD = new UserDAL().GetPasswordUser(objUser);

            if (passwordBD.Equals(objUser.password_user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Consultar as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public User GetUserInfo(User objUser)
        {
            return new UserDAL().GetUserInfo(objUser);
        }

        /// <summary>
        /// Inserir as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool PostUser(User objUser)
        {
            return new UserDAL().PostUser(objUser);
        }
        
        /// <summary>
        /// Atualizar as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool PutUser(User objUser)
        {
            return new UserDAL().PostUser(objUser);
        }
    }
}
