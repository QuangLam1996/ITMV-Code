using DTO;
using System;
using System.Collections.Generic;
using System.IO.Ports;
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
    /// Interaction logic for WndPLCComSetting.xaml
    /// </summary>
    public partial class WndPLCComSetting : Window
    {
        private static MyLogger logger = new MyLogger("PLCComSettings");
        public WndPLCComSetting()
        {
            InitializeComponent();
            this.Loaded += WndPLCComSetting_Loaded;
            this.btOk.Click += BtOk_Click;
            this.btCancel.Click += BtCancel_Click;
        }

        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtOk_Click(object sender, RoutedEventArgs e)
        {
            UiManager.appSettings.SettingPLCCom.PortName = this.cbPortName.SelectedValue.ToString();
            UiManager.appSettings.SettingPLCCom.Baudrate = int.Parse(this.cbBaudrate.SelectedValue.ToString());
            UiManager.appSettings.SettingPLCCom.DataBits = int.Parse(this.cbDataBits.SelectedValue.ToString());
            UiManager.appSettings.SettingPLCCom.StopBits = ComSettings.ParseStopBits(this.cbStopBits.SelectedValue.ToString());
            UiManager.appSettings.SettingPLCCom.Parity   = ComSettings.ParseParity(this.cbParity.SelectedValue.ToString());
            UiManager.appSettings.SettingPLCCom.Address  = ushort.Parse(this.txtAddressMB.Text);
            this.Close();
        }

        private void WndPLCComSetting_Loaded(object sender, RoutedEventArgs e)
        {
           

            this.cbPortName.SelectedValue = UiManager.appSettings.SettingPLCCom.PortName.ToString();
            this.cbBaudrate.SelectedValue = UiManager.appSettings.SettingPLCCom.Baudrate.ToString();
            this.cbDataBits.SelectedValue = UiManager.appSettings.SettingPLCCom.DataBits.ToString();
            this.cbParity.SelectedValue   = UiManager.appSettings.SettingPLCCom.Parity.ToString();
            this.cbStopBits.SelectedValue = UiManager.appSettings.SettingPLCCom.StopBits.ToString();
            this.txtAddressMB.Text        = UiManager.appSettings.SettingPLCCom.Address.ToString();

            this.LoadComPort();
        }
        private void LoadComPort()
        {
            try
            {
                var portNames = SerialPort.GetPortNames();
                foreach (var pn in portNames)
                {
                    var cbi = new ComboBoxItem();
                    cbi.Content = pn;
                    this.cbPortName.Items.Add(cbi);
                }
            }
            catch (Exception ex)
            {
                logger.Create("LoadComPort: " + ex.Message, LogLevel.Error);
            }
        }
    }
}
