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
using DTO;
using KeyPad;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgMechanicalMenu5.xaml
    /// </summary>
    public partial class PgMechanicalMenu5 : Page
    {
        private MyLogger logger = new MyLogger("PG_Mechanical_ScannerTCP");
       
        public PgMechanicalMenu5()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;


            this.Loaded += PgMechanicalMenu5_Loaded;
           
            this.btnOpen.Click += BtnOpen_Click;
            this.Unloaded += PgMechanicalMenu5_Unloaded;
            this.btnReadQrCode.Click += BtnReadQrCode_Click;

            this.BtnSave.Click += BtnSave_Click;
            this.BtnUndo.Click += BtnUndo_Click;

            this.txbIp.PreviewMouseDown += PG_PreviewMouseDown;
            this.txbIp.TouchDown += PG_TouchDown;

            this.txbPort.PreviewMouseDown += PG_PreviewMouseDown;
            this.txbPort.TouchDown += PG_TouchDown;
                
           
        }

        

        private void PG_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
        }

        private void PG_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
        }

        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("Bạn chắc chắn muốn đặt lại cài đặt thông số mặc định ?")) return;
            this.txbIp.Text = "192.168.0.1";
            this.txbPort.Text = "1000";
            UiManager.SaveAppSetting();
        }

        private async void BtnReadQrCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (UiManager.Scanner.IsConnected)
                {
                    UserManager.createUserLog(UserActions.MN_MECHANICAL_BARCODE_READING_PKG);

                    // Select Bank 01:


                    // Signal User:
                    this.UpdateLogs("Reading ...");

                    await Task.Delay(10);

                    // Start reading:
                    var qr = UiManager.Scanner.ReadQR();
                    this.UpdateLogs(qr);
                }
               
            }
            catch (Exception ex)
            {
                logger.Create(("BtPkgRead_Click error:" + ex.Message),LogLevel.Error) ;
            }
        }

        private void PgMechanicalMenu5_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            //Scanner = new ScannerTCP(UiManager.appSettings.SettingScannerTCP.IpAdress1,UiManager.appSettings.SettingScannerTCP.Port1);
            if (UiManager.Scanner.Start())
            {
               
                this.UpdateLogs("Connecte SUCCESS!");
                this.btnReadQrCode.IsEnabled = true;
            }
            else
            {
                
                this.UpdateLogs("Connect FAILED!");
            }
        }

     

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want Save Setting ?")) return;
            UiManager.appSettings.SettingScannerTCP.IpAdress1 = this.txbIp.Text;
            int port;
            if (int.TryParse(this.txbPort.Text, out port))
            {
                UiManager.appSettings.SettingScannerTCP.Port1 = port;
            }
            else
            {
                // Xử lý trường hợp không thể chuyển đổi giá trị textbox sang kiểu integer
                // Ví dụ: Hiển thị một thông báo lỗi cho người dùng
                MessageBox.Show("Invalid port number. Please enter a valid integer value.");
            }
            UiManager.SaveAppSetting();
           
        }

        private void PgMechanicalMenu5_Loaded(object sender, RoutedEventArgs e)
        {
            this.txbIp.Text = UiManager.appSettings.SettingScannerTCP.IpAdress1.ToString();
            this.txbPort.Text = UiManager.appSettings.SettingScannerTCP.Port1.ToString();
            
            this.btnClose.IsEnabled = false;
            //this.btnReadQrCode.IsEnabled = false;
            this.btnTurning.IsEnabled = false;
            this.btnForcus.IsEnabled = false;
            this.cbSelectBank.IsEnabled = false;
        }

        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_MENU3);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU3);
        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_SETUP_TCP_PLC);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_SETUP_TCP_PLC);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_SETUP_TCP_SCANNER);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_SETUP_TCP_SCANNER);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_MENU);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_MENU);
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
