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
using System.Xml.Linq;
using DTO;
using KeyPad;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndFTPSetting.xaml
    /// </summary>
    public partial class WndFTPSetting : Window
    {
        private FTPClientSettings settings;
        private MyLogger logger = new MyLogger("Wnd_FTP_SETTING");
        public WndFTPSetting()
        {
            InitializeComponent();
            this.Loaded += WndFTPSetting_Loaded;
            this.btnCancel.Click += BtnCancel_Click;
            this.btnSave.Click += BtnSave_Click;

            this.txtFileName.TouchDown += Txt_TouchDown;      
            this.txtFolder.TouchDown += Txt_TouchDown;
            this.txtFTPUser.TouchDown += Txt_TouchDown;
            this.txtFTPPw.TouchDown += Txt_TouchDown;
            this.txtFTPIp.TouchDown += Txtint_TouchDown;
            this.txtFTPPort.TouchDown += Txtint_TouchDown;
           
        }

        private void Txtint_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
            }
        }
        private void Txt_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            VirtualKeyboard keyboardWindow = new VirtualKeyboard();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            
                int port;
                if (int.TryParse(this.txtFTPPort.Text, out port))
                {
                    settings.Host = this.txtFTPIp.Text;
                    settings.Port = port;
                    settings.UserID = this.txtFTPUser.Text;
                    settings.PassWord = this.txtFTPPw.Text;
                    settings.FolderServer = this.txtFolder.Text;
                    settings.Image = this.txtFileName.Text;
                    this.Close();
                }
           
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }

        private void WndFTPSetting_Loaded(object sender, RoutedEventArgs e)
        {
            settings = UiManager.appSettings.FTPClientSettings;
            this.txtFTPIp.Text = settings.Host;
            this.txtFTPPort.Text = settings.Port.ToString();
            this.txtFTPUser.Text = settings.UserID;
            this.txtFTPPw.Text = settings.PassWord;
            this.txtFolder.Text = settings.FolderServer;
            this.txtFileName.Text = settings.Image;
        }
        public FTPClientSettings DoSettings(Window owner, FTPClientSettings oldSettings)
        {
            this.Owner = owner;
            this.settings = oldSettings.Clone();

            this.txtFTPIp.Text = settings.Host;
            this.txtFTPPort.Text = settings.Port.ToString();
            this.txtFTPUser.Text = settings.UserID;
            this.txtFTPPw.Text = settings.PassWord;
            this.txtFolder.Text = settings.FolderServer;
            this.txtFileName.Text = settings.Image;
            this.ShowDialog();

            return this.settings;
        }

    }
}
