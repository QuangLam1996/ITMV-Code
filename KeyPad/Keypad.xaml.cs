

/*
 * Copyright (c) 2008, Andrzej Rusztowicz (ekus.net)
* All rights reserved.

* Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.

* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

* Neither the name of ekus.net nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
/*
 * Added by Michele Cattafesta (mesta-automation.com) 29/2/2011
 * The code has been totally rewritten to create a control that can be modified more easy even without knowing the MVVM pattern.
 * If you need to check the original source code you can download it here: http://wosk.codeplex.com/
 */
using System;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Linq;

namespace KeyPad
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class Keypad : Window,INotifyPropertyChanged
    {
        #region Public Properties

        private string _result;
        public string Result
        {
            get { return _result; }
            private set { _result = value; this.OnPropertyChanged("Result"); }
        }

        #endregion
        
        public Keypad()
        {
            InitializeComponent();
            this.DataContext = this;
            Result = "";
            this.Loaded += Keypad_Loaded;
            this.PreviewTouchDown += Keypad_PreviewTouchDown; // Add touch event

        }
        private void Keypad_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            // Kiểm tra chỉ sự kiện cảm ứng, không thay đổi vị trí dựa trên con trỏ chuột
            if (Mouse.PrimaryDevice.LeftButton != MouseButtonState.Pressed)
            {
                // Đợi 10ms trước khi lấy vị trí chạm
               
                // Lấy vị trí chạm trong hệ tọa độ của cửa sổ
                Point touchPoint = e.GetTouchPoint(this).Position;

                // Chuyển đổi sang tọa độ màn hình
                Point screenPoint = this.PointToScreen(touchPoint);

                // Đặt cửa sổ dưới vị trí chạm
                PositionWindowUnderTouch(screenPoint);
            }
        }
        //private void PositionWindowUnderTouch(Point touchPoint)
        //{
        //    double screenWidth = SystemParameters.PrimaryScreenWidth;
        //    double screenHeight = SystemParameters.PrimaryScreenHeight;
        //    double windowWidth = this.Width;
        //    double windowHeight = this.Height;

        //    double newLeft = touchPoint.X;
        //    double newTop = touchPoint.Y + 20; // Offset to display under the touch point

        //    // Ensure the window does not go out of the screen bounds
        //    if (newLeft + windowWidth > screenWidth)
        //    {
        //        newLeft = (screenWidth - windowWidth) / 2; // Center horizontally if too wide
        //    }

        //    if (newTop + windowHeight > screenHeight)
        //    {
        //        newTop = (screenHeight - windowHeight) / 2; // Center vertically if too tall
        //    }

        //    this.Left = newLeft;
        //    this.Top = newTop;
        //}


        private void PositionWindowUnderTouch(Point touchPoint)
        {
            // Lấy tỷ lệ DPI của màn hình
            PresentationSource source = PresentationSource.FromVisual(this);
            double dpiX = 1.0, dpiY = 1.0;
            if (source != null)
            {
                dpiX = source.CompositionTarget.TransformToDevice.M11;
                dpiY = source.CompositionTarget.TransformToDevice.M22;
            }

            // Chuyển đổi điểm chạm sang tọa độ màn hình
            double screenX = touchPoint.X * dpiX;
            double screenY = touchPoint.Y * dpiY;

            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            double newLeft = screenX;
            double newTop = screenY + 20; // Offset để hiển thị dưới vị trí chạm

            // Đảm bảo cửa sổ không vượt ra ngoài màn hình
            if (newLeft + windowWidth > screenWidth)
            {
                newLeft = (screenWidth - windowWidth) / 2;
            }

            if (newTop + windowHeight > screenHeight)
            {
                newTop = (screenHeight - windowHeight) / 2;
            }

            this.Left = newLeft;
            this.Top = newTop;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(ref POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }
        private async void Keypad_Loaded(object sender, RoutedEventArgs e)
        {
            // Chỉ đặt vị trí dưới con trỏ chuột nếu không có sự kiện cảm ứng
            if (!TouchesOver.Any())
            {
                await PositionWindowUnderCursor();
            }
          
        }
        private async Task PositionWindowUnderCursor()
        {
            POINT cursorPos = new POINT();
            if (GetCursorPos(ref cursorPos) && !TouchesOver.Any()) // Chỉ thay đổi khi không có sự kiện cảm ứng
            {
                // Đặt vị trí dưới con trỏ chuột
                double screenWidth = SystemParameters.PrimaryScreenWidth;
                double screenHeight = SystemParameters.PrimaryScreenHeight;
                double windowWidth = this.Width;
                double windowHeight = this.Height;

                double newLeft = cursorPos.X;
                double newTop = cursorPos.Y + 20;

                if (newLeft + windowWidth > screenWidth)
                {
                    newLeft = (screenWidth - windowWidth) / 2;
                }

                if (newTop + windowHeight > screenHeight)
                {
                    newTop = (screenHeight - windowHeight) / 2;
                }
                await Task.Delay(1);
                this.Left = newLeft;
                this.Top = newTop;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            
            switch (e.Key)
            {
                case Key.D0:
                case Key.NumPad0:
                    ButtonClick(button0);
                    break;
                case Key.D1:
                case Key.NumPad1:
                    ButtonClick(button1);
                    break;
                case Key.D2:
                case Key.NumPad2:
                    ButtonClick(button2);
                    break;
                case Key.D3:
                case Key.NumPad3:
                    ButtonClick(button3);
                    break;
                case Key.D4:
                case Key.NumPad4:
                    ButtonClick(button4);
                    break;
                case Key.D5:
                case Key.NumPad5:
                    ButtonClick(button5);
                    break;
                case Key.D6:
                case Key.NumPad6:
                    ButtonClick(button6);
                    break;
                case Key.D7:
                case Key.NumPad7:
                    ButtonClick(button7);
                    break;
                case Key.D8:
                case Key.NumPad8:
                    ButtonClick(button8);
                    break;
                case Key.D9:
                case Key.NumPad9:
                    ButtonClick(button9);
                    break;
                case Key.Enter:
                    ButtonClick(buttonEnter);
                    break;
                case Key.Escape:
                    ButtonClick(buttonEsc);
                    break;
                case Key.Back:
                    ButtonClick(buttonBackspace);
                    break;
                case Key.OemMinus:
                case Key.Subtract:
                    ButtonClick(buttonMinus);
                    break;
                case Key.OemPeriod:
                case Key.Decimal:
                    ButtonClick(buttonDecimal);
                    break;
            }
        }
        private void ButtonClick(Button button)
        {
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void button_TouchDown(object sender, TouchEventArgs e)
        {
            ButtonClick((Button)sender);
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
          
           try
            {
                Button button = sender as Button;
                switch (button.CommandParameter.ToString())
                {

                    case "ESC":
                        this.DialogResult = false;
                        break;

                    case "RETURN":
                        this.DialogResult = true;
                        break;

                    case "BACK":
                        if (Result.Length > 0)
                            Result = Result.Remove(Result.Length - 1);
                        break;
                    case "MINUS":
                        Result += "-";
                        break;
                    default:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Result += button.Content.ToString();
                        });
                        break;
                }
            } 
            catch (Exception) { }
               
            
           
        }    

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }


        #endregion



    }
}
