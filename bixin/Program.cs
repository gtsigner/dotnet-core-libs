using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using bx_core;
using bx_core.Model;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
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

            var fs = File.Open("account.txt", FileMode.OpenOrCreate);
            var sr = new StreamWriter(fs);

            Task.Run(async () =>
            {
                var account = new BxApi(clientFactory);
                account.Account = "17311301741";
                account.Password = "zhaojunlike";
                //await account.PassWordLogin();
                account.AccessToken =
                    "V3fNw_fOx2cXwOlbOPOu0vQ5AlA6ZqWuY-3qVDMRsAIenX6l0i655EIny-mUOQ0Gyt09wYyskUZb-b_HK-0G0Ft57APMSaHYH7WfBslW2eJApTe6BY3-3O1B4fRkz_O1G-M65ZMBApdQKkkK3uBYfzEqxIlOBqpKOgIrL3xKli8";


                var arc = 0;
                while (true)
                {
                    var res = await account.YuerSearch("2", arc, 1000);
                    if (res.Ok)
                    {
                        var dt = res.Data as JObject;
                        var userList = dt.GetValue("result").ToObject<JObject>().GetValue("valueMap")
                            .ToObject<JObject>()
                            .GetValue("userList").ToObject<JObject>().GetValue("contentList").ToObject<JArray>();
                        Console.WriteLine(userList);
                        var page = dt.GetValue("result").ToObject<JObject>().GetValue("anchor").ToObject<int>();
                        Console.WriteLine(page);
                        if (userList.Count <= 0)
                        {
                            break;
                        }

                        foreach (var jToken in userList)
                        {
                            var user = jToken.ToObject<BxUser>();
                            Console.WriteLine(user);
                            sr.WriteLine(
                                $"{user.userId}_{user.yppNo}_{user.age}_{user.uid}_{user.diamondVipLevel}_{user.gender}");
                        }
                    }
                    else
                    {
                        break;
                    }

                    Thread.Sleep(1 * 1000);
                    arc++;
                }

                sr.Close();
                fs.Close();
                //1.通过关键词搜索，然后存储
            }).Wait();
        }
    }
}