using Mitsubishi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for PgMechanicalMenu4.xaml
    /// </summary>
    public partial class PgMechanicalMenu4 : Page
    {
        private Q_Enthernet PLC;
        public PgMechanicalMenu4()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.Loaded += PgMechanicalMenu4_Loaded;
            this.Unloaded += PgMechanicalMenu4_Unloaded;

       
            this.BtnSave.Click += BtnSave_Click1;

            this.btnOpen.Click += BtnOpen_Click;
            this.btnClose.Click += BtnClose_Click;
            this.btnLogClear.Click += BtnLogClear_Click;

            this.btnReadBit.Click += BtnReadBit_Click;
            this.btnReadWord.Click += BtnReadWord_Click;
            this.btnWriteWord.Click += BtnWriteWord_Click;
        }

        private void BtnSave_Click1(object sender, RoutedEventArgs e)
        {

            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want Save Setting PLC ?")) return;
            UiManager.appSettings.Setting_PLCTcp.Ip1 = this.txbIp.Text;
            UiManager.appSettings.Setting_PLCTcp.Port1 = Convert.ToInt32(this.txbPort.Text);
            UiManager.SaveAppSetting();
            MessageBox.Show("Đã lưu cài đặt thành công địa chỉ PLC");
        }

        private void BtnWriteWord_Click(object sender, RoutedEventArgs e)
        {
            if (PLC != null && PLC.isOpen() == true)
            {
                int a = Convert.ToInt32(txtAddressReadWord.Text);
                int c;
                int b = Convert.ToInt32(txtValueWriteWord.Text);
                PLC.WriteDoubleWord(DeviceCode.D, a, b);
                PLC.ReadDoubleWord(DeviceCode.D, a, out c);
                this.UpdateLogs($"Bit D {a} = {c} ");
            }
        }

        private void BtnReadWord_Click(object sender, RoutedEventArgs e)
        {
            if (PLC != null && PLC.isOpen() == true)
            {
                int a = Convert.ToInt32(txtAddressReadWord.Text);
                int b;
                PLC.ReadDoubleWord(DeviceCode.D, a, out b);

                this.UpdateLogs($"Bit D {a} = {b} ");
            }

        }

        private void BtnReadBit_Click(object sender, RoutedEventArgs e)
        {
           if(PLC != null && PLC.isOpen() == true)
            {
                int a = Convert.ToInt32(txtAddressReadBit.Text);
                bool b = false;
                PLC.ReadBit(DeviceCode.M, a, out b);
                
                this.UpdateLogs($"Bit M {a} = {b} ");
            }    
        }

        private void BtnLogClear_Click(object sender, RoutedEventArgs e)
        {
            this.txtLogs.Text = "";
        }

        private void PgMechanicalMenu4_Unloaded(object sender, RoutedEventArgs e)
        {
            if (PLC != null && PLC.isOpen() == true)
            {
                PLC.Disconnect();

            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (PLC != null && PLC.isOpen() == true)
            {
                PLC.Disconnect();
                this.UpdateLogs("Đã ngắt kết nối với PLC");

                this.btnClose.IsEnabled = false;
                this.btnReadBit.IsEnabled = false;
                this.btnReadWord.IsEnabled = false;
                //this.btnWriteBit.IsEnabled = false;
                //this.btnWriteWord.IsEnabled = false;

            }
            else
            {
                this.UpdateLogs("Chưa có kết nối với PLC. Vui lòng kiểm tra lại");
            }
           
        }

        private async void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            PLC = new Q_Enthernet(UiManager.appSettings.Setting_PLCTcp.Ip1,UiManager.appSettings.Setting_PLCTcp.Port1);
            await ConnectToPLC();
            if (PLC != null && PLC.isOpen())
            {
                this.btnClose.IsEnabled = true;
                this.btnReadBit.IsEnabled = true;
                this.btnReadWord.IsEnabled = true;
                //this.btnWriteBit.IsEnabled = true;
                //this.btnWriteWord.IsEnabled = true;
            }    
           

        }
        private async Task ConnectToPLC()
        {
            try
            {
                await Task.Run(() => PLC.Connect());
                if (PLC.isOpen())
                {
                    this.UpdateLogs("Kết nối thành công tới PLC");
                }
                else
                {
                    this.UpdateLogs("Kết nối tới PLC thất bại");
                }
            }
            catch (Exception ex)
            {
                this.UpdateLogs("Lỗi khi kết nối tới PLC: " + ex.Message);
            }
        }


        private void PgMechanicalMenu4_Loaded(object sender, RoutedEventArgs e)
        {
            this.txbPort.Text = UiManager.appSettings.Setting_PLCTcp.Port1.ToString();
            this.txbIp.Text = UiManager.appSettings.Setting_PLCTcp.Ip1.ToString();


            this.btnClose.IsEnabled = false;
            this.btnReadBit.IsEnabled = false;
            this.btnReadWord.IsEnabled = false;
            this.btnWriteBit.IsEnabled = false;
            this.btnWriteWord.IsEnabled = false;
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
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_MENU5);
            UiManager.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU5);
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
