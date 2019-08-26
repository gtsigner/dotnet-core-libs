using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using rvcore.Http;

namespace bixin
{
    class Program
    {
        public static IServiceProvider MyServiceProvider;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            MyServiceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            var clientFactory = (IHttpClientFactory) MyServiceProvider.GetService(typeof(IHttpClientFactory));
            var url = "https://baidu.com";
            var client = clientFactory.CreateClient("boc");

            Task.Run(async () =>
            {
                var req = new HttpRequestMessage {Method = HttpMethod.Get, RequestUri = new Uri(url)};
                var res = await HttpHelper.Request(client, req);
                Console.WriteLine(res);
            }).Wait();
            
        }
    }
}