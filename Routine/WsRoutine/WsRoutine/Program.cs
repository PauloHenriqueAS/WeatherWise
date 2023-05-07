using Microsoft.Extensions.Configuration;
using System.Globalization;
using WsRoutine.Code.BLL;
using WsRoutine.Helpers;

namespace WsRoutine
{

    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            try
            {
                Log.InicioProcesso();

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
