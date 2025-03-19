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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;



namespace KeyPad
{
    /// <summary>
    /// Logica di interazione per VirtualKeyboard.xaml
    /// </summary>
    public partial class VirtualKeyboard : Window, INotifyPropertyChanged
    {
        #region Public Properties

        private bool _showNumericKeyboard;
        public bool ShowNumericKeyboard
        {
            get { return _showNumericKeyboard; }
            set { _showNumericKeyboard = value; this.OnPropertyChanged("ShowNumericKeyboard"); }
        }

        private string _result;
        public string Result
        {
            get { return _result; }
            private set { _result = value; this.OnPropertyChanged("Result"); }
        }

        #endregion

        #region Constructor
       
        public VirtualKeyboard()
        {
            InitializeComponent();         
            this.DataContext = this;
            Result = "";
            this.Loaded += VirtualKeyboard_Loaded;
            this.PreviewTouchDown += VirtualKeyboard_PreviewTouchDown; // Add touch event
        }
        private void VirtualKeyboard_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            // Kiểm tra chỉ sự kiện cảm ứng, không thay đổi vị trí dựa trên con trỏ chuột
            if (Mouse.PrimaryDevice.LeftButton != MouseButtonState.Pressed)
            {
                Point touchPoint = e.GetTouchPoint(this).Position;
                Point screenPoint = this.PointToScreen(touchPoint);
                PositionWindowUnderTouch(screenPoint);
            }
        }
        private void PositionWindowUnderTouch(Point touchPoint)
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;

            double newLeft = touchPoint.X;
            double newTop = touchPoint.Y + 20; // Offset to display under the touch point

            // Ensure the window does not go out of the screen bounds
            if (newLeft + windowWidth > screenWidth)
            {
                newLeft = (screenWidth - windowWidth) / 2; // Center horizontally if too wide
            }

            if (newTop + windowHeight > screenHeight)
            {
                newTop = (screenHeight - windowHeight) / 2; // Center vertically if too tall
            }

