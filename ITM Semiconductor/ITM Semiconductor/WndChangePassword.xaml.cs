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
    /// Interaction logic for WndChangePassword.xaml
    /// </summary>
    public partial class WndChangePassword : Window
    {
      
        private bool isSuccess = false;

        public WndChangePassword()
        {
            InitializeComponent();

            this.btOk.Click += this.BtOk_Click;
            this.btCancel.Click += this.BtCancel_Click;
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Close();
            }
        }
        public bool DoChangePassword(Window owner = null)
        {
            UserManager.createUserLog(UserActions.CHANGEPASS_SHOW);

            this.Owner = owner;
            this.ShowDialog();
            return isSuccess;
        }
        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ChangePass();
            }
        }
        private void ChangePass()
        {
            UserManager.createUserLog(UserActions.CHANGEPASS_BUTTON_OK);

            ComboBoxItem selectedItem = (ComboBoxItem)cboUserName.SelectedItem;
            string usename = selectedItem?.Content.ToString();

            var passNew = this.txtPassNew.Password;
            var passOld = this.txtPassOld.Password;
            if (String.IsNullOrEmpty(passNew) || (!passNew.Equals(this.txtConfirm.Password)) && usename != null)
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
        private void BtOk_Click(object sender, RoutedEventArgs e)
        {         
           ChangePass();
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.CHANGEPASS_BUTTON_CANCEL);

            this.Close();
        }
    }
}
