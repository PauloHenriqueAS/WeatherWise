using Microsoft.Extensions.Configuration;

namespace WsRoutine.Extensions
{
    public class ConnnectionsStrings
    {
        /// <summary>
        /// PATH CONFIGURATION FILE
        /// </summary>
        public static string ConfigPath { get; set; }

        /// <summary>
        /// CONFIGURATION FILE NAME
        /// </summary>
        public static string ConfigName { get; set; }

        public static string GetApiKey(string ApiName)
        {
            IConfiguration configDatabase = new ConfigurationBuilder()
                               .SetBasePath("C:\\WeatherApiConfig")
                               .AddJsonFile("weather.api.json", optional: true, reloadOnChange: true)
                               .Build();

            return ConfigurationExtensions.GetConnectionString(configDatabase, $"{ApiName}Key");
        }

        public static string GetDatabaseConnectionString()
        {
            IConfiguration configDatabase = new ConfigurationBuilder()
                                .SetBasePath("C:\\WeatherApiConfig")
                                .AddJsonFile("weather.api.json", optional: true, reloadOnChange: true)
                                .Build();

            return ConfigurationExtensions.GetConnectionString(configDatabase, "Database"); ;
        }

        public static string GetRootCOnfigProperties(string key)
        {
            var baseTop = AppDomain.CurrentDomain.BaseDirectory;
            IConfiguration configDatabase = new ConfigurationBuilder()
                                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                .Build();

            return ConfigurationExtensions.GetConnectionString(configDatabase, key);
        }
    }
}
