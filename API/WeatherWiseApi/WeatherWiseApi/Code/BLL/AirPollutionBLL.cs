using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Code.DAL;
using WeatherWiseApi.Helpers;
using System.Transactions;
using WeatherWiseApi.Api;

namespace WeatherWiseApi.Code.BLL
{
    /// <summary>
    /// Camada de Regras de Negócio da Poluição do Ar
    /// </summary>
    public class AirPollutionBLL
    {
        private readonly IConfiguration _configuration;

        public AirPollutionBLL(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Consultar informações da poluição do ar
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public AirPollution GetAirPollution(Coordinate coordinate)
        {
            var result = new OpenWeatherApi(_configuration).GetAirPollution(coordinate);
            result.air_pollution_description = GetAirPollutionSituationDescription(result.list.FirstOrDefault()!.main.aqi);

            return result;
        }

        public string GetAirPollutionSituationDescription(int aqi)
        {
            switch (aqi)
            {
                case 1:
                    return "Bom";
                case 2:
                    return "Normal";
                case 3:
                    return "Moderado";
                case 4:
                    return "Pobre";
                case 5:
                    return "Muito pobre";
                default:
                    return "Inexistente";
            }
        }

        /// <summary>
        /// Inserir as Informações de Poluição no Banco de Dados
        /// </summary>
        /// <param name="airPollution"></param>
        /// <returns></returns>
        public RetornoObj PostAirPollution(AirPollution airPollution)
        {
            RetornoObj retornoObj = new RetornoObj();
            try
            {
                AirPollutionDAL airDal = new AirPollutionDAL(_configuration);
                AirPollutionDB airPollutionDB = new AirPollutionDB();
                ComumDAL comumDal = new ComumDAL(_configuration);

                retornoObj.obj = airPollution;
                airPollutionDB.data_save = DateTime.Now;
                airPollutionDB.dt = airPollution.dt;
                airPollutionDB.aqi = airPollution.aqi;

                using (TransactionScope scope = new TransactionScope())
                {
                    int idCord = comumDal.GetCoordenateInfo(airPollution.coord);

                    if (idCord == 0)
                        airPollutionDB.id_coordenate = comumDal.PostCoordenate(airPollution.coord);
                    else
                        airPollutionDB.id_coordenate = idCord;

                    foreach (var item in airPollution.list)
                    {
                        airPollutionDB.id_component = comumDal.PostComponent(item.components);
                        airPollutionDB.id_main = comumDal.PostMain(item.main);
                        airDal.PostAirPollution(airPollutionDB);
                    }

                    retornoObj.StatusRetorno = true;
                    retornoObj.MensagemErro = "Objeto inserido com sucesso.";
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                retornoObj.StatusRetorno = false;
                retornoObj.MensagemErro = ex.Message;
            }

            return retornoObj;
        }
    }
}
