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
using System.Xml.Linq;
using DTO;
using ITM_Semiconductor.Properties;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgMechanicalMenu3.xaml
    /// </summary>
    public partial class PgMechanicalMenu3 : Page
    {
        private MyLogger logger = new MyLogger("PG_Setting_FTP");
        public PgMechanicalMenu3()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;

            this.btSetting.Click += BtSetting_Click;
            this.Loaded += PgMechanicalMenu3_Loaded;
        }

        private void PgMechanicalMenu3_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtLogs.Text = "";
        }

        private void BtSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newSettings = new WndFTPSetting().DoSettings(Window.GetWindow(this), UiManager.appSettings.FTPClientSettings);
            
                UpdateLogs($"IP: {newSettings.Host}");
                UpdateLogs($"PORT: {newSettings.Port}");
                UpdateLogs($"USE NAME:  {newSettings.UserID}");
                UpdateLogs($"PASSWORD: {newSettings.PassWord}");
                UpdateLogs($"FOLDER FTP: {newSettings.FolderServer}");
                UpdateLogs($"NAME IMAGE: {newSettings.Image}");

                if (newSettings != null) 
                {
                    UiManager.appSettings.FTPClientSettings = newSettings.Clone();
                    UiManager.SaveAppSetting();
                    UpdateLogs($"SAVE SETTING COMPLETE");
                }
            }
            catch (Exception ex)
            {
                logger.Create(String.Format("Action Button Setting Error: ") + ex.Message,LogLevel.Error);
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
    }
}