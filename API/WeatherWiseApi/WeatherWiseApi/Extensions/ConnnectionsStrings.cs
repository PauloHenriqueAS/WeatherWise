namespace WeatherWiseApi.Extensions
{
    public static class ConnnectionsStrings
    {
        /// <summary>
        /// 
        /// </summary>
        public static string ConfigPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public static string ConfigName { get; set; }

        public static string GetApiKey(IConfiguration configuration)
        {
            ConfigPath = configuration.GetSection("ConfigPath").Value;
            ConfigName = configuration.GetSection("ConfigName").Value;

            IConfiguration configApi = new ConfigurationBuilder()
                                .SetBasePath(ConfigPath)
                                .AddJsonFile(ConfigName, optional: true, reloadOnChange: true)
                                .Build();

            var teste = configuration.GetSection();

            return "key";
        }
    }
}
