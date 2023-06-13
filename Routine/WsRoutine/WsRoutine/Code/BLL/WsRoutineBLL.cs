using Microsoft.Extensions.Configuration;
using WsRoutine.Code.Model;
using WsRoutine.Helpers;

namespace WsRoutine.Code.BLL
{
    /// <summary>
    /// Camada de Regras de Negócio do processamento da rotina
    /// </summary>
    public class WsRoutineBLL
    {
        public void Processar()
        {
            try
            {
                Log.Salvar("INICIO DO PROCESSAMENTO");
                Log.Salvar("Consultando a lista de pontos importantes de Uberlândia.");
                List<Coordinate> listCoordenates = new Coordinate().GetListCoordenatesUberlandia();
                AirPollutionBLL airPollutionBLL = new AirPollutionBLL();
                WeatherBLL weatherBLL = new WeatherBLL();

                foreach (var itemCoordenate in listCoordenates)
                {
                    Log.Salvar($"Consultando informações e previsão do tempo para {itemCoordenate.DisplayName} na Latitude: {itemCoordenate.Lat} e Longitude: {itemCoordenate.Lon}.");
                    var curretWeather = weatherBLL.GetCurrentWeather(itemCoordenate);
                    var forecast = weatherBLL.GetForecastWeather(itemCoordenate);

                    if(curretWeather == null || forecast == null)
                    {
                        Log.Salvar("Erro na consulta das informações na API da OpenWeather.");
                        throw new Exception("Erro, por favor consultar Log");
                    }

                    Log.Salvar("Inserindo informações obtidas no banco de dados.");
                    var retornoCurrent = weatherBLL.PostCurrentWeather(curretWeather);
                    //var retornoForecast = weatherBLL.PostForecast(forecast);

                    if (retornoCurrent.StatusRetorno == false)
                        Log.Salvar($"Erro: {retornoCurrent.MensagemErro} na inserção das informações obtidas de {itemCoordenate.DisplayName} no banco de dados");
                    //if (retornoForecast.StatusRetorno == false)
                    //    Log.Salvar($"Erro: {retornoForecast.MensagemErro} na inserção das informações obtidas de {itemCoordenate.DisplayName} no banco de dados");

                    Log.Salvar($"Consultando informações sobre a poluição do ar para {itemCoordenate.DisplayName} na Latitude: {itemCoordenate.Lat} e Longitude: {itemCoordenate.Lon}.");
                    var airPollution = airPollutionBLL.GetAirPollution(itemCoordenate);

                    if (airPollution == null)
                    {
                        Log.Salvar("Erro na consulta das informações na API da OpenWeather.");
                        throw new Exception("Erro, por favor consultar Log");
                    }

                    var retornoAirPollution = airPollutionBLL.PostAirPollution(airPollution);
                    //if (retornoAirPollution.StatusRetorno == false)
                    //    Log.Salvar($"Erro: {retornoForecast.MensagemErro} na inserção das informações obtidas de {itemCoordenate.DisplayName} no banco de dados");
                }
                Log.Salvar("FIM DO PROCESSAMENTO");
            }
            catch (Exception e)
            {

            }
            //consultar os 3 endoints e inserir eles no banco de dados
        }
    }
}
