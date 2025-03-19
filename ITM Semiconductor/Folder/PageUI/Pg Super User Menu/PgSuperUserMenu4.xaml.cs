using DTO;
using KeyPad;
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

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgSuperUserMenu4.xaml
    /// </summary>
    public partial class PgSuperUserMenu4 : Page
    {
        private List<short> D_ListUpdateDevicePLC_3600 = new List<short>();
        private MyLogger logger = new MyLogger("PG_SuperUser_SettingRobot");
        public PgSuperUserMenu4()
        {
            InitializeComponent();
            this.Loaded += PgSuperUserMenu4_Loaded;
            this.BtnSave.Click += BtnSave_Click;
            

            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.btSetting5.Click += BtSetting5_Click;


            this.TbxDevice1.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice1.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice2.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice2.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice3.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice3.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice4.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice4.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice19.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice19.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice20.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice20.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice21.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice21.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice22.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice22.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice23.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice23.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            this.TbxDevice24.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice24.PreviewMouseDown += TbxDevice_PreviewMouseDown;

            

            
        }

        private void TbxDevice_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;

            }
        }

        private void TbxDevice_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You Want Save Setting?")) return;

            int Model_Robot = Convert.ToInt16(TbxDevice1.Text);
            int Speed_Robot = Convert.ToInt16(TbxDevice2.Text);
            int Accel_Robot = Convert.ToInt16(TbxDevice3.Text);
            int SpeedHome_Robot = Convert.ToInt16(TbxDevice4.Text);

            int Speed_Z_Slow_Robot = Convert.ToInt16(TbxDevice19.Text);
            int Distance_Z_Slow_Robot = Convert.ToInt16(TbxDevice20.Text);
            int Speed_Z_DownStop_Robot = Convert.ToInt16(TbxDevice21.Text);
            int Pitch_Z_DownStop_Robot = Convert.ToInt16(TbxDevice22.Text);
            int Number_Of_CheckPoint_Robot = Convert.ToInt16(TbxDevice23.Text);
            int Number_Of_QRPCB_Robot = Convert.ToInt16(TbxDevice24.Text);


            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteWord(DeviceCode.D, 3600, Model_Robot);
                logger.Create($"PC_Write_Model_Robot: {3600}  = {Model_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3601, Speed_Robot);
                logger.Create($"PC_Write_Speed_Robot: {3601}  = {Speed_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3602, Accel_Robot);
                logger.Create($"PC_Write_Accel_Robot: {3602}  = {Accel_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3603, SpeedHome_Robot);
                logger.Create($"PC_Write_SpeedHome_Robot: {3603}  = {SpeedHome_Robot}", LogLevel.Information);



                UiManager.PLC.WriteWord(DeviceCode.D, 3610, Speed_Z_Slow_Robot);
                logger.Create($"PC_Write_Speed_Z_Slow_Robot: {3610}  = {Speed_Z_Slow_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3611, Distance_Z_Slow_Robot);
                logger.Create($"PC_Write_Distance_Z_Slow_Robot: {3611}  = {Distance_Z_Slow_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3612, Speed_Z_DownStop_Robot);
                logger.Create($"PC_Write_Speed_Z_DownStop_Robot: {3612}  = {Speed_Z_DownStop_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3613, Pitch_Z_DownStop_Robot);
                logger.Create($"PC_Write_Pitch_Z_DownStop_Robot: {3613}  = {Pitch_Z_DownStop_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3614, Number_Of_CheckPoint_Robot);
                logger.Create($"PC_Write_Number_Of_CheckPoint_Robot: {3614}  = {Number_Of_CheckPoint_Robot}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 3615, Number_Of_QRPCB_Robot);
                logger.Create($"PC_Write_Number_Of_QRPCB_Robot: {3615}  = {Number_Of_QRPCB_Robot}", LogLevel.Information);

                WndMessenger ShowMessenger = new WndMessenger();
                ShowMessenger.MessengerShow("Messenger : Save Data Successfully ");

            }
            else
            {
                WndMessenger ShowMessenger = new WndMessenger();
                ShowMessenger.MessengerShow("Messenger : Save Data Was NOT Successful ");
            }
            this.UpdateUIOneShot();

        }

        private void BtSetting5_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_SETTING_ROBOT);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_ROBOT);
        }
        private void BtSetting4_Click(object sender, RoutedEventArgs e)
        {

            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_SETTING_SERVO);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_SERVO);

        }

        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_SETTING_ALARM);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_ALARM);
        }

        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU_DELAY_MACHINE);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_DELAY_MACHINE);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU);
        }

        private async void PgSuperUserMenu4_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1);
            this.UpdateUIOneShot();
        }

        private void UpdateUIOneShot()
        {
            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.ReadMultiWord(DeviceCode.D, 3600, 900, out D_ListUpdateDevicePLC_3600);

            }
            this.UpdateUIDevice();
        }
        private void UpdateUIDevice()
        {
            if (UiManager.PLC.isOpen())
            {
                Dispatcher.Invoke(() =>
                {
                    if (D_ListUpdateDevicePLC_3600.Count >= 1)
                    {
                        this.TbxDevice1.Text = D_ListUpdateDevicePLC_3600[0].ToString();
                        this.TbxDevice2.Text = D_ListUpdateDevicePLC_3600[1].ToString();
                        this.TbxDevice3.Text = D_ListUpdateDevicePLC_3600[2].ToString();
                        this.TbxDevice4.Text = D_ListUpdateDevicePLC_3600[3].ToString();

                        this.TbxDevice19.Text = D_ListUpdateDevicePLC_3600[10].ToString();
                        this.TbxDevice20.Text = D_ListUpdateDevicePLC_3600[11].ToString();
                        this.TbxDevice21.Text = D_ListUpdateDevicePLC_3600[12].ToString();
                        this.TbxDevice22.Text = D_ListUpdateDevicePLC_3600[13].ToString();
                        this.TbxDevice23.Text = D_ListUpdateDevicePLC_3600[14].ToString();
                        this.TbxDevice24.Text = D_ListUpdateDevicePLC_3600[15].ToString();
                       
                    }
                });
            }
        }
    }
}
