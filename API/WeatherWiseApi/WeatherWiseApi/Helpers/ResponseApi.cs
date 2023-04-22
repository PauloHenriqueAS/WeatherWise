namespace WeatherWiseApi.Helpers;

public class ResponseApi
{
    public object? data { get; set; }
    public bool success { get; set; }

    public string? message { get; set; }

    public ResponseApi(object? data, bool success, string? message)
    {
        this.data = data;
        this.success = success;
        this.message = message;
    }
}
