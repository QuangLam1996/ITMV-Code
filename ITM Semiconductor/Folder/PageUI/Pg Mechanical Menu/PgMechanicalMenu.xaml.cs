using KeyPad;
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
    /// Interaction logic for PgMechanicalMenu.xaml
    /// </summary>
    public partial class PgMechanicalMenu : Page
    {
        
        public PgMechanicalMenu()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;

            this.txtLocalIp1.PreviewMouseDown += TxtLocalIp_PreviewMouseDown;
            this.txtLocalIp1.TouchDown += TxtLocalIp_TouchDown;

            this.txtLocalIp2.PreviewMouseDown += TxtLocalIp_PreviewMouseDown;
            this.txtLocalIp2.TouchDown += TxtLocalIp_TouchDown;

            this.txtLocalIp3.PreviewMouseDown += TxtLocalIp_PreviewMouseDown;
            this.txtLocalIp3.TouchDown += TxtLocalIp_TouchDown;

            this.txtLocalIp4.PreviewMouseDown += TxtLocalIp_PreviewMouseDown;
            this.txtLocalIp4.TouchDown += TxtLocalIp_TouchDown;

            this.txtLocalPort.PreviewMouseDown += TxtLocalIp_PreviewMouseDown;
            this.txtLocalPort.TouchDown += TxtLocalIp_TouchDown;

            this.btSave.Click += BtSave_Click;
            this.Loaded += PgMechanicalMenu_Loaded;

        }

        private void PgMechanicalMenu_Loaded(object sender, RoutedEventArgs e)
        {

            var arr = UiManager.appSettings.MesSettings1.localIp.Split('.');
            if (arr.Length == 4)
            {
                this.txtLocalIp1.Text = arr[0];
                this.txtLocalIp2.Text = arr[1];
                this.txtLocalIp3.Text = arr[2];
                this.txtLocalIp4.Text = arr[3];
            }
            this.txtLocalPort.Text = UiManager.appSettings.MesSettings1.localPort.ToString();
        }

        private void BtSave_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want Save Setting ?")) return;

            UiManager.appSettings.MesSettings1.localIp = String.Format("{0}.{1}.{2}.{3}",
                   txtLocalIp1.Text, txtLocalIp2.Text, txtLocalIp3.Text, txtLocalIp4.Text);
            UiManager.appSettings.MesSettings1.localPort = int.Parse(txtLocalPort.Text);
            UiManager.SaveAppSetting();
        }

        private void TxtLocalIp_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
        }

        private void TxtLocalIp_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
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
    }
}
