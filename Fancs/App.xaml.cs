using System;
using System.Net.Http;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace Fancs
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public static IServiceProvider MyServiceProvider;
        public static IHttpClientFactory HttpClientFactory;

        App()
        {
            MyServiceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            HttpClientFactory = (IHttpClientFactory) MyServiceProvider.GetService(typeof(IHttpClientFactory));
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Console.WriteLine("Fucker ");
        }
    }
}