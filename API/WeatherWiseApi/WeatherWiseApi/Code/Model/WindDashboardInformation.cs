namespace WeatherWiseApi.Code.Model;

public class WindDashboardInformation
{
    public List<DateTime> GeneralLabels { get; set; }
    public string Label { get; set; }
    public List<double> Data { get; set; }
    public string BorderColor { get; set; }
    public string BackgroundColor { get; set; }

    public WindDashboardInformation(List<WindStatistics> windStatistics)
    {
        var region = windStatistics.FirstOrDefault()!.Region;

        this.GeneralLabels = windStatistics.Select(x => x.DateWeather).ToList();
        this.Label = region;
        this.Data = windStatistics.Select(x => x.Speed).ToList();
        this.BorderColor = GetHexColorByRegion(region);
        this.BackgroundColor = GetHexColorByRegion(region);
    }

    public string GetHexColorByRegion(string region)
    {
        switch (region)
        {
            case "Região Norte":
                return "#9ebcda";
            case "Região Sul":
                return "#8856a7";
            case "Região Leste":
                return "#fdbb84";
            case "Região Oeste":
                return "#99d8c9";
            case "Centro (Sérgio Pacheco)":
                return "#a8ddb5";
            default:
                return "#F1F1F1";
        }
    }
    //label: 'Norte',
    //data: [7.2, 3.1, 2.2, 5.7, 1.7, 3.5],
    //fill: false,
    //borderColor: '#9ebcda',
    //backgroundColor: '#9ebcda',
}
