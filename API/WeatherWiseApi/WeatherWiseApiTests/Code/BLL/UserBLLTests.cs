using WeatherWiseApi.Code.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WeatherWiseApi.Code.Model;
using WeatherWiseApi.Extensions;
using Microsoft.Extensions.Configuration;
using Moq;

namespace WeatherWiseApi.Code.BLL.Tests
{
    [TestFixture]
    public class UserBLLTests
    {
        private UserBLL _userBLL;

        [SetUp]
        public void Setup()
        {
            IConfiguration configDatabase = InfraestructureConnectionExtensions.GetConnectionConfiguration();

            _userBLL = new UserBLL(configDatabase);
        }

        [TestCase("invalidUser", "invalidPassword")]
        [TestCase("validUser", "invalidPassword")]
        public void Should_ReturnNull_WhenUserCredentialsAreInvalid(string username, string password)
        {
            var user = new UserCredentials { email_user = username, password_user = password };

            var result = _userBLL.AuthorizeUser(user);

            Assert.That(result, Is.Null);
        }        
    }   
}