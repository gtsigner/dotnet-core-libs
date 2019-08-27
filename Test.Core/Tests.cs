using System;
using System.Net.Http;
using bx_core;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using rv_core.Utils;

namespace Test.Core
{
    [TestFixture]
    public class Tests
    {
        private static IServiceProvider MyServiceProvider;
        private IHttpClientFactory _factory;

        public Tests()
        {
            MyServiceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            _factory = (IHttpClientFactory) MyServiceProvider.GetService(typeof(IHttpClientFactory));
        }

        [Test]
        public void TestAccount()
        {
            var account = new BxApi(_factory);
            account.Account = "17311301921";
            account.Password = "zhaojunlike";
            account.PassWordLogin().Wait();
        }

        [Test]
        public void Test1()
        {
            var str = "123";
            var dec = Encoder.EncodeBase64(str);
            dec = Encoder.DecodeBase64(dec);
            LogManager.GetLogger("demo").Debug(str);
        }
    }
}