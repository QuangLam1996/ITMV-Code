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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for WndUnitControl.xaml
    /// </summary>
    public partial class WndUnitControl : Window
    {
        private List<bool> L_ListUpdatePLC_10200 = new List<bool>();
        private List<short> D_ListUpdatePLC_800 = new List<short>();
        private bool IsRunningPage = false;
        private static WndUnitControl currentInstance;
        private Color Colo_ON;
        private Color Colo_OFF;
        private Color Colo_ON1;
        private Color Colo_OFF1;
        private Color Colo_ON2;

        public WndUnitControl()
        {
            InitializeComponent();
            this.Onlywindow();

            this.btClose.Click += BtClose_Click;
            this.Loaded += WndUnitControl_Loaded;
            this.Unloaded += WndUnitControl_Unloaded;

            this.btUnit1Start.Click += BtUnit1Start_Click;
            this.btUnit1Stop.Click += BtUnit1Stop_Click;
            this.btUnit1Initial.Click += BtUnit1Initial_Click;

            this.btUnit3Start.Click += BtUnit3Start_Click;
            this.btUnit3Stop.Click += BtUnit3Stop_Click;
            this.btUnit3Initial.Click += BtUnit3Initial_Click;

            this.btUnit4Start.Click += BtUnit4Start_Click;
            this.btUnit4Stop.Click += BtUnit4Stop_Click;
            this.btUnit4Initial.Click += BtUnit4Initial_Click;

            this.btUnit5Start.Click += BtUnit5Start_Click;
            this.btUnit5Stop.Click += BtUnit5Stop_Click;
            this.btUnit5Initial.Click += BtUnit5Initial_Click;

            this.btUnit6Start.Click += BtUnit6Start_Click;
            this.btUnit6Stop.Click += BtUnit6Stop_Click;
            this.btUnit6Initial.Click += BtUnit6Initial_Click;

            this.btUnit8Start.Click += BtUnit8Start_Click;
            this.btUnit8Stop.Click += BtUnit8Stop_Click;
            this.btUnit8Initial.Click += BtUnit8Initial_Click;

            this.btCVLan2Start.Click += BtCVLan2Start_Click;
            this.btCVLan2Stop.Click += BtCVLan2Stop_Click;
            this.btCVLan2Initial.Click += BtCVLan2Initial_Click;
        }

        private async void BtCVLan2Initial_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("Do you want to confirm the button")) return;
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 235, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 235, false);
            }
        }

        private async void BtCVLan2Stop_Click(object sender, RoutedEventArgs e)
        {
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 233, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 233, false);
            }
        }

        private async void BtCVLan2Start_Click(object sender, RoutedEventArgs e)
        {
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 232, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 232, false);
            }
        }

        private void WndUnitControl_Unloaded(object sender, RoutedEventArgs e)
        {
            this.IsRunningPage = false;
        }

        private void WndUnitControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.IsRunningPage = true;
            Thread ReadUpdatePLC = new Thread(() => ReadPLC());
            ReadUpdatePLC.IsBackground = true;
            ReadUpdatePLC.Start();

            string hexColor01 = "#66FF66"; // Mã màu ON (XANH )
            string hexColorBlue = "#00FFFF"; // Mã màu ON (Blue )
            string hexColor02 = "#EEEEEE"; // Mã màu OFF (TRẮNG)
            string hexColor03 = "#FF6600"; // Mã màu OFF (ĐỎ)
         
            Colo_ON = (Color)ColorConverter.ConvertFromString(hexColor01);
            Colo_ON1 = (Color)ColorConverter.ConvertFromString(hexColor03);
            Colo_ON2 = (Color)ColorConverter.ConvertFromString(hexColorBlue);
            Colo_OFF = (Color)ColorConverter.ConvertFromString(hexColor02);
            Colo_OFF1 = (Color)ColorConverter.ConvertFromString(hexColor03);

        }
        private void Onlywindow()
        {
            if (currentInstance != null && currentInstance != this)
            {
                CloseWindowSmoothly(currentInstance);
            }

            currentInstance = this;
        }
        private void CloseWindowSmoothly(Window windowToClose)
        {
            // Tạo một animation
            var animation = new DoubleAnimation(0, (Duration)TimeSpan.FromSeconds(0.1));
            animation.Completed += (sender, e) =>
            {
                windowToClose.Close();
            };

            // Áp dụng animation vào cửa sổ
            windowToClose.BeginAnimation(UIElement.OpacityProperty, animation);
        }
        private void ReadPLC()
        {
            while (IsRunningPage)
            {
                if (UiManager.PLC.isOpen())
                {
                    UiManager.PLC.ReadMultiBits(DeviceCode.L, 10200, 500, out L_ListUpdatePLC_10200);
                    UiManager.PLC.ReadMultiWord(DeviceCode.D, 800, 500, out D_ListUpdatePLC_800);
                    if(L_ListUpdatePLC_10200.Count > 1 && D_ListUpdatePLC_800.Count > 1 ) 
                    {
                        this.UpdateUiPLC();
                    }
                }

                Task.Delay(1);
            }
        }
        private void UpdateUiPLC()
        {
            if (UiManager.PLC.isOpen())
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    this.btUnit1Start.Background = new SolidColorBrush(L_ListUpdatePLC_10200[0] ? Colo_ON : Colo_OFF);
                    this.btUnit1Stop.Background = new SolidColorBrush(L_ListUpdatePLC_10200[1] ? Colo_ON1 : Colo_OFF);
                    this.btUnit1Initial.Background = new SolidColorBrush(L_ListUpdatePLC_10200[3] ? Colo_ON2 : Colo_OFF);

                    this.btUnit3Start.Background = new SolidColorBrush(L_ListUpdatePLC_10200[8] ? Colo_ON : Colo_OFF);
                    this.btUnit3Stop.Background = new SolidColorBrush(L_ListUpdatePLC_10200[9] ? Colo_ON1 : Colo_OFF);
                    this.btUnit3Initial.Background = new SolidColorBrush(L_ListUpdatePLC_10200[11] ? Colo_ON2 : Colo_OFF);

                    this.btUnit4Start.Background = new SolidColorBrush(L_ListUpdatePLC_10200[12] ? Colo_ON : Colo_OFF);
                    this.btUnit4Stop.Background = new SolidColorBrush(L_ListUpdatePLC_10200[13] ? Colo_ON1 : Colo_OFF);
                    this.btUnit4Initial.Background = new SolidColorBrush(L_ListUpdatePLC_10200[15] ? Colo_ON2 : Colo_OFF);

                    this.btUnit5Start.Background = new SolidColorBrush(L_ListUpdatePLC_10200[16] ? Colo_ON : Colo_OFF);
                    this.btUnit5Stop.Background = new SolidColorBrush(L_ListUpdatePLC_10200[17] ? Colo_ON1 : Colo_OFF);
                    this.btUnit5Initial.Background = new SolidColorBrush(L_ListUpdatePLC_10200[19] ? Colo_ON2 : Colo_OFF);

                    this.btUnit6Start.Background = new SolidColorBrush(L_ListUpdatePLC_10200[20] ? Colo_ON : Colo_OFF);
                    this.btUnit6Stop.Background = new SolidColorBrush(L_ListUpdatePLC_10200[21] ? Colo_ON1 : Colo_OFF);
                    this.btUnit6Initial.Background = new SolidColorBrush(L_ListUpdatePLC_10200[23] ? Colo_ON2 : Colo_OFF);

                    this.btUnit8Start.Background = new SolidColorBrush(L_ListUpdatePLC_10200[28] ? Colo_ON : Colo_OFF);
                    this.btUnit8Stop.Background = new SolidColorBrush(L_ListUpdatePLC_10200[29] ? Colo_ON1 : Colo_OFF);
                    this.btUnit8Initial.Background = new SolidColorBrush(L_ListUpdatePLC_10200[31] ? Colo_ON2 : Colo_OFF);

                    this.btCVLan2Start.Background = new SolidColorBrush(L_ListUpdatePLC_10200[32] ? Colo_ON : Colo_OFF);
                    this.btCVLan2Stop.Background = new SolidColorBrush(L_ListUpdatePLC_10200[33] ? Colo_ON1 : Colo_OFF);
                    this.btCVLan2Initial.Background = new SolidColorBrush(L_ListUpdatePLC_10200[35] ? Colo_ON2 : Colo_OFF);


                    this.lbUnit1Deco1.Content = D_ListUpdatePLC_800[1].ToString();
                    this.lbUnit1Deco2.Content = D_ListUpdatePLC_800[3].ToString();

                    this.lbUnit3Deco.Content = D_ListUpdatePLC_800[5].ToString();

                    this.lbUnit4Deco.Content = D_ListUpdatePLC_800[7].ToString();

                    this.lbUnit5Deco.Content = D_ListUpdatePLC_800[11].ToString();

                    this.lbUnit6Deco1.Content = D_ListUpdatePLC_800[13].ToString();
                    this.lbUnit6Deco2.Content = D_ListUpdatePLC_800[15].ToString();

                    this.lbUnit8Deco.Content = D_ListUpdatePLC_800[21].ToString();

                    this.lbCVLan2.Content = D_ListUpdatePLC_800[31].ToString();
                }));
            }    
        }
        #region Button Unit Control
        private async void BtUnit8Initial_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You want to confirm press the button")) return;
            if(UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L,231,true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 231, false);
            }    
        }

        private async void BtUnit8Stop_Click(object sender, RoutedEventArgs e)
        {
           
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 229, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 229, false);
            }
        }

        private async void BtUnit8Start_Click(object sender, RoutedEventArgs e)
        {
            
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 228, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 228, false);
            }
        }

        private async void BtUnit6Initial_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You want to confirm press the button")) return;
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 223, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 223, false);
            }
        }

        private async void BtUnit6Stop_Click(object sender, RoutedEventArgs e)
        {
          ;
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 221, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 221, false);
            }
        }

        private async void BtUnit6Start_Click(object sender, RoutedEventArgs e)
        {
            
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 220, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 220, false);
            }
        }

        private async void BtUnit5Initial_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You want to confirm press the button")) return;
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 219, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 219, false);
            }
        }

        private async void BtUnit5Stop_Click(object sender, RoutedEventArgs e)
        {
           
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 217, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 217, false);
            }
        }

        private async void BtUnit5Start_Click(object sender, RoutedEventArgs e)
        {
            
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 216, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 216, false);
            }
        }

        private async void BtUnit4Initial_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You want to confirm press the button")) return;
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 215, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 215, false);
            }
        }

        private async void BtUnit4Stop_Click(object sender, RoutedEventArgs e)
        {
            
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 213, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 213, false);
            }
        }

        private async void BtUnit4Start_Click(object sender, RoutedEventArgs e)
        {
           
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 212, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 212, false);
            }
        }

        private async void BtUnit3Initial_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You want to confirm press the button")) return;
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 211, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 211, false);
            }
        }

        private async void BtUnit3Stop_Click(object sender, RoutedEventArgs e)
        {
           
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 209, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 209, false);
            }
        }

        private async void BtUnit3Start_Click(object sender, RoutedEventArgs e)
        {
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 208, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 208, false);
            }
        }

        private async void BtUnit1Initial_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You want to confirm press the button")) return;
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 203, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 203, false);
            }
        }

        private async void BtUnit1Stop_Click(object sender, RoutedEventArgs e)
        {
            
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 201, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 201, false);
            }
        }

        private async void BtUnit1Start_Click(object sender, RoutedEventArgs e)
        {
            
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteBit(DeviceCode.L, 200, true);
                await Task.Delay(20);
                UiManager.PLC.WriteBit(DeviceCode.L, 200, false);
            }
        }
        #endregion
        private void Image_Mouseup(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
        private void BtClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
