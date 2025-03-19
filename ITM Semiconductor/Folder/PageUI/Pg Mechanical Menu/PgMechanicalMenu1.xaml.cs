using DTO;
using KeyPad;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for PgMechanicalMenu1.xaml
    /// </summary>
    public partial class PgMechanicalMenu1 : Page
    {
        private ComSettings comSettings;
        private ScannerComm scanner;
        private ConcurrentQueue<byte> rxQueue = new ConcurrentQueue<byte>();
        private AutoResetEvent rxSignal = new AutoResetEvent(false);


        private MyLogger logger = new MyLogger("PgMechanicalMenu1");
       
       // private NotifyEvenCOM notifyEvenCOM;
        public PgMechanicalMenu1()
        {
            InitializeComponent();
            this.comSettings = ComSettings.Parse("COM7,115200,8,One,Even");//new ComSettings();
            this.Loaded += PgMechanicalMenu1_Loaded;
            this.Unloaded += PgMechanicalMenu1_Unloaded;
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.btnSetting.Click += BtnSetting_Click;

            this.btnReadQrCode.Click += BtnReadQrCode_Click;
            this.btnSave.Click += BtnSave_Click;
            this.btnRestore.Click += BtnRestore_Click;
            this.btnTurning.Click += BtnTurning_Click;
            this.btnForcus.Click += BtnForcus_Click;
            this.btnOpen.Click += BtnOpen_Click;
            this.btnClose.Click += BtnClose_Click;
            this.btnLogClear.Click += BtnLogClear_Click;

            this.BtnSave.Click += BtnSave_Click;
            this.BtnUndo.Click += BtnUndo_Click;
        }

        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("Bạn chắc chắn muốn đặt lại cài đặt thông số mặc định ?")) return;
            
                this.comSettings = ComSettings.Parse("COM7,115200,8,One,None");//new ComSettings();
                UiManager.SaveAppSetting();
            
        }

        private void PgMechanicalMenu1_Unloaded(object sender, RoutedEventArgs e)
        {
           // scanner.Stop();
        }

        private void BtnReadQrCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManager.createUserLog(UserActions.MN_MECHANICAL_BARCODE_READING_PKG);

                // Start reading:
                var qr = this.scanner.ReadQRHoneywell();
                this.UpdateLogs("Read Scanner :");
                this.UpdateLogs(qr);
               
            }
            catch (Exception ex)
            {
                logger.Create("BtPkgRead_Click error:" + ex.Message,LogLevel.Error);
            }
        }

        private void PgMechanicalMenu1_Loaded(object sender, RoutedEventArgs e)
        {
            comSettings = UiManager.appSettings.connection.scanner;

            this.btnReadQrCode.IsEnabled = false;
            this.btnTurning.IsEnabled = false;
            this.btnForcus.IsEnabled = false;
            this.cbSelectBank.IsEnabled = false;
        }

        private void BtnLogClear_Click(object sender, RoutedEventArgs e)
        {

            this.txtLogs.Text = "";
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManager.createUserLog(UserActions.MN_MECHANICAL_BARCODE_PORT_CLOSE);
                this.UpdateLogs("Đã Đóng Kết Nối Scanner ");
                scanner.Stop();
                this.btnSetting.IsEnabled = true;
                this.btnOpen.IsEnabled = true;
                this.btnClose.IsEnabled = true;
                this.btnReadQrCode.IsEnabled = false;
                this.btnTurning.IsEnabled = false;
                this.btnForcus.IsEnabled = false;
            }
            catch (Exception ex)
            {

                logger.Create("BtnClose_Click error:" + ex.Message, LogLevel.Error);
            }
           
        }
        private void DataReceived_Handler(byte rx)
        {
            // Update to UI.log:
            this.rxQueue.Enqueue(rx);
            this.rxSignal.Set();
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                UserManager.createUserLog(UserActions.MN_MECHANICAL_BARCODE_PORT_OPEN);

                if (this.comSettings != null)
                {
                    this.scanner = new ScannerComm(this.comSettings, this.DataReceived_Handler);
                    this.scanner.Start();
                   bool isconnect = this.scanner.IsConnected();
                    if (isconnect)
                    {
                        this.UpdateLogs("Connected Scanner Sucessful ( Kết Nối Scanner Thành công ) "); 
                        this.btnReadQrCode.IsEnabled = true;
                        this.btnTurning.IsEnabled = true;
                        this.btnForcus.IsEnabled = true;
                    }
                    else
                    {
                        this.UpdateLogs("Connected Scanner Faild ( Kết Nối Scanner Lỗi ) ");
                        this.btnReadQrCode.IsEnabled = false;
                        this.btnTurning.IsEnabled = false;
                        this.btnForcus.IsEnabled = false;
                    }    

                }
                this.btnSetting.IsEnabled = false;
                this.btnOpen.IsEnabled = false;
                this.btnClose.IsEnabled = true;
            }
            catch (Exception ex)
            {
                logger.Create("BtPortOpen_Click error:" + ex.Message, LogLevel.Error);
            }

        }

        private void BtnForcus_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tính năng chưa cập nhật","Thông Báo");
        }

        private void BtnTurning_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tính năng chưa cập nhật","Thông Báo");
        }

        private void BtnRestore_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MN_MECHANICAL_BARCODE_SAVE);
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want Save Setting ?")) return;
            UiManager.SaveAppSetting();
        }

        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
          
            try
            {
                UserManager.createUserLog(UserActions.MN_MECHANICAL_BARCODE_PORT_SETTINGS);

                var newSettings = new WndComSettings().DoSettings(Window.GetWindow(this), this.comSettings);
                if (newSettings != null)
                {
                    UiManager.appSettings.connection.scanner = newSettings.Clone();
                    this.comSettings = UiManager.appSettings.connection.scanner;
                    UiManager.SaveAppSetting();
                }
            }
            catch (Exception ex)
            {
                logger.Create("BtPortSetting_Click error:" + ex.Message,LogLevel.Error);
            }


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

        private void textBox2_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}