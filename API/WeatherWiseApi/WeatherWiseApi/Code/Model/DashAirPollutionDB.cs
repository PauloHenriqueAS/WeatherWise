namespace WeatherWiseApi.Code.Model
{
    /// <summary>
    /// Poluição do Ar
    /// </summary>
    public class DashAirPollutionDB
    {
        /// <summary>
        /// Carbon monoxide (CO)
        /// </summary>
        public double co { get; set; }

        /// <summary>
        /// Nitrogen monoxide (NO)
        /// </summary>
        public double no { get; set; }

        /// <summary>
        /// Nitrogen dioxide (NO2)
        /// </summary>
        public double no2 { get; set; }

        /// <summary>
        /// Ozone (O3)
        /// </summary>
        public double o3 { get; set; }

        /// <summary>
        /// Sulphur dioxide (SO2)
        /// </summary>
        public double so2 { get; set; }

        /// <summary>
        /// PM2.5 (Partículas Finas)
        /// </summary>
        public double pm2_5 { get; set; }

        /// <summary>
        /// PM10 (Partículas Finas)
        /// </summary>
        public double pm10 { get; set; }

        /// <summary>
        /// Ammonia (NH3)
        /// </summary>
        public double nh3 { get; set; }

        public DashAirPollutionDB() { }
        public DashAirPollutionDB(DashAirPollutionDB dash, double Total)
        {
            co = Math.Round((dash.co / Total * 100), 2);
            no = Math.Round((dash.no / Total * 100), 2);
            no2 = Math.Round((dash.no2 / Total * 100), 2);
            o3 = Math.Round((dash.o3 / Total * 100), 2);
            so2 = Math.Round((dash.so2 / Total * 100), 2);
            pm2_5 = Math.Round((dash.pm2_5 / Total * 100), 2);
            pm10 = Math.Round((dash.pm10 / Total * 100), 2);
            nh3 = Math.Round((dash.nh3 / Total * 100), 2);
        }
    }

    public class DashAirPollutionRequest
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public DateTime date_query { get; set; }
    }
}
