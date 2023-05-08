using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.IO;
using WsRoutine.Code.BLL;
using WsRoutine.Code.Model;
using WsRoutine.Helpers;

namespace WsRoutine
{

    public class Program
    {

        static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            try
            {
                Log.InicioProcesso();

                var configurationBuilder = new ConfigurationBuilder();
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}";

                new WsRoutineBLL().Processar();

                Log.FimProcesso();
                Environment.Exit(0);
            }
            catch(Exception e)
            {
                Log.FimProcesso(true);
                Environment.Exit(12);
            }
        }
    }
}
