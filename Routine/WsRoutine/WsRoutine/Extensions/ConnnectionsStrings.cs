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

        public static string GetApiKey(IConfiguration configuration, string ApiName)
        {
            ConfigPath = configuration.GetSection("ConfigPath").Value;
            ConfigName = configuration.GetSection("ConfigName").Value;

            IConfiguration configApi = new ConfigurationBuilder()
                                .SetBasePath(ConfigPath)
                                .AddJsonFile(ConfigName, optional: true, reloadOnChange: true)
                                .Build();

            return ConfigurationExtensions.GetConnectionString(configApi, $"{ApiName}Key");
        }

        public static string GetDatabaseConnectionString(IConfiguration configuration)
        {
            ConfigPath = configuration.GetSection("ConfigPath").Value;
            ConfigName = configuration.GetSection("ConfigName").Value;

            IConfiguration configDatabase = new ConfigurationBuilder()
                                .SetBasePath(ConfigPath)
                                .AddJsonFile(ConfigName, optional: true, reloadOnChange: true)
                                .Build();

            return ConfigurationExtensions.GetConnectionString(configDatabase, "Database");
        }
    }
}
