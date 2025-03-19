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
using System.IO;
using System.Reflection;
using System.ComponentModel;
using DTO;
using Newtonsoft.Json;
using System.Net;
using static System.Data.Entity.Infrastructure.Design.Executor;



namespace ITM_Semiconductor
{
  

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        private static MyLogger logger = new MyLogger("WndWindow");
        public const String AppVersionNumber = "1.2.23";
        public const String AppVersionTime = "14-Jun-2021";
        private System.Timers.Timer clock = new System.Timers.Timer(1000);

        private static WndAutoUpdate wndAutoUpdate;
        private AutoUpdateSettings appSettings { get; set; }
        private const String APP_SETTINGS_FILE_NAME = "autoUpdate_settings.json";
        private ConnectionSetting connection;

       
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
          
            this.Closed += MainWindow_Closed;
            this.btPower.Click += BtPower_Click;
            this.btMenu.Click += BtMenu_Click;
            this.btMain.Click += BtMain_Click;
            this.btIO.Click += BtIO_Click;
            this.btLastJam.Click += BtLastJam_Click;
        }

        private void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            this.clock.Stop();
            // Stop connect PLC
            UiManager.DisconnectPLC();

           // // Stop connect Scanner
           //UiManager.DisconnectScanner();

           // // Stop Connect FTP Keyence
           //UiManager.StopConnectFTP_KEYENCE();

           // // Stop Connect MES
           // UiManager.StopConnectionMES();

            // Log Stop Program
            logger.Create(String.Format("Stop Program"),LogLevel.Information);
        }

        public void MainWindow_Closed(object sender, EventArgs e)
        {
            this.clock.Stop();

            // Stop connect PLC
            UiManager.DisconnectPLC();
            logger.Create(String.Format("Disconnect PLC"), LogLevel.Information);

            //// Stop connect Scanner
            //UiManager.DisconnectScanner();
            //logger.Create(String.Format("Disconnect Scanner"), LogLevel.Information);

            //// Stop Connect FTP Keyence
            //UiManager.StopConnectFTP_KEYENCE();
            //logger.Create(String.Format("Disconnect FTP"), LogLevel.Information);

            //// Stop Connect MES
            //UiManager.StopConnectionMES();
            //logger.Create(String.Format("Disconnect MES"), LogLevel.Information);

            // Log Stop Program
            logger.Create(String.Format("Stop Program"), LogLevel.Information);
            Environment.Exit(0);
        }

        private void BtLastJam_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.WND_ALARM);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_ALARM);
        }

        private void BtIO_Click(object sender, RoutedEventArgs e)
        {
           UserManager.createUserLog(UserActions.WND_IO);
           UiManager.Instance.SwitchPage(PAGE_ID.PAGE_IO);
        }

        private void BtMain_Click(object sender, RoutedEventArgs e)
        {
           UserManager.createUserLog(UserActions.WND_MAIN);
           UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MAIN);
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
           UserManager.createUserLog(UserActions.WND_MENU);
           UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MENU);
        }

        private void BtPower_Click(object sender, RoutedEventArgs e)
        {
          UserManager.createUserLog(UserActions.WND_CLOSE);
          App.Current.Shutdown();
        }    

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadSettings();
            //CheckUpdate();
            clock.AutoReset = true;
            clock.Elapsed += this.Clock_Elapsed;
            clock.Start();
            DateTime buildDate = GetBuildDate();
            // Hiển thị ngày giờ build
            string ver = string.Format( buildDate.ToString("G"));
            TblVersion.Content = $"Update : {UiManager.appSettings.connection.VerUpDate} : {UiManager.appSettings.connection.DateUpdate}";
            this.txbNameMachine.Text = UiManager.appSettings.SettingModel.NameMachine1;
        }

        private DateTime GetBuildDate()
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.LastWriteTime;
        }

        private void Clock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try {
                   this.Dispatcher.Invoke(() => { this.lblCurrentDate.Content = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now); });
            } 
            catch { }
            
        }

        public void UpdateMainContent(object obj)
        {
            this.frmMainContent.Navigate(obj);
        }

        #region AutoUpdate
        private void LoadSettings()
        {
            try
            {
                // Load File AppSettings
                this.LoadAppSettings(APP_SETTINGS_FILE_NAME);

                if (this.appSettings == null) { this.appSettings = new AutoUpdateSettings(); }

                // Define Connection Settings
                this.connection = this.appSettings.Connection;
            }
            catch (Exception ex)
            {
               
                logger.Create(String.Format("Load Settings AutoUpdate Error:" + ex.Message), LogLevel.Error);
            }
        }
        private void LoadAppSettings(String FileName)
        {
            try
            {
                String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), FileName);
                if (File.Exists(filePath))
                {
                    using (StreamReader file = File.OpenText(filePath))
                    {
                        this.appSettings = AutoUpdateSettings.FromJSON(file.ReadToEnd());
                    }
                }
                else { this.appSettings = new AutoUpdateSettings(); }
            }
            catch (Exception ex)
            {
                logger.Create(String.Format("Load File AppSettings Error: " + ex.Message), LogLevel.Error);
            }
        }

        public void CheckUpdate()
        {
            string Url = connection.FTPClient.Host;
            int Port = connection.FTPClient.Port;
            string Folder = connection.FTPClient.FolderServer;
            string NameFile = "Ver.json";

            string ftpUrl = $"ftp://{Url}:{Port}/{Folder}/{NameFile}";
            string username = connection.FTPClient.UserID; // Tên đăng nhập
            string password = connection.FTPClient.PassWord; // Mật khẩu

            try
            {
                // Tạo yêu cầu FTP
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.DownloadFile;

                // Cung cấp thông tin đăng nhập
                request.Credentials = new NetworkCredential(username, password);

                // Đọc phản hồi từ FTP
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    // Đọc nội dung file JSON
                    string jsonContent = reader.ReadToEnd();
                    AutoUpdate update = JsonConvert.DeserializeObject<AutoUpdate>(jsonContent);
                    if (UiManager.appSettings.connection.VerUpDate == update.VerName)
                    {

                    }
                    else
                    {
                        UiManager.appSettings.connection.VerUpDate = update.VerName;
                        UiManager.appSettings.connection.DateUpdate = update.InformationVer.DateTimeUpdate;
                        wndAutoUpdate = new WndAutoUpdate();
                        wndAutoUpdate.MessengerShow($"Nội Dung Update:{update.InformationVer.HistoryUpdate} ");
                        
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đọc file từ FTP: {ex.Message}");
            }
        }
        #endregion
       

    }



}
