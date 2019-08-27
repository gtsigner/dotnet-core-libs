using System.Windows;

namespace Fancs
{
    public partial class AuthWindow : Window
    {
        private static AuthWindow _instance;


        public static AuthWindow GetInstance()
        {
            return _instance ?? (_instance = new AuthWindow());
        }

        public AuthWindow()
        {
            InitializeComponent();
            _init();
        }

        static AuthWindow()
        {
            _instance = null;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void _init()
        {
            this.Title = "授权器";
        }
    }
}