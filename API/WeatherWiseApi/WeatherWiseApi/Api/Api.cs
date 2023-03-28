using Newtonsoft.Json;
using RestSharp;

namespace WeatherWiseApi.Api;
public class Api
{
    /// <summary>
    /// CONFIGURATION INFORMATIONS
    /// </summary>
    public IConfiguration _configuration { get; set; }

    /// <summary>
    /// URL
    /// </summary>
    public string? URL { get; set; }

    /// <summary>
    /// CLIENT
    /// </summary>
    public RestClient? CLIENT { get; set; }

    /// <summary>
    /// HOST
    /// </summary>
    public string? HOST { get; set; }

    /// <summary>
    /// API KEY
    /// </summary>
    public string API_KEY { get; set; }

    /// <summary>
    /// REQUEST
    /// </summary>
    public RestRequest? REQUEST { get; set; }

    public Api(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public T Get<T>(string uri, Dictionary<string, string>? headers = null)
    {
        this.URL = this.HOST + uri;
        this.CLIENT = new RestClient(this.URL);
        this.REQUEST = new RestRequest("", Method.Get)
        {
            RequestFormat = DataFormat.Json,
            Timeout = int.MaxValue
        };

        this.REQUEST.AddHeader("cache-control", "no-cache");
        this.REQUEST.AddHeader("Content-Type", "application/json");

        if (headers != null)
            foreach (var header in headers)
                this.REQUEST.AddHeader(header.Key, header.Value);

        var resultData = this.CLIENT.Execute<T>(this.REQUEST);

        return JsonConvert.DeserializeObject<T>(resultData.Content!)!;
    }
}