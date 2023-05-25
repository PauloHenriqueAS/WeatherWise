namespace WeatherWiseApi.Code.Model;

public class Alert
{
    /// <summary>
    /// Email do usuário que cria o alerta
    /// </summary>
    public string email_user { get; set; }

    /// <summary>
    /// Velocidade do tempo
    /// </summary>
    public double? wind_speed { get; set; } = 0;

    /// <summary>
    /// Visibilidade
    /// </summary>
    public double? visibility { get; set; } = 0;

    /// <summary>
    /// Indice de boluição do ar
    /// </summary>
    public int? air_pollution_aqi { get; set; } = 0;

    /// <summary>
    /// Precipitação
    /// </summary>
    public double? preciptation { get; set; } = 0;
}