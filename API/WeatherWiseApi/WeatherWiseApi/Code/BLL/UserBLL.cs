using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Code.DAL;
using WeatherWiseApi.Helpers;
using System.Transactions;

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

        #region AUTH

        /// <summary>
        /// Validar se a senha enviada é igual a senha inserida no banco para realizar a autorização do usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public User AuthorizeUser(UserCredentials objUser)
        {
            objUser.password_user = Comum.EncriptyUserPassword(objUser.password_user);
            string passwordBD = new UserDAL(_configuration).GetPasswordUser(objUser);

            if (passwordBD is null)
                return null;

            if (passwordBD.Equals(objUser.password_user))
                return GetUserInfo(objUser.email_user);
            else
                return null;
        }
        #endregion

        #region GET

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
        /// Consulta de lista de coordenadas salvas pelo usuário
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public List<Coordinate> GetHistoricCoordenatesUserInfo(string emailUser)
        {
            int idUser = GetUserInfo(emailUser).id_user;
            if (idUser >= 0)
                return new UserDAL(_configuration).GetHistoricCoordenatesUserInfo(idUser);
            else
                return null;
        }

        #endregion

        #region POST
        /// <summary>
        /// Inserir as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool PostUser(User objUser)
        {
            objUser.password_user = Comum.EncriptyUserPassword(objUser.password_user);
            objUser.email_user = objUser.email_user.Trim();
            if (String.IsNullOrEmpty(objUser.profile_image))
                objUser.profile_image = " ";
            return new UserDAL(_configuration).PostUser(objUser);
        }

        /// <summary>
        /// Inserir as localidades favoritas do Usuário
        /// </summary>
        /// <param name = "objHistoricCoordenatesUser" ></ param >
        /// < returns ></ returns >
        public bool PostHistoricCoordenatesUser(HistoricCoordenatesUser objHistoricCoordenatesUser)
        {
            ComumDAL comumDal = new ComumDAL(_configuration);
            var histCoordUser = new HistoricCoordenatesUserDB();

            int idCord = comumDal.GetCoordenateInfo(objHistoricCoordenatesUser.coordenate);

            if (idCord == 0)
                histCoordUser.id_coordenate = comumDal.PostCoordenate(objHistoricCoordenatesUser.coordenate);
            else
                histCoordUser.id_coordenate = idCord;

            var dataUser = this.GetUserInfo(objHistoricCoordenatesUser.email_user);

            histCoordUser.id_user = dataUser.id_user;
            histCoordUser.nom_locate = objHistoricCoordenatesUser.coordenate.DisplayName;

            return new UserDAL(_configuration).PostHistoricCoordenatesUser(histCoordUser);
        }

        #endregion

        #region PUT
        /// <summary>
        /// Atualizar as informações do Usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool PutUser(User objUser)
        {
            objUser.password_user = Comum.EncriptyUserPassword(objUser.password_user);
            objUser.email_user = objUser.email_user.Trim();
            if (String.IsNullOrEmpty(objUser.profile_image))
                objUser.profile_image = " ";
            return new UserDAL(_configuration).PutUser(objUser);
        }

        /// <summary>
        /// Atualizar lista de coordenadas favoritas dos usuários
        /// </summary>
        /// <param name="objHist"></param>
        /// <returns></returns>
        public bool UpdateHistoricCoordenatesUser(HistoricCoordenatesUser objHist)
        {
            var success = false;
            using (TransactionScope scope = new TransactionScope())
            {
                var deleteHist = DeleteHistoricCoordenatesUser(objHist);
                if (deleteHist == false)
                    scope.Dispose();
                var insertHist = PostHistoricCoordenatesUser(objHist);
                if (insertHist == false)
                    scope.Dispose();

                success = true;
                scope.Complete();
            }

            return success;
        }

        #endregion

        #region DELETE
        /// <summary>
        /// Delete de localidade favorita do usuário
        /// </summary>
        /// <param name="objHist"></param>
        /// <returns></returns>
        public bool DeleteHistoricCoordenatesUser(HistoricCoordenatesUser objHist)
        {
            ComumDAL comumDal = new ComumDAL(_configuration);
            var histCoordUser = new HistoricCoordenatesUserDB();

            int idCord = comumDal.GetCoordenateInfo(objHist.coordenate);

            if (idCord == 0)
                return false;

            var dataUser = this.GetUserInfo(objHist.email_user);

            histCoordUser.id_user = dataUser.id_user;

            return new UserDAL(_configuration).DeleteHistoricCoordenatesUser(histCoordUser);
        }
        #endregion
    }
}