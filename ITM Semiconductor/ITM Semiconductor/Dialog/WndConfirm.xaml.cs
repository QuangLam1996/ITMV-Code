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
    /// Interaction logic for WndConfirm.xaml
    /// </summary>
    public partial class WndConfirm : Window
    {

        private Boolean isConfirmYes = false;
        public WndConfirm()
        {
            InitializeComponent();
            this.btnYes.Click += BtYes_Click;
            this.btnNo.Click += BtNo_Click;
        }

        private void BtNo_Click(object sender, RoutedEventArgs e)
        {
            this.isConfirmYes = false;
            this.Close();
        }

        private void BtYes_Click(object sender, RoutedEventArgs e)
        {
            this.isConfirmYes = true;
            this.Close();
        }

        public bool DoComfirmYesNo(string message, Window owner = null)
        {
            this.Owner = owner;
            this.lblMessage.Content = message;

            this.ShowDialog();
            return isConfirmYes;
        }

      
    }
}

