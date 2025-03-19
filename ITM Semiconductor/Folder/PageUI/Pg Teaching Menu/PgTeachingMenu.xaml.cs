using DTO;
using KeyPad;
using Mitsubishi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
    /// Interaction logic for PgTeachingMenu.xaml
    /// </summary>
    public partial class PgTeachingMenu : Page
    {
        MotorParameter Robot = UiManager.appSettings.Robot;
        MyLogger logger = new MyLogger("PG_TeachingMenu");

        private bool BendingPos = false;
        private bool ReadyPos = false;
        private bool QrPos = false;
        private bool QrScrapPos = false;
        private bool MesScapPos = false;
        private bool MatingPos = false;
        private bool BendingPos1 = false;
        private bool ReadyPos1 = false;
        private bool QrPos1 = false;
        private bool QrScrapPos1 = false;
        private bool MesScapPos1 = false;
        private bool MatingPos1 = false;
        int CurrentPositionPosXData = 0;
        int CurrentPositionPosZData = 0;

        private CancellationTokenSource canceTokenSource;


        private List<int> D_7000_7900 = new List<int>();

        private List<bool> M_0_1000 = new List<bool>();
        private List<bool> M_1000_2000 = new List<bool>();
        private List<bool> M_2000_3000 = new List<bool>();
        private List<bool> M_3000_4000 = new List<bool>();
        private List<bool> M_4000_5000 = new List<bool>();
        private List<bool> M_5000_6000 = new List<bool>();
        private List<bool> M_6000_7000 = new List<bool>();
        private List<bool> M_7000_8000 = new List<bool>();

        public PgTeachingMenu()
        {
            InitializeComponent();
            this.Loaded += PgTeachingMenu_Loaded;
            this.Unloaded += PgTeachingMenu_Unloaded;

            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click1;

            #region Position Selection
            this.btnBendingPos.PreviewTouchDown += BtnBendingPos_Click;
            this.btnBendingPos.Click += BtnBendingPos_Click;

            this.btnReadyPos.PreviewTouchDown += ReadyPos_Clciked;
            this.btnReadyPos.Click += ReadyPos_Clciked;

            this.btnQrPos.Click += QrPos_CLicked;
            this.btnQrScap.Click += QrScrapPos_Clicked;
            this.btnMesScap.Click += MesScapPos_Clicked;

            this.btnBendingPos1.PreviewTouchDown += BendingPos1_Clicked;
            this.btnBendingPos1.Click += BendingPos1_Clicked;

            this.btnReadyPos1.PreviewTouchDown += ReadyPos1_Clciked;
            this.btnReadyPos1.Click += ReadyPos1_Clciked;

            this.btnQrPos1.Click += QrPos1_CLicked;
            this.btnQrScap1.Click += QrScrapPos1_Clicked;
            this.btnMesScap1.Click += MesScapPos1_Clicked;

            #endregion

            #region Motor Control
            this.btnAllAxisHome.PreviewTouchDown += BtnAllAxisHome_PreviewTouchDown;

            this.btnXAxisHome.PreviewTouchDown += BtnXAxisHome_PreviewTouchDown;
            this.btnZAxisHome.PreviewTouchDown += BtnZAxisHome_PreviewTouchDown;

            this.btnJogXFw.PreviewTouchDown += BtnJogXFw_PreviewTouchDown;
            this.btnJogXFw.PreviewTouchUp += BtnJogXFw_PreviewTouchUp;

            this.btnJogXRv.PreviewTouchDown += BtnJogXRv_PreviewTouchDown;
            this.btnJogXRv.PreviewTouchUp += BtnJogXRv_PreviewTouchUp;

            this.btnJogZFw.PreviewTouchDown += BtnJogZFw_PreviewTouchDown;
            this.btnJogZFw.PreviewTouchUp += BtnJogZFw_PreviewTouchUp;

            this.btnJogZRv.PreviewTouchDown += BtnJogZRv_PreviewTouchDown;
            this.btnJogZRv.PreviewTouchUp += BtnJogZRv_PreviewTouchUp;

            this.btSave.PreviewTouchDown += BtSave_PreviewTouchDown;

            this.JogXSpeed.ValueChanged += JogXSpeed_ValueChanged;
            this.JogZSpeed.ValueChanged += JogZSpeed_ValueChanged;

            this.btnXMoveX.PreviewTouchDown += BtnXMoveX_PreviewTouchDown;
            this.btnPickPlaceOneCycle.PreviewTouchDown += BtnPickPlaceOneCycle_PreviewTouchDown;

            this.btnXServoOn.PreviewTouchDown += BtnXServoOn_PreviewTouchDown;
            this.btnXServoOff.PreviewTouchDown += BtnXServoOff_PreviewTouchDown;

            this.btnZServoOn.PreviewTouchDown += BtnZServoOn_PreviewTouchDown;
            this.btnZServoOff.PreviewTouchDown += BtnZServoOff_PreviewTouchDown;

            this.PreviewTouchUp += PgTeachingMenu_PreviewTouchUp;
            this.btnZMoveZ.PreviewTouchDown += BtnZMoveZ_PreviewTouchDown;

            #endregion

            #region Magazine
            this.btnClampMaga1.Click += BtnClampMaga1_Click;
            this.btnClampMagaL1.Click += BtnClampMagaL1_Click;

            this.btnPushMaga1.Click += BtnPushMaga1_Click;
            this.btnPushMagaL1.Click += BtnPushMagaL1_Click;

            #endregion

            #region Carriage
            this.btnUdCylCarriage.Click += BtnUdCylCarriage_Click;
            this.btnUdCylCarriageL.Click += BtnUdCylCarriageL_Click;
            #endregion

            #region Open file exe
            this.txtBendingPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtReadyPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtReadyPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrScapPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtMesScapPosAxisX.TouchDown += TxtBendingPosAxisX_TouchDown;

            this.txtBendingPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtReadyPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrScapPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrScapPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtMesScapPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;

            this.txtBendingPosAxisX1.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtReadyPosAxisX1.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrPosAxisX1.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrScapPosAxisX1.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtMesScapPosAxisX1.TouchDown += TxtBendingPosAxisX_TouchDown;

            this.txtBendingPosAxisZ.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtReadyPosAxisZ1.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrPosAxisZ1.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtQrScapPosAxisZ1.TouchDown += TxtBendingPosAxisX_TouchDown;
            this.txtMesScapPosAxisZ1.TouchDown += TxtBendingPosAxisX_TouchDown;

            #endregion

        }



        private void PgTeachingMenu_Unloaded(object sender, RoutedEventArgs e)
        {
            this.canceTokenSource?.Cancel();
        }

        public void StartTask()
        {
            canceTokenSource = new CancellationTokenSource();
            Task.Run(() =>
            {

                try
                {
                    while (!canceTokenSource.Token.IsCancellationRequested)
                    {
                        if (!UiManager.PLC.isOpen())
                            return;

                        UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 7000, 900, out D_7000_7900);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 0000, 1000, out M_0_1000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 1000, 1000, out M_1000_2000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 2000, 1000, out M_2000_3000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 3000, 1000, out M_3000_4000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 4000, 1000, out M_4000_5000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 5000, 1000, out M_5000_6000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 6000, 1000, out M_6000_7000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 7000, 1000, out M_7000_8000);
                        UpdateUIData();
                        Thread.Sleep(1);
                    }
                }
                catch (Exception ex)
                {

                    logger.Create("PLC Com Err" + ex.ToString(), LogLevel.Error);
                    return;
                }

            });
        }

        private void PgTeachingMenu_Loaded(object sender, RoutedEventArgs e)
        {
            Task tsk = new Task(() =>
            {
                this.StartTask();
                this.upDateDataTeach();

            });
            tsk.Start();
        }
        private void upDateDataTeach()
        {
            if (!UiManager.PLC.isOpen())
                return;
            this.Dispatcher.Invoke(() =>
            {
                //  transfer cariage pos
                txtBendingPosAxisX.Text = ((double)D_7000_7900[244] / 1000).ToString();
                txtReadyPosAxisX.Text = ((double)D_7000_7900[250] / 1000).ToString();
                txtQrPosAxisX.Text = ((double)D_7000_7900[256] / 1000).ToString();
                txtQrScapPosAxisX.Text = ((double)D_7000_7900[262] / 1000).ToString();

                //  transfer cariage speed
                txtBendingPosAxisZ.Text = ((double)D_7000_7900[246] / 1000).ToString();
                txtReadyPosAxisZ.Text = ((double)D_7000_7900[252] / 1000).ToString();
                txtQrPosAxisZ.Text = ((double)D_7000_7900[258] / 1000).ToString();
                txtQrScapPosAxisZ.Text = ((double)D_7000_7900[264] / 1000).ToString();

                // U/D magazine pos
                txtBendingPosAxisX1.Text = ((double)D_7000_7900[164] / 1000).ToString();
                txtReadyPosAxisX1.Text = ((double)D_7000_7900[170] / 1000).ToString();
                txtQrPosAxisX1.Text = ((double)D_7000_7900[176] / 1000).ToString();
                txtQrScapPosAxisX1.Text = ((double)D_7000_7900[182] / 1000).ToString();
                ////2910
                txtMesScapPosAxisX1.Text = ((double)D_7000_7900[188] / 1000).ToString();


                //  U/D magazine speed
                txtBendingPosAxisZ1.Text = ((double)D_7000_7900[166] / 1000).ToString();
                txtReadyPosAxisZ1.Text = ((double)D_7000_7900[172] / 1000).ToString();
                txtQrPosAxisZ1.Text = ((double)D_7000_7900[178] / 1000).ToString();
                txtQrScapPosAxisZ1.Text = ((double)D_7000_7900[184] / 1000).ToString();
                ////2610
                txtMesScapPosAxisZ1.Text = ((double)D_7000_7900[190] / 1000).ToString();


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
                ActualPositionX.Text = ((double)D_7000_7900[240] / 1000).ToString();
                ActualPositionZ.Text = ((double)D_7000_7900[160] / 1000).ToString();
                txtCurrentPositionXJog.Text = ((double)D_7000_7900[240] / 1000).ToString();
                txtCurrentPositionZJog.Text = ((double)D_7000_7900[160] / 1000).ToString();


                //Current Position
                if (BendingPos)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[244] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[164] / 1000).ToString();
                }
                else if (ReadyPos)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[250] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[170] / 1000).ToString();
                }
                else if (QrPos)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[256] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[176] / 1000).ToString();
                }
                else if (QrScrapPos)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[262] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[182] / 1000).ToString();
                }
                else if (MesScapPos)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[268] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[188] / 1000).ToString();
                }
                else if (BendingPos1)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[244] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[164] / 1000).ToString();
                }
                else if (ReadyPos1)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[250] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[170] / 1000).ToString();
                }
                else if (QrPos1)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[256] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[176] / 1000).ToString();
                }
                else if (QrScrapPos1)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[262] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[182] / 1000).ToString();
                }
                else if (MesScapPos1)
                {
                    txtPositionDataX.Text = ((double)D_7000_7900[268] / 1000).ToString();
                    txtPositionDataZ.Text = ((double)D_7000_7900[188] / 1000).ToString();
                }


                if (TabControl.SelectedIndex == 0)
                {
                    if (M_0_1000[270])
                        btnXAxisHome.Background = Brushes.Cyan;
                    else
                        btnXAxisHome.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    if (M_0_1000[271])
                        btnZAxisHome.Background = Brushes.Cyan;
                    else
                        btnZAxisHome.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    if (M_0_1000[203])
                    {
                        btnXServoOn.Background = Brushes.Cyan;
                        btnXServoOff.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        rectXServOff.Fill = Brushes.DarkRed;
                        rectXServoOn.Fill = Brushes.Gray;
                    }

                    else
                    {
                        btnXServoOff.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnXServoOn.Background = Brushes.Cyan;
                        rectXServOff.Fill = Brushes.Gray;
                        rectXServoOn.Fill = Brushes.DarkRed;
                    }
                    if (M_0_1000[204])
                    {
                        btnZServoOn.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        btnZServoOff.Background = Brushes.Cyan;
                        rectZServoOn.Fill = Brushes.Gray;
                        rectZServoOff.Fill = Brushes.DarkRed;
                    }

                    else
                    {
                        btnZServoOn.Background = Brushes.Cyan;
                        btnZServoOff.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                        rectZServoOff.Fill = Brushes.Gray;
                        rectZServoOn.Fill = Brushes.DarkRed;
                    }

                    //OUT MAGAZINE

                    if (M_0_1000[864])
                    {
                        btnClampMaga1.Background = Brushes.Cyan;
                        btnClampMagaL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnClampMagaL1.Background = Brushes.Cyan;
                        btnClampMaga1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[860] && !M_0_1000[871])
                    {
                        btnPushMaga1.Background = Brushes.Cyan;
                        btnPushMagaL1.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else if (!M_0_1000[860] && M_0_1000[871])
                    {
                        btnPushMagaL1.Background = Brushes.Cyan;
                        btnPushMaga1.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                }

                //Robot
                if (TabControl.SelectedIndex == 1)
                {
                    //carriage
                    if (M_0_1000[870])
                    {
                        btnUdCylCarriage.Background = Brushes.Cyan;
                        btnUdCylCarriageL.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        btnUdCylCarriageL.Background = Brushes.Cyan;
                        btnUdCylCarriage.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    // ss magazine
                    if (M_0_1000[755])
                    {
                        rectClampMaga11.Fill = Brushes.Yellow;
                        rectClampMaga1L1.Fill = Brushes.DarkRed;
                    }
                    if (M_0_1000[754])
                    {
                        rectClampMaga11.Fill = Brushes.DarkRed;
                        rectClampMaga1L1.Fill = Brushes.Yellow;
                    }
                    if (M_0_1000[745])
                    {
                        rectbtnPushMaga1.Fill = Brushes.Yellow;
                        rectbtnPushMagaL1.Fill = Brushes.DarkRed;
                    }
                    if (M_0_1000[744])
                    {
                        rectbtnPushMaga1.Fill = Brushes.DarkRed;
                        rectbtnPushMagaL1.Fill = Brushes.Yellow;
                    }


                    // ss carriage
                    if (M_0_1000[772])
                    {
                        rectUdCylCarriage.Fill = Brushes.Yellow;
                        rectUdCylCarriageL.Fill = Brushes.DarkRed;
                    }
                    if (M_0_1000[771])
                    {
                        rectUdCylCarriage.Fill = Brushes.DarkRed;
                        rectUdCylCarriageL.Fill = Brushes.Yellow;
                    }
                }

                //Bending / Mating
                if (TabControl.SelectedIndex == 2)
                {


                }

            });




        }
        private void TxtBendingPosAxisX_TouchDown(object sender, TouchEventArgs e)
        {
            try
            {
                Process.Start(@"C:\Windows\System32\osk.exe");
            }
            catch (Exception ex)
            {
                logger.Create($"Open Exe +{ex}", LogLevel.Error);
            }


        }

        #region Carriage
        private void BtnUdCylCarriageL_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1278);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1279);

            UiManager.PLC.WriteBit(DeviceCode.M, 1278, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1279, true);
        }

        private void BtnUdCylCarriage_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1279);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1278);

            UiManager.PLC.WriteBit(DeviceCode.M, 1279, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1278, true);
        }
        #endregion

        #region Magazine
        private void BtnPushMagaL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1268);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1269);

            UiManager.PLC.WriteBit(DeviceCode.M, 1268, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1269, true);
        }

        private void BtnPushMaga1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1269);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1268);

            UiManager.PLC.WriteBit(DeviceCode.M, 1269, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1268, true);
        }

        private void BtnClampMagaL1_Click(object sender, RoutedEventArgs e)
        {

            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1273);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1274);

            UiManager.PLC.WriteBit(DeviceCode.M, 1273, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1274, true);
        }

        private void BtnClampMaga1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //plcComm.RstBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1274);
            //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1273);
            UiManager.PLC.WriteBit(DeviceCode.M, 1274, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1273, true);
        }
        #endregion

        #region Motor Control
        private void BtnZMoveZ_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //M80
            if (BendingPos1)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1294);
                UiManager.PLC.WriteBit(DeviceCode.M, 1294, true);
            else if (ReadyPos1)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1312);
                UiManager.PLC.WriteBit(DeviceCode.M, 1312, true);
            else if (QrPos1)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1292);
                UiManager.PLC.WriteBit(DeviceCode.M, 1292, true);
            else if (QrScrapPos1)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1293);
                UiManager.PLC.WriteBit(DeviceCode.M, 1293, true);
            else if (MesScapPos1)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1317);
                UiManager.PLC.WriteBit(DeviceCode.M, 1317, true);

        }

        private void PgTeachingMenu_PreviewTouchUp(object sender, TouchEventArgs e)
        {

        }

        private void BtnZServoOff_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1306, false);
        }

        private void BtnZServoOn_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1271, true);
        }

        private void BtnXServoOff_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1309, true);
        }

        private void BtnXServoOn_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1276, true);
        }

        private void BtnPickPlaceOneCycle_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 202, true);
        }

        private void BtnXMoveX_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
            {

                return;
            }
            //M80
            if (BendingPos)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1299);
                UiManager.PLC.WriteBit(DeviceCode.M, 1299, true);
            else if (ReadyPos)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1297);
                UiManager.PLC.WriteBit(DeviceCode.M, 1297, true);
            else if (QrPos)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1298);
                UiManager.PLC.WriteBit(DeviceCode.M, 1298, true);
            else if (QrScrapPos)
                //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1313);
                UiManager.PLC.WriteBit(DeviceCode.M, 1313, true);

        }

        private void JogZSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int speed = 0;
            try
            {
                speed = Convert.ToInt32(JogZSpeed.Value);
            }
            catch
            {
                return;
            }
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 2860, new int[] { speed });
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 2860, speed);
        }

        private void JogXSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            int speed = 0;
            try
            {
                speed = Convert.ToInt32(JogXSpeed.Value);
            }
            catch
            {
                return;
            }
            if (!UiManager.PLC.isOpen())
            {
                return;
            }
            //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3160, new int[] { speed });
            UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3160, speed);
        }

        private void BtSave_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            int D3500, D3502, D3506, D3508, D3512, D3514, D3518, D3520/*, D3524, D3526*/;
            int D3400, D3402, D3406, D3408, D3412, D3414, D3418, D3420, D3424, D3426;
            try
            {
                // transfer cariage

                // point 1

                D3500 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisX.Text), 4) * 1000);
                D3502 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisZ.Text), 4) * 1000);


                // point 2

                D3506 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisX.Text), 4) * 1000);
                D3508 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisZ.Text), 4) * 1000);


                // point 3

                D3512 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisX.Text), 4) * 1000);
                D3514 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisZ.Text), 4) * 1000);


                // point 4

                D3518 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisX.Text), 4) * 1000);
                D3520 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisZ.Text), 4) * 1000);


                // point 5

                //D3524 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisX.Text), 4) * 10000);
                //D3526 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisZ.Text), 4) * 10000);

                // U/D magazine
                // point 1

                D3400 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisX1.Text), 4) * 1000);
                D3402 = Convert.ToInt32(Math.Round(double.Parse(this.txtBendingPosAxisZ1.Text), 4) * 1000);

                // point 2

                D3406 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisX1.Text), 4) * 1000);
                D3408 = Convert.ToInt32(Math.Round(double.Parse(this.txtReadyPosAxisZ1.Text), 4) * 1000);

                // point 3

                D3412 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisX1.Text), 4) * 1000);
                D3414 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrPosAxisZ1.Text), 4) * 1000);

                // point 4

                D3418 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisX1.Text), 4) * 1000);
                D3420 = Convert.ToInt32(Math.Round(double.Parse(this.txtQrScapPosAxisZ1.Text), 4) * 1000);

                // point 5

                D3424 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisX1.Text), 4) * 1000);
                D3426 = Convert.ToInt32(Math.Round(double.Parse(this.txtMesScapPosAxisZ1.Text), 4) * 1000);





            }
            catch (Exception ex)
            {
                logger.Create("Convert Textbox teaching " + ex.Message, LogLevel.Error);
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
                    if (BendingPos)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3500, new int[] { D3500 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3502, new int[] { D3502 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3500, D3500);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3502, D3502);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (ReadyPos)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3506, new int[] { D3506 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3508, new int[] { D3508 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3506, D3506);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3508, D3508);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (QrPos)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3512, new int[] { D3512 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3514, new int[] { D3514 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3512, D3512);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3514, D3514);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (QrScrapPos)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3518, new int[] { D3518 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3520, new int[] { D3520 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3518, D3518);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3520, D3520);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (MesScapPos)
                    {

                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3424, new int[] { D3424 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3426, new int[] { D3426 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3424, D3424);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3426, D3426);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (BendingPos1)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3400, new int[] { D3400 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3402, new int[] { D3402 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3400, D3400);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3402, D3402);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (ReadyPos1)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3406, new int[] { D3406 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3408, new int[] { D3408 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3406, D3406);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3408, D3408);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (QrPos1)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3412, new int[] { D3412 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3414, new int[] { D3414 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3412, D3412);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3414, D3414);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (QrScrapPos1)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3418, new int[] { D3418 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3420, new int[] { D3420 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3418, D3418);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3420, D3420);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }
                    else if (MesScapPos1)
                    {
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3424, new int[] { D3424 });
                        //plcComm.WriteDoubleWord(MCProtocol.DeviceCode.D, 3426, new int[] { D3426 });
                        //plcComm.SetBit(MCProtocol.DevicePLC.M, MCProtocol.DeviceCode.M, 1316);

                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3424, D3424);
                        UiManager.PLC.WriteDoubleWord(DeviceCode.D, 3426, D3426);
                        UiManager.PLC.WriteBit(DeviceCode.M, 1316, true);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.Create("BtJigSave_Click error:" + ex.Message, LogLevel.Error);
            }
        }


        private void BtnJogZRv_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1091, false);
        }

        private void BtnJogZRv_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1091, true);
        }

        private void BtnJogZFw_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1090, false);
        }

        private void BtnJogZFw_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1090, true);
        }

        private void BtnJogXRv_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1096, false);
        }

        private void BtnJogXRv_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1096, true);
        }

        private void BtnJogXFw_PreviewTouchUp(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1095, false);
        }

        private void BtnJogXFw_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1095, true);
        }

        private void BtnZAxisHome_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1107, true);
        }

        private void BtnXAxisHome_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1110, true);
        }

        private void BtnAllAxisHome_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1111, true);

        }
        #endregion

        #region Position Selection
        private void MesScapPos1_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = true;
            this.MatingPos1 = false;
            int D3603 = 5;

            UiManager.PLC.WriteWord(DeviceCode.D, 3603, D3603);
            UpdatePos1(4);
        }
        private void QrScrapPos1_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = true;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3603 = 4;

            UiManager.PLC.WriteWord(DeviceCode.D, 3603, D3603);
            UpdatePos1(3);
        }
        private void QrPos1_CLicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = true;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3603 = 3;

            UiManager.PLC.WriteWord(DeviceCode.D, 3603, D3603);
            UpdatePos1(2);
        }
        private void ReadyPos1_Clciked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            this.BendingPos1 = false;
            this.ReadyPos1 = true;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3603 = 2;

            UiManager.PLC.WriteWord(DeviceCode.D, 3603, D3603);
            UpdatePos1(1);
        }
        private void BendingPos1_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            this.BendingPos1 = true;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3603 = 1;

            UiManager.PLC.WriteWord(DeviceCode.D, 3603, D3603);
            UpdatePos1(0);
        }
        private void UpdatePos1(int Pos)
        {
            var converter = new BrushConverter();
            Button[] btnList = new Button[] {
                btnBendingPos1,
                btnReadyPos1,
                btnQrPos1, btnQrScap1,
                btnMesScap1
                //btnMatingPos1
                };

            List<bool> AxisPos = new List<bool> {
                BendingPos1,
                ReadyPos1, QrPos1,
                QrScrapPos1,
                MesScapPos1,
                MatingPos1  };
            // POSITION
            int[] DataXaxis = new int[] {
                Robot.TeachDataXaxis1.BendingPos1,
                Robot.TeachDataXaxis1.readyPos1,
                Robot.TeachDataXaxis1.QrPos1,
                Robot.TeachDataXaxis1.QrScapPos1,
                Robot.TeachDataXaxis1.MesScapPos1,
                Robot.TeachDataXaxis1.MatingPos1 };
            //SPEED
            int[] DataZaxis = new int[] {
                Robot.TeachDataZaxis1.BendingPos1,
                Robot.TeachDataZaxis1.readyPos1,
                Robot.TeachDataZaxis1.QrPos1,
                Robot.TeachDataZaxis1.QrScapPos1,
                Robot.TeachDataZaxis1.MesScapPos1,
                Robot.TeachDataZaxis1.MatingPos1 };

            for (int i = 0; i < 5; i++)
            {
                btnList[i].Background = (Brush)converter.ConvertFromString("#EEEEEE");
                btnList[i].Foreground = Brushes.DarkBlue;
            }

            btnList[Pos].Background = (Brush)converter.ConvertFromString("#E65305");
            btnList[Pos].Foreground = Brushes.White;
            CurrentPositionPosXData = DataXaxis[Pos];
            CurrentPositionPosZData = DataZaxis[Pos];

        }
        private void MesScapPos_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = true;
            this.MatingPos = false;
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3604 = 5;

            UiManager.PLC.WriteWord(DeviceCode.D, 3604, D3604);
            UpdatePos(4);
        }
        private void QrScrapPos_Clicked(object sender, RoutedEventArgs e)
        {
            this.BendingPos = false;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = true;
            this.MesScapPos = false;
            this.MatingPos = false;
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3604 = 4;

            UiManager.PLC.WriteWord(DeviceCode.D, 3604, D3604);
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
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3604 = 3;

            UiManager.PLC.WriteWord(DeviceCode.D, 3604, D3604);
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
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3604 = 2;

            UiManager.PLC.WriteWord(DeviceCode.D, 3604, D3604);
            UpdatePos(1);
        }
        private void BtnBendingPos_Click(object sender, RoutedEventArgs e)
        {
            this.BendingPos = true;
            this.ReadyPos = false;
            this.QrPos = false;
            this.QrScrapPos = false;
            this.MesScapPos = false;
            this.MatingPos = false;
            this.BendingPos1 = false;
            this.ReadyPos1 = false;
            this.QrPos1 = false;
            this.QrScrapPos1 = false;
            this.MesScapPos1 = false;
            this.MatingPos1 = false;
            int D3604 = 1;

            UiManager.PLC.WriteWord(DeviceCode.D, 3604, D3604);
            UpdatePos(0);
        }
        private void UpdatePos(int Pos)
        {
            var converter = new BrushConverter();
            Button[] btnList = new Button[] {
                btnBendingPos,
                btnReadyPos,
                btnQrPos, btnQrScap, 
                //btnMesScap, 
                btnMatingPos };

            List<bool> AxisPos = new List<bool> {
                BendingPos,
                ReadyPos, QrPos,
                QrScrapPos,
                MesScapPos,
                MatingPos  };
            // POSITION
            int[] DataXaxis = new int[] {
                Robot.TeachDataXaxis.BendingPos,
                Robot.TeachDataXaxis.readyPos,
                Robot.TeachDataXaxis.QrPos,
                Robot.TeachDataXaxis.QrScapPos,
                Robot.TeachDataXaxis.MesScapPos,
                Robot.TeachDataXaxis.MatingPos };
            //SPEED
            int[] DataZaxis = new int[] {
                Robot.TeachDataZaxis.BendingPos,
                Robot.TeachDataZaxis.readyPos,
                Robot.TeachDataZaxis.QrPos,
                Robot.TeachDataZaxis.QrScapPos,
                Robot.TeachDataZaxis.MesScapPos,
                Robot.TeachDataZaxis.MatingPos };

            for (int i = 0; i < 5; i++)
            {
                btnList[i].Background = (Brush)converter.ConvertFromString("#EEEEEE");
                btnList[i].Foreground = Brushes.DarkBlue;
            }

            btnList[Pos].Background = (Brush)converter.ConvertFromString("#E65305");
            btnList[Pos].Foreground = Brushes.White;
            CurrentPositionPosXData = DataXaxis[Pos];
            CurrentPositionPosZData = DataZaxis[Pos];

        }

        #endregion

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("[^0-9]");
            //Boolean Huysukien = regex.IsMatch(e.Text);
            //e.Handled = Huysukien;
        }
        #region BUTTON PAGE
        private void BtSetting4_Click1(object sender, RoutedEventArgs e)
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
