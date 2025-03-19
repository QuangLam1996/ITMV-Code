using ITM_Semiconductor;
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
    /// Interaction logic for WndLogon.xaml
    /// </summary>
    public partial class WndLogon : Window
    {
        
        private int isLogonSuccess = 0;
        
        public WndLogon()
        {
            InitializeComponent();
            this.btOk.Click += this.BtOk_Click;
            this.btCancel.Click += this.BtCancel_Click;
            this.btChangePassword.Click += this.BtChangePassword_Click;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }

        public int DoLogOn(Window owner = null)
        {
            UserManager.createUserLog(UserActions.LOGON_SHOW);

            this.Owner = owner;
            this.txtPassword.Focus();
            this.ShowDialog();
            return isLogonSuccess;
        }
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }
        private void Login()
        {
            // Lấy mục được chọn
            ComboBoxItem selectedItem = (ComboBoxItem)cboUserName.SelectedItem;
            string usename = selectedItem?.Content.ToString();
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
        private void BtOk_Click(object sender, RoutedEventArgs e)
        {
           Login();
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGON_BUTTON_CANCEL);
            this.Close();
            isLogonSuccess = 0;
        }

        private void BtChangePassword_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.LOGON_BUTTON_CHANGE_PASSWORD);

            if (new WndChangePassword().DoChangePassword(this.Owner))
            {
                UserManager.createUserLog(UserActions.LOGON_CHANGE_PASS_SUCCESS);
                MessageBox.Show("Password changed!");
            }
        }
    }
}
