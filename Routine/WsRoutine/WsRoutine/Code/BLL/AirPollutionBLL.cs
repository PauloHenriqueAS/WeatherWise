﻿using Microsoft.Extensions.Configuration;
using WsRoutine.Code.Model;
using System.Transactions;
using WsRoutine.Code.DAL;
using WsRoutine.Helpers;
using WsRoutine.Api;

namespace WsRoutine.Code.BLL
{
    /// <summary>
    /// Camada de Regras de Negócio da Poluição do Ar
    /// </summary>
    public class AirPollutionBLL
    {
        /// <summary>
        /// Consultar informações da poluição do ar
        /// </summary>
        /// <param name="coordinate"></param>
        /// <returns></returns>
        public AirPollution GetAirPollution(Coordinate coordinate)
        {
            var result = new OpenWeatherApi().GetAirPollution(coordinate);
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
                AirPollutionDAL airDal = new AirPollutionDAL();
                AirPollutionDB airPollutionDB = new AirPollutionDB();
                ComumDAL comumDal = new ComumDAL();

                retornoObj.obj = airPollution;
                airPollutionDB.data_save = DateTime.Now;
                airPollutionDB.dt = airPollution.dt;
                airPollutionDB.aqi = airPollution.aqi;

                using (TransactionScope scope = new TransactionScope())
                {
                    airPollutionDB.id_coordenate = comumDal.PostCoordenate(airPollution.coord);

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
