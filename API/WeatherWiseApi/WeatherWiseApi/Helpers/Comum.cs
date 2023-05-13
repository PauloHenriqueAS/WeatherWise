using System.Security.Cryptography;
using WeatherWiseApi.Code.Model;
using System.Reflection;
using System.Text;

namespace WeatherWiseApi.Helpers
{
    /// <summary>
    /// Objeto Comum para Métodos Genéricos e Globais
    /// </summary>
    public class Comum
    {
        /// <summary>
        /// Método para a Validação das Coordenadas recebidas pela API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objJason"></param>
        /// <returns></returns>
        public bool ValidateObjCoordenate<T>(T objJason)
        {
            PropertyInfo LatProperty = typeof(T).GetProperty("Lat");
            PropertyInfo LongProperty = typeof(T).GetProperty("Lon");

            if (objJason != null)
            {
                double latitude = (double)LatProperty.GetValue(objJason);
                double longitude = (double)LongProperty.GetValue(objJason);

                if (latitude == 0 || longitude == 0)
                {
                    return false;
                }
                else { return true; }
            }
            else { return false; }
        }

        /// <summary>
        /// Método para a Validação do objeto de poluição do ar
        /// </summary>
        /// <param name="objAirPollution"></param>
        /// <returns></returns>
        public bool ValidateObjAirPollution(AirPollution objAirPollution)
        {
            if (ValidateObjCoordenate(objAirPollution.coord))
            {
                if (objAirPollution != null && objAirPollution.list.Count > 0 && objAirPollution.list != null)
                {
                    if (objAirPollution.list.FirstOrDefault().main == null || objAirPollution.list.FirstOrDefault().components == null)
                    {
                        return false;
                    }
                    else { return true; }
                }
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// Método para a Validação do objeto de tempo atual
        /// </summary>
        /// <param name="objCurrentWeather"></param>
        /// <returns></returns>
        public bool ValidateObjCurrentWeather(CurrentWeather objCurrentWeather)
        {
            if (objCurrentWeather != null)
            {
                if (ValidateObjCoordenate(objCurrentWeather.coord))
                {
                    if (objCurrentWeather.weather.FirstOrDefault() == null ||
                        objCurrentWeather.main == null || objCurrentWeather.wind == null ||
                        objCurrentWeather.clouds == null || objCurrentWeather.sys == null)
                    {
                        return false;
                    }
                    else { return true; }
                }
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// Método para a Validação do objeto de tempo futuro
        /// </summary>
        /// <param name="objForecast"></param>
        /// <returns></returns>
        public bool ValidateObjForecastWeather(Forecast objForecast)
        {
            if (objForecast != null)
            {
                if (objForecast.list.FirstOrDefault() == null || objForecast.city == null)
                {
                    return false;
                }
                else { return true; }
            }
            else { return false; }
        }

        /// <summary>
        /// Método para validar informações do usuário
        /// </summary>
        /// <param name="objUser"></param>
        /// <returns></returns>
        public bool ValidateObjUser(User objUser)
        {
            if (objUser != null)
            {
                if (String.IsNullOrEmpty(objUser.name_user) || String.IsNullOrEmpty(objUser.password_user) || String.IsNullOrEmpty(objUser.email_user) || objUser.type_user == null)  
                {
                    return false;
                }
                else { return true; }
            }
            else { return false; }
        }

        /// <summary>
        /// Método para validar informações do alerta a ser criado
        /// </summary>
        /// <param name="objAlert"></param>
        /// <returns></returns>
        public bool ValidateObjAlert(Alert objAlert)
        {
            if (objAlert != null)
            {
                if (!String.IsNullOrEmpty(objAlert.email_user))
                {
                    if (objAlert.air_pollution_aqi == 0 && objAlert.precipitation == 0 && objAlert.wind_speed == 0 && objAlert.visibility == 0)
                    {
                        return false;
                    }
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }

        /// <summary>
        /// Método para a encriptar a senha so usuário
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncriptyUserPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                return sb.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Valida objeto de localidade favorita do usuário
        /// </summary>
        /// <param name="objHistoricCoordenatesUser"></param>
        /// <returns></returns>
        public bool ValidateObjHistoricUser(HistoricCoordenatesUser objHistoricCoordenatesUser)
        {
            if (objHistoricCoordenatesUser != null)
            {
                if (!String.IsNullOrEmpty(objHistoricCoordenatesUser.email_user))
                {
                    if (!ValidateObjCoordenate(objHistoricCoordenatesUser.coordenate))
                    {
                        return false;
                    }
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
    }
}
