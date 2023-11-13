using WeatherWiseApi.Code.BLL;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using WeatherWiseApi.Extensions;
using Microsoft.Extensions.Configuration;
using WeatherWiseApi.Code.Model;

namespace WeatherWiseApi.Code.BLL.Tests
{
    [TestFixture]
    public class AirPollutionBLLTests
    {
        private AirPollutionBLL _airPollutionBLL;

        [SetUp]
        public void Setup()
        {
            IConfiguration configDatabase = InfraestructureConnectionExtensions.GetConnectionConfiguration();

            _airPollutionBLL = new AirPollutionBLL(configDatabase);
        }

        [TestCase(-18.8819, -48.2830, "Região Norte")]
        [TestCase(-18.9462, -48.2731, "Região Sul")]
        [TestCase(-18.9210, -48.2351, "Região Leste")]
        [TestCase(-18.9322, -48.3294, "Região Oeste")]
        [TestCase(-18.9103, -48.2757, "Centro (Sérgio Pacheco)")]
        public void Should_ReturnPositiveAirPollution_WhenGetAllMainRegionsAirPollutionDashboardIsCalled(double latitude, double longitude, string region)
        {
            AirPollution airPollution = _airPollutionBLL.GetAirPollution(new Coordinate(latitude, longitude));
            List<Components> components = airPollution.list.Select(x => x.components).ToList();
            bool thereIsNegativeAirPollution = components.Any(x => x.o3 < 0) || components.Any(x => x.nh3 < 0) || components.Any(x => x.pm10 < 0)  || components.Any(x => x.so2 < 0);

            Assert.That(thereIsNegativeAirPollution, Is.False);
        }
    }
}