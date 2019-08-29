using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using bx_core;
using bx_core.Model;
using Fancs.ControlWindows.Bx;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace Fancs.ControlWindows
{
    public partial class BxWindow : Window
    {
        private BxApi _api;
        private IMongoDatabase _database;
        private readonly BxViewModel _viewModel = new BxViewModel();
        private BxUserSpider _spider;

        public BxWindow()
        {
            InitializeComponent();

            DataContext = _viewModel;
            _api = new BxApi(App.HttpClientFactory);
            _spider = new BxUserSpider(_api);
            _spider.OnRecvUser += On_Bx_User;
            _spider.OnFinish += On_SpiderFinish;
            _spider.OnThreadFinish += On_ThreadFinish;
            //1.连接数据库
            var client = new MongoClient("mongodb://192.168.10.167:27017"); //CLIENT
            _database = client.GetDatabase("bixin");
            _viewModel.LogText = "连接数据库成功";
        }

        private void On_SpiderFinish(object data)
        {
            _viewModel.IsRunning = false; //停止
        }

        private void On_ThreadFinish(object data)
        {
            _viewModel.LogText = "当前线程执行任务结束";
        }

        private async void Login()
        {
            //获取账号密码
            var sirs = _viewModel.AccountStr.Split('-');
            if (sirs.Length != 2)
            {
                _viewModel.LogText = "账号或者密码格式错误";
                return;
            }

            _api.Udid = _viewModel.Udid;
            _api.Account = sirs[0];
            _api.Password = sirs[1];
            var res = await _api.PassWordLogin();
            if (res.Ok)
            {
                _viewModel.LogText = "用户登录成功=" + _viewModel.Udid;
            }
            else
            {
                _viewModel.LogText = "用户登录失败：" + res.Message;
            }
        }

        private void StartSpider()
        {
            var bxConfig = new BxConfig {ThreadCount = 5};
            var words = _viewModel.Keywords.Replace("\r", "").Split('\n');

            var keys = new List<string>();
            keys.AddRange(words);
            bxConfig.Keywords = keys;
            _spider.Run(bxConfig);
        }

        /// <summary>
        /// 开始采集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            //开始
            if (btn != null && btn.Name.Equals(BtnStart.Name))
            {
                _viewModel.Count = 0;
                _viewModel.IsRunning = true;
                StartSpider();
            }

            //停止
            if (btn != null && btn.Name.Equals(BtnStop.Name))
            {
                _viewModel.IsRunning = false;
                _spider.Stop();
            }

            //导出
            if (btn != null && btn.Name.Equals(BtnExport.Name))
            {
                ExportData();
            }

            //刷新数据总数
            if (btn != null && btn.Name.Equals(BtnScan.Name))
            {
                RefreshCount();
            }

            //登录
            if (btn != null && btn.Name.Equals(BtnLogin.Name))
            {
                Login();
            }
        }

        private async void RefreshCount()
        {
            var filter = Builders<BxUser>.Filter.Empty;
            var count = await _database.GetCollection<BxUser>("users").CountAsync(filter);
            _viewModel.Total = count;
            _viewModel.LogText = $"系统存储的总用户数量：{count}";
        }

        /// <summary>
        /// 导出用户数据
        /// </summary>
        private async void ExportData()
        {
            _viewModel.LogText = "正在导入，请勿使用其他操作";

            var collection = _database.GetCollection<BxUser>("users");
            var filter = Builders<BxUser>.Filter.Empty;
            var data = await collection.FindAsync(filter);
            var list = data.ToList();
            //file
            var fs = File.Open("account.txt", FileMode.OpenOrCreate);
            var sw = new StreamWriter(fs);
            foreach (var user in list)
            {
                sw.WriteLine($"{user.accId}_{user.gender}_{user.nickname}");
            }

            _viewModel.LogText = $"共导入文件:{list.Count}条数据";
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// bx用户信息
        /// </summary>
        /// <param name="data"></param>
        protected virtual void On_Bx_User(object data)
        {
            var user = data as BxUser;
            if (user == null) return;
            //获取用户的详情
            _viewModel.LogText = $"采集到新用户：{user.nickname} 性别:{user.gender} 年龄:{user.age}";
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
            _viewModel.Count++;
        }
    }
}