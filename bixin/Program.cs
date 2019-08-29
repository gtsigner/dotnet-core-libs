using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using bx_core;
using bx_core.Model;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using rvcore.Http;

namespace bixin
{
    class Program
    {
        private static IServiceProvider _myServiceProvider;
        private static IHttpClientFactory _factory;
        private static IMongoDatabase _database;

        public static void Main(string[] args)
        {
            _myServiceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            _factory = (IHttpClientFactory) _myServiceProvider.GetService(typeof(IHttpClientFactory));

            //數據庫
            var client = new MongoClient("mongodb://192.168.10.167:27017"); //CLIENT
            _database = client.GetDatabase("bixin");

            Start().ContinueWith((res) => { }).Wait(); //爲啥？？？
            Console.ReadLine();
            Console.ReadLine();
        }

        private static int[] _types = {0, 1, 2, 3, 4, 5, 6, 6, 7, 8, 9, 10, 12, 13, 14, 15, 16, 18, 19, 20, 22, 23, 24};

        /// <summary>
        /// 开始
        /// </summary>
        private static async Task Start()
        {
            var account = new BxApi(_factory)
            {
                Account = "17311301741",
                Password = "zhaojunlike",
                Udid = "c64a11a9c335422790085bf5b3efba0f"
            };
            //account.AccessToken = token;
            var roomIds = new List<string>();
            Console.WriteLine("進程結束");


            for (int i = 0; i < _types.Length; i++)
            {
                //這個沒有進行授權的檢測             //13 14 19 22 12
                var res = await account.ChatRooms(0, 500, _types[i]);
                var list = (res.Data as JObject)?.GetValue("result").ToObject<JObject>().GetValue("normalRoom")
                    .ToObject<JObject>()
                    .GetValue("contentList").ToObject<JArray>();
                foreach (var jt in list)
                {
                    var room = jt.ToObject<BxRoom>();
                    roomIds.Add(room.roomId);
                }
            }


            var config = new BxConfig {Keywords = roomIds, ThreadCount = 2};

            //采集
            await account.PassWordLogin();
            var spider = new BxRoomSpider(account);
            spider.OnRecvUser += OnRecvUser;
            spider.OnFinish += OnFinish;
            spider.Run(config);
        }

        private static void OnFinish(object data)
        {
            Console.WriteLine("系統完成");
        }

        /// <summary>
        /// 收集到用戶
        /// </summary>
        /// <param name="data"></param>
        private static void OnRecvUser(object data)
        {
            var user = data as BxUser;
            if (user == null) return;
            //获取用户的详情
            Console.WriteLine($"采集到新用户：{user.nickname} 性别:{user.gender} 年龄:{user.age}");
            var where = Builders<BxUser>.Filter.Eq("userId", user.userId);
            var update = Builders<BxUser>.Update.SetOnInsert("userId", user.userId).Set("uid", user.uid)
                .Set("yppNo", user.yppNo).Set("avatar", user.avatar)
                .Set("nickname", user.nickname)
                .Set("age", user.age)
                .Set("diamondVipName", user.diamondVipName)
                .Set("gender", user.gender)
                .Set("birthday", user.birthday)
                .Set("isFollow", user.isFollow)
                .Set("accId", user.accId)
                .Set("fansCount", user.fansCount)
                .Set("isAuth", user.isAuth)
                .Set("isGod", user.isGod);
            var opt = new FindOneAndUpdateOptions<BxUser> {IsUpsert = true};
            _database.GetCollection<BxUser>("users").FindOneAndUpdateAsync(where, update, opt);
        }
    }
}