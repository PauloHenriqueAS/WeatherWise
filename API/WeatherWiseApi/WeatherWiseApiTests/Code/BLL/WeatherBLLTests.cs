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
using System.Transactions;

namespace WeatherWiseApi.Code.BLL.Tests
{
    [TestFixture]
    public class WeatherBLLTests
    {
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
            var alert = _weatherBLL.GetAlertByUser(email);

            Assert.That(alert, Is.TypeOf<List<Alert>>());
        }

        [TestCase("teste@gmail.com", 10.5, 20.0, 30, 5, null)]
        [TestCase("matheus@ufu.br", 15.0, 25.0, 40, 8, null)]
        public void CreateAndInsertAlert_AlertInsertedSuccessfully(string email, double windSpeed, double visibility, int airAqi, double precipitation, DateTime? desactivationDate)
        {
            bool wasInserted = true;
            var alertMock = new Alert
            {
                email_user = email,
                wind_speed = windSpeed,
                visibility = visibility,
                air_pollution_aqi = airAqi,
                preciptation = precipitation,
                desactivationDate = desactivationDate
            };

            using (TransactionScope scope = new TransactionScope())
            {
                wasInserted = _weatherBLL.InsertAlert(alertMock);

                scope.Dispose();
            }

            Assert.That(wasInserted, Is.True);
        }
    }
}