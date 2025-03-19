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
    /// Interaction logic for PgSystemMenu01.xaml
    /// </summary>
    public partial class PgSystemMenu01 : Page
    {
        private bool IsRunning;
        private Color Colo_ON_GR;
        private Color Colo_ON_RE;
        private Color Colo_OFF;
        private System.Timers.Timer cycleTimer;

        //TIME DELAY CH1
        private List<int> D300 = new List<int>();
        //TIME DELAY CH2
        private List<int> D700 = new List<int>();

        public PgSystemMenu01()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.Loaded += PgSystemMenu01_Loaded;
            this.Unloaded += PgSystemMenu01_Unloaded;

            this.cycleTimer = new System.Timers.Timer(100);
            this.cycleTimer.AutoReset = true;

            //TIME DELAY CH1
            this.txtVacumnTimeCH1.TextChanged += TxtVacumnTimeCH1_TextChanged;
            this.txtBlowTimeCH1.TextChanged += TxtBlowTimeCH1_TextChanged;
            this.txtToolUpCH1.TextChanged += TxtToolUpCH1_TextChanged;
            this.txtToolDnCH1.TextChanged += TxtToolDnCH1_TextChanged;
            this.txtPanelRetryCH1.TextChanged += TxtPanelRetryCH1_TextChanged;

            //TIME DELAY CH2
            this.txtVacumnTimeCH2.TextChanged += TxtVacumnTimeCH2_TextChanged;
            this.txtBlowTimeCH2.TextChanged += TxtBlowTimeCH2_TextChanged;
            this.txtToolUpCH2.TextChanged += TxtToolUpCH2_TextChanged;
            this.txtToolDnCH2.TextChanged += TxtToolDnCH2_TextChanged;
            this.txtPanelRetryCH2.TextChanged += TxtPanelRetryCH2_TextChanged;
        }
        #region TIME DELAY
        private void TxtPanelRetryCH2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtPanelRetryCH2.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 708, int.Parse(txtPanelRetryCH2.Text));
        }

        private void TxtToolDnCH2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtToolDnCH2.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 706, int.Parse(txtToolDnCH2.Text));
        }

        private void TxtToolUpCH2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtToolUpCH2.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 704, int.Parse(txtToolUpCH2.Text));
        }

        private void TxtBlowTimeCH2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtBlowTimeCH2.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 702, int.Parse(txtBlowTimeCH2.Text));
        }

        private void TxtVacumnTimeCH2_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtVacumnTimeCH2.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 700, int.Parse(txtVacumnTimeCH2.Text));
        }
        private void TxtPanelRetryCH1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtPanelRetryCH1.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 308, int.Parse(txtPanelRetryCH1.Text));
        }

        private void TxtToolDnCH1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtToolDnCH1.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 306, int.Parse(txtToolDnCH1.Text));
        }

        private void TxtToolUpCH1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtToolUpCH1.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 304, int.Parse(txtToolUpCH1.Text));
        }

        private void TxtBlowTimeCH1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtBlowTimeCH1.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 302, int.Parse(txtBlowTimeCH1.Text));
        }

        private void TxtVacumnTimeCH1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            if (txtVacumnTimeCH1.Text == "")
                return;
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 300, int.Parse(txtVacumnTimeCH1.Text));
        }
        #endregion
        private void PgSystemMenu01_Unloaded(object sender, RoutedEventArgs e)
        {
            this.IsRunning = false;
        }
        private void PgSystemMenu01_Loaded(object sender, RoutedEventArgs e)
        {
            Task tsk = new Task(() =>
            {
                cycleTimer.Start();
                this.UpdateUIOneShort();
            });
            tsk.Start();
            Thread.Sleep(100);
        }

        private void UpdateUIOneShort()
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 300, 50, out D300);
            UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 700, 50, out D700);
            Dispatcher.Invoke(new Action(() =>
            {
                //TIME DELAY CH1
                this.txtVacumnTimeCH1.Text = D300[00].ToString();
                this.txtBlowTimeCH1.Text = D300[02].ToString();
                this.txtToolUpCH1.Text = D300[04].ToString();
                this.txtToolDnCH1.Text = D300[06].ToString();
                this.txtPanelRetryCH1.Text = D300[00].ToString();

                //TIME DELAY CH2
                this.txtVacumnTimeCH2.Text = D700[00].ToString();
                this.txtBlowTimeCH2.Text = D700[02].ToString();
                this.txtToolUpCH2.Text = D700[04].ToString();
                this.txtToolDnCH2.Text = D700[06].ToString();
                this.txtPanelRetryCH2.Text = D700[08].ToString();
            }));
        }
        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SYSTEM_MENU_SYSTEM_MACHINE);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SYSTEM_MENU_SYSTEM_MACHINE);
        }
        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SYSTEM_MENU);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SYSTEM_MENU);
        }
    }
}
