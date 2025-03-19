using DTO;
using ITM_Semiconductor.Properties;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime;
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
    /// Interaction logic for WndComSettings.xaml
    /// </summary>
    public partial class WndComSettings : Window
    {
        private static MyLogger logger = new MyLogger("ScannerComSettings");
        private ComSettings settings;


        public WndComSettings()
        {
            InitializeComponent();

            this.Loaded += WndComSetting_Loaded;
            this.btOk.Click += BtOk_Click;
            this.btOk.TouchDown += BtOk_Click;

            this.btCancel.Click += BtCancel_Click;
            this.btCancel.TouchDown += BtCancel_Click;
        }
        public ComSettings DoSettings(Window owner, ComSettings oldSettings)
        {
            this.Owner = owner;

            this.settings = ComSettings.Clone(oldSettings);
            this.cbPortName.SelectedValue = settings.portName;
            this.cbBaudrate.SelectedValue = settings.baudrate.ToString();
            this.cbDataBits.SelectedValue = settings.dataBits.ToString();
            var s = settings.parity.ToString();
            this.cbParity.SelectedValue = s;
            s = settings.stopBits.ToString();
            this.cbStopBits.SelectedValue = s;

            this.ShowDialog();
            return this.settings;
        }
        private void BtCancel_Click(object sender, RoutedEventArgs e)
        {
            this.settings = null;
            this.Close();
        }

        private void BtOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                settings.portName = this.cbPortName.SelectedValue.ToString();
                settings.baudrate = int.Parse(this.cbBaudrate.SelectedValue.ToString());
                settings.dataBits = int.Parse(this.cbDataBits.SelectedValue.ToString());
                settings.stopBits = ComSettings.ParseStopBits(this.cbStopBits.SelectedValue.ToString());
                settings.parity = ComSettings.ParseParity(this.cbParity.SelectedValue.ToString());
                this.Close();
            }
            catch (Exception ex)
            {
                logger.Create("BtOk_Click error:" + ex.Message,LogLevel.Error);
            }
        }

        private void WndComSetting_Loaded(object sender, RoutedEventArgs e)
        {
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
