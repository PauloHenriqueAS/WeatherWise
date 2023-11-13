using WeatherWiseApi.Constants;

namespace WeatherWiseApi.Extensions
{
    public static class ConnnectionsStrings
    {
        /// <summary>
        /// PATH CONFIGURATION FILE
        /// </summary>
        public static string ConfigPath { get; set; }

        /// <summary>
        /// CONFIGURATION FILE NAME
        /// </summary>
        public static string ConfigName { get; set; }

        public static string GetApiKey(IConfiguration configuration, string ApiName)
        {
            var ConfigPath = ConfigurationVariables.configPath;
            var ConfigName = ConfigurationVariables.configName;

            IConfiguration configApi = new ConfigurationBuilder()
                                .SetBasePath(ConfigPath)
                                .AddJsonFile(ConfigName, optional: true, reloadOnChange: true)
                                .Build();

            return ConfigurationExtensions.GetConnectionString(configApi, $"{ApiName}Key");
        }

        public static string GetDatabaseConnectionString(IConfiguration configuration)  
        {
            var ConfigPath = ConfigurationVariables.configPath;
            var ConfigName = ConfigurationVariables.configName;

            IConfiguration configDatabase = new ConfigurationBuilder()
                                .SetBasePath(ConfigPath)
                                .AddJsonFile(ConfigName, optional: true, reloadOnChange: true)
                                .Build();

            return ConfigurationExtensions.GetConnectionString(configDatabase, "Database");
        }
    }
}
