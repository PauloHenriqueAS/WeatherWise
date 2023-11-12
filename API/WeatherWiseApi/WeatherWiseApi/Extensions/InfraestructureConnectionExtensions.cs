using WeatherWiseApi.Constants;

namespace WeatherWiseApi.Extensions;

public static class InfraestructureConnectionExtensions
{
    public static IConfiguration GetConnectionConfiguration()
    {
        var ConfigPath = ConfigurationVariables.configPath;
        var ConfigName = ConfigurationVariables.configName;

        IConfiguration configDatabase = new ConfigurationBuilder()
                            .SetBasePath(ConfigPath)
                            .AddJsonFile(ConfigName, optional: true, reloadOnChange: true)
                            .Build();

        return configDatabase;
    }
}