            this.Left = newLeft;
            this.Top = newTop;
        }
        private async void VirtualKeyboard_Loaded(object sender, RoutedEventArgs e)
        {
            // Chỉ đặt vị trí dưới con trỏ chuột nếu không có sự kiện cảm ứng
            if (!TouchesOver.Any())
            {
                await PositionWindowUnderCursor();
            }
        }
        #endregion
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(ref POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
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
        #region Callbacks
        public static class Win32Helper
        {
            [DllImport("user32.dll")]
            private static extern short GetKeyState(int nKeyState);

            private const int VK_CAPITAL = 0x14; // Caps Lock key code
            private const int VK_LSHIFT = 0xA0;   // Left Shift key code
            private const int VK_RSHIFT = 0xA1;   // Right Shift key code

            public static bool IsCapsLockOn()
            {
                return (GetKeyState(VK_CAPITAL) & 0x0001) != 0;
            }
            public static bool IsLeftShiftOn()
            {
                return (GetKeyState(VK_LSHIFT) & 0x8000) != 0;
            }

            public static bool IsRightShiftOn()
            {
                return (GetKeyState(VK_RSHIFT) & 0x8000) != 0;
            }

            public static bool IsAnyShiftOn()
            {
                return IsLeftShiftOn() || IsRightShiftOn();
            }
        }
       
        private void Keyboard_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            bool isCapsLockActive = Win32Helper.IsCapsLockOn();
            string keyPressed = "";

            bool isShiftActive = Win32Helper.IsAnyShiftOn();
            // Kiểm tra trạng thái của phím Caps Lock
            if (isShiftActive)
            {
                // Xử lý các phím khi Shift đang được nhấn
                switch (e.Key)
                {
                    case Key.D1: keyPressed = "!"; break;
                    case Key.D2: keyPressed = "@"; break;
                    case Key.D3: keyPressed = "#"; break;
                    case Key.D4: keyPressed = "$"; break;
                    case Key.D5: keyPressed = "%"; break;
                    case Key.D6: keyPressed = "^"; break;
                    case Key.D7: keyPressed = "&"; break;
                    case Key.D8: keyPressed = "*"; break;
                    case Key.D9: keyPressed = "("; break;
                    case Key.D0: keyPressed = ")"; break;
                    case Key.OemMinus: keyPressed = "_"; break;
                    case Key.OemPlus: keyPressed = "+"; break;
                    case Key.OemOpenBrackets: keyPressed = "{"; break;
                    case Key.OemCloseBrackets: keyPressed = "}"; break;
                    case Key.OemPipe: keyPressed = "|"; break;
                    case Key.OemComma: keyPressed = "<"; break;
                    case Key.OemPeriod: keyPressed = ">"; break;
                    case Key.OemQuestion: keyPressed = "?"; break;
                    case Key.OemSemicolon: keyPressed = ":"; break;
                    case Key.OemQuotes: keyPressed = "\""; break;
                        // Thêm các phím khác nếu cần
                }
            }
            else
            {
                // Xử lý các phím bình thường
                switch (e.Key)
                {
                    case Key.Q: keyPressed = "Q"; break;
                    case Key.W: keyPressed = "W"; break;
                    case Key.E: keyPressed = "E"; break;
                    case Key.R: keyPressed = "R"; break;
                    case Key.T: keyPressed = "T"; break;
                    case Key.Y: keyPressed = "Y"; break;
                    case Key.U: keyPressed = "U"; break;
                    case Key.I: keyPressed = "I"; break;
                    case Key.O: keyPressed = "O"; break;
                    case Key.P: keyPressed = "P"; break;
                    case Key.A: keyPressed = "A"; break;
                    case Key.S: keyPressed = "S"; break;
                    case Key.D: keyPressed = "D"; break;
                    case Key.F: keyPressed = "F"; break;
                    case Key.G: keyPressed = "G"; break;
                    case Key.H: keyPressed = "H"; break;
                    case Key.J: keyPressed = "J"; break;
                    case Key.K: keyPressed = "K"; break;
                    case Key.L: keyPressed = "L"; break;
                    case Key.Z: keyPressed = "Z"; break;
                    case Key.X: keyPressed = "X"; break;
                    case Key.C: keyPressed = "C"; break;
                    case Key.V: keyPressed = "V"; break;
                    case Key.B: keyPressed = "B"; break;
                    case Key.N: keyPressed = "N"; break;
                    case Key.M: keyPressed = "M"; break;
                    case Key.Space: keyPressed = " "; break;

                    case Key.D1: keyPressed = "1"; break;
                    case Key.D2: keyPressed = "2"; break;
                    case Key.D3: keyPressed = "3"; break;
                    case Key.D4: keyPressed = "4"; break;
                    case Key.D5: keyPressed = "5"; break;
                    case Key.D6: keyPressed = "6"; break;
                    case Key.D7: keyPressed = "7"; break;
                    case Key.D8: keyPressed = "8"; break;
                    case Key.D9: keyPressed = "9"; break;
                    case Key.D0: keyPressed = "0"; break;

                    case Key.OemMinus: keyPressed = "-"; break;
                    case Key.OemPlus: keyPressed = "="; break;
                    case Key.OemOpenBrackets: keyPressed = "["; break;
                    case Key.OemCloseBrackets: keyPressed = "]"; break;
                    case Key.OemPipe: keyPressed = "\\"; break;
                    case Key.OemComma: keyPressed = ","; break;
                    case Key.OemPeriod: keyPressed = "."; break;
                    case Key.OemQuestion: keyPressed = "/"; break;
                    case Key.OemSemicolon: keyPressed = ";"; break;
                    case Key.OemQuotes: keyPressed = "\'"; break;

                    case Key.CapsLock:
                        isCapsLockActive = !isCapsLockActive;
                        e.Handled = true;
                        return;
                    case Key.Enter:
                        this.DialogResult = true;
                        break;
                    case Key.Back:
                        if (Result.Length > 0)
                            Result = Result.Remove(Result.Length - 1);
                        break;
                    case Key.Escape:
                        this.DialogResult = false;
                        break;
                }
            }

            // Thay đổi keyPressed nếu Caps Lock đang hoạt động
            if (isCapsLockActive)
            {
                keyPressed = keyPressed.ToUpper();
            }
            else
            {
                keyPressed = keyPressed.ToLower();
            }

            if (!string.IsNullOrEmpty(keyPressed))
            {
                // Xử lý việc nhấn phím
                Result += keyPressed;
            }

            e.Handled = true; // Ngăn chặn xử lý mặc định
        }

        private void ButtonClick(Button button)
        {
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            if (button != null)
            {

                switch (button.CommandParameter.ToString())
                {
                    case "LSHIFT":
                        Regex upperCaseRegex = new Regex("[A-Z]");
                        Regex lowerCaseRegex = new Regex("[a-z]");
                        Button btn;
                        foreach (UIElement elem in AlfaKeyboard.Children) //iterate the main grid
                        {
                            Grid grid = elem as Grid;
                            if (grid != null)
                            {
                                foreach (UIElement uiElement in grid.Children)  //iterate the single rows
                                {
                                    btn = uiElement as Button;
                                    if (btn != null) // if button contains only 1 character
                                    {
                                        if (btn.Content.ToString().Length == 1)
                                        {
                                            if (upperCaseRegex.Match(btn.Content.ToString()).Success) // if the char is a letter and uppercase
                                                btn.Content = btn.Content.ToString().ToLower();
                                            else if (lowerCaseRegex.Match(button.Content.ToString()).Success) // if the char is a letter and lower case
                                                btn.Content = btn.Content.ToString().ToUpper();
                                        }

                                    }
                                }
                            }
                        }
                        break;

                    case "ALT":
                    case "CTRL":
                        break;

                    case "RETURN":
                        this.DialogResult = true;
                        break;

                    case "BACK":
                        if (Result.Length > 0)
                            Result = Result.Remove(Result.Length - 1);
                        break;

                    default:
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            Result += button.Content.ToString();
                        });

                        break;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;
      
        private void OnPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        #endregion        
    }
}
