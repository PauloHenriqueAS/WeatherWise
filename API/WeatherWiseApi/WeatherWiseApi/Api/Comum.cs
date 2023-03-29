using System.Reflection;

namespace WeatherWiseApi.Api
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
            PropertyInfo LongProperty = typeof(T).GetProperty("Long");

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
    }
}
