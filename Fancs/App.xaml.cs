using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using rvcore.Http;

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
            //软件进行授权
            Task.Run(this._initAuth).Wait(-1);
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //task线程内未处理捕获
            MessageBox.Show("捕获线程内未处理异常：" + e.Exception.Message);
            e.SetObserved(); //设置该异常已察觉（这样处理后就不会引起程序崩溃）
        }


        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            try
            {
                e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
                MessageBox.Show("捕获未处理异常:" + e.Exception.Message);
            }
            catch (Exception ex)
            {
                //此时程序出现严重异常，将强制结束退出
                MessageBox.Show("程序发生致命错误，请重启软件");
            }
        }

        /// <summary>
        /// 致命错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            StringBuilder sbEx = new StringBuilder();
            if (e.IsTerminating)
            {
                sbEx.Append("程序发生致命错误，请重启软件！\n");
            }

            sbEx.Append("捕获未处理异常：");
            if (e.ExceptionObject is Exception)
            {
                sbEx.Append(((Exception) e.ExceptionObject).Message);
            }
            else
            {
                sbEx.Append(e.ExceptionObject);
            }

            MessageBox.Show(sbEx.ToString());
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            //Task
            DispatcherUnhandledException += App_DispatcherUnhandledException;
            //Task线程内未捕获异常处理事件
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            //非UI线程未捕获异常处理事件
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        /// <summary>
        /// 检测授权
        /// </summary>
        private async void _initAuth()
        {
            var api = new SysApi(HttpClientFactory);
            var res = await api.CheckAuth();
            Console.WriteLine(res);
            if (res.Status != 200)
            {
                Dispatcher?.Invoke(() => { Current.Shutdown(); });
            }
        }
    }
}