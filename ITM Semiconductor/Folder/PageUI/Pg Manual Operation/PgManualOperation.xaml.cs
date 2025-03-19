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
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgManualOperation.xaml
    /// </summary>
    public partial class PgManualOperation : Page
    {
        MyLogger logger = new MyLogger("PGManual1");
        private System.Timers.Timer cycleTimer;

        private List<bool> M_0_1000 = new List<bool>();
        private List<bool> M_1000_2000 = new List<bool>();
        private List<bool> M_2000_3000 = new List<bool>();
        private List<bool> M_3000_4000 = new List<bool>();
        private List<bool> M_4000_5000 = new List<bool>();
        private List<bool> M_5000_6000 = new List<bool>();
        private List<bool> M_6000_7000 = new List<bool>();
        private List<bool> M_7000_8000 = new List<bool>();

        private List<bool> X_330_350 = new List<bool>();
        private List<bool> X_1340_1360 = new List<bool>();

        private List<int> D_7000_7900 = new List<int>();

        private int TimeDelay = 20;
        private bool isRunning = false;

        public PgManualOperation()
        {
            InitializeComponent();
            this.cycleTimer = new System.Timers.Timer(150);
            this.cycleTimer.AutoReset = true;
            this.cycleTimer.Elapsed += this.CycleTimer_Elapsed;

            //PG Loaded
            this.Loaded += PgManualOperation_Loaded;
            this.Unloaded += PgManualOperation_Unloaded;

            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;

            this.btnLightVision_CH1.Click += BtnLightVision_CH1_Click;


            #region TabCL1 Tool Robot Manual
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

            this.btnBlowFrame.Click += BtnBlowFrame_Click;
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
            this.btnAllToolOff.Click += BtnAllToolOff_Click;
            #endregion 

            #region TabCL1 Robot Position

            this.btnRoboHome.Click += BtnRoboHome_Click;
            this.btnRoPickUpPos.Click += BtnRoPickUpPos_Click;
            this.btnRoPlacePos.Click += BtnRoPlacePos_Click;

            this.btnMoveTray.Click += BtnMoveTray_Click;
            this.btnMovJig.Click += BtnMovJig_Click;

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

            #region TabCL 4 Magazine
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

        private void BtnLightVision_CH1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 502, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 502, false);
        }

        private void BtnMovJig_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Positon Jig", System.Windows.Window.GetWindow(this)))
            {
                int NumberPlaceJig = Convert.ToInt32(PlaceNumberJig.Value);
                int NumberPaletJig = Convert.ToInt32(PlacePaletJig.Value);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7580, NumberPlaceJig);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7592, NumberPaletJig);
                UiManager.PLC.WriteBit(DeviceCode.M, 1560, true);
                Task.Delay(20).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1560, false);
            }
        }
        private void BtnMoveTray_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Positon Tray", System.Windows.Window.GetWindow(this)))
            {
                int NumberPlaceTray = Convert.ToInt32(PlaceNumberTray.Value);
                int NumberToolTray = Convert.ToInt32(PlaceToolTray.Value);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7582, NumberPlaceTray);
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, 7584, NumberToolTray);
                UiManager.PLC.WriteBit(DeviceCode.M, 1561, true);
                Task.Delay(20).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1561, false);
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

        private void PgManualOperation_Unloaded(object sender, RoutedEventArgs e)
        {
            this.isRunning = false;
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

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1310, false);
        }

        private void BtnAllHomeCarr_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1311, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1311, false);
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

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1307, false);
        }

        private void BtnAllHomeMaga_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1308, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1308, false);
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
            UiManager.PLC.WriteBit(DeviceCode.M, 1264, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1265, true);
        }

        private void BtnCylUDLampVision1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1265, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1264, true);
        }

        private void BtnCylIOJigL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1260, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1261, true);
        }

        private void BtnCylIOJig1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1261, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1260, true);
        }
        #endregion

        #region TabCL2 Center Tray
        private void BtnTrayPosOut1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1284, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1384, false);
        }

        private void BtnTrayPosSupply1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1283, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1283, false);
        }

        private void BtnTrayPosIn1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1282, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1382, false);
        }

        private void BtnServoTrayOff1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1243, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1300, true);
        }

        private void BtnServoTrayOn1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1300, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1243, true);
        }

        private void BtnServoHomeTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1301, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1301, false);
        }

        private void BtnAllHomeTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1302, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1302, false);
        }

        private void BtnCenterTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1253, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1252, true);
        }

        private void BtnKeepCenterTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1248, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1249, true);
        }

        private void BtnClampPullTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1238, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1239, true);
        }

        private void BtnCenterTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1252, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1253, true);
        }

        private void BtnKeepCenterTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1249, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1248, true);
        }

        private void BtnClampPullTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1239, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1238, true);
        }
        #endregion

        #region TabCl2 Out Tray
        private void BtnCylUDOutTrayL1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1256, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1257, true);
        }

        private void BtnCylUDOutTray1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1257, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1256, true);
        }
        #endregion

        #region TabCL2 In Tray
        private void BtnCylPosLow1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1234, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1234, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1232, true);
        }

        private void BtnCylPosHight1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1232, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1233, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1234, true);
        }

        private void BtnCylPosCen1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1232, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1234, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1233, true);
        }

        private void BtnSplitTray34L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1222, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1223, true);
        }

        private void BtnSplitTray341_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1223, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1222, true);
        }

        private void BtnSplitTray12L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1220, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1221, true);
        }

        private void BtnSplitTray121_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1221, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1220, true);
        }

        private void BtnCenHoldTray34L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1218, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1219, true);
        }

        private void BtnCenHoldTray341_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1219, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1218, true);
        }

        private void BtnCenHoldTray12L1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1216, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1217, true);
        }

        private void BtnCenHoldTray121_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1217, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1216, true);
        }
        #endregion

        #region TabCL1 Robot Position

        private void BtnRoPlacePos_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Positon Place", System.Windows.Window.GetWindow(this)))
            {
                if (!UiManager.PLC.isOpen())
                    return;
                UiManager.PLC.WriteBit(DeviceCode.M, 1552, true);

                Task.Delay(TimeDelay).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1552, false);
            }
        }

        private void BtnRoPickUpPos_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move PickUp Robot", System.Windows.Window.GetWindow(this)))
            {
                if (!UiManager.PLC.isOpen())
                    return;
                UiManager.PLC.WriteBit(DeviceCode.M, 1551, true);

                Task.Delay(TimeDelay).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1551, false);
            }
        }

        private void BtnRoboHome_Click(object sender, RoutedEventArgs e)
        {
            if (new WndConfirm().DoComfirmYesNo("Do You Want to Move Home Robot", System.Windows.Window.GetWindow(this)))
            {
                if (!UiManager.PLC.isOpen())
                    return;
                UiManager.PLC.WriteBit(DeviceCode.M, 1550, true);

                Task.Delay(TimeDelay).Wait();
                UiManager.PLC.WriteBit(DeviceCode.M, 1550, false);
            }
        }
        #endregion

        #region TabCL1 Tool Robot Manual 
        private void BtnAllToolOff_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1377, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1377, false);
        }

        private void BtnTool9Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1398, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1398, false);
        }

        private void BtnTool8Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1393, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1393, false);
        }

        private void BtnTool7Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1388, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1388, false);
        }

        private void BtnTool6Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1374, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1374, false);
        }

        private void BtnTool5Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1369, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1369, false);
        }

        private void BtnTool4Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1364, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1364, false);
        }

        private void BtnTool3Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1335, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1335, false);
        }

        private void BtnTool2Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1333, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1333, false);

        }

        private void BtnTool1Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;

            UiManager.PLC.WriteBit(DeviceCode.M, 1331, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1331, false);
        }

        private void BtnVacAllToolOn_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1375, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1322, false);
        }

        private void BtnAllToolBlow_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1376, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1376, false);
        }

        private void BtnBlowTool9_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1397, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1397, false);
        }

        private void BtnBlowTool8_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1392, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1392, false);
        }

        private void BtnBlowTool7_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1387, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1387, false);
        }

        private void BtnBlowTool6_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1373, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1373, false);
        }

        private void BtnBlowTool5_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1368, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1368, false);
        }

        private void BtnBlowTool4_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1363, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1363, false);
        }

        private void BtnBlowTool3_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1383, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1383, false);
        }

        private void BtnBlowTool2_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1382, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1382, false);
        }

        private void BtnBlowTool1_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1381, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1381, false);
        }

        private void BtnBlowFrame_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1380, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1380, false);
        }

        private void BtnVacTool9On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1396, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1396, false);
        }

        private void BtnVacTool8On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1391, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1391, false);
        }

        private void BtnVacTool7On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1386, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1386, false);
        }

        private void BtnVacTool6On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1372, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1372, false);
        }

        private void BtnVacTool5On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1367, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1367, false);
        }

        private void BtnVacTool4On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1362, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1362, false);
        }

        private void BtnVacTool3On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1334, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1334, false);
        }

        private void BtnVacTool2On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1332, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1332, false);
        }

        private void BtnVacTool1On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1330, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1330, false);
        }

        private void BtnVacToolFrameOn_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1328, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1328, false);
        }

        private void BtnAllToolD_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1353, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1353, false);
        }

        private void BtnAllToolU_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1352, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1352, false);
        }

        private void BtnTool9D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1395, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1395, false);
        }

        private void BtnTool9U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1394, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1394, false);
        }

        private void BtnTool8D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1390, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1390, false);
        }

        private void BtnTool8U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1389, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1389, false);
        }

        private void BtnTool7D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1385, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1385, false);
        }

        private void BtnTool7U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1384, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1384, false);
        }

        private void BtnTool6D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1371, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1371, false);
        }

        private void BtnTool6U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1370, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1370, false);
        }

        private void BtnTool5D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1366, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1366, false);
        }

        private void BtnTool5U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1365, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1365, false);
        }

        private void BtnTool4D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1361, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1361, false);
        }

        private void BtnTool4U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1360, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1360, false);
        }

        private void BtnTool3D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1327, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1327, false);
        }

        private void BtnTool3U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1326, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1326, false);
        }

        private void BtnTool2D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1325, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1325, false);
        }

        private void BtnTool1D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1323, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1323, false);
        }

        private void BtnToolFrameD_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1320, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1321, true);
        }

        private void BtnTool2U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1324, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1324, false);
        }

        private void BtnTool1U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1322, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1322, false);

        }

        private void BtnToolFrameU_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1321, false);
            UiManager.PLC.WriteBit(DeviceCode.M, 1320, true);

        }

        private void BtnTool14Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1640, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1640, false);
        }

        private void BtnTool13Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1635, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1635, false);
        }

        private void BtnTool12Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1630, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1630, false);
        }

        private void BtnTool11Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1625, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1625, false);
        }

        private void BtnTool10Off_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1620, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1620, false);
        }






        private void BtnBlowTool14_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1639, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1639, false);
        }

        private void BtnBlowTool13_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1634, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1634, false);
        }

        private void BtnBlowTool12_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1629, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1629, false);
        }

        private void BtnBlowTool11_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1624, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1624, false);
        }

        private void BtnBlowTool10_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1619, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1619, false);
        }





        private void BtnVacTool14On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1638, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1638, false);
        }

        private void BtnVacTool13On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1633, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1633, false);
        }

        private void BtnVacTool12On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1628, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1628, false);
        }

        private void BtnVacTool11On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1623, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1623, false);
        }

        private void BtnVacTool10On_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1618, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1618, false);
        }







        private void BtnTool14D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1637, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1637, false);
        }

        private void BtnTool14U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1636, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1636, false);
        }

        private void BtnTool13D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1632, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1632, false);
        }

        private void BtnTool13U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1631, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1631, false);
        }

        private void BtnTool12D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1627, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1627, false);
        }

        private void BtnTool12U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1626, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1626, false);
        }

        private void BtnTool11D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1622, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1622, false);
        }

        private void BtnTool11U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1621, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1621, false);
        }

        private void BtnTool10D_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1617, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1617, false);
        }

        private void BtnTool10U_Click(object sender, RoutedEventArgs e)
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.WriteBit(DeviceCode.M, 1616, true);

            Task.Delay(TimeDelay).Wait();
            UiManager.PLC.WriteBit(DeviceCode.M, 1616, false);
        }

        #endregion

        #region TabCL1 Button Page
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION3);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION3);
        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MANUAL_OPERATION2);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION2);
        }

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

        private void PgManualOperation_Loaded(object sender, RoutedEventArgs e)
        {
            this.isRunning = true;
            this.ThreadUpDatePLC();
            cycleTimer.Start();
            this.UpdateUIJIGTRAY();
            //Load Interface - model
            if (UiManager.appSettings.connection.model == "X2833")
            {
                //Column2833.Width = new GridLength(3, GridUnitType.Star);
                //Column2835.Width = new GridLength(0, GridUnitType.Star);
                //Column2836.Width = new GridLength(0, GridUnitType.Star);
            }
            else if (UiManager.appSettings.connection.model == "X2835")
            {
                //Column2833.Width = new GridLength(0, GridUnitType.Star);
                //Column2835.Width = new GridLength(3, GridUnitType.Star);
                //Column2836.Width = new GridLength(0, GridUnitType.Star);
            }
            else if (UiManager.appSettings.connection.model == "X2836")
            {
                //Column2833.Width = new GridLength(0, GridUnitType.Star);
                //Column2835.Width = new GridLength(0, GridUnitType.Star);
                //Column2836.Width = new GridLength(3, GridUnitType.Star);
            }
            if (!UiManager.PLC.isOpen())
                return;
        }
        private void ThreadUpDatePLC()
        {
          
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
               
                    if(UiManager.PLC.isOpen())
                    {
                        UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 7000, 900, out D_7000_7900);

                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 0, 900, out M_0_1000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 1000, 1000, out M_1000_2000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 2000, 1000, out M_2000_3000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 3000, 1000, out M_3000_4000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 4000, 1000, out M_4000_5000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 5000, 1000, out M_5000_6000);
                        UiManager.PLC.ReadMultiBits(DeviceCode.M, 6000, 1000, out M_6000_7000);

                      
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
                logger.Create("PLC Com Err PG1" + ex.ToString(), LogLevel.Error);
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
                UpdateRectangle(rectTool1U, M_1000_2000[450]);
                UpdateRectangle(rectTool1D, M_1000_2000[451]);
                UpdateRectangle(rectTool2U, M_1000_2000[455]);
                UpdateRectangle(rectTool2D, M_1000_2000[456]);
                UpdateRectangle(rectTool3U, M_1000_2000[460]);
                UpdateRectangle(rectTool3D, M_1000_2000[461]);
                UpdateRectangle(rectTool4U, M_1000_2000[465]);
                UpdateRectangle(rectTool4D, M_1000_2000[466]);
                UpdateRectangle(rectTool5U, M_1000_2000[470]);
                UpdateRectangle(rectTool5D, M_1000_2000[471]);
                UpdateRectangle(rectTool6U, M_1000_2000[475]);
                UpdateRectangle(rectTool6D, M_1000_2000[476]);

                UpdateRectangle(rectTool7U, M_1000_2000[720]);
                UpdateRectangle(rectTool8U, M_1000_2000[721]);
                UpdateRectangle(rectTool9U, M_1000_2000[722]);
                UpdateRectangle(rectTool10U, M_1000_2000[750]);
                UpdateRectangle(rectTool11U, M_1000_2000[751]);
                UpdateRectangle(rectTool12U, M_1000_2000[752]);
                UpdateRectangle(rectTool13U, M_1000_2000[753]);
                UpdateRectangle(rectTool14U, M_1000_2000[754]);

                UpdateRectangle(rectTool7D, M_1000_2000[730]);
                UpdateRectangle(rectTool8D, M_1000_2000[733]);
                UpdateRectangle(rectTool9D, M_1000_2000[736]);
                UpdateRectangle(rectTool10D, M_1000_2000[795]);
                UpdateRectangle(rectTool11D, M_1000_2000[798]);
                UpdateRectangle(rectTool12D, M_1000_2000[801]);
                UpdateRectangle(rectTool13D, M_1000_2000[804]);
                UpdateRectangle(rectTool14D, M_1000_2000[807]);

                UpdateRectangle(rectVacTool1On, M_1000_2000[452]);
                UpdateRectangle(rectVacTool2On, M_1000_2000[457]);
                UpdateRectangle(rectVacTool3On, M_1000_2000[462]);
                UpdateRectangle(rectVacTool4On, M_1000_2000[467]);
                UpdateRectangle(rectVacTool5On, M_1000_2000[472]);
                UpdateRectangle(rectVacTool6On, M_1000_2000[477]);

                UpdateRectangle(rectVacTool7On, M_1000_2000[702]);
                UpdateRectangle(rectVacTool8On, M_1000_2000[705]);
                UpdateRectangle(rectVacTool9On, M_1000_2000[708]);
                UpdateRectangle(rectVacTool10On, M_1000_2000[762]);
                UpdateRectangle(rectVacTool11On, M_1000_2000[765]);
                UpdateRectangle(rectVacTool12On, M_1000_2000[768]);
                UpdateRectangle(rectVacTool13On, M_1000_2000[771]);
                UpdateRectangle(rectVacTool14On, M_1000_2000[774]);

                UpdateRectangle(rectVacTool1Off, M_1000_2000[453]);
                UpdateRectangle(rectVacTool2Off, M_1000_2000[458]);
                UpdateRectangle(rectVacTool3Off, M_1000_2000[463]);
                UpdateRectangle(rectVacTool4Off, M_1000_2000[468]);
                UpdateRectangle(rectVacTool5Off, M_1000_2000[473]);
                UpdateRectangle(rectVacTool6Off, M_1000_2000[478]);

                UpdateRectangle(rectVacTool7Off, M_1000_2000[732]);
                UpdateRectangle(rectVacTool8Off, M_1000_2000[735]);
                UpdateRectangle(rectVacTool9Off, M_1000_2000[738]);
                UpdateRectangle(rectVacTool10Off, M_1000_2000[797]);
                UpdateRectangle(rectVacTool11Off, M_1000_2000[800]);
                UpdateRectangle(rectVacTool12Off, M_1000_2000[803]);
                UpdateRectangle(rectVacTool13Off, M_1000_2000[806]);
                UpdateRectangle(rectVacTool14Off, M_1000_2000[809]);
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
            this.UpdateToolSection();

            this.Dispatcher.Invoke(() =>
            {
               
                var converter = new BrushConverter();
                string Colo = "#EEEEEE";

                #region TabCL2 In Tray
                if (D_7000_7900.Count >= 1)
                {
                    PosCurJig1AxX.Text = ((double)D_7000_7900[0] / 1000).ToString();
                    PosCurAxZ.Text = ((double)D_7000_7900[160] / 1000).ToString();
                    PosCurAxXCarr.Text = ((double)D_7000_7900[240] / 1000).ToString();
                }
                // IN 
                if (M_0_1000.Count >= 1)
                {
                    if (M_0_1000[620] && M_0_1000[622])
                    {
                        rectCenHoldTray12L1.Fill = Brushes.Orange;
                        rectCenHoldTray121.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[621] && M_0_1000[623])
                    {
                        rectCenHoldTray121.Fill = Brushes.Orange;
                        rectCenHoldTray12L1.Fill = Brushes.LawnGreen;

                    }
                    if (M_0_1000[624] && M_0_1000[626])
                    {
                        rectCenHoldTray34L1.Fill = Brushes.Orange;
                        rectCenHoldTray341.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[625] && M_0_1000[627])
                    {
                        rectCenHoldTray341.Fill = Brushes.Orange;
                        rectCenHoldTray34L1.Fill = Brushes.LawnGreen;

                    }

                    if (M_0_1000[640])
                    {
                        rectSplitTray12L1.Fill = Brushes.Orange;
                        rectSplitTray121.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[641])
                    {
                        rectSplitTray121.Fill = Brushes.Orange;
                        rectSplitTray12L1.Fill = Brushes.LawnGreen;

                    }
                    if (M_0_1000[642])
                    {
                        rectSplitTray34L1.Fill = Brushes.Orange;
                        rectSplitTray341.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[643])
                    {
                        rectSplitTray341.Fill = Brushes.Orange;
                        rectSplitTray34L1.Fill = Brushes.LawnGreen;

                    }
                    //Nam
                    if (M_0_1000[662] && M_0_1000[661])
                    {
                        rectCylPosHight1.Fill = Brushes.LawnGreen;
                        rectCylPosCen1.Fill = Brushes.Orange;
                        rectCylPosLow1.Fill = Brushes.Orange;

                    }
                    if (!M_0_1000[662] && M_0_1000[661])
                    {
                        rectCylPosHight1.Fill = Brushes.Orange;
                        rectCylPosCen1.Fill = Brushes.OrangeRed;
                        rectCylPosLow1.Fill = Brushes.Orange;
                    }
                    if (M_0_1000[660])
                    {
                        rectCylPosHight1.Fill = Brushes.Orange;
                        rectCylPosCen1.Fill = Brushes.Orange;
                        rectCylPosLow1.Fill = Brushes.LawnGreen;
                    }
                    // Out Tray
                    if (M_0_1000[720])
                    {
                        rectCylUDOutTrayL1.Fill = Brushes.LawnGreen;
                        rectCylUDOutTray1.Fill = Brushes.Orange;
                    }
                    if (M_0_1000[721])
                    {
                        rectCylUDOutTray1.Fill = Brushes.LawnGreen;
                        rectCylUDOutTrayL1.Fill = Brushes.Orange;
                        // Center Tray

                        if (M_0_1000[675] && M_0_1000[677])
                        {
                            rectClampPullTray1.Fill = Brushes.LawnGreen;
                            rectClampPullTrayL1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[674] && M_0_1000[676])
                        {
                            rectClampPullTrayL1.Fill = Brushes.LawnGreen;
                            rectClampPullTray1.Fill = Brushes.Orange;
                        }

                        if (M_0_1000[703] && M_0_1000[705])
                        {
                            rectKeepCenterTray1.Fill = Brushes.LawnGreen;
                            rectKeepCenterTrayL1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[702] && M_0_1000[704])
                        {
                            rectKeepCenterTrayL1.Fill = Brushes.LawnGreen;
                            rectKeepCenterTray1.Fill = Brushes.Orange;
                        }

                        if (M_0_1000[713])
                        {
                            rectCenterTray1.Fill = Brushes.LawnGreen;
                            rectCenterTrayL1.Fill = Brushes.Orange;
                        }
                        if (M_0_1000[712])
                        {
                            rectCenterTrayL1.Fill = Brushes.LawnGreen;
                            rectCenterTray1.Fill = Brushes.Orange;
                        }

                        if (M_0_1000[837])
                        {
                            rectServoTrayOn1.Fill = Brushes.LawnGreen;
                            rectServoTrayOff1.Fill = Brushes.Orange;
                        }
                        if (!M_0_1000[837])
                        {
                            rectServoTrayOn1.Fill = Brushes.Orange;
                            rectServoTrayOff1.Fill = Brushes.LawnGreen;
                        }

                    }
                    //OUT Carriage
                    if (M_0_1000[870])
                    {
                        btnUdCylCarriage.Background = Brushes.Cyan;
                        btnUdCylCarriageL.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnUdCylCarriageL.Background = Brushes.Cyan;
                        btnUdCylCarriage.Background = (Brush)converter.ConvertFromString(Colo);

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
                        btnAllHomeCarr.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (!M_0_1000[200] && M_0_1000[205])
                    {
                        btnAllHomeCarr.Visibility = Visibility.Visible;
                        btnAllHomeCarr.Background = Brushes.LightCyan;

                    }
                    if (M_0_1000[866])
                    {
                        btnServoCarrOn.Background = Brushes.Cyan;
                        btnServoCarrOff.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnServoCarrOff.Background = Brushes.Cyan;
                        btnServoCarrOn.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    //out robot
                    if (M_0_1000[563])
                    {
                        //btnToolFrameU.Background = Brushes.Cyan;
                        //btnToolFrameD.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else if (M_0_1000[564])
                    {
                        //btnToolFrameD.Background = Brushes.Cyan;
                        //btnToolFrameU.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[566])
                    {
                        //btnTool1U.Background = Brushes.Cyan;
                        //btnTool1D.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else if (M_0_1000[567])
                    {
                        //btnTool1D.Background = Brushes.Cyan;
                        //btnTool1U.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[569])
                    {
                        //btnTool2U.Background = Brushes.Cyan;
                        //btnTool2D.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else if (M_0_1000[570])
                    {
                        //btnTool2D.Background = Brushes.Cyan;
                        //btnTool2U.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[572])
                    {
                        //btnTool3U.Background = Brushes.Cyan;
                        //btnTool3D.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else if (M_0_1000[573])
                    {
                        //btnTool3D.Background = Brushes.Cyan;
                        //btnTool3U.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }

                    if (M_0_1000[562])
                    {
                        //btnVacToolFrameOn.Background = Brushes.Cyan;
                        //btnVacToolFrameOff.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        //btnVacToolFrameOff.Background = Brushes.Cyan;
                        //btnVacToolFrameOn.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[565])
                    {
                        //btnVacTool1On.Background = Brushes.Cyan;
                        //btnVacTool1Off.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        //btnVacTool1Off.Background = Brushes.Cyan;
                        //btnVacTool1On.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[568])
                    {
                        //btnVacTool2On.Background = Brushes.Cyan;
                        //btnVacTool2Off.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    {
                        //btnVacTool2Off.Background = Brushes.Cyan;
                        //btnVacTool2On.Background = (Brush)converter.ConvertFromString("#D5D5D5");

                    }
                    if (M_0_1000[571])
                    {
                        //btnVacTool3On.Background = Brushes.Cyan;
                        //btnVacTool3Off.Background = (Brush)converter.ConvertFromString("#D5D5D5");
                    }
                    else
                    #endregion
                    // SS JIG
                    if (M_0_1000[733])
                    {
                        rectCylIOJig1.Fill = Brushes.OrangeRed;
                        rectCylIOJigL1.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[732])
                    {
                        rectCylIOJig1.Fill = Brushes.LawnGreen;
                        rectCylIOJigL1.Fill = Brushes.Orange;
                    }
                    if (M_0_1000[737])
                    {
                        rectCylUDLampVision1.Fill = Brushes.OrangeRed; // clamp 
                        rectCylUDLampVisionL1.Fill = Brushes.Orange;
                    }
                    if (M_0_1000[736])
                    {
                        rectCylUDLampVision1.Fill = Brushes.Orange;
                        rectCylUDLampVisionL1.Fill = Brushes.OrangeRed;
                    }
                    // ss magazine
                    if (M_0_1000[755])
                    {
                        rectClampMaga11.Fill = Brushes.OrangeRed;
                        rectClampMaga1L1.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[754])
                    {
                        rectClampMaga11.Fill = Brushes.LawnGreen;
                        rectClampMaga1L1.Fill = Brushes.OrangeRed;
                    }
                    if (M_0_1000[745])
                    {
                        rectbtnPushMaga1.Fill = Brushes.OrangeRed;
                        rectbtnPushMagaL1.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[744])
                    {
                        rectbtnPushMaga1.Fill = Brushes.LawnGreen;
                        rectbtnPushMagaL1.Fill = Brushes.OrangeRed;
                    }
                    if (M_0_1000[503])
                    {
                        rectLightVision_CH1.Fill = Brushes.OrangeRed;
                    }
                    else if (!M_0_1000[503])
                    {
                        rectLightVision_CH1.Fill = Brushes.LawnGreen;
                    }
                    if (!M_0_1000[862])
                    {
                        rectServoMagaOn.Fill = Brushes.LawnGreen;
                        rectServoMagaOff.Fill = Brushes.OrangeRed;
                    }
                    if (M_0_1000[862])
                    {
                        rectServoMagaOn.Fill = Brushes.OrangeRed;
                        rectServoMagaOff.Fill = Brushes.LawnGreen;
                    }
                    // ss carriage
                    if (M_0_1000[772])
                    {
                        rectUdCylCarriage.Fill = Brushes.OrangeRed;
                        rectUdCylCarriageL.Fill = Brushes.LawnGreen;
                    }
                    if (M_0_1000[771])
                    {
                        rectUdCylCarriage.Fill = Brushes.LawnGreen;
                        rectUdCylCarriageL.Fill = Brushes.OrangeRed;
                    }
                    if (!M_0_1000[866])
                    {
                        rectServoCarrOn.Fill = Brushes.LawnGreen;
                        rectServoCarrOff.Fill = Brushes.OrangeRed;
                    }
                    if (M_0_1000[866])
                    {
                        rectServoCarrOn.Fill = Brushes.OrangeRed;
                        rectServoCarrOff.Fill = Brushes.LawnGreen;
                    }
                }
                if (M_7000_8000.Count >= 1)
                {
                    if (M_7000_8000[231])
                    {
                        btnTrayPosIn1.Background = Brushes.Cyan;
                        btnTrayPosSupply1.Background = (Brush)converter.ConvertFromString(Colo);
                        btnTrayPosOut1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_7000_8000[232])
                    {
                        btnTrayPosSupply1.Background = Brushes.Cyan;
                        btnTrayPosIn1.Background = (Brush)converter.ConvertFromString(Colo);
                        btnTrayPosOut1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_7000_8000[233])
                    {
                        btnTrayPosOut1.Background = Brushes.Cyan;
                        btnTrayPosSupply1.Background = (Brush)converter.ConvertFromString(Colo);
                        btnTrayPosIn1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_7000_8000[271])
                    {
                        rectTrayPosIn1.Fill = Brushes.LawnGreen;
                        rectTrayPosSupply1.Fill = Brushes.Orange;
                        rectTrayPosOut1.Fill = Brushes.Orange;
                    }
                    if (M_7000_8000[272])
                    {

                        rectTrayPosSupply1.Fill = Brushes.LawnGreen;
                        rectTrayPosIn1.Fill = Brushes.Orange;
                        rectTrayPosOut1.Fill = Brushes.Orange;
                    }
                    if (M_7000_8000[273])
                    {

                        rectTrayPosOut1.Fill = Brushes.LawnGreen;
                        rectTrayPosIn1.Fill = Brushes.Orange;
                        rectTrayPosSupply1.Fill = Brushes.Orange;
                    }
                }
                if (M_0_1000.Count >= 1)
                {
                    // OUTPUT      intray  
                    if (M_0_1000[820])
                    {
                        btnCenHoldTray121.Background = Brushes.Cyan;
                        btnCenHoldTray12L1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnCenHoldTray12L1.Background = Brushes.Cyan;
                        btnCenHoldTray121.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_0_1000[821])
                    {
                        btnCenHoldTray341.Background = Brushes.Cyan;
                        btnCenHoldTray34L1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnCenHoldTray34L1.Background = Brushes.Cyan;
                        btnCenHoldTray341.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_0_1000[822])
                    {
                        btnSplitTray121.Background = Brushes.Cyan;
                        btnSplitTray12L1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnSplitTray12L1.Background = Brushes.Cyan;
                        btnSplitTray121.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_0_1000[823])
                    {
                        btnSplitTray341.Background = Brushes.Cyan;
                        btnSplitTray34L1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnSplitTray34L1.Background = Brushes.Cyan;
                        btnSplitTray341.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_0_1000[838] && M_0_1000[839])
                    {
                        btnCylPosHight1.Background = Brushes.Cyan;
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString(Colo);
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_0_1000[838] && !M_0_1000[839])
                    {
                        btnCylPosCen1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString(Colo);
                        btnCylPosLow1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (!M_0_1000[838] && !M_0_1000[839])
                    {
                        btnCylPosLow1.Background = Brushes.Cyan;
                        btnCylPosHight1.Background = (Brush)converter.ConvertFromString(Colo);
                        btnCylPosCen1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    // output out tray
                    if (M_0_1000[848])
                    {
                        btnCylUDOutTray1.Background = Brushes.Cyan;
                        btnCylUDOutTrayL1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnCylUDOutTrayL1.Background = Brushes.Cyan;
                        btnCylUDOutTray1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    // OUTPUT CENTER TRAY
                    if (M_0_1000[834])
                    {
                        btnClampPullTray1.Background = Brushes.Cyan;
                        btnClampPullTrayL1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnClampPullTrayL1.Background = Brushes.Cyan;
                        btnClampPullTray1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (M_0_1000[844])
                    {
                        btnKeepCenterTray1.Background = Brushes.Cyan;
                        btnKeepCenterTrayL1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnKeepCenterTrayL1.Background = Brushes.Cyan;
                        btnKeepCenterTray1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (M_0_1000[846])
                    {
                        btnCenterTrayL1.Background = Brushes.Cyan;
                        btnCenterTray1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnCenterTray1.Background = Brushes.Cyan;
                        btnCenterTrayL1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (M_0_1000[200] && !M_0_1000[206])
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
                    if (!M_0_1000[200] && !M_0_1000[206])
                    {
                        btnAllHomeTray1.Visibility = Visibility.Visible;
                        btnAllHomeTray1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (!M_0_1000[200] && M_0_1000[206])
                    {
                        btnAllHomeTray1.Visibility = Visibility.Visible;
                        btnAllHomeTray1.Background = Brushes.Cyan;

                    }
                    if (M_0_1000[503])
                    {
                        btnLightVision_CH1.Background = Brushes.Cyan;
                    }
                    if (!M_0_1000[503])
                    {
                        btnLightVision_CH1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_0_1000[837])
                    {
                        btnServoTrayOn1.Background = Brushes.Cyan;
                        btnServoTrayOff1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnServoTrayOff1.Background = Brushes.Cyan;
                        btnServoTrayOn1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    // OUPUT JIG
                    if (M_0_1000[852])
                    {
                        btnCylIOJig1.Background = Brushes.Cyan;
                        btnCylIOJigL1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnCylIOJigL1.Background = Brushes.Cyan;
                        btnCylIOJig1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (M_0_1000[855])
                    {
                        btnCylUDLampVision1.Background = Brushes.Cyan;
                        btnCylUDLampVisionL1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnCylUDLampVisionL1.Background = Brushes.Cyan;
                        btnCylUDLampVision1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    //OUT MAGAZINE
                    if (M_0_1000[864])
                    {
                        btnClampMaga1.Background = Brushes.Cyan;
                        btnClampMagaL1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnClampMagaL1.Background = Brushes.Cyan;
                        btnClampMaga1.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (M_0_1000[860] && !M_0_1000[871])
                    {
                        btnPushMaga1.Background = Brushes.Cyan;
                        btnPushMagaL1.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else if (!M_0_1000[860] && M_0_1000[871])
                    {
                        btnPushMagaL1.Background = Brushes.Cyan;
                        btnPushMaga1.Background = (Brush)converter.ConvertFromString(Colo);

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
                        btnAllHomeMaga.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                    if (!M_0_1000[200] && M_0_1000[205])
                    {
                        btnAllHomeMaga.Visibility = Visibility.Visible;
                        btnAllHomeMaga.Background = Brushes.Cyan;

                    }
                    if (M_0_1000[862])
                    {
                        btnServoMagaOn.Background = Brushes.Cyan;
                        btnServoMagaOff.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    else
                    {
                        btnServoMagaOff.Background = Brushes.Cyan;
                        btnServoMagaOn.Background = (Brush)converter.ConvertFromString(Colo);

                    }
                }
                if (M_2000_3000.Count >= 1)
                {
                    if (M_2000_3000[701])
                    {
                        rectMagaPosSupply.Fill = Brushes.LawnGreen;
                        rectMagaPosStepDown.Fill = Brushes.OrangeRed;
                        rectMagaStepUp.Fill = Brushes.OrangeRed;
                        rectMagaPosRecycle.Fill = Brushes.OrangeRed;
                    }
                    if (M_2000_3000[703])
                    {
                        rectMagaPosSupply.Fill = Brushes.OrangeRed;
                        rectMagaPosStepDown.Fill = Brushes.LawnGreen;
                        rectMagaStepUp.Fill = Brushes.OrangeRed;
                        rectMagaPosRecycle.Fill = Brushes.OrangeRed;
                    }
                    if (M_2000_3000[704])
                    {
                        rectMagaPosSupply.Fill = Brushes.OrangeRed;
                        rectMagaPosStepDown.Fill = Brushes.OrangeRed;
                        rectMagaStepUp.Fill = Brushes.LawnGreen;
                        rectMagaPosRecycle.Fill = Brushes.OrangeRed;
                    }
                    if (M_2000_3000[702])
                    {
                        rectMagaPosSupply.Fill = Brushes.OrangeRed;
                        rectMagaPosStepDown.Fill = Brushes.OrangeRed;
                        rectMagaStepUp.Fill = Brushes.OrangeRed;
                        rectMagaPosRecycle.Fill = Brushes.LawnGreen;
                    }
                    if (M_2000_3000[703])
                    {
                        btnMagaPosSupply.Background = Brushes.Cyan;
                        btnMagaPosStepDown.Background = (Brush)converter.ConvertFromString(Colo);
                        btnMagaStepUp.Background = (Brush)converter.ConvertFromString(Colo);
                        btnMagaPosRecycle.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_2000_3000[701])
                    {
                        btnMagaPosStepDown.Background = Brushes.Cyan;
                        btnMagaPosSupply.Background = (Brush)converter.ConvertFromString(Colo);
                        btnMagaStepUp.Background = (Brush)converter.ConvertFromString(Colo);
                        btnMagaPosRecycle.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_2000_3000[704])
                    {
                        btnMagaStepUp.Background = Brushes.Cyan;
                        btnMagaPosStepDown.Background = (Brush)converter.ConvertFromString(Colo);

                        btnMagaPosSupply.Background = (Brush)converter.ConvertFromString(Colo);
                        btnMagaPosRecycle.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                    if (M_2000_3000[702])
                    {
                        btnMagaPosRecycle.Background = Brushes.Cyan;
                        btnMagaPosStepDown.Background = (Brush)converter.ConvertFromString(Colo);
                        btnMagaStepUp.Background = (Brush)converter.ConvertFromString(Colo);

                        btnMagaPosSupply.Background = (Brush)converter.ConvertFromString(Colo);
                    }
                }
               
  
            });
        }


    }
}
