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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgManualOperation2.xaml
    /// </summary>
    public partial class PgManualOperation2 : Page
    {
        public PgManualOperation2()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
        }

        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION3);
            UiManager.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION3);
        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION2);
            UiManager.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION2);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION1);
            UiManager.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION1);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION);
            UiManager.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION);
        }
    }
}