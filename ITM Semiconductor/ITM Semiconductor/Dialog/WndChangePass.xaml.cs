using KeyPad;
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

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndChangePass.xaml
    /// </summary>
    public partial class WndChangePass : Window
    {
        private bool isSuccess = false;
        public WndChangePass()
        {
            InitializeComponent();
            
            this.btManager.Click += BtManager_Click;
            this.btAutoteam.Click += BtAutoteam_Click;
            this.btcancel.Click += Btcancel_Click;
            this.btChangePassword.Click += BtChangePassword_Click;
            this.Topmost = true;
            this.txtOldPassword.PreviewMouseDown += TxtOldPassword_PreviewMouseDown;
        }

        private void TxtOldPassword_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PasswordBox textbox = sender as PasswordBox;
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Password = keyboardWindow.Result;
        }

        private void BtChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePass();
        }

        public bool DoChangePassword(Window owner = null)
        {
            UserManager.createUserLog(UserActions.CHANGEPASS_SHOW);

            this.Owner = owner;
            this.ShowDialog();
            return isSuccess;
        }
        private void Btcancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Enter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChangePass();
            }
        }
        private void ChangePass()
        {
            UserManager.createUserLog(UserActions.CHANGEPASS_BUTTON_OK);

           
            string usename = this.textId.Text;

            var passNew = this.txtNewPassword.Password;
            var passOld = this.txtOldPassword.Password;
            var CfPass = this.txtCfPassword .Password;
            if (String.IsNullOrEmpty(passNew) || (!passNew.Equals(this.txtCfPassword.Password)) && usename != null)
            {
                MessageBox.Show("Please re-confirm the new password!");
            }
            else
            {

                isSuccess = UserManager.ChangePassword(usename, passOld, passNew);
               
                if (!isSuccess)
                {
                    MessageBox.Show("Password does NOT change!");
                }
                
                this.Close();
            }
        }
        private void txtNewPassword_Changed(object sender, RoutedEventArgs e)
        {
            this.textNewPassword.Text = string.Empty;
        }
        private void txtNewPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {

            this.txtNewPassword.Focus();
           

        }
        private void txtCfPassword_Changed(object sender, RoutedEventArgs e)
        {
            this.textCfPassword.Text = string.Empty;
        }
        private void txtCfPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {

            this.txtCfPassword.Focus();

        }
        private void txtOldPassword_Changed(object sender, RoutedEventArgs e)
        {
            this.textOldPassword.Text = string.Empty;
        }
        private void textOldPassword_MouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            this.txtOldPassword.Focus();
           

        }
        private void BtAutoteam_Click(object sender, RoutedEventArgs e)
        {
            this.textId.Text = "AutoTeams";
            UiManager.appSettings.UseName = this.textId.Text;
        }

        private void BtManager_Click(object sender, RoutedEventArgs e)
        {
            this.textId.Text = "Manager";
            UiManager.appSettings.UseName = this.textId.Text;
        }

        private void BtOperator_Click(object sender, RoutedEventArgs e)
        {
            this.textId.Text = "Operator";
            UiManager.appSettings.UseName = this.textId.Text;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        private void Image_Mouseup(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
