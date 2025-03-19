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
    /// Interaction logic for WindowPopUp3.xaml
    /// </summary>
    public partial class WindowPopUp3 : Window
    {
        private Boolean isConfirmYes = false;
        public WindowPopUp3()
        {
            InitializeComponent();
            this.btYes.Click += this.BtYes_Click;
            this.btNo.Click += this.BtNo_Click;
            this.btOne.Click += this.BtOne_Click;
        }

        private void BtOne_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.CONFIRM_BUTTON_YES);

            isConfirmYes = true;
            this.Close();
        }

        private void BtNo_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.CONFIRM_BUTTON_NO);

            isConfirmYes = false;
            this.Close();
        }

        private void BtYes_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.CONFIRM_BUTTON_YES);

            isConfirmYes = true;
            this.Close();
        }

        public Boolean DoConfirmYesNo(String msg, Window owner = null)
        {
            UserManager.createUserLog(UserActions.CONFIRM_SHOW_YESNO);

            this.Owner = owner;
            this.lblMessage.Content = msg;
            this.btOne.Visibility = Visibility.Hidden;

            this.ShowDialog();
            return isConfirmYes;
        }

        public Boolean DoConfirmYes(String msg, Window owner = null)
        {
            UserManager.createUserLog(UserActions.CONFIRM_SHOW_YES);

            this.Owner = owner;
            this.lblMessage.Content = msg;
            this.btYes.Visibility = Visibility.Hidden;
            this.btNo.Visibility = Visibility.Hidden;
            this.btOne.Visibility = Visibility.Visible;
            this.ShowDialog();
            return isConfirmYes;
        }
    }
}
