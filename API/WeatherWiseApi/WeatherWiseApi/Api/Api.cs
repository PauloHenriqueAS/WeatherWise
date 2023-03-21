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
    /// REQUEST
    /// </summary>
    public RestRequest? REQUEST { get; set; }

    public Api(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public T Post<T>(string uri, object body, Dictionary<string, string> headers)
    {
        this.URL = this.HOST + uri;
        this.CLIENT = new RestClient(this.URL);
        this.REQUEST = new RestRequest("", Method.Post)
        {
            RequestFormat = DataFormat.Json,
            Timeout = int.MaxValue
        };

        this.REQUEST.AddHeader("cache-control", "no-cache");
        this.REQUEST.AddHeader("Content-Type", "application/json");

        if (headers != null)
            foreach (var header in headers)
                this.REQUEST.AddHeader(header.Key, header.Value);

        var dataJson = JsonConvert.SerializeObject(body);

        if (body != null)
            this.REQUEST.AddParameter("application/json", dataJson, ParameterType.RequestBody);

        var resultData = this.CLIENT.Execute<T>(this.REQUEST);

        return JsonConvert.DeserializeObject<T>(resultData.Content!)!;
    }
}