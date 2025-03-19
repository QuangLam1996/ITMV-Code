using Mitsubishi;
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
using DTO;
using System.Threading;
using OpenCvSharp;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgManualOperation1.xaml
    /// </summary>
    public partial class PgManualOperation1 : Page
    {
        private MyLogger logger = new MyLogger("PGManual1");
        private System.Timers.Timer cycleTimer;

        private List<bool> M_0_1000 = new List<bool>();
        private List<bool> M_1000_2000 = new List<bool>();
        private List<bool> M_2000_3000 = new List<bool>();
        private List<bool> M_3000_4000 = new List<bool>();
        private List<bool> M_4000_5000 = new List<bool>();
        private List<bool> M_5000_6000 = new List<bool>();
        private List<bool> M_6000_7000 = new List<bool>();
        private List<bool> M_7000_8000 = new List<bool>();

        private List<int> D_7000_7900 = new List<int>();

        private int TimeDelay = 20;

        private bool isRunning = false;
        public PgManualOperation1()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;

            this.Loaded += PgManualOperation1_Loaded;
            this.Unloaded += PgManualOperation1_Unloaded;

            this.cycleTimer = new System.Timers.Timer(150);
            this.cycleTimer.AutoReset = true;
            this.cycleTimer.Elapsed += this.CycleTimer_Elapsed;

            #region TabCL1 Robot Tool
            this.btnToolFrameU.Click += BtnToolFrameU_Click;
            this.btnToolFrameD.Click += BtnToolFrameD_Click;

            this.btnTool1U.Click += BtnTool1U_Click;
            this.btnTool1D.Click += BtnTool1D_Click;

            this.btnTool2U.Click += BtnTool2U_Click;
            this.btnTool2D.Click += BtnTool2D_Click;

            this.btnTool3U.Click += BtnTool3U_Click;
            this.btnTool3D.Click += BtnTool3D_Click;

            this.btnTool4U.Click += BtnTool4U_Click;
            this.btnTool4D.Click += BtnTool4D_Click;

            this.btnTool5U.Click += BtnTool5U_Click;
            this.btnTool5D.Click += BtnTool5D_Click;

            this.btnTool6U.Click += BtnTool6U_Click;
            this.btnTool6D.Click += BtnTool6D_Click;

            this.btnTool7U.Click += BtnTool7U_Click;
            this.btnTool7D.Click += BtnTool7D_Click;

            this.btnTool8U.Click += BtnTool8U_Click;
            this.btnTool8D.Click += BtnTool8D_Click;

            this.btnTool9U.Click += BtnTool9U_Click;
            this.btnTool9D.Click += BtnTool9D_Click;

            this.btnTool10U.Click += BtnTool10U_Click;
            this.btnTool10D.Click += BtnTool10D_Click;

            this.btnTool11U.Click += BtnTool11U_Click;
            this.btnTool11D.Click += BtnTool11D_Click;

            this.btnTool12U.Click += BtnTool12U_Click;
            this.btnTool12D.Click += BtnTool12D_Click;

            this.btnTool13U.Click += BtnTool13U_Click;
            this.btnTool13D.Click += BtnTool13D_Click;

            this.btnTool14U.Click += BtnTool14U_Click;
            this.btnTool14D.Click += BtnTool14D_Click;

            this.btnAllToolU.Click += BtnAllToolU_Click;
            this.btnAllToolD.Click += BtnAllToolD_Click;

            this.btnVacToolFrameOn.Click += BtnVacToolFrameOn_Click;
            this.btnBlowFrame.Click += BtnBlowFrame_Click;
            this.btnToolPCBOff.Click += BtnToolPCBOff_Click;

            this.btnVacTool1On.Click += BtnVacTool1On_Click;
            this.btnVacTool2On.Click += BtnVacTool2On_Click;
            this.btnVacTool3On.Click += BtnVacTool3On_Click;
            this.btnVacTool4On.Click += BtnVacTool4On_Click;
            this.btnVacTool5On.Click += BtnVacTool5On_Click;
            this.btnVacTool6On.Click += BtnVacTool6On_Click;
            this.btnVacTool7On.Click += BtnVacTool7On_Click;
            this.btnVacTool8On.Click += BtnVacTool8On_Click;
            this.btnVacTool9On.Click += BtnVacTool9On_Click;
            this.btnVacTool10On.Click += BtnVacTool10On_Click;
            this.btnVacTool11On.Click += BtnVacTool11On_Click;
            this.btnVacTool12On.Click += BtnVacTool12On_Click;
            this.btnVacTool13On.Click += BtnVacTool13On_Click;
            this.btnVacTool14On.Click += BtnVacTool14On_Click;

            this.btnBlowTool1.Click += BtnBlowTool1_Click;
            this.btnBlowTool2.Click += BtnBlowTool2_Click;
            this.btnBlowTool3.Click += BtnBlowTool3_Click;
            this.btnBlowTool4.Click += BtnBlowTool4_Click;
            this.btnBlowTool5.Click += BtnBlowTool5_Click;
            this.btnBlowTool6.Click += BtnBlowTool6_Click;
            this.btnBlowTool7.Click += BtnBlowTool7_Click;
            this.btnBlowTool8.Click += BtnBlowTool8_Click;
            this.btnBlowTool9.Click += BtnBlowTool9_Click;
            this.btnBlowTool10.Click += BtnBlowTool10_Click;
            this.btnBlowTool11.Click += BtnBlowTool11_Click;
            this.btnBlowTool12.Click += BtnBlowTool12_Click;
            this.btnBlowTool13.Click += BtnBlowTool13_Click;
            this.btnBlowTool14.Click += BtnBlowTool14_Click;

            this.btnAllToolBlow.Click += BtnAllToolBlow_Click;
            this.btnVacAllToolOn.Click += BtnVacAllToolOn_Click;
            this.btnAllToolOff.Click += BtnAllToolOff_Click;

            this.btnTool1Off.Click += BtnTool1Off_Click;
            this.btnTool2Off.Click += BtnTool2Off_Click;
            this.btnTool3Off.Click += BtnTool3Off_Click;
            this.btnTool4Off.Click += BtnTool4Off_Click;
            this.btnTool5Off.Click += BtnTool5Off_Click;
            this.btnTool6Off.Click += BtnTool6Off_Click;
            this.btnTool7Off.Click += BtnTool7Off_Click;
            this.btnTool8Off.Click += BtnTool8Off_Click;
            this.btnTool9Off.Click += BtnTool9Off_Click;
            this.btnTool10Off.Click += BtnTool10Off_Click;
            this.btnTool11Off.Click += BtnTool11Off_Click;
            this.btnTool12Off.Click += BtnTool12Off_Click;
            this.btnTool13Off.Click += BtnTool13Off_Click;
            this.btnTool14Off.Click += BtnTool14Off_Click;

            this.btnLightVision_CH2.Click += BtnLightVision_CH2_Click;

            this.btnMoveTray.Click += BtnMoveTray_Click;
            this.btnMovJig.Click += BtnMovJig_Click;



            #endregion

            #region TabCL1 Robot Position
            this.btnRoboHome.Click += BtnRoboHome_Click;
            this.btnRoPickUpPos.Click += BtnRoPickUpPos_Click;
            this.btnRoPlacePos.Click += BtnRoPlacePos_Click;


            #endregion

            #region TabCL2 In Tray
            this.btnCenHoldTray121.Click += BtnCenHoldTray121_Click;
            this.btnCenHoldTray12L1.Click += BtnCenHoldTray12L1_Click;

            this.btnCenHoldTray341.Click += BtnCenHoldTray341_Click;
            this.btnCenHoldTray34L1.Click += BtnCenHoldTray34L1_Click;

            this.btnSplitTray121.Click += BtnSplitTray121_Click;
            this.btnSplitTray12L1.Click += BtnSplitTray12L1_Click;

            this.btnSplitTray341.Click += BtnSplitTray341_Click;
            this.btnSplitTray34L1.Click += BtnSplitTray34L1_Click;

            this.btnCylPosCen1.Click += BtnCylPosCen1_Click;
            this.btnCylPosHight1.Click += BtnCylPosHight1_Click;
            this.btnCylPosLow1.Click += BtnCylPosLow1_Click;
            #endregion

            #region TabCL2 Out Tray
            this.btnCylUDOutTray1.Click += BtnCylUDOutTray1_Click;
            this.btnCylUDOutTrayL1.Click += BtnCylUDOutTrayL1_Click;
            #endregion

            #region TabCL2 Center Tray
            this.btnClampPullTray1.Click += BtnClampPullTray1_Click;
            this.btnKeepCenterTray1.Click += BtnKeepCenterTray1_Click;
            this.btnCenterTray1.Click += BtnCenterTray1_Click;

            this.btnClampPullTrayL1.Click += BtnClampPullTrayL1_Click;
            this.btnKeepCenterTrayL1.Click += BtnKeepCenterTrayL1_Click;
            this.btnCenterTrayL1.Click += BtnCenterTrayL1_Click;

            this.btnAllHomeTray1.Click += BtnAllHomeTray1_Click;
            this.btnServoHomeTray1.Click += BtnServoHomeTray1_Click;

            this.btnServoTrayOn1.Click += BtnServoTrayOn1_Click;
            this.btnServoTrayOff1.Click += BtnServoTrayOff1_Click;

            this.btnTrayPosIn1.Click += BtnTrayPosIn1_Click;
            this.btnTrayPosSupply1.Click += BtnTrayPosSupply1_Click;
            this.btnTrayPosOut1.Click += BtnTrayPosOut1_Click;
            #endregion

            #region TabCL3 JIG
            this.btnCylIOJig1.Click += BtnCylIOJig1_Click;
            this.btnCylIOJigL1.Click += BtnCylIOJigL1_Click;

            this.btnCylUDLampVision1.Click += BtnCylUDLampVision1_Click;
            this.btnCylUDLampVisionL1.Click += BtnCylUDLampVisionL1_Click;
            #endregion

            #region TabCL4 Magazine
            this.btnClampMaga1.Click += BtnClampMaga1_Click;
            this.btnClampMagaL1.Click += BtnClampMagaL1_Click;

            this.btnPushMaga1.Click += BtnPushMaga1_Click;
            this.btnPushMagaL1.Click += BtnPushMagaL1_Click;

            this.btnAllHomeMaga.Click += BtnAllHomeMaga_Click;
            this.btnServoHomeMaga.Click += BtnServoHomeMaga_Click;

            this.btnServoMagaOn.Click += BtnServoMagaOn_Click;
            this.btnServoMagaOff.Click += BtnServoMagaOff_Click;

            this.btnMagaPosSupply.Click += BtnMagaPosSupply_Click;
            this.btnMagaPosStepDown.Click += BtnMagaPosStepDown_Click;
            this.btnMagaStepUp.Click += BtnMagaStepUp_Click;
            this.btnMagaPosRecycle.Click += BtnMagaPosRecycle_Click;

            #endregion

            #region TabCL4 Carriage
            this.btnUdCylCarriage.Click += BtnUdCylCarriage_Click;
            this.btnUdCylCarriageL.Click += BtnUdCylCarriageL_Click;

            this.btnAllHomeCarr.Click += BtnAllHomeCarr_Click;
            this.btnServoHomeCarr.Click += BtnServoHomeCarr_Click;

            this.btnServoCarrOn.Click += BtnServoCarrOn_Click;
            this.btnServoCarrOff.Click += BtnServoCarrOff_Click;

            this.btnCarrPick.Click += BtnCarrPick_Click;
            this.btnCarrPlace.Click += BtnCarrPlace_Click;
            this.btnCarrReadyPush.Click += BtnCarrReadyPush_Click;
            this.btnCarrRecycle.Click += BtnCarrRecycle_Click;
            #endregion


        }
        private void BtnMovJig_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Positon Jig", System.Windows.Window.GetWindow(this)))
            {
                int NumberPlaceJig = Convert.ToInt32(PlaceNumberJig.Value);
                int NumberPaletJig = Convert.ToInt32(PlacePaletJig.Value);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7586, NumberPlaceJig);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7594, NumberPaletJig);
                UiManager.PLC.WriteBit(DeviceCode.M, 1580, true);
                Task.Delay(20).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1580, false);
            }
        }
        private void BtnMoveTray_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Positon Tray", System.Windows.Window.GetWindow(this)))
            {
                int NumberPlaceTray = Convert.ToInt32(PlaceNumberTray.Value);
                int NumberToolTray = Convert.ToInt32(PlaceToolTray.Value);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7588, NumberPlaceTray);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7590, NumberToolTray);
                UiManager.PLC.WriteBit(DeviceCode.M, 1581, true);
                Task.Delay(20).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1581, false);
            }

        }
        private void UpdateUIJIGTRAY()
        {
            if (!UiManager.PLC.isOpen())
                return;
            // UI Tray
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7706, out int RowTray);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7708, out int ColumTray);
            GenerateButtonsTray(RowTray, ColumTray);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7700, out int ToolOn);
            PlaceToolTray.Maximum = ToolOn;
            PlaceNumberTray.Maximum = RowTray * ColumTray;

            // UI Jig
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7714, out int RowJig);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7716, out int ColumJig);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7712, out int PalletJig);
            PlacePaletJig.Maximum = PalletJig;
            PlaceNumberJig.Maximum = RowJig * ColumJig;
            GenerateButtonsJig(RowJig, ColumJig);



        }
        private void GenerateButtonsTray(int rows, int columns)
        {
            // Xóa các hàng, cột và nút hiện tại trong Grid
            ButtonGridTray.RowDefinitions.Clear();
            ButtonGridTray.ColumnDefinitions.Clear();
            ButtonGridTray.Children.Clear();

            // Tạo các hàng và cột
            for (int i = 0; i < rows; i++)
            {
                ButtonGridTray.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < columns; j++)
            {
                ButtonGridTray.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Tạo nút nhấn và gán thứ tự
            int buttonIndex = 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Button button = new Button
                    {
                        Content = buttonIndex.ToString(),
                        Margin = new Thickness(5)
                    };
                    button.Click += Button_ClickTray;

                    // Đặt nút vào Grid tại vị trí (i, j)
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    ButtonGridTray.Children.Add(button);

                    buttonIndex++;
                }
            }
        }
        private void Button_ClickTray(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            int buttonIndex = Convert.ToInt32(clickedButton.Content);
            this.PlaceNumberTray.Value = buttonIndex;
        }
        private void GenerateButtonsJig(int rows, int columns)
        {
            // Xóa các hàng, cột và nút hiện tại trong Grid
            ButtonGridJig.RowDefinitions.Clear();
            ButtonGridJig.ColumnDefinitions.Clear();
            ButtonGridJig.Children.Clear();

            // Tạo các hàng và cột
            for (int i = 0; i < rows; i++)
            {
                ButtonGridJig.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < columns; j++)
            {
                ButtonGridJig.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Tạo nút nhấn và gán thứ tự
            int buttonIndex = 1;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Button button = new Button
                    {
                        Content = buttonIndex.ToString(),
                        Margin = new Thickness(5)
                    };
                    button.Click += Button_ClickJig;

                    // Đặt nút vào Grid tại vị trí (i, j)
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);
                    ButtonGridJig.Children.Add(button);

                    buttonIndex++;
                }
            }
        }
        private void Button_ClickJig(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            int buttonIndex = Convert.ToInt32(clickedButton.Content);
            this.PlaceNumberJig.Value = buttonIndex;
        }

        private void PgManualOperation1_Unloaded(object sender, RoutedEventArgs e)
        {
            this.isRunning = false;
            if (!UiManager.PLC.isOpen())
                return;
        }

        private void BtnTool14Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1665, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1665, false);
        }

        private void BtnTool13Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1660, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1660, false);
        }

        private void BtnTool12Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1655, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1655, false);
        }

        private void BtnTool11Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1650, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1650, false);
        }

        private void BtnTool10Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1645, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1645, false);
        }




        private void BtnBlowTool14_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1664, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1664, false);
        }

        private void BtnBlowTool13_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1659, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1659, false);
        }

        private void BtnBlowTool12_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1654, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1654, false);
        }

        private void BtnBlowTool11_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1649, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1649, false);
        }

        private void BtnBlowTool10_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1644, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1644, false);
        }




        private void BtnVacTool14On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1663, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1663, false);
        }

        private void BtnVacTool13On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1658, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1658, false);
        }

        private void BtnVacTool12On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1653, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1653, false);
        }

        private void BtnVacTool11On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1648, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1648, false);
        }

        private void BtnVacTool10On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1643, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1643, false);
        }




        private void BtnTool14D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1662, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1662, false);
        }

        private void BtnTool14U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1661, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1661, false);
        }

        private void BtnTool13D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1657, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1657, false);
        }

        private void BtnTool13U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1656, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1656, false);
        }

        private void BtnTool12D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1652, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1652, false);
        }

        private void BtnTool12U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1651, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1651, false);
        }

        private void BtnTool11D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1647, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1647, false);
        }

        private void BtnTool11U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1646, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1646, false);
        }

        private void BtnTool10D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1642, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1642, false);
        }

        private void BtnTool10U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1641, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1641, false);
        }
        private void ThreadUpDatePLC()
        {
            // Run Mes
            Thread ThreadMES = new Thread(new ThreadStart(ReadPLC));
            ThreadMES.IsBackground = true;
            ThreadMES.Start();
        }
        private void ReadPLC()
        {
            try
            {
                while (isRunning)
                 {
                    if (UiManager.PLC.isOpen())
                    {
                        UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 7000, 900, out D_7000_7900);

                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 0, 1000, out M_0_1000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 1000, 1000, out M_1000_2000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 2000, 1000, out M_2000_3000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 3000, 1000, out M_3000_4000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 4000, 1000, out M_4000_5000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 5000, 1000, out M_5000_6000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 6000, 1000, out M_6000_7000);
                        //UiManager.PLC.ReadMultiBits(DeviceCode.M, 7000, 1000, out M_7000_8000);

                       
                        this.Dispatcher.Invoke(() =>
                        {
                             UpdateUIData();
                        });
                      

                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                logger.Create("PLC Com Err PG2" + ex.ToString(), LogLevel.Error);
                return;
            }

        }
        private async void CycleTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            //ReadPLC();

        }

        private void UpdateToolSection()
        {
            if (M_1000_2000.Count >= 1)
            {
                UpdateRectangle(rectTool1U, M_1000_2000[500]);
                UpdateRectangle(rectTool1D, M_1000_2000[501]);
                UpdateRectangle(rectTool2U, M_1000_2000[505]);
                UpdateRectangle(rectTool2D, M_1000_2000[506]);
                UpdateRectangle(rectTool3U, M_1000_2000[510]);
                UpdateRectangle(rectTool3D, M_1000_2000[511]);
                UpdateRectangle(rectTool4U, M_1000_2000[515]);
                UpdateRectangle(rectTool4D, M_1000_2000[516]);
                UpdateRectangle(rectTool5U, M_1000_2000[520]);
                UpdateRectangle(rectTool5D, M_1000_2000[521]);
                UpdateRectangle(rectTool6U, M_1000_2000[525]);
                UpdateRectangle(rectTool6D, M_1000_2000[526]);

                UpdateRectangle(rectTool7U, M_1000_2000[709]);
                UpdateRectangle(rectTool8U, M_1000_2000[712]);
                UpdateRectangle(rectTool9U, M_1000_2000[715]);
                UpdateRectangle(rectTool10U, M_1000_2000[780]);
                UpdateRectangle(rectTool11U, M_1000_2000[783]);
                UpdateRectangle(rectTool12U, M_1000_2000[786]);
                UpdateRectangle(rectTool13U, M_1000_2000[789]);
                UpdateRectangle(rectTool14U, M_1000_2000[792]);

                UpdateRectangle(rectTool7D, M_1000_2000[710]);
                UpdateRectangle(rectTool8D, M_1000_2000[713]);
                UpdateRectangle(rectTool9D, M_1000_2000[716]);
                UpdateRectangle(rectTool10D, M_1000_2000[781]);
                UpdateRectangle(rectTool11D, M_1000_2000[784]);
                UpdateRectangle(rectTool12D, M_1000_2000[787]);
                UpdateRectangle(rectTool13D, M_1000_2000[790]);
                UpdateRectangle(rectTool14D, M_1000_2000[793]);

                UpdateRectangle(rectVacTool1On, M_1000_2000[502]);
                UpdateRectangle(rectVacTool2On, M_1000_2000[507]);
                UpdateRectangle(rectVacTool3On, M_1000_2000[512]);
                UpdateRectangle(rectVacTool4On, M_1000_2000[517]);
                UpdateRectangle(rectVacTool5On, M_1000_2000[522]);
                UpdateRectangle(rectVacTool6On, M_1000_2000[527]);

                UpdateRectangle(rectVacTool7On, M_1000_2000[711]);
                UpdateRectangle(rectVacTool8On, M_1000_2000[714]);
                UpdateRectangle(rectVacTool9On, M_1000_2000[717]);
                UpdateRectangle(rectVacTool10On, M_1000_2000[782]);
                UpdateRectangle(rectVacTool11On, M_1000_2000[785]);
                UpdateRectangle(rectVacTool12On, M_1000_2000[788]);
                UpdateRectangle(rectVacTool13On, M_1000_2000[791]);
                UpdateRectangle(rectVacTool14On, M_1000_2000[794]);
            }
        }
        private void UpdateRectangle(Rectangle rect, bool condition)
        {
            this.Dispatcher.Invoke(() =>
            {
                rect.Fill = condition ? Brushes.LawnGreen : Brushes.LightGray;
            });
        }
        private void UpdateUIData()
        {
            UpdateToolSection();

            this.Dispatcher.Invoke(() =>
            {
               
                var converter = new BrushConverter();
                string Color = "#EEEEEE";
      

                //Tray
                if (TabControl.SelectedIndex == 1)
                {
                    if (D_7000_7900.Count >= 1)
                    {
                        PosCurJig1AxX.Text = ((double)D_7000_7900[0] / 1000).ToString();
                    }
                    if (M_0_1000.Count >= 1)
                    {
                        // IN Iray
                        if (M_0_1000[630] && M_0_1000[632])
                        {
                            rectCenHoldTray12L1.Fill = Brushes.Orange;
                            rectCenHoldTray121.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[631] && M_0_1000[633])
                        {
                            rectCenHoldTray121.Fill = Brushes.Orange;
                            rectCenHoldTray12L1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[634] && M_0_1000[636])
                        {
                            rectCenHoldTray34L1.Fill = Brushes.Orange;
                            rectCenHoldTray341.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[635] && M_0_1000[637])
                        {
                            rectCenHoldTray341.Fill = Brushes.Orange;
                            rectCenHoldTray34L1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[650])
                        {
                            rectSplitTray12L1.Fill = Brushes.Orange;
                            rectSplitTray121.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[651])
                        {
                            rectSplitTray121.Fill = Brushes.Orange;
                            rectSplitTray12L1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[652])
                        {
                            rectSplitTray34L1.Fill = Brushes.Orange;
                            rectSplitTray341.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[653])
                        {
                            rectSplitTray341.Fill = Brushes.Orange;
                            rectSplitTray34L1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[665] && M_0_1000[664])
                        {
                            rectCylPosHight1.Fill = Brushes.LawnGreen;
                            rectCylPosCen1.Fill = Brushes.Orange;
                            rectCylPosLow1.Fill = Brushes.Orange;

                        }
                        if (M_0_1000[664] && !M_0_1000[665])
                        {
                            rectCylPosHight1.Fill = Brushes.Orange;
                            rectCylPosCen1.Fill = Brushes.LawnGreen;
                            rectCylPosLow1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[663])
                        {
                            rectCylPosHight1.Fill = Brushes.Orange;
                            rectCylPosCen1.Fill = Brushes.Orange;
                            rectCylPosLow1.Fill = Brushes.LawnGreen;
                        }
                        // Out Tray
                        if (M_0_1000[722])
                        {
                            rectCylUDOutTray1.Fill = Brushes.Orange;
                            rectCylUDOutTrayL1.Fill = Brushes.LawnGreen;

                        }
                        if (M_0_1000[723])
                        {
                            rectCylUDOutTrayL1.Fill = Brushes.Orange;
                            rectCylUDOutTray1.Fill = Brushes.LawnGreen;
                        }
                        // Center Tray
                        if (M_0_1000[681] && M_0_1000[683])
                        {
                            rectClampPullTrayL1.Fill = Brushes.Orange;
                            rectClampPullTray1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[680] && M_0_1000[682])
                        {
                            rectClampPullTray1.Fill = Brushes.Orange;
                            rectClampPullTrayL1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[707] && M_0_1000[711])
                        {
                            rectKeepCenterTrayL1.Fill = Brushes.Orange;
                            rectKeepCenterTray1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[706] && M_0_1000[710])
                        {
                            rectKeepCenterTray1.Fill = Brushes.Orange;
                            rectKeepCenterTrayL1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[715])
                        {
                            rectCenterTray1.Fill = Brushes.Orange;
                            rectCenterTrayL1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[714])
                        {
                            rectCenterTrayL1.Fill = Brushes.Orange;
                            rectCenterTray1.Fill = Brushes.LawnGreen;
                        }
                    }
                    if (M_7000_8000.Count >= 1)
                    {
                        if (!M_7000_8000[391])
                        {
                            rectServoTrayOn1.Fill = Brushes.Orange;
                            rectServoTrayOff1.Fill = Brushes.LawnGreen;
                        }
                        if (M_7000_8000[391])
                        {
                            rectServoTrayOn1.Fill = Brushes.LawnGreen;
                            rectServoTrayOff1.Fill = Brushes.Orange;
                        }
                        if (M_7000_8000[375])
                        {
                            rectTrayPosIn1.Fill = Brushes.LawnGreen;
                            rectTrayPosSupply1.Fill = Brushes.Orange;
                            rectTrayPosOut1.Fill = Brushes.Orange;
                        }
                        if (M_7000_8000[376])
                        {

                            rectTrayPosSupply1.Fill = Brushes.Orange;
                            rectTrayPosIn1.Fill = Brushes.LawnGreen;
                            rectTrayPosOut1.Fill = Brushes.Orange;
                        }
                        if (M_7000_8000[377])
                        {

                            rectTrayPosOut1.Fill = Brushes.Orange;
                            rectTrayPosIn1.Fill = Brushes.Orange;
                            rectTrayPosSupply1.Fill = Brushes.LawnGreen;
                        }
                    }
                }

                if (TabControl.SelectedIndex == 2)
                {
                    // SS JIG
                    if (M_0_1000.Count >= 1)
                    {
                        if (M_0_1000[735])
                        {
                            rectCylIOJig1.Fill = Brushes.LawnGreen;
                            rectCylIOJigL1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[734])
                        {
                            rectCylIOJig1.Fill = Brushes.Orange;
                            rectCylIOJigL1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[741])
                        {
                            rectCylUDLampVision1.Fill = Brushes.LawnGreen;
                            rectCylUDLampVisionL1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[740])
                        {
                            rectCylUDLampVision1.Fill = Brushes.Orange;
                            rectCylUDLampVisionL1.Fill = Brushes.LawnGreen;
                        }
                    }
                }

                if (TabControl.SelectedIndex == 3)
                {
                    if (D_7000_7900.Count >= 1)
                    {
                        PosCurAxZ.Text = ((double)D_7000_7900[160] / 1000).ToString();
                        PosCurAxXCarr.Text = ((double)D_7000_7900[240] / 1000).ToString();
                    }
                    // ss magazine
                    if (M_0_1000.Count >= 1)
                    {
                        if (M_0_1000[755])
                        {
                            rectClampMaga11.Fill = Brushes.LawnGreen;
                            rectClampMaga1L1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[754])
                        {
                            rectClampMaga11.Fill = Brushes.Orange; ;
                            rectClampMaga1L1.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[745])
                        {
                            rectbtnPushMaga1.Fill = Brushes.LawnGreen;
                            rectbtnPushMagaL1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[744])
                        {
                            rectbtnPushMaga1.Fill = Brushes.Orange;
                            rectbtnPushMagaL1.Fill = Brushes.LawnGreen;
                        }
                        if (!M_0_1000[523])
                        {
                            //rectLightVision_CH2.Fill = Brushes.Yellow;
                        }
                        else if (!M_0_1000[523])
                        {
                            //rectLightVision_CH2.Fill = Brushes.DarkRed;
                        }
                        if (M_0_1000[491])
                        {
                            rectServoMagaOn.Fill = Brushes.LawnGreen;
                            rectServoMagaOff.Fill = Brushes.Orange;
                        }
                        if (!M_0_1000[491])
                        {
                            rectServoMagaOn.Fill = Brushes.Orange;
                            rectServoMagaOff.Fill = Brushes.LawnGreen;
                        }
                        if (M_0_1000[477])
                        {
                            rectMagaPosSupply.Fill = Brushes.LawnGreen;
                            rectMagaPosStepDown.Fill = Brushes.Orange;
                            rectMagaStepUp.Fill = Brushes.Orange;
                            rectMagaPosRecycle.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[475])
                        {
                            rectMagaPosSupply.Fill = Brushes.Orange;
                            rectMagaPosStepDown.Fill = Brushes.LawnGreen;
                            rectMagaStepUp.Fill = Brushes.Orange;
                            rectMagaPosRecycle.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[478])
                        {
                            rectMagaPosSupply.Fill = Brushes.Orange;
                            rectMagaPosStepDown.Fill = Brushes.Orange;
                            rectMagaStepUp.Fill = Brushes.LawnGreen;
                            rectMagaPosRecycle.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[476])
                        {
                            rectMagaPosSupply.Fill = Brushes.Orange;
                            rectMagaPosStepDown.Fill = Brushes.Orange;
                            rectMagaStepUp.Fill = Brushes.Orange;
                            rectMagaPosRecycle.Fill = Brushes.LawnGreen;
                        }
                        // ss carriage
                        if (M_0_1000[772])
                        {
                            rectUdCylCarriage.Fill = Brushes.LawnGreen;
                            rectUdCylCarriageL.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[771])
                        {
                            rectUdCylCarriage.Fill = Brushes.Orange;
                            rectUdCylCarriageL.Fill = Brushes.LawnGreen;
                        }
                    }

                    if (M_7000_8000.Count >= 1)
                    {
                        if (M_7000_8000[591])
                        {
                            rectServoCarrOn.Fill = Brushes.LawnGreen;
                            rectServoCarrOff.Fill = Brushes.Orange;
                        }
                        if (!M_7000_8000[591])
                        {
                            rectServoCarrOn.Fill = Brushes.Orange;
                            rectServoCarrOff.Fill = Brushes.LawnGreen;
                        }
                        if (M_7000_8000[576])
                        {
                            rectCarrPick.Fill = Brushes.LawnGreen;
                            rectCarrPlace.Fill = Brushes.Orange;
                            rectCarrReadyPush.Fill = Brushes.Orange;
                            rectCarrRecycle.Fill = Brushes.Orange;
                        }
                        if (M_7000_8000[575])
                        {
                            rectCarrPick.Fill = Brushes.Orange;
                            rectCarrPlace.Fill = Brushes.LawnGreen;
                            rectCarrReadyPush.Fill = Brushes.Orange;
                            rectCarrRecycle.Fill = Brushes.Orange;
                        }
                        if (M_7000_8000[577])
                        {
                            rectCarrPick.Fill = Brushes.Orange;
                            rectCarrPlace.Fill = Brushes.Orange;
                            rectCarrReadyPush.Fill = Brushes.LawnGreen;
                            rectCarrRecycle.Fill = Brushes.Orange;
                        }
                        if (M_7000_8000[578])
                        {
                            rectCarrPick.Fill = Brushes.Orange;
                            rectCarrPlace.Fill = Brushes.Orange;
                            rectCarrReadyPush.Fill = Brushes.Orange;
                            rectCarrRecycle.Fill = Brushes.LawnGreen;
                        }
                    }
                }

                if (M_0_1000.Count >= 1)
                {
                    // OUTPUT      intray      
                    if (M_0_1000[824])
                    {
                        btnCenHoldTray121.Background = Brushes.Cyan;
                        btnCenHoldTray12L1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnCenHoldTray12L1.Background = Brushes.Cyan;
                        btnCenHoldTray121.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    if (M_0_1000[825])
                    {
                        btnCenHoldTray341.Background = Brushes.Cyan;
                        btnCenHoldTray34L1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnCenHoldTray34L1.Background = Brushes.Cyan;
                        btnCenHoldTray341.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    if (M_0_1000[826])
                    {
                        btnSplitTray121.Background = Brushes.Cyan;
                        btnSplitTray12L1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnSplitTray12L1.Background = Brushes.Cyan;
                        btnSplitTray121.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    if (M_0_1000[827])
                    {
                        btnSplitTray341.Background = Brushes.Cyan;
                        btnSplitTray34L1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnSplitTray34L1.Background = Brushes.Cyan;
                        btnSplitTray341.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    if (M_0_1000[858] && M_0_1000[859])
                    {
                        btnCylPosHight1.Background = Brushes.Cyan;
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString(Color);
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    if (M_0_1000[858] && !M_0_1000[859])
                    {
                        btnCylPosCen1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString(Color);
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    if (!M_0_1000[858] && !M_0_1000[859])
                    {
                        btnCylPosLow1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString(Color);
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    // output out tray

                    if (M_0_1000[868])
                    {
                        btnCylUDOutTray1.Background = Brushes.Cyan;
                        btnCylUDOutTrayL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnCylUDOutTrayL1.Background = Brushes.Cyan;
                        btnCylUDOutTray1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    // OUTPUT CENTER TRAY

                    if (M_0_1000[835])
                    {
                        btnClampPullTray1.Background = Brushes.Cyan;
                        btnClampPullTrayL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnClampPullTrayL1.Background = Brushes.Cyan;
                        btnClampPullTray1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (M_0_1000[845])
                    {
                        btnKeepCenterTray1.Background = Brushes.Cyan;
                        btnKeepCenterTrayL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnKeepCenterTrayL1.Background = Brushes.Cyan;
                        btnKeepCenterTray1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (M_0_1000[847])
                    {
                        btnCenterTray1.Background = Brushes.Cyan;
                        btnCenterTrayL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnCenterTrayL1.Background = Brushes.Cyan;
                        btnCenterTray1.Background = (Brush)converter.ConvertFromString(Color);

                    }

                    if (M_0_1000[250] && !M_0_1000[256])
                    {
                        btnAllHomeTray1.Background = Brushes.Cyan;
                        if (btnAllHomeTray1.Visibility == Visibility.Visible)
                        {

                            btnAllHomeTray1.Visibility = Visibility.Hidden;
                        }
                        else
                        {

                            btnAllHomeTray1.Visibility = Visibility.Visible;
                        }

                    }
                    if (!M_0_1000[250] && !M_0_1000[256])
                    {
                        btnAllHomeTray1.Visibility = Visibility.Visible;
                        btnAllHomeTray1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (!M_0_1000[250] && M_0_1000[256])
                    {
                        btnAllHomeTray1.Visibility = Visibility.Visible;
                        btnAllHomeTray1.Background = Brushes.Cyan;

                    }

                    if (M_0_1000[523])
                    {
                        btnLightVision_CH2.Background = Brushes.Cyan;
                    }
                    if (!M_0_1000[523])
                    {
                        btnLightVision_CH2.Background = (Brush)converter.ConvertFromString(Color);
                    }

                    if (M_0_1000[842])
                    {
                        btnServoTrayOn1.Background = Brushes.Cyan;
                        btnServoTrayOff1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnServoTrayOff1.Background = Brushes.Cyan;
                        btnServoTrayOn1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (M_0_1000[854])
                    {
                        btnCylIOJig1.Background = Brushes.Cyan;
                        btnCylIOJigL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnCylIOJigL1.Background = Brushes.Cyan;
                        btnCylIOJig1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (M_0_1000[857])
                    {
                        btnCylUDLampVision1.Background = Brushes.Cyan;
                        btnCylUDLampVisionL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnCylUDLampVisionL1.Background = Brushes.Cyan;
                        btnCylUDLampVision1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    //OUT MAGAZINE

                    if (M_0_1000[864])
                    {
                        btnClampMaga1.Background = Brushes.Cyan;
                        btnClampMagaL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnClampMagaL1.Background = Brushes.Cyan;
                        btnClampMaga1.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (M_0_1000[860] && !M_0_1000[871])
                    {
                        btnPushMaga1.Background = Brushes.Cyan;
                        btnPushMagaL1.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    if (!M_0_1000[860] && M_0_1000[871])
                    {
                        btnPushMagaL1.Background = Brushes.Cyan;
                        btnPushMaga1.Background = (Brush)converter.ConvertFromString(Color);

                    }

                    if (M_0_1000[200] && !M_0_1000[205])
                    {
                        btnAllHomeTray1.Background = Brushes.Cyan;
                        if (btnAllHomeMaga.Visibility == Visibility.Visible)
                        {

                            btnAllHomeMaga.Visibility = Visibility.Hidden;
                        }
                        else
                        {

                            btnAllHomeMaga.Visibility = Visibility.Visible;
                        }

                    }
                    if (!M_0_1000[200] && !M_0_1000[205])
                    {
                        btnAllHomeMaga.Visibility = Visibility.Visible;
                        btnAllHomeMaga.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (!M_0_1000[200] && M_0_1000[205])
                    {
                        btnAllHomeMaga.Visibility = Visibility.Visible;
                        btnAllHomeMaga.Background = Brushes.Cyan;

                    }

                    if (M_0_1000[862])
                    {
                        btnServoMagaOn.Background = Brushes.Cyan;
                        btnServoMagaOff.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnServoMagaOff.Background = Brushes.Cyan;
                        btnServoMagaOn.Background = (Brush)converter.ConvertFromString(Color);

                    }


                    if (M_0_1000[870])
                    {
                        btnUdCylCarriage.Background = Brushes.Cyan;
                        btnUdCylCarriageL.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnUdCylCarriage.Background = Brushes.Cyan;
                        btnUdCylCarriageL.Background = (Brush)converter.ConvertFromString(Color);

                    }



                    if (M_0_1000[200] && !M_0_1000[205])
                    {
                        btnAllHomeCarr.Background = Brushes.Cyan;
                        if (btnAllHomeCarr.Visibility == Visibility.Visible)
                        {

                            btnAllHomeCarr.Visibility = Visibility.Hidden;
                        }
                        else
                        {

                            btnAllHomeCarr.Visibility = Visibility.Visible;
                        }

                    }
                    if (!M_0_1000[200] && !M_0_1000[205])
                    {
                        btnAllHomeCarr.Visibility = Visibility.Visible;
                        btnAllHomeCarr.Background = (Brush)converter.ConvertFromString(Color);

                    }
                    if (!M_0_1000[200] && M_0_1000[205])
                    {
                        btnAllHomeCarr.Visibility = Visibility.Visible;
                        btnAllHomeCarr.Background = Brushes.Cyan;

                    }

                    if (M_0_1000[866])
                    {
                        btnServoCarrOn.Background = Brushes.Cyan;
                        btnServoCarrOff.Background = (Brush)converter.ConvertFromString(Color);
                    }
                    else
                    {
                        btnServoCarrOff.Background = Brushes.Cyan;
                        btnServoCarrOn.Background = (Brush)converter.ConvertFromString(Color);

                    }
                }

            });


        }
        private void PgManualOperation1_Loaded(object sender, RoutedEventArgs e)
        {
            this.isRunning = true;
            ThreadUpDatePLC();
            cycleTimer.Start();
            this.UpdateUIJIGTRAY();



            //Load Interface - model
            if (UiManager.appSettings.connection.model == "X2833")
            {

            }
            else if (UiManager.appSettings.connection.model == "X2835")
            {

            }
            else if (UiManager.appSettings.connection.model == "X2836")
            {

            }
            if (!UiManager.PLC.isOpen())
                return;
        }
        #region TabCL4 Carriage

        private void BtnCarrRecycle_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1298, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1299, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1297, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1313, true);
        }

        private void BtnCarrReadyPush_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1297, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1299, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1313, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1298, true);
        }

        private void BtnCarrPlace_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1298, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1297, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1313, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1299, true);
        }

        private void BtnCarrPick_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1298, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1299, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1313, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1297, true);
        }

        private void BtnServoCarrOff_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1276, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1309, true);
        }

        private void BtnServoCarrOn_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1309, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1276, true);
        }

        private void BtnServoHomeCarr_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1310, true);
        }

        private void BtnAllHomeCarr_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1311, true);
        }

        private void BtnUdCylCarriageL_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1278, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1279, true);
        }

        private void BtnUdCylCarriage_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1279, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1278, true);
        }
        #endregion

        #region TabCL4 Magazine
        private void BtnMagaPosRecycle_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1292, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1293, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1294, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1312, true);
        }

        private void BtnMagaStepUp_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1294, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1292, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1312, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1293, true);
        }

        private void BtnMagaPosStepDown_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1293, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1292, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1312, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1294, true);
        }

        private void BtnMagaPosSupply_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1293, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1294, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1312, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1292, true);
        }

        private void BtnServoMagaOff_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1271, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1306, true);
        }

        private void BtnServoMagaOn_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1306, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1271, true);
        }

        private void BtnServoHomeMaga_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1307, true);
        }

        private void BtnAllHomeMaga_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1308, true);
        }

        private void BtnPushMagaL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1268, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1269, true);
        }

        private void BtnPushMaga1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1269, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1268, true);
        }

        private void BtnClampMagaL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1273, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1274, true);
        }

        private void BtnClampMaga1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1274, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1273, true);
        }
        #endregion

        #region TabCL3 JIG
        private void BtnCylUDLampVisionL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1266, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1267, true);
        }

        private void BtnCylUDLampVision1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1267, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1266, true);
        }

        private void BtnCylIOJigL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1262, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1263, true);
        }

        private void BtnCylIOJig1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1263, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1262, true);
        }

        #endregion

        #region TabCL2 Center Tray
        private void BtnTrayPosOut1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1289, true);
        }

        private void BtnTrayPosSupply1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1288, true);
        }

        private void BtnTrayPosIn1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1287, true);
        }

        private void BtnServoTrayOff1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1246, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1303, true);
        }

        private void BtnServoTrayOn1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1303, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1246, true);
        }

        private void BtnServoHomeTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1304, true);
        }

        private void BtnAllHomeTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1305, true);
        }

        private void BtnCenterTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1254, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1255, true);
        }

        private void BtnKeepCenterTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1250, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1251, true);
        }

        private void BtnClampPullTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1240, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1241, true);
        }

        private void BtnCenterTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1255, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1254, true);
        }

        private void BtnKeepCenterTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1251, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1250, true);
        }

        private void BtnClampPullTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1241, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1240, true);
        }
        #endregion

        #region TabCL2 Out Tray
        private void BtnCylUDOutTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1258, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1259, true);
        }

        private void BtnCylUDOutTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1259, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1258, true);
        }
        #endregion

        #region TabCL2 In Tray
        private void BtnCylPosLow1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1237, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1236, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1235, true);
        }

        private void BtnCylPosHight1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1235, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1236, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1237, true);
        }

        private void BtnCylPosCen1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1235, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1237, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1236, true);
        }

        private void BtnSplitTray34L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1230, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1231, true);
        }

        private void BtnSplitTray341_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1231, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1230, true);
        }

        private void BtnSplitTray12L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1228, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1229, true);
        }

        private void BtnSplitTray121_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1229, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1228, true);
        }

        private void BtnCenHoldTray34L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1225, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1224, true);
        }

        private void BtnCenHoldTray341_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1227, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1226, true);
        }

        private void BtnCenHoldTray12L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1224, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1225, true);
        }

        private void BtnCenHoldTray121_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1225, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1224, true);
        }

        #endregion

        #region TabCL1 Robot Position

        private void BtnRoPlacePos_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Positon Place", System.Windows.Window.GetWindow(this)))
            {
                if (!UiManager.PLC.isOpen())
                    return;

                UiManager.PLC.WriteBit(DeviceCode.M, 1572, true);
                Task.Delay(TimeDelay).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1572, false);
            }
        }

        private void BtnRoPickUpPos_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move PickUp Robot", System.Windows.Window.GetWindow(this)))
            {
                if (!UiManager.PLC.isOpen())
                    return;

                UiManager.PLC.WriteBit(DeviceCode.M, 1571, true);
                Task.Delay(TimeDelay).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1571, false);
            }
        }

        private void BtnRoboHome_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Home Robot", System.Windows.Window.GetWindow(this)))
            {
                if (!UiManager.PLC.isOpen())
                    return;

                UiManager.PLC.WriteBit(DeviceCode.M, 1570, true);
                Task.Delay(TimeDelay).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1570, false);
            }
        }


        #endregion

        #region TabCL1 Robot Tool


        private void BtnLightVision_CH2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 522, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 522, false);
        }

        private void BtnToolPCBOff_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1345, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1345, false);
        }

        private void BtnBlowFrame_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1420, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1420, false);
        }

        private void BtnAllToolOff_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1417, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1417, false);
        }

        private void BtnTool9Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1438, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1438, false);
        }

        private void BtnTool8Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1433, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1433, false);
        }

        private void BtnTool7Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1428, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1428, false);
        }

        private void BtnTool6Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1414, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1414, false);
        }

        private void BtnTool5Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1409, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1409, false);
        }

        private void BtnTool4Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1404, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1404, false);
        }

        private void BtnTool3Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1351, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1351, false);
        }

        private void BtnTool2Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1349, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1349, false);
        }

        private void BtnTool1Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1347, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1347, false);
        }

        private void BtnVacAllToolOn_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1415, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1415, false);
        }

        private void BtnAllToolBlow_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1416, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1416, false);
        }

        private void BtnBlowTool9_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1437, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1437, false);
        }

        private void BtnBlowTool8_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1432, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1432, false);
        }

        private void BtnBlowTool7_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1427, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1427, false);
        }

        private void BtnBlowTool6_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1413, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1413, false);
        }

        private void BtnBlowTool5_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1408, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1408, false);
        }

        private void BtnBlowTool4_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1403, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1403, false);
        }

        private void BtnBlowTool3_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1423, true);
            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1423, false);

        }

        private void BtnBlowTool2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1422, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1422, false);
        }

        private void BtnBlowTool1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1421, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1421, false);
        }

        private void BtnVacTool9On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1436, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1436, false);
        }

        private void BtnVacTool8On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1431, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1431, false);
        }

        private void BtnVacTool7On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1426, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1426, false);
        }

        private void BtnVacTool6On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1412, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1412, false);
        }

        private void BtnVacTool5On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1407, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1407, false);
        }

        private void BtnVacTool4On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1402, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1402, false);
        }

        private void BtnVacTool3On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1350, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1350, false);
        }

        private void BtnVacTool2On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1348, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1348, false);
        }

        private void BtnVacTool1On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1346, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1346, false);
        }

        private void BtnVacToolFrameOn_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1344, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1344, false);
        }

        private void BtnAllToolD_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1355, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1355, false);
        }

        private void BtnAllToolU_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1354, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1354, false);
        }

        private void BtnTool9D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1435, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1435, false);
        }

        private void BtnTool9U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1434, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1434, false);
        }

        private void BtnTool8D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1430, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1430, false);
        }

        private void BtnTool8U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1429, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1429, false);
        }

        private void BtnTool7D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1425, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1425, false);
        }

        private void BtnTool7U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1424, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1424, false);
        }

        private void BtnTool6D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1411, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1411, false);
        }

        private void BtnTool6U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1410, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1410, false);
        }

        private void BtnTool5D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1406, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1406, false);
        }

        private void BtnTool5U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1405, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1405, false);
        }

        private void BtnTool4D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1401, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1401, false);
        }

        private void BtnTool4U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1400, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1400, false);
        }

        private void BtnTool3D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1343, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1343, false);
        }

        private void BtnTool3U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1342, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1342, false);
        }

        private void BtnTool2D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1341, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1341, false);
        }

        private void BtnTool2U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1340, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1340, false);
        }

        private void BtnTool1D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1339, true);
            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1339, false);


        }

        private void BtnTool1U_Click(object sender, RoutedEventArgs e)
        {
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.M, 1338, true);
                Task.Delay(TimeDelay).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1338, false);
            }
         
        }

        private void BtnToolFrameD_Click(object sender, RoutedEventArgs e)
        {
          
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.M, 1336, false);
                UiManager.PLC.WriteBit(DeviceCode.M, 1337, true);
            }
        }

        private void BtnToolFrameU_Click(object sender, RoutedEventArgs e)
        {
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.M, 1337, false);
                UiManager.PLC.WriteBit(DeviceCode.M, 1336, true);
            }
           
        }
        #endregion

        #region Button Pg
       
        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION1);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION1);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION);
        }
        #endregion
    }
}