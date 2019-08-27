using System.Windows;
using System.Windows.Controls;
using bx_core;
using Fancs.ControlWindows.Bx;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Fancs.ControlWindows
{
    public partial class BxWindow : Window
    {
        private BxApi _api;
        private IMongoDatabase _database;
        private readonly BxViewModel _viewModel = new BxViewModel();

        public BxWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
            _api = new BxApi(App.HttpClientFactory);
            //1.连接数据库
            var client = new MongoClient("mongodb://192.168.10.167:27017");
            _database = client.GetDatabase("bixin");
            _viewModel.LogText = "连接数据库成功";
            _database.GetCollection<BsonDocument>("users").InsertOne(new BsonDocument());
        }

        private void Login()
        {
        }

        /// <summary>
        /// 按钮登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;

            //开始
            if (btn != null && btn.Name.Equals(BtnStart.Name))
            {
            }

            //停止
            if (btn != null && btn.Name.Equals(BtnStop.Name))
            {
            }
        }
    }
}