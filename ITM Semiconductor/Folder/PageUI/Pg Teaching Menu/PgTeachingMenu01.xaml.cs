using DTO;
using Mitsubishi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgTeachingMenu01.xaml
    /// </summary>
    public partial class PgTeachingMenu01 : Page
    {
        MotorParameter Robot = UiManager.appSettings.Robot;

        MyLogger logger = new MyLogger("PG Teaching manual 1");

        private bool BendingPos = false;
        private bool ReadyPos = false;
        private bool QrPos = false;
        private bool QrScrapPos = false;
        private bool MesScapPos = false;
        private bool MatingPos = false;
        int CurrentPositionPosXData = 0;

        private CancellationTokenSource canceTokenSource;
        private int D3848;

        private List<int> D_7000_7900 = new List<int>();
        private List<bool> M_0_1000 = new List<bool>();

        public PgTeachingMenu01()
        {
            InitializeComponent();
            this.Loaded += PgTeachingMenu01_Loaded;
            this.Unloaded += PgTeachingMenu01_Unloaded;


            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;

            this.txtBendingPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtReadyPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrScapPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;

            this.txtBendingPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtReadyPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrScapPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;

            #region Position Selection
            this.btnBendingPos.PreviewTouchDown += BendingPos_Clicked;
            this.btnBendingPos.Click += BendingPos_Clicked;

            this.btnReadyPos.PreviewTouchDown += ReadyPos_Clciked;
            this.btnReadyPos.Click += ReadyPos_Clciked;

            this.btnQrPos.Click += QrPos_CLicked;
            this.btnQrScap.Click += QrScrapPos_Clicked;
            this.btnMatingPos.Click += MatingPos_Clicked;
            #endregion

            #region Motor Control 
            this.btnAllAxisHome1.PreviewTouchDown += btnAllAxisHome_Clciked;
            this.btnXAxisHome1.PreviewTouchDown += btnXAxisHome_Clicked;
            this.btnZAxisHome1.PreviewTouchDown += btnZAxisHome_Clicked;
            this.btnJogXFw1.PreviewTouchDown += btnJogXFw_MouseEnter;
            this.btnJogXFw1.PreviewTouchUp += btnJogXFw_MouseLeft;
            this.btnJogXRv1.PreviewTouchDown += btnJogXRv_MouseEnter;
            this.btnJogXRv1.PreviewTouchUp += btnJogXRv_MouseLeft;

            this.btSave1.PreviewTouchDown += btSave_Clicked;
            this.JogXSpeed.ValueChanged += JogXSpeed_ValueChanged;
            this.btnXMoveX1.PreviewTouchDown += btnXMoveX_Clicked;

            this.btnPickPlaceOneCycle1.PreviewTouchDown += btnPickPlaceOneCycle_Clicked;
            this.btnXServoOn1.PreviewTouchDown += btnXServoOn_Clicked;
            this.btnXServoOff1.PreviewTouchDown += btnXServoOff_Clicked;
            this.txtSetSpeedRobot1_CH1.TouchDown += TxtSetSpeedRobot1_CH1_TouchDown;

            this.PreviewTouchUp += this.Form_touchUp;
            #endregion

            #region In Tray Test
            this.btnCenHoldTray121.Click += btnCenHoldTray121_Clicked;
            this.btnCenHoldTray12L1.Click += btnCenHoldTray12L1_Clicked;
            this.btnCenHoldTray341.Click += btnCenHoldTray341_Clicked;
            this.btnCenHoldTray34L1.Click += btnCenHoldTray34L1_Clicked;
            this.btnSplitTray121.Click += btnSplitTray121_Clicked;
            this.btnSplitTray12L1.Click += btnSplitTray12L1_Clicked;
            this.btnSplitTray341.Click += btnSplitTray341_Clicked;
            this.btnSplitTray34L1.Click += btnSplitTray34L1_Clicked;
            this.btnCylPosCen1.Click += btnCylPosCenter1_Clicked;
            this.btnCylPosHight1.Click += btnCylPosHight1_Clicked;
            this.btnCylPosLow1.Click += btnCylPosLow1_Clicked;
            #endregion

            #region Out Tray
            this.btnCylUDOutTray1.Click += btnCylUDOutTray1_Clicked;
            this.btnCylUDOutTrayL1.Click += btnCylUDOutTrayL1_Clicked;
            #endregion

            #region Center Tray
            this.btnClampPullTray1.Click += btnClampPullTray1_Clicked;
            this.btnKeepCenterTray1.Click += btnKeepCenterTray1_Clicked;
            this.btnCenterTray1.Click += btnCenterTray1_Clicked;
            this.btnClampPullTrayL1.Click += btnClampPullTrayL1_Clicked;
            this.btnKeepCenterTrayL1.Click += btnKeepCenterTrayL1_Clicked;
            this.btnCenterTrayL1.Click += btnCenterTrayL1_Clicked;
            #endregion

        }



        public async Task StartTask()
        {
            if (!UiManager.PLC.isOpen())
                return;
            canceTokenSource = new CancellationTokenSource();
            try
            {
                while (!canceTokenSource.Token.IsCancellationRequested)
                {


                    UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 7000, 900, out D_7000_7900);
                    UiManager.PLC.ReadMultiBits(DeviceCode.M, 0000, 1000, out M_0_1000);
                    UpdateUIData();
                    await Task.Delay(1);

                }
            }
            catch (Exception ex)
            {
                logger.Create("PLC Com Err" + ex.ToString(), LogLevel.Error);
                return;
            }
        }

        private void PgTeachingMenu01_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canceTokenSource?.Cancel();
        }

        private async void PgTeachingMenu01_Loaded(object sender, RoutedEventArgs e)
        {
            await StartTask();
            this.PLCConnected();
        }

        #region Center Tray
        private void btnCenterTrayL1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1252, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1253, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1252);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1253);
        }
        private void btnKeepCenterTrayL1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1248, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1249, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1248);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1249);
        }
        private void btnClampPullTrayL1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1238, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1239, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1238);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1239);
        }
        private void btnCenterTray1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1253, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1252, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1253);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1252);
        }
        private void btnKeepCenterTray1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1249, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1248, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1249);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1248);
        }
        private void btnClampPullTray1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1239, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1238, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1239);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1238);
        }
        #endregion

        #region Out Tray
        private void btnCylUDOutTrayL1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1258, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1259, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1258);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1259);
        }
        private void btnCylUDOutTray1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1259, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1258, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1259);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1258);
        }
        #endregion

        #region In Tray Test
        private void btnCylPosLow1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1237, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1236, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1235, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1237);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1236);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1235);
        }
        private void btnCylPosHight1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1235, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1236, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1237, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1235);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1236);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1237);
        }
        private void btnCylPosCenter1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1235, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1237, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1236, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1235);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1237);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1236);
        }
        private void btnSplitTray34L1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1230, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1231, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1230);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1231);
        }
        private void btnSplitTray341_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1231, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1230, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1231);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1230);
        }
        private void btnSplitTray12L1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1228, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1229, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1228);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1229);
        }
        private void btnSplitTray121_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1229, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1228, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1229);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1228);
        }
        private void btnCenHoldTray34L1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1226, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1227, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1226);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1227);
        }
        private void btnCenHoldTray341_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1227, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1226, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1227);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1226);
        }
        private void btnCenHoldTray12L1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1224, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1225, true);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1224);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1225);
        }
        private void btnCenHoldTray121_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1225, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1224, true);

            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1225);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1224);
        }
        #endregion

        #region Motor Control
        private async void Form_touchUp(object sender, RoutedEventArgs e)
        {
            await Task.Delay(0);
        }
        private void TxtSetSpeedRobot1_CH1_TouchDown(object sender, TouchEventArgs e)
        {
            Process.Start(@"C:\Windows\System32\osk.exe");
        }
        private void btnXServoOff_Clicked(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 203, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 203);
        }
        private void btnXServoOn_Clicked(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 203, false);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 203);
        }
        private void btnPickPlaceOneCycle_Clicked(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 202, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 202);
        }
        private void btnXMoveX_Clicked(object sender, TouchEventArgs e)
        {

            if (!UiManager.PLC.isOpen())
            {
                //displayAlarm(AlarmInfo.Err_Sol_Hight_Cyl_Split_Tray_01_02_Ch1);
                return;
            }
            //M80
            if (BendingPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1082, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1282);
            else if (ReadyPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1083, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1283);
            else if (QrPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1084, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1284);
            else if (QrScrapPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1119, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1319);
            //else if (MesScapPos)
            //    plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 172);
            //else if (MatingPos)
            //    plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 173);
        }
        private void JogXSpeed_ValueChanged(object sender, RoutedEventArgs e)
        {
            int speed = 0;
            try
            {
                //speed = Convert.ToInt32(JogXSpeed.Value);
            }
            catch
            {
                return;
            }
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 2260, speed);
            //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 2260, new int[] { speed });
        }
        private void btSave_Clicked(object sender, TouchEventArgs e)
        {
            int D3200, D3202, D3206, D3208, D3212, D3214, D3218, D3220;/* D3224, D3226;*/
            //int D3300, D3302, D3306, D3308, D3312, D3314, D3318, D3320, D3324, D3326;
            int D3848;
            try
            {
                //Tray Ch1

                // point 1

                D3200 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisX.Text), 4) * 1000);
                D3202 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisZ.Text), 4) * 1000);


                // point 2

                D3206 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisX.Text), 4) * 1000);
                D3208 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisZ.Text), 4) * 1000);


                // point 3

                D3212 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisX.Text), 4) * 1000);
                D3214 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisZ.Text), 4) * 1000);


                // point 4

                D3218 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisX.Text), 4) * 1000);
                D3220 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisZ.Text), 4) * 1000);


                //Nam Update Set Speed Robot
                D3848 = Convert.ToInt32(Math.Round(double.Parse(this.txtSetSpeedRobot1_CH1.Value.ToString()), 4));

            }
            catch
            {
                //displayAlarm(AlarmInfo.Err_Sol_Low_Cyl_Split_Tray_03_04_Ch1);
                return;
            }
            if (!UiManager.PLC.isOpen())
            {
                //displayAlarm(AlarmInfo.Err_Sol_Hight_Cyl_Split_Tray_01_02_Ch1);
                return;
            }
            try
            {
                UserManager.createUserLog(UserActions.MN_TEACHING_JIG_SAVE);

                if (MessageBox.Show("Are you sure to save data position?", "NOTE",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)

                {
                    UiManager.PLC.WriteWord(DeviceCode.D, 3848, D3848);
                    //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3848, new int[] { D3848 });
                    if (BendingPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3200, D3200);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3202, D3202);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1314, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3200, new int[] { D3200 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3202, new int[] { D3202 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1314);
                    }
                    else if (ReadyPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3206, D3206);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3208, D3208);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1314, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3206, new int[] { D3206 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3208, new int[] { D3208 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1314);
                    }
                    else if (QrPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3212, D3212);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3214, D3214);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1314, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3212, new int[] { D3212 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3214, new int[] { D3214 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1314);
                    }
                    else if (QrScrapPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3218, D3218);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3220, D3220);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1314, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3218, new int[] { D3218 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3220, new int[] { D3220 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1314);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Create("BtJigSave_Click error:" + ex.Message, LogLevel.Error);
            }
        }
        private async void btnJogXRv_MouseLeft(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            await Task.Delay(0);
            UiManager.PLC.WriteBit(DeviceCode.M, 1081, false);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1281);
            UpdateJog(71);
        }
        private async void btnJogXRv_MouseEnter(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            await Task.Delay(0);
            UiManager.PLC.WriteBit(DeviceCode.M, 1081, true);
            //M71: JogX - 
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1281);
        }
        private async void btnJogXFw_MouseLeft(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            await Task.Delay(0);
            UiManager.PLC.WriteBit(DeviceCode.M, 1080, false);
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1280);
            UpdateJog(70);
        }
        private async void btnJogXFw_MouseEnter(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            await Task.Delay(0);
            UiManager.PLC.WriteBit(DeviceCode.M, 1080, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1280);
        }
        private void btnZAxisHome_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 76);
        }
        private void btnXAxisHome_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1301, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1301);
        }
        private void btnAllAxisHome_Clciked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            //M80
            UiManager.PLC.WriteBit(DeviceCode.M, 1302, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1302);
        }

        private void UpdateJog(int JogLeft)
        {
            Thread.Sleep(30);
            if (JogLeft == 70)
            {
                UiManager.PLC.WriteBit(DeviceCode.M, 70, false);
                //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 70);
            }
            else if (JogLeft == 71)
            {
                UiManager.PLC.WriteBit(DeviceCode.M, 71, false);
                //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 71);
            }
            else if (JogLeft == 71)
            {
                UiManager.PLC.WriteBit(DeviceCode.M, 72, false);
                //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 72);
            }
            else if (JogLeft == 71)
            {
                UiManager.PLC.WriteBit(DeviceCode.M, 73, false);
                //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 73);
            }
        }
        #endregion

        #region Position Selection
        private void MatingPos_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = true;
            int D3601 = 6;
            UiManager.PLC.WriteWord(DeviceCode.D, 3601, D3601);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3601, new int[] { D3601 });
            UpdatePos(5);
        }
        private void QrScrapPos_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = true;
            this.MesScapPos = false;
            this.MatingPos = false;
            int D3601 = 4;
            UiManager.PLC.WriteWord(DeviceCode.D, 3601, D3601);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3601, new int[] { D3601 });
            UpdatePos(3);
        }
        private void QrPos_CLicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = true;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            int D3601 = 3;
            UiManager.PLC.WriteWord(DeviceCode.D, 3601, D3601);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3601, new int[] { D3601 });
            UpdatePos(2);
        }
        private void ReadyPos_Clciked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = true;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            int D3601 = 2;
            UiManager.PLC.WriteWord(DeviceCode.D, 3601, D3601);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3601, new int[] { D3601 });
            UpdatePos(1);
        }
        private void BendingPos_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = true;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            int D3601 = 1;
            UiManager.PLC.WriteWord(DeviceCode.D, 3601, D3601);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3601, new int[] { D3601 });
            UpdatePos(0);
        }
        private void UpdatePos(int Pos)
        {
            var converter = new BrushConverter();
            Button[] btnList = new Button[] {
                btnBendingPos,
                btnReadyPos,
                btnQrPos,
                btnQrScap,
                //btnMesScap,
                btnMatingPos };

            List<bool> AxisPos = new List<bool> {
                BendingPos,
                ReadyPos, QrPos,
                QrScrapPos,
                MesScapPos,
                MatingPos };

            int[] DataYaxis = new int[] {
                Robot.TeachDataYaxis.BendingPos,
                Robot.TeachDataYaxis.readyPos,
                Robot.TeachDataYaxis.QrPos,
                Robot.TeachDataXaxis.QrScapPos,
                Robot.TeachDataYaxis.MesScapPos,
                Robot.TeachDataYaxis.MatingPos };

            int[] DataSpeedYaxis = new int[] {
                Robot.TeachSpeedYaxis.BendingPos,
                Robot.TeachSpeedYaxis.readyPos,
                Robot.TeachSpeedYaxis.QrPos,
                Robot.TeachSpeedYaxis.QrScapPos,
                Robot.TeachSpeedYaxis.MesScapPos,
                Robot.TeachSpeedYaxis.MatingPos };

            for (int i = 0; i < 5; i++)
            {
                btnList[i].Background = (Brush)converter.ConvertFromString("#EEEEEE");
                btnList[i].Foreground = Brushes.DarkBlue;
            }

            btnList[Pos].Background = (Brush)converter.ConvertFromString("#E65305");
            btnList[Pos].Foreground = Brushes.White;
            CurrentPositionPosXData = DataYaxis[Pos];


        }
        #endregion

        private void PLCConnected()
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 7000, 900, out D_7000_7900);
            UiManager.PLC.ReadDoubleWord(DeviceCode.D, 3848, out D3848);
            UiManager.PLC.ReadMultiBits(DeviceCode.M, 0, 1000, out M_0_1000);
            upDateDataTeach();
        }
        private void upDateDataTeach()
        {

            this.Dispatcher.Invoke(() =>
            {

                //  tray ch1 pos
                txtBendingPosAxisX.Text = ((double)D_7000_7900[4] / 1000).ToString();
                txtReadyPosAxisX.Text = ((double)D_7000_7900[10] / 1000).ToString();
                txtQrPosAxisX.Text = ((double)D_7000_7900[16] / 1000).ToString();
                //
                txtQrScapPosAxisX.Text = ((double)D_7000_7900[22] / 1000).ToString();

                //  tray ch1 speed
                txtBendingPosAxisZ.Text = ((double)D_7000_7900[6] / 1000).ToString();
                txtReadyPosAxisZ.Text = ((double)D_7000_7900[12] / 1000).ToString();
                txtQrPosAxisZ.Text = ((double)D_7000_7900[18] / 1000).ToString();
                //
                txtQrScapPosAxisZ.Text = ((double)D_7000_7900[24] / 1000).ToString();


                this.txtPositionDataX1.Text = ((double)D_7000_7900[0] / 1000).ToString();

                //Nam Update Data Robot
                this.txtPositionRobot_X_CH1.Text = ((double)D_7000_7900[400] / 1000).ToString();
                this.txtPositionRobot_Y_CH1.Text = ((double)D_7000_7900[402] / 1000).ToString();
                this.txtPositionRobot_Z_CH1.Text = ((double)D_7000_7900[404] / 1000).ToString();
                this.txtPositionRobot_R_CH1.Text = ((double)D_7000_7900[406] / 1000).ToString();
                this.txtSpeedCurrentRobot_CH1.Text = ((double)D_7000_7900[408] / 1000).ToString();
                this.txtSetSpeedRobot1_CH1.Value = ((int)D3848);
            });
        }
        private void TxtBendingPosAxisX_TouchDown(object sender, TouchEventArgs e)
        {
            Process.Start(@"C:\Windows\System32\osk.exe");
        }
        private void UpdateUIData()
        {
            if (!UiManager.PLC.isOpen())
                return;
            var converter = new BrushConverter();
            //Motor Control
            string testA = ((double)D_7000_7900[0] / 1000).ToString();
            //string b = ((double)D32[(7000 - 7000) / 2] / 10000).ToString();
            string testB = testA;
            this.Dispatcher.Invoke(() =>
            {

                ActualPositionX1.Text = testA;
                txtCurrentPositionXJog1.Text = testA;


                //Current Position
                if (BendingPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[4] / 1000).ToString();
                }
                else if (ReadyPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[10] / 1000).ToString();
                }
                else if (QrPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[16] / 1000).ToString();
                }
                else if (QrScrapPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[22] / 1000).ToString();
                }
                else if (MesScapPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[28] / 1000).ToString();
                }

                int a = D_7000_7900[500];
                // Convert D7500 -> D7500
                if (TabControl.SelectedIndex == 0)
                {
                    if (M_0_1000[270])
                        btnXAxisHome1.Background = Brushes.Cyan;
                    else
                        btnXAxisHome1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    if (M_0_1000[271])
                        btnZAxisHome1.Background = Brushes.Cyan;
                    else
                        btnZAxisHome1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    if (M_0_1000[203])
                    {
                        btnXServoOn1.Background = Brushes.Cyan;
                        btnXServoOff1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        rectXServOff1.Fill = Brushes.DarkRed;
                        rectXServoOn1.Fill = Brushes.Gray;
                    }

                    else
                    {
                        btnXServoOff1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnXServoOn1.Background = Brushes.Cyan;
                        rectXServOff1.Fill = Brushes.Gray;
                        rectXServoOn1.Fill = Brushes.DarkRed;
                    }
                    if (M_0_1000[204])
                    {
                        //btnZServoOn1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        //btnZServoOff1.Background = Brushes.Cyan;
                        //rectZServoOn1.Fill = Brushes.Gray;
                        //rectZServoOff1.Fill = Brushes.DarkRed;
                    }

                    else
                    {
                        //btnZServoOn1.Background = Brushes.Cyan;
                        //btnZServoOff1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        //rectZServoOff1.Fill = Brushes.Gray;
                        //rectZServoOn1.Fill = Brushes.DarkRed;
                    }
                    //OUT MAGAZINE


                }

                //Robot
                if (TabControl.SelectedIndex == 1)
                {
                    // IN Iray
                    if (M_0_1000[620] && M_0_1000[622])
                    {
                        rectCenHoldTray121.Fill = Brushes.DarkRed;
                        rectCenHoldTray12L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[621] && M_0_1000[623])
                    {
                        rectCenHoldTray12L1.Fill = Brushes.DarkRed;
                        rectCenHoldTray121.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[624] && M_0_1000[626])
                    {
                        rectCenHoldTray341.Fill = Brushes.DarkRed;
                        rectCenHoldTray34L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[625] && M_0_1000[627])
                    {
                        rectCenHoldTray34L1.Fill = Brushes.DarkRed;
                        rectCenHoldTray341.Fill = Brushes.Gray;
                    }

                    if (M_0_1000[640] && M_0_1000[642])
                    {
                        rectSplitTray121.Fill = Brushes.DarkRed;
                        rectSplitTray12L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[641] && M_0_1000[643])
                    {
                        rectSplitTray12L1.Fill = Brushes.DarkRed;
                        rectSplitTray121.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[644] && M_0_1000[646])
                    {
                        rectSplitTray341.Fill = Brushes.DarkRed;
                        rectSplitTray34L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[645] && M_0_1000[647])
                    {
                        rectSplitTray34L1.Fill = Brushes.DarkRed;
                        rectSplitTray341.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[662])
                    {
                        rectCylPosHight1.Fill = Brushes.DarkRed;
                        rectCylPosCen1.Fill = Brushes.Gray;
                        rectCylPosLow1.Fill = Brushes.Gray;

                    }
                    if (M_0_1000[661])
                    {
                        rectCylPosHight1.Fill = Brushes.Gray;
                        rectCylPosCen1.Fill = Brushes.DarkRed;
                        rectCylPosLow1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[660])
                    {
                        rectCylPosHight1.Fill = Brushes.Gray;
                        rectCylPosCen1.Fill = Brushes.Gray;
                        rectCylPosLow1.Fill = Brushes.DarkRed;
                    }
                    // Out Tray
                    if (M_0_1000[720])
                    {
                        rectCylUDOutTrayL1.Fill = Brushes.DarkRed;
                        rectCylUDOutTray1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[721])
                    {
                        rectCylUDOutTray1.Fill = Brushes.DarkRed;
                        rectCylUDOutTrayL1.Fill = Brushes.Gray;
                    }
                    // OUTPUT      intray      
                    if (M_0_1000[820])
                    {
                        btnCenHoldTray121.Background = Brushes.Cyan;
                        btnCenHoldTray12L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnCenHoldTray12L1.Background = Brushes.Cyan;
                        btnCenHoldTray121.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[821])
                    {
                        btnCenHoldTray341.Background = Brushes.Cyan;
                        btnCenHoldTray34L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnCenHoldTray34L1.Background = Brushes.Cyan;
                        btnCenHoldTray341.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[822])
                    {
                        btnSplitTray121.Background = Brushes.Cyan;
                        btnSplitTray12L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnSplitTray12L1.Background = Brushes.Cyan;
                        btnSplitTray121.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[823])
                    {
                        btnSplitTray341.Background = Brushes.Cyan;
                        btnSplitTray34L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnSplitTray34L1.Background = Brushes.Cyan;
                        btnSplitTray341.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }

                    if (M_0_1000[830] && M_0_1000[831])
                    {
                        btnCylPosHight1.Background = Brushes.Cyan;
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[830] && !M_0_1000[831])
                    {
                        btnCylPosCen1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (!M_0_1000[830] && !M_0_1000[831])
                    {
                        btnCylPosLow1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    // output out tray

                    if (M_0_1000[850])
                    {
                        btnCylUDOutTray1.Background = Brushes.Cyan;
                        btnCylUDOutTrayL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnCylUDOutTrayL1.Background = Brushes.Cyan;
                        btnCylUDOutTray1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }

                }

                //Bending / Mating
                if (TabControl.SelectedIndex == 2)
                {
                    // Center Tray

                    if (M_0_1000[675] && M_0_1000[677])
                    {
                        rectClampPullTray1.Fill = Brushes.DarkRed;
                        rectClampPullTrayL1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[674] && M_0_1000[676])
                    {
                        rectClampPullTrayL1.Fill = Brushes.DarkRed;
                        rectClampPullTray1.Fill = Brushes.Gray;
                    }

                    if (M_0_1000[703] && M_0_1000[705])
                    {
                        rectKeepCenterTray1.Fill = Brushes.DarkRed;
                        rectKeepCenterTrayL1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[702] && M_0_1000[704])
                    {
                        rectKeepCenterTrayL1.Fill = Brushes.DarkRed;
                        rectKeepCenterTray1.Fill = Brushes.Gray;
                    }

                    if (M_0_1000[713])
                    {
                        rectCenterTray1.Fill = Brushes.DarkRed;
                        rectCenterTrayL1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[712])
                    {
                        rectCenterTrayL1.Fill = Brushes.DarkRed;
                        rectCenterTray1.Fill = Brushes.Gray;
                    }
                    // OUTPUT CENTER TRAY

                    if (M_0_1000[834])
                    {
                        btnClampPullTray1.Background = Brushes.Cyan;
                        btnClampPullTrayL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnClampPullTrayL1.Background = Brushes.Cyan;
                        btnClampPullTray1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[844])
                    {
                        btnKeepCenterTray1.Background = Brushes.Cyan;
                        btnKeepCenterTrayL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnKeepCenterTrayL1.Background = Brushes.Cyan;
                        btnKeepCenterTray1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[846])
                    {
                        btnCenterTray1.Background = Brushes.Cyan;
                        btnCenterTrayL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnCenterTrayL1.Background = Brushes.Cyan;
                        btnCenterTray1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                }
                return;
                //CheckPlcErr();
            });

            return;


        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]");
            //Boolean Huysukien = regex.IsMatch(e.Text);
            //e.Handled = Huysukien;
        }
        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {

            UserManager.createUserLog(UserActions.PAGE_TEACHING_MENU_MANUAL);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_MANUAL2);
        }
        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {

            UserManager.createUserLog(UserActions.PAGE_TEACHING_MENU_MANUAL);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_MANUAL1);
        }
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_TEACHING_MENU_MANUAL);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_JIG_SETUP);
        }
        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            if (UserManager.IsLogOn() == 2 || UserManager.IsLogOn() == 1)
            {
                UserManager.createUserLog(UserActions.PAGE_TEACHING_MENU);
                UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU);
            }

        }
    }
}
