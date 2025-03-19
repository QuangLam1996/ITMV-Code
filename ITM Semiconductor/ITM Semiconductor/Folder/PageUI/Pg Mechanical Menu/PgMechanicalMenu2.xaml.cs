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
    /// Interaction logic for PgMechanicalMenu2.xaml
    /// </summary>
    public partial class PgMechanicalMenu2 : Page
    {
        public PgMechanicalMenu2()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;


            this.btnSettingDevice.Click += BtnSettingDevice_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnOpen.Click += BtnOpen_Click;
            this.btnClose.Click += BtnClose_Click;

            this.btnReadBit.Click += BtnReadBit_Click;
        }

        private void BtnReadBit_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateLogs("Đã nhấn close");
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            this.UpdateLogs("Đã nhấn Open");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn lưu lại cài đặt","Thông báo" ,MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if(result == MessageBoxResult.OK)
            {
                UiManager.SaveAppSetting();
            }    
        }

        private void BtnSettingDevice_Click(object sender, RoutedEventArgs e)
        {
            WndPLCComSetting wndPLCComSetting = new WndPLCComSetting();
            wndPLCComSetting.ShowDialog();
        }

        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_MENU3);
            UiManager.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU3);
        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_MENU2);
            UiManager.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU4);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_MENU1);
            UiManager.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU1);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_MENU);
            UiManager.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU);
        }
        private void UpdateLogs(string notify)
        {
            this.Dispatcher.Invoke(() => {
                this.txtLogs.Text += "\r\n" + notify;
                this.txtLogs.ScrollToEnd();
            });
        }
    }
}