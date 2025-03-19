using KeyPad;
using System;
using Mitsubishi;
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
using System.Threading;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgSystemMenu.xaml
    /// </summary>
    public partial class PgSystemMenu : Page
    {
        MyLogger logger = new MyLogger("PG_SYSTEM_MES");
        private System.Timers.Timer cycleTimer;
        //FUNCTION
        private List<bool> L500 = new List<bool>();
        private List<bool> L520 = new List<bool>();

        public PgSystemMenu()
        {
            InitializeComponent();
            this.btSetting2.Click += BtSetting2_Click;

            this.cycleTimer = new System.Timers.Timer(100);
            this.cycleTimer.AutoReset = true;
            this.cycleTimer.Elapsed += CycleTimer_Elapsed;

            this.Loaded += PgSystemMenu_Loaded;
            this.Unloaded += PgSystemMenu_Unloaded;

            //VISION CH1
            this.btnVisionOn_ch1.Click += BtnVisionOn_ch1_Click;
            this.btnVisionOff_ch1.Click += BtnVisionOff_ch1_Click;
            this.btnCH1Lamp_On.Click += BtnCH1Lamp_On_Click;
            this.btnCH1Lamp_Off.Click += BtnCH1Lamp_Off_Click;

            //VISION CH2
            this.btnVisionOn_ch2.Click += BtnVisionOn_ch2_Click;
            this.btnVisionOff_ch2.Click += BtnVisionOff_ch2_Click;
            this.btnCH2Lamp_On.Click += BtnCH2Lamp_On_Click;
            this.btnCH2Lamp_Off.Click += BtnCH2Lamp_Off_Click;

            //AREA CH1
            this.btnLightCurtainOn_ch1.Click += BtnLightCurtainOn_ch1_Click;
            this.btnLightCurtainOff_ch1.Click += BtnLightCurtainOff_ch1_Click;

            //AREA CH2
            this.btnLightCurtainOn_ch2.Click += BtnLightCurtainOn_ch2_Click;
            this.btnLightCurtainOff_ch2.Click += BtnLightCurtainOff_ch2_Click;

            //DOOR CH1
            this.btnDoorOn_ch1.Click += BtnDoorOn_ch1_Click;
            this.btnDoorOff_ch1.Click += BtnDoorOff_ch1_Click;

            //DOOR CH2
            this.btnDoorOn_ch2.Click += BtnDoorOn_ch2_Click;
            this.btnDoorOff_ch2.Click += BtnDoorOff_ch2_Click;

            //DRY CH1
            this.btnDryCH1_On.Click += btnDryCH1_On_Click;
            this.btnDryCH1_Off.Click += btnDryCH1_Off_Click;

            //DRY CH2
            this.btnDryCH2_On.Click += BtnDryCH2_On_Click;
            this.btnDryCH2_Off.Click += BtnDryCH2_Off_Click;

            //MAGAZINE CH1
            this.btnMagazineCH1_On.Click += BtnMagazineCH1_On_Click;
            this.btnMagazineCH1_Off.Click += BtnMagazineCH1_Off_Click;

            //MAGAZINE CH2
            this.btnMagazineCH2_On.Click += BtnMagazineCH2_On_Click;
            this.btnMagazineCH2_Off.Click += BtnMagazineCH2_Off_Click;


            //MODEL NAME
            this.btnSave.Click += BtnSave_Click1;
        }
        private void BtnSave_Click1(object sender, RoutedEventArgs e)
        {
            if (!(txtModelName.Text == "") && !(txtModelName.Text == UiManager.appSettings.connection.modelName))
            {
                UiManager.appSettings.connection.modelName = this.txtModelName.Text;
            }
            UiManager.SaveAppSetting();
        }
        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SYSTEM_MENU_SYSTEM_MACHINE);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SYSTEM_MENU_SYSTEM_MACHINE);
        }
        #region FUNCTION
        private void BtnCH2Lamp_Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 524, false);
        }

        private void BtnCH2Lamp_On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 524, true);
        }

        private void BtnCH1Lamp_Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 504, false);
        }

        private void BtnCH1Lamp_On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 504, true);
        }

        private void BtnDoorOff_ch2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 521, false);
        }
        private void BtnDoorOn_ch2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 521, true);
        }
        private void BtnDoorOff_ch1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 501, false);
        }
        private void BtnDoorOn_ch1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 501, true);
        }
        private void BtnLightCurtainOff_ch2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 522, false);
        }
        private void BtnLightCurtainOn_ch2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 522, true);
        }
        private void BtnLightCurtainOff_ch1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 502, false);
        }
        private void BtnLightCurtainOn_ch1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 502, true);
        }
        private void BtnVisionOff_ch2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 523, false);
        }
        private void BtnVisionOn_ch2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 523, true);
        }
        private void BtnVisionOff_ch1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 503, false);
        }
        private void BtnVisionOn_ch1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.L, 503, true);
        }
        private void btnDryCH1_Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 500, false);
        }
        private void btnDryCH1_On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 500, true);
        }

        private void BtnDryCH2_Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 500, false);
        }

        private void BtnDryCH2_On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 500, true);
        }
        private void BtnMagazineCH2_Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 525, false);
        }

        private void BtnMagazineCH2_On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 525, true);
        }

        private void BtnMagazineCH1_Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 505, false);
        }

        private void BtnMagazineCH1_On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            UiManager.PLC.WriteBit(DeviceCode.L, 505, true);
        }

        #endregion

        #region Load Page
        private void PgSystemMenu_Unloaded(object sender, RoutedEventArgs e)
        {

        }
        private void PgSystemMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.txtModelName.Text = UiManager.appSettings.connection.modelName;
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
            this.Dispatcher.Invoke(() =>
            {

            });
        }

        private void CycleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            try
            {
                //FUNCTION
                UiManager.PLC.ReadMultiBits(DeviceCode.L, 500, 20, out L500);
                UiManager.PLC.ReadMultiBits(DeviceCode.L, 520, 20, out L520);
                UpdateUIData();
                return;
            }
            catch (Exception ex)
            {
                logger.Create("PLC Com Err" + ex.ToString(), LogLevel.Error);
                return;
            }
        }
        private void UpdateUIData()
        {
            if (!UiManager.PLC.isOpen())
                return;
            var converter = new BrushConverter();

            this.Dispatcher.Invoke(() =>
            {
                //FUNCTION CH1 STATUS
                if (L500.Count >= 1)
                {
                    if (L500[10])
                    {
                        btnDryCH1_On.Background = Brushes.LightGreen;
                        btnDryCH1_Off.Background = Brushes.White;
                    }
                    else
                    {
                        btnDryCH1_On.Background = Brushes.White;
                        btnDryCH1_Off.Background = Brushes.LightGreen;
                    }
                    if (L500[11])
                    {
                        btnDoorOn_ch1.Background = Brushes.LightGreen;
                        btnDoorOff_ch1.Background = Brushes.White;
                    }
                    else
                    {
                        btnDoorOn_ch1.Background = Brushes.White;
                        btnDoorOff_ch1.Background = Brushes.LightGreen;
                    }
                    if (L500[12])
                    {
                        btnLightCurtainOn_ch1.Background = Brushes.LightGreen;
                        btnLightCurtainOff_ch1.Background = Brushes.White;
                    }
                    else
                    {
                        btnLightCurtainOn_ch1.Background = Brushes.White;
                        btnLightCurtainOff_ch1.Background = Brushes.LightGreen;
                    }
                    if (L500[13])
                    {
                        btnVisionOn_ch1.Background = Brushes.LightGreen;
                        btnVisionOff_ch1.Background = Brushes.White;
                    }
                    else
                    {
                        btnVisionOn_ch1.Background = Brushes.White;
                        btnVisionOff_ch1.Background = Brushes.LightGreen;
                    }
                    if (L500[14])
                    {
                        btnCH1Lamp_On.Background = Brushes.LightGreen;
                        btnCH1Lamp_Off.Background = Brushes.White;
                    }
                    else
                    {
                        btnCH1Lamp_On.Background = Brushes.White;
                        btnCH1Lamp_Off.Background = Brushes.LightGreen;
                    }
                    if (L500[15])
                    {
                        btnMagazineCH1_On.Background = Brushes.LightGreen;
                        btnMagazineCH1_Off.Background = Brushes.White;
                    }
                    else
                    {
                        btnMagazineCH1_On.Background = Brushes.White;
                        btnMagazineCH1_Off.Background = Brushes.LightGreen;
                    }
                }
                //FUNCTION CH2 STATUS
                if (L520.Count >= 1)
                {
                    if (L520[10])
                    {
                        btnDryCH2_On.Background = Brushes.LightGreen;
                        btnDryCH2_Off.Background = Brushes.White;
                    }
                    else
                    {
                        btnDryCH2_On.Background = Brushes.White;
                        btnDryCH2_Off.Background = Brushes.LightGreen;
                    }
                    if (L520[11])
                    {
                        btnDoorOn_ch2.Background = Brushes.LightGreen;
                        btnDoorOff_ch2.Background = Brushes.White;
                    }
                    else
                    {
                        btnDoorOn_ch2.Background = Brushes.White;
                        btnDoorOff_ch2.Background = Brushes.LightGreen;
                    }
                    if (L520[12])
                    {
                        btnLightCurtainOn_ch2.Background = Brushes.LightGreen;
                        btnLightCurtainOff_ch2.Background = Brushes.White;
                    }
                    else
                    {
                        btnLightCurtainOn_ch2.Background = Brushes.White;
                        btnLightCurtainOff_ch2.Background = Brushes.LightGreen;
                    }
                    if (L520[13])
                    {
                        btnVisionOn_ch2.Background = Brushes.LightGreen;
                        btnVisionOff_ch2.Background = Brushes.White;
                    }
                    else
                    {
                        btnVisionOn_ch2.Background = Brushes.White;
                        btnVisionOff_ch2.Background = Brushes.LightGreen;
                    }
                    if (L520[14])
                    {
                        btnCH2Lamp_On.Background = Brushes.LightGreen;
                        btnCH2Lamp_Off.Background = Brushes.White;
                    }
                    else
                    {
                        btnCH2Lamp_On.Background = Brushes.White;
                        btnCH2Lamp_Off.Background = Brushes.LightGreen;
                    }
                    if (L520[15])
                    {
                        btnMagazineCH2_On.Background = Brushes.LightGreen;
                        btnMagazineCH2_Off.Background = Brushes.White;
                    }
                    else
                    {
                        btnMagazineCH2_On.Background = Brushes.White;
                        btnMagazineCH2_Off.Background = Brushes.LightGreen;
                    }
                }
            });
        }
        #endregion
    }

}
