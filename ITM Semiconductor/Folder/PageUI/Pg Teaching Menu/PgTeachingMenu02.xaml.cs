using DTO;
using FluentFTP.Helpers;
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

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgTeachingMenu02.xaml
    /// </summary>
    public partial class PgTeachingMenu02 : Page
    {
        MyLogger logger = new MyLogger("PG Teaching manual 1");
        MotorParameter Robot = UiManager.appSettings.Robot;
        private int D4648;

        private List<int> D_7000_7900 = new List<int>();
        private List<bool> M_0_1000 = new List<bool>();

        private bool BendingPos = false;
        private bool ReadyPos = false;
        private bool QrPos = false;
        private bool QrScrapPos = false;
        private bool MesScapPos = false;
        private bool MatingPos = false;

        int CurrentPositionPosXData = 0;
        private CancellationTokenSource canceTokenSource;
        public PgTeachingMenu02()
        {
            InitializeComponent();


            this.Loaded += PgTeachingMenu02_Loaded;
            this.Unloaded += PgTeachingMenu02_Unloaded;

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
            this.txtSetSpeedRobot2.TouchDown += TxtSetSpeedRobot2_TouchDown;

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
            this.btnJogXFw1.PreviewMouseDown += BtnJogXFw1_PreviewMouseDown;
            this.btnJogXFw1.PreviewMouseUp += BtnJogXFw1_PreviewMouseUp;

            this.btnJogXRv1.PreviewTouchDown += btnJogXRv_MouseEnter;
            this.btnJogXRv1.PreviewTouchUp += btnJogXRv_MouseLeft;
            this.btnJogXRv1.PreviewMouseDown += BtnJogXRv1_PreviewMouseDown;
            this.btnJogXRv1.PreviewMouseUp += BtnJogXRv1_PreviewMouseUp;

            this.btSave1.PreviewTouchDown += btSave_TouchDown;
            this.btSave1.Click += btSave_Clicked;

            this.JogXSpeed.ValueChanged += JogXSpeed_ValueChanged;
            this.btnXMoveX1.PreviewTouchDown += btnXMoveX_Clicked;

            this.btnPickPlaceOneCycle1.PreviewTouchDown += btnPickPlaceOneCycle_Clicked;
            this.btnXServoOn1.PreviewTouchDown += btnXServoOn_Clicked;
            this.btnXServoOff1.PreviewTouchDown += btnXServoOff_Clicked;

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
                    UiManager.PLC.ReadMultiBits(DeviceCode.M, 0, 1000, out M_0_1000);
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

        #region Center Tray 
        private void btnCenterTrayL1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1254, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1255, true);

            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1254);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1255);
        }
        private void btnKeepCenterTrayL1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1250, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1251, true);

            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1250);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1251);
        }
        private void btnClampPullTrayL1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1240, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1241, true);

            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1240);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1241);
        }
        private void btnCenterTray1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1255, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1254, true);

            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1255);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1254);
        }
        private void btnKeepCenterTray1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1251, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1250, true);

            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1251);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1250);
        }
        private void btnClampPullTray1_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1241, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1240, true);

            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1241);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1240);
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

        #region In Tray
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
            //e.Handled = true;
            //if (!plcComm.IsConnected())
            //{
            //    displayAlarm(AlarmInfo.Err_Sol_Hight_Cyl_Split_Tray_01_02_Ch1);
            //    return;
            //}
            await Task.Delay(0);
            ////M72 Off
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 72);
            //UpdateJog(70);
            //UpdateJog(71);
            //UpdateJog(72);
            //UpdateJog(73);
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
                return;
            }
            //M80
            if (BendingPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1087, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1287);
            else if (ReadyPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1088, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1288);
            else if (QrPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1089, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1289);
            else if (QrScrapPos)
                UiManager.PLC.WriteBit(DeviceCode.M, 1118, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1318);
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
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 2560, speed);
            //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 2560, new int[] { speed });
        }
        private void btSave_Clicked(object sender, RoutedEventArgs e)
        {

            int D3300, D3302, D3306, D3308, D3312, D3314, D3318, D3320;
            int D4648;
            try
            {


                D3300 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisX.Text), 4) * 1000);
                D3302 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisZ.Text), 4) * 1000);

                // point 2

                D3306 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisX.Text), 4) * 1000);
                D3308 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisZ.Text), 4) * 1000);

                // point 3

                D3312 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisX.Text), 4) * 1000);
                D3314 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisZ.Text), 4) * 1000);

                // point 4

                D3318 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisX.Text), 4) * 1000);
                D3320 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisZ.Text), 4) * 1000);

                // point 5

                //D3324 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisX.Text), 4) * 1000);
                //D3326 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisZ.Text), 4) * 1000);

                //Nam Add Speed Robot
                D4648 = Convert.ToInt32(Math.Round(double.Parse(this.txtSetSpeedRobot2.Text), 4));

            }
            catch (Exception ex)
            {

                logger.Create(ex.Message, LogLevel.Error);
                return;
            }
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            try
            {
                UserManager.createUserLog(UserActions.MN_TEACHING_JIG_SAVE);

                if (MessageBox.Show("Are you sure to save data position?", "NOTE",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)

                {
                    UiManager.PLC.WriteWord(DeviceCode.D, 4648, D4648);
                    /* plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 4648, new int[] { D4648 }); */ //Nam Add Write Speed Robot

                    if (BendingPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3300, D3300);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3302, D3302);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);


                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3300, new int[] { D3300 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3302, new int[] { D3302 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
                    }
                    else if (ReadyPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3306, D3306);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3308, D3308);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3306, new int[] { D3306 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3308, new int[] { D3308 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
                    }
                    else if (QrPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3312, D3312);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3314, D3314);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3312, new int[] { D3312 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3314, new int[] { D3314 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
                    }
                    else if (QrScrapPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3318, D3318);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3320, D3320);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3318, new int[] { D3318 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3320, new int[] { D3320 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Create("BtJigSave_Click error:" + ex.Message, LogLevel.Error);
            }
        }
        private void btSave_TouchDown(object sender, TouchEventArgs e)
        {

            int D3300, D3302, D3306, D3308, D3312, D3314, D3318, D3320;
            int D4648;
            try
            {


                D3300 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisX.Text), 4) * 1000);
                D3302 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisZ.Text), 4) * 1000);

                // point 2

                D3306 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisX.Text), 4) * 1000);
                D3308 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisZ.Text), 4) * 1000);

                // point 3

                D3312 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisX.Text), 4) * 1000);
                D3314 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisZ.Text), 4) * 1000);

                // point 4

                D3318 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisX.Text), 4) * 1000);
                D3320 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisZ.Text), 4) * 1000);

                // point 5

                //D3324 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisX.Text), 4) * 1000);
                //D3326 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisZ.Text), 4) * 1000);

                //Nam Add Speed Robot
                D4648 = Convert.ToInt32(Math.Round(double.Parse(this.txtSetSpeedRobot2.Text), 4));

            }
            catch
            {

                return;
            }
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            try
            {
                UserManager.createUserLog(UserActions.MN_TEACHING_JIG_SAVE);

                if (MessageBox.Show("Are you sure to save data position?", "NOTE",
                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)

                {
                    UiManager.PLC.WriteWord(DeviceCode.D, 4648, D4648);
                    /* plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 4648, new int[] { D4648 }); */ //Nam Add Write Speed Robot

                    if (BendingPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3300, D3300);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3302, D3302);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3300, new int[] { D3300 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3302, new int[] { D3302 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
                    }
                    else if (ReadyPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3306, D3306);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3308, D3308);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3306, new int[] { D3306 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3308, new int[] { D3308 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
                    }
                    else if (QrPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3312, D3312);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3314, D3314);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3312, new int[] { D3312 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3314, new int[] { D3314 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
                    }
                    else if (QrScrapPos)
                    {
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3318, D3318);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3320, D3320);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1315, true);

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3318, new int[] { D3318 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3320, new int[] { D3320 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1315);
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
            UiManager.PLC.WriteBit(DeviceCode.M, 1086, false);
            //M71 Off: JogX - 
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1286);
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
            UiManager.PLC.WriteBit(DeviceCode.M, 1086, true);
            //M71: JogX - 
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1286);
        }
        private void BtnJogXRv1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1086, false);
        }
        private void BtnJogXRv1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1086, true);
        }
        private async void btnJogXFw_MouseLeft(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            await Task.Delay(0);
            UiManager.PLC.WriteBit(DeviceCode.M, 1085, false);
            //M70 Off
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1285);
            UpdateJog(70);
        }
        private void BtnJogXFw1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1085, false);
        }
        private void BtnJogXFw1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1085, true);
            //M70 ON: JOGX+
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1285);
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

        private async void btnJogXFw_MouseEnter(object sender, TouchEventArgs e)
        {
            e.Handled = true;
            if (!UiManager.PLC.isOpen())
            {
                return;
            }

            await Task.Delay(0);
            UiManager.PLC.WriteBit(DeviceCode.M, 1085, true);
            //M70 ON: JOGX+
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1285);
        }
        private void btnZAxisHome_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 76, true);
            //M76
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 76);
        }
        private void btnXAxisHome_Clicked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            UiManager.PLC.WriteBit(DeviceCode.M, 1304, true);
            //M74
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1304);
        }
        private void btnAllAxisHome_Clciked(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            //M80
            UiManager.PLC.WriteBit(DeviceCode.M, 1305, true);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1305);
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
            int D3602 = 6;
            UiManager.PLC.WriteWord(DeviceCode.D, 3602, D3602);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3602, new int[] { D3602 });
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
            int D3602 = 4;
            UiManager.PLC.WriteWord(DeviceCode.D, 3602, D3602);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3602, new int[] { D3602 });
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
            int D3602 = 3;
            UiManager.PLC.WriteWord(DeviceCode.D, 3602, D3602);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3602, new int[] { D3602 });
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
            int D3602 = 2;
            UiManager.PLC.WriteWord(DeviceCode.D, 3602, D3602);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3602, new int[] { D3602 });
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
            int D3602 = 1;
            UiManager.PLC.WriteWord(DeviceCode.D, 3602, D3602);
            //plcComm.WriteSingleWord(MCProtocol.DeviceCode.D, 3602, new int[] { D3602 });
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

            int[] DataY1axis = new int[] {
                Robot.TeachDataYaxis1.BendingPos,
                Robot.TeachDataYaxis1.readyPos,
                Robot.TeachDataYaxis1.QrPos,
                Robot.TeachDataYaxis1.QrScapPos,
                Robot.TeachDataYaxis1.MesScapPos,
                Robot.TeachDataYaxis1.MatingPos };

            int[] DataSpeedY1axis = new int[] {
                Robot.TeachSpeedYaxis1.BendingPos,
                Robot.TeachSpeedYaxis1.readyPos,
                Robot.TeachSpeedYaxis1.QrPos,
                Robot.TeachSpeedYaxis1.QrScapPos,
                Robot.TeachSpeedYaxis1.MesScapPos,
                Robot.TeachSpeedYaxis1.MatingPos };

            for (int i = 0; i < 5; i++)
            {
                btnList[i].Background = (Brush)converter.ConvertFromString("#EEEEEE");
                btnList[i].Foreground = Brushes.DarkBlue;
            }

            btnList[Pos].Background = (Brush)converter.ConvertFromString("#E65305");
            btnList[Pos].Foreground = Brushes.White;
            CurrentPositionPosXData = DataY1axis[Pos];
            //CurrentPositionPosZData = DataZaxis[Pos];

        }
        #endregion

        #region Load / Unload
        private void PgTeachingMenu02_Unloaded(object sender, RoutedEventArgs e)
        {

            this.canceTokenSource?.Cancel();
        }
        private async void PgTeachingMenu02_Loaded(object sender, RoutedEventArgs e)
        {
            await StartTask();
            this.PLCConnected();
        }
        private void PLCConnected()
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 7000, 900, out D_7000_7900);
            UiManager.PLC.ReadDoubleWord(DeviceCode.D, 4648, out D4648);
            UiManager.PLC.ReadMultiBits(DeviceCode.M, 0, 1000, out M_0_1000);
            upDateDataTeach();
        }
        private void upDateDataTeach()
        {

            this.Dispatcher.Invoke(() =>
            {

                //  tray ch1 pos
                txtBendingPosAxisX.Text = ((double)D_7000_7900[84] / 1000).ToString();
                txtReadyPosAxisX.Text = ((double)D_7000_7900[90] / 1000).ToString();
                txtQrPosAxisX.Text = ((double)D_7000_7900[96] / 1000).ToString();
                ////
                txtQrScapPosAxisX.Text = ((double)D_7000_7900[102] / 1000).ToString();
                //  tray ch1 speed
                txtBendingPosAxisZ.Text = ((double)D_7000_7900[86] / 1000).ToString();
                txtReadyPosAxisZ.Text = ((double)D_7000_7900[92] / 1000).ToString();
                txtQrPosAxisZ.Text = ((double)D_7000_7900[98] / 1000).ToString();
                ////
                txtQrScapPosAxisZ.Text = ((double)D_7000_7900[104] / 1000).ToString();

                this.txtPositionDataX1.Text = ((double)D_7000_7900[80] / 1000).ToString();

                ////Nam Update Data Robot
                this.txtPositionRobot_X.Text = ((double)D_7000_7900[410] / 1000).ToString();
                this.txtPositionRobot_Y.Text = ((double)D_7000_7900[412] / 1000).ToString();
                this.txtPositionRobot_Z.Text = ((double)D_7000_7900[414] / 1000).ToString();
                this.txtPositionRobot_R.Text = ((double)D_7000_7900[416] / 1000).ToString();
                this.txtSpeedCurrentRobot.Text = ((double)D_7000_7900[418] / 1000).ToString();
                this.txtSetSpeedRobot2.Value = (int)D4648;
            });
        }
        private void UpdateUIData()
        {
            if (!UiManager.PLC.isOpen())
                return;
            var converter = new BrushConverter();
            //Motor Control
            this.Dispatcher.Invoke(() =>
            {
                ActualPositionX1.Text = ((double)D_7000_7900[80] / 1000).ToString();

                txtCurrentPositionXJog1.Text = ((double)D_7000_7900[80] / 1000).ToString();


                //Current Position
                if (BendingPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[84] / 1000).ToString();
                    //txtPositionDataZ1.Text = ((double)D32[3400 / 2] / 10000).ToString();
                }
                else if (ReadyPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[90] / 1000).ToString();
                    //txtPositionDataZ1.Text = ((double)D32[3406 / 2] / 10000).ToString();
                }
                else if (QrPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[96] / 1000).ToString();
                    //txtPositionDataZ1.Text = ((double)D32[3412 / 2] / 10000).ToString();
                }
                else if (QrScrapPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[102] / 1000).ToString();
                    //txtPositionDataZ1.Text = ((double)D32[3418 / 2] / 10000).ToString();
                }
                else if (MesScapPos)
                {
                    txtPositionDataX1.Text = ((double)D_7000_7900[108] / 1000).ToString();
                    //txtPositionDataZ1.Text = ((double)D32[3424 / 2] / 10000).ToString();
                }
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
                    /// IN Iray
                    if (M_0_1000[630] && M_0_1000[632])
                    {
                        rectCenHoldTray121.Fill = Brushes.DarkRed;
                        rectCenHoldTray12L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[631] && M_0_1000[633])
                    {
                        rectCenHoldTray12L1.Fill = Brushes.DarkRed;
                        rectCenHoldTray121.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[634] && M_0_1000[636])
                    {
                        rectCenHoldTray341.Fill = Brushes.DarkRed;
                        rectCenHoldTray34L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[635] && M_0_1000[637])
                    {
                        rectCenHoldTray34L1.Fill = Brushes.DarkRed;
                        rectCenHoldTray341.Fill = Brushes.Gray;
                    }

                    if (M_0_1000[650] && M_0_1000[652])
                    {
                        rectSplitTray121.Fill = Brushes.DarkRed;
                        rectSplitTray12L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[651] && M_0_1000[653])
                    {
                        rectSplitTray12L1.Fill = Brushes.DarkRed;
                        rectSplitTray121.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[654] && M_0_1000[656])
                    {
                        rectSplitTray341.Fill = Brushes.DarkRed;
                        rectSplitTray34L1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[655] && M_0_1000[657])
                    {
                        rectSplitTray34L1.Fill = Brushes.DarkRed;
                        rectSplitTray341.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[665])
                    {
                        rectCylPosHight1.Fill = Brushes.DarkRed;
                        rectCylPosCen1.Fill = Brushes.Gray;
                        rectCylPosLow1.Fill = Brushes.Gray;

                    }
                    if (M_0_1000[664])
                    {
                        rectCylPosHight1.Fill = Brushes.Gray;
                        rectCylPosCen1.Fill = Brushes.DarkRed;
                        rectCylPosLow1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[663])
                    {
                        rectCylPosHight1.Fill = Brushes.Gray;
                        rectCylPosCen1.Fill = Brushes.Gray;
                        rectCylPosLow1.Fill = Brushes.DarkRed;
                    }
                    // Out Tray
                    if (M_0_1000[722])
                    {
                        rectCylUDOutTrayL1.Fill = Brushes.DarkRed;
                        rectCylUDOutTray1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[723])
                    {
                        rectCylUDOutTray1.Fill = Brushes.DarkRed;
                        rectCylUDOutTrayL1.Fill = Brushes.Gray;
                    }
                    // Center Tray

                    if (M_0_1000[681] && M_0_1000[683])
                    {
                        rectClampPullTray1.Fill = Brushes.DarkRed;
                        rectClampPullTrayL1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[680] && M_0_1000[682])
                    {
                        rectClampPullTrayL1.Fill = Brushes.DarkRed;
                        rectClampPullTray1.Fill = Brushes.Gray;
                    }

                    if (M_0_1000[707] && M_0_1000[711])
                    {
                        rectKeepCenterTray1.Fill = Brushes.DarkRed;
                        rectKeepCenterTrayL1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[706] && M_0_1000[710])
                    {
                        rectKeepCenterTrayL1.Fill = Brushes.DarkRed;
                        rectKeepCenterTray1.Fill = Brushes.Gray;
                    }

                    if (M_0_1000[715])
                    {
                        rectCenterTray1.Fill = Brushes.DarkRed;
                        rectCenterTrayL1.Fill = Brushes.Gray;
                    }
                    if (M_0_1000[714])
                    {
                        rectCenterTrayL1.Fill = Brushes.DarkRed;
                        rectCenterTray1.Fill = Brushes.Gray;
                    }


                    /// OUTPUT      intray      
                    if (M_0_1000[824])
                    {
                        btnCenHoldTray121.Background = Brushes.Cyan;
                        btnCenHoldTray12L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnCenHoldTray12L1.Background = Brushes.Cyan;
                        btnCenHoldTray121.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[825])
                    {
                        btnCenHoldTray341.Background = Brushes.Cyan;
                        btnCenHoldTray34L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnCenHoldTray34L1.Background = Brushes.Cyan;
                        btnCenHoldTray341.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[826])
                    {
                        btnSplitTray121.Background = Brushes.Cyan;
                        btnSplitTray12L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnSplitTray12L1.Background = Brushes.Cyan;
                        btnSplitTray121.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[827])
                    {
                        btnSplitTray341.Background = Brushes.Cyan;
                        btnSplitTray34L1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnSplitTray34L1.Background = Brushes.Cyan;
                        btnSplitTray341.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }

                    if (M_0_1000[832] && M_0_1000[833])
                    {
                        btnCylPosHight1.Background = Brushes.Cyan;
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (M_0_1000[832] && !M_0_1000[833])
                    {
                        btnCylPosCen1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    if (!M_0_1000[832] && !M_0_1000[833])
                    {
                        btnCylPosLow1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    // output out tray

                    if (M_0_1000[851])
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
                    // OUTPUT CENTER TRAY

                    if (M_0_1000[835])
                    {
                        btnClampPullTray1.Background = Brushes.Cyan;
                        btnClampPullTrayL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnClampPullTrayL1.Background = Brushes.Cyan;
                        btnClampPullTray1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[845])
                    {
                        btnKeepCenterTray1.Background = Brushes.Cyan;
                        btnKeepCenterTrayL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnKeepCenterTrayL1.Background = Brushes.Cyan;
                        btnKeepCenterTray1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[847])
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
                //CheckPlcErr();
            });




        }
        private void TxtSetSpeedRobot2_TouchDown(object sender, TouchEventArgs e)
        {
            Process.Start(@"C:\Windows\System32\osk.exe");
        }
        private void TxtBendingPosAxisX_TouchDown(object sender, TouchEventArgs e)
        {
            Process.Start(@"C:\Windows\System32\osk.exe");
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]");
            //Boolean Huysukien = regex.IsMatch(e.Text);
            //e.Handled = Huysukien;
        }
        #endregion

        #region Button Page
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_TEACHING_MENU_MANUAL);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU_JIG_SETUP);
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
        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            if (UserManager.IsLogOn() == 2 || UserManager.IsLogOn() == 1)
            {
                UserManager.createUserLog(UserActions.PAGE_TEACHING_MENU);
                UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU);
            }

        }
        #endregion
    }
}
