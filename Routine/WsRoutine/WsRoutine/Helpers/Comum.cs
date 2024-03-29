﻿using System.Security.Cryptography;
using WsRoutine.Code.Model;
using System.Reflection;
using System.Text;

namespace WsRoutine.Helpers
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
        /// Método para a encriptar a senha so usuário
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string EncriptyUserPassword(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            SHA256 encryptor = SHA256.Create();
            byte[] hashBytes = encryptor.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes);
        }
    }
}
