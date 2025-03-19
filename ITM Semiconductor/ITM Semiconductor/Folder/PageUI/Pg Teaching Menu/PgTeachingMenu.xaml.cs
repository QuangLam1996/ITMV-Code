using DTO;
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
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgTeachingMenu.xaml
    /// </summary>
    public partial class PgTeachingMenu : Page
    {
        Q_Enthernet PLC = new Q_Enthernet();
        MyLogger logger = new MyLogger("PG_TeachingMenu");
        bool isUpdate;
        private List<bool> ListUpdateBitPLC = new List<bool>();


      

        // Chuyển đổi mã màu sang đối tượng Color
       
        public PgTeachingMenu()
        {
            InitializeComponent();

            this.Loaded += PgTeachingMenu_Loaded;
            this.Unloaded += PgTeachingMenu_Unloaded;

        #region Button
            this.BtServo1.PreviewMouseDown += BtServo1_PreviewMouseDown;
            this.BtServo1.PreviewMouseUp += BtServo1_PreviewMouseUp;


            this.BtBake1.PreviewMouseDown += BtBake1_PreviewMouseDown;
            this.BtBake1.PreviewMouseUp += BtBake1_PreviewMouseUp;

           

        #endregion


        }


       
        private void BtBake1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PLC.WriteBit(DeviceCode.M, 101, false);
        }

        private void BtServo1_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            PLC.WriteBit(DeviceCode.M, 100, false);
        }

        private void BtBake1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PLC.WriteBit(DeviceCode.M, 101, true);
           
        }

        private void BtServo1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            PLC.WriteBit(DeviceCode.M, 100, true);
           
        }

       

        private void PgTeachingMenu_Unloaded(object sender, RoutedEventArgs e)
        {
            this.StopPLC();
        }

        private async void PgTeachingMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.btSetting2.IsEnabled = false;
            this.btSetting3.IsEnabled = false;
            this.btSetting4.IsEnabled = false;

            await this.ConnectPLCAsync();
            isUpdate = true;
            await this.UpdateUIAsync();
        }
        #region Connect PLC
        private async Task ConnectPLCAsync()
        {
            PLC = new Q_Enthernet(UiManager.appSettings.Setting_PLCTcp.Ip1, UiManager.appSettings.Setting_PLCTcp.Port1);
            try
            {
                await Task.Run(() => PLC.ConnectWithTimeOut(2000));

            }
            catch (Exception ex)
            {
                logger.Create($"ConnectPLCAsync: {ex.Message}", LogLevel.Error);

            }
        }

        private void StopPLC()
        {

            if (PLC != null || PLC.isOpen() == true)
            {

                PLC.Disconnect();
            }
           
        }

        #endregion



        private async Task UpdateUIAsync()
        {
            while (isUpdate)
            {
                if (PLC.isOpen() == true)
                {
                  
                    PLC.ReadMultiBits(DeviceCode.M, 100, 1000, out ListUpdateBitPLC);
                    
                }

                UpdateUIPLC();
                await Task.Delay(1);
            }
        }

        private void UpdateUIPLC ()
        {
            string hexColorOn = "#99FFCC"; // Mã màu ON
            string hexColorOff = "#F08080"; // Mã màu OFF
            Color ON = (Color)ColorConverter.ConvertFromString(hexColorOn);
            Color OFF = (Color)ColorConverter.ConvertFromString(hexColorOff);
            if (PLC.isOpen() == true)
            {

                Dispatcher.BeginInvoke(new Action(() =>
                {
                    //this.BtBake1.Background = new SolidColorBrush(ListUpdateBitPLC[1] ? Colors.LightGreen : Colors.OrangeRed);
                    //this.BtBake1.Content = ListUpdateBitPLC[1] ? "Bake ON" : "Bake OFF";


                    this.BtServo1.Background = new SolidColorBrush(ListUpdateBitPLC[0] ? ON : OFF);
                    this.BtServo1.Content = ListUpdateBitPLC[0] ? "Servo ON" : "Servo OFF";

                    this.BtBake1.Background = new SolidColorBrush(ListUpdateBitPLC[1] ? ON : OFF);
                    this.BtBake1.Content = ListUpdateBitPLC[1] ? "Bake ON" : "Bake OFF";


                }));

            }
           
        }


       
    }
}
