using System.Windows;
using System.Windows.Controls;
using Fancs.ControlWindows;

namespace Fancs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button btn)) return;
            
            
            if (btn.Name == BtnBx.Name)
            {
                var window = new BxWindow();
                window.Show();
            }
        }
    }
}