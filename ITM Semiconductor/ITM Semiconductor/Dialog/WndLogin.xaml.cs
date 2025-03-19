using System;
using System.Collections.Generic;
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
using KeyPad;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndLogin.xaml
    /// </summary>
    public partial class WndLogin : Window
    {

        private int isLogonSuccess = 0;
        public WndLogin()
        {
            InitializeComponent();
            this.btcancel.Click += Btcancel_Click;
            this.btChangePassword.Click += BtChangePassword_Click;
            this.btAutoteam.Click += BtAutoteam_Click;
            this.btManager.Click += BtManager_Click;
            this.btSignin.Click += BtSignin_Click;
            this.btOperator.Click += BtOperator_Click;
            this.Topmost = true;
            this.Loaded += WndLogin_Loaded;
           
            this.txtPassword.TouchDown += TxtPassword_TouchDown;
            this.txtPassword.PreviewMouseDown += TxtPassword_PreviewMouseDown;
        }

        private void TxtPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            //PasswordBox textbox = sender as PasswordBox;
            //VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            //if (keyboardWindow.ShowDialog() == true)
            //    textbox.Password = keyboardWindow.Result;
        }

        private void TxtPassword_TouchDown(object sender, TouchEventArgs e)
        {
            PasswordBox textbox = sender as PasswordBox;
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Password = keyboardWindow.Result;
        }

        // Đặt Topmost của cửa sổ là true tại một thời điểm sau khi cửa sổ đã được tạo
        private void SetWindowTopMost()
        {
            //this.Topmost = true;
        }

        private void WndLogin_Loaded(object sender, RoutedEventArgs e)
        {
           this.SetWindowTopMost();
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.Login();
            }
        }

        private void BtSignin_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGIN_BUTTON_SIGNIN);
            this.Login();
        }

        private void BtManager_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGIN_BUTTON_MANAGER);
            this.textId.Text = "Manager";
        }

        private void BtAutoteam_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGIN_BUTTON_AUTOTEAMS);
            this.textId.Text = "AutoTeams";
        }
        private void BtOperator_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGIN_BUTTON_OPERATOR);
            this.textId.Text = "Operator";
            this.Login();
            this.Close();
        }
        private void Login()
        {
            // Lấy mục được chọn
            string usename = this.textId.Text;
            UserManager.createUserLog(UserActions.LOGON_BUTTON_ENTER);
            UiManager.appSettings.UseName = usename;
            isLogonSuccess = UserManager.LogOn(UiManager.appSettings.UseName, this.txtPassword.Password);
            if (isLogonSuccess == 0)
            {
                MessageBox.Show("Wrong Password!");
            }
            else
            {
                this.Close();
            }
        }
        private void BtChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGON_BUTTON_CHANGE_PASSWORD);
            WndChangePass wndChangePass = new WndChangePass();
            wndChangePass.ShowDialog();
            this.Close();
        }

        private void txtpassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.txtPassword.Focus();
            //this.textPassword.Text = string.Empty;



        }
        private void txtPs_Changed (object sender , RoutedEventArgs e)
        {
            //this.textPassword.Text = string.Empty;

        }
        private void textId_MouseDown (object sender, MouseButtonEventArgs e)
        {
           this.textId.Focus();
           
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        private void Btcancel_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGON_BUTTON_CANCEL);
            this.Close();
            this.Close();
        }

        private void Image_Mouseup(object sender, MouseButtonEventArgs e)
        {       
           this.Close();     
        }
    }
}
