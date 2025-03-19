using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndAutoUpdate.xaml
    /// </summary>
    public partial class WndAutoUpdate : Window
    {
        public WndAutoUpdate()
        {
            InitializeComponent();
            this.btnNo.Click += BtnNo_Click;
            this.btnYes.Click += BtnYes_Click;
        }
        public void MessengerShow(string message, Window owner = null)
        {
            this.Owner = owner;
            this.lblMessage.Content = message;

            this.ShowDialog();
        }
        private void BtnYes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string itmPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdate.exe");
                if (System.IO.File.Exists(itmPath))
                {
                    Process.Start(itmPath);
                    UiManager.SaveAppSetting();
                }
                else
                {
                    MessageBox.Show("Không tìm thấy AutoUpdate.exe trong thư mục ứng dụng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở ứng dụng ITM: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            UiManager.Instance.wndMain.MainWindow_Closed(this, null);
        }

        private void BtnNo_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
