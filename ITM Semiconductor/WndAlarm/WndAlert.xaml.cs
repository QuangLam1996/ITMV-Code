using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndAlert.xaml
    /// </summary>
    public partial class WndAlert : Window
    {
        private static MyLogger logger = new MyLogger("WndAlert");
        private static WndAlert currentInstance;

       


        public WndAlert()
        {
            InitializeComponent();
            this.Onlywindow();
        }
       
        private void Onlywindow()
        {
            if (currentInstance != null && currentInstance != this)
            {
                CloseWindowSmoothly(currentInstance);
            }

            currentInstance = this;
        }
        private void CloseWindowSmoothly(Window windowToClose)
        {
            // Tạo một animation
            var animation = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.3));
            animation.Completed += (sender, e) =>
            {
                windowToClose.Close();
            };

            // Áp dụng animation vào cửa sổ
            windowToClose.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        public void UpdateErrorList(List<int> errorCodes)
        {
            if (errorCodes != null && errorCodes.Count == 0)
            {
                WndAlert wndAlert = new WndAlert();

                wndAlert.Close();
                return;
            }

            for (int i = 0; i < errorCodes.Count; i++)
            {
                int code = errorCodes[i];
                switch (i)
                {
                    case 0:  this.DisplayAlarm(1,  code); break;
                    case 1:  this.DisplayAlarm(2,  code); break;
                    case 2:  this.DisplayAlarm(3,  code); break;
                    case 3:  this.DisplayAlarm(4,  code); break;
                    case 4:  this.DisplayAlarm(5,  code); break;
                    case 5:  this.DisplayAlarm(6,  code); break;
                    case 6:  this.DisplayAlarm(7,  code); break;
                    case 7:  this.DisplayAlarm(8,  code); break;
                    case 8:  this.DisplayAlarm(9,  code); break;
                    case 9:  this.DisplayAlarm(10, code); break;
                    case 10: this.DisplayAlarm(11, code); break;
                    case 11: this.DisplayAlarm(12, code); break;
                    case 12: this.DisplayAlarm(13, code); break;
                    case 13: this.DisplayAlarm(14, code); break;
                    case 14: this.DisplayAlarm(15, code); break;
                    case 15: this.DisplayAlarm(16, code); break;
                    case 16: this.DisplayAlarm(17, code); break;
                    case 17: this.DisplayAlarm(18, code); break;
                    case 18: this.DisplayAlarm(19, code); break;
                    case 19: this.DisplayAlarm(20, code); break;
                    case 20: this.DisplayAlarm(21, code); break;
                    case 21: this.DisplayAlarm(22, code); break;
                    case 22: this.DisplayAlarm(23, code); break;
                    case 23: this.DisplayAlarm(24, code); break;
                    case 24: this.DisplayAlarm(25, code); break;
                    case 25: this.DisplayAlarm(26, code); break;
                    case 26: this.DisplayAlarm(27, code); break;
                    case 27: this.DisplayAlarm(28, code); break;
                    case 28: this.DisplayAlarm(29, code); break;
                    case 29: this.DisplayAlarm(30, code); break;
                    // Thêm các trường hợp khác tương ứng với số lượng lỗi cần hiển thị
                    default:
                        break;
                }
            }
           
        }
        private void DisplayAlarm(int index, int code)
        {
            try
            {
                TextBlock lbMes = (TextBlock)FindName($"lbMes{index}");
                if (lbMes != null)
                {
                    string mes = AlarmList.GetMes(code);
                    this.Dispatcher.Invoke(() => { lbMes.Text = mes; });
                }
                Label lbCode = (Label)FindName($"lbCode{index}");
                if (lbCode != null)
                {
                    string Code = code.ToString();
                    this.Dispatcher.Invoke(() => { lbCode.Content = Code ; });
                }
                TextBlock lbSolution = (TextBlock)FindName($"lbSolution{index}");
                if (lbSolution != null)
                {
                    string Solution = AlarmList.GetSolution(code);
                    this.Dispatcher.Invoke(() => { lbSolution.Text = Solution; });
                }
                Label lbTime = (Label)FindName($"lbTime{index}");
                if (lbTime != null)
                {
                    string Time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    this.Dispatcher.Invoke(() => { lbTime.Content = Time; });
                }
            }
            catch (Exception ex)
            {
                logger.Create($"DisplayAlarm : {ex.Message}", LogLevel.Error);
            }
        }
        private void Label_ButtonDown(object sender, MouseButtonEventArgs e)
        {
           this.Close();
        }
        private void Image_Mouseup(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void Image_Up(object sender, MouseButtonEventArgs e)
        {
            scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset - 150);
        }
        private void Image_Down(object sender, MouseButtonEventArgs e)
        {
            scrollView.ScrollToVerticalOffset(scrollView.VerticalOffset + 150);
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
       

    }
}
