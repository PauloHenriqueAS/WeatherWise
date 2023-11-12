using WeatherWiseApi.Code.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWiseApi.Code.Model;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Moq;
using System.Configuration;
using WeatherWiseApi.Extensions;

namespace WeatherWiseApi.Code.BLL.Tests
{
    [TestFixture]
    public class WeatherBLLTests
    {
        private IConfiguration _configuration;
        private  WeatherBLL _weatherBLL;

        [SetUp]
        public void Setup() {
            IConfiguration configDatabase = InfraestructureConnectionExtensions.GetConnectionConfiguration();

            _weatherBLL = new WeatherBLL(configDatabase);
        }

        [TestCase("teste@gmail.com")]
        [TestCase("pauloH10@ufu.br")]
        [TestCase("matheus@ufu.br")]
        public void Should_ReturnAnAlertList_WhenGetByUserIsCalled(string email)
        {
            var alert = _weatherBLL.GetAlertByUser("teste@gmail.com");

            Assert.That(alert, Is.TypeOf<List<Alert>>());
        }
    }
}