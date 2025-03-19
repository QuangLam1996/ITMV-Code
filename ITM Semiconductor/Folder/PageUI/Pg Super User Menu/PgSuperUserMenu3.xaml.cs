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
    /// Interaction logic for PgSuperUserMenu3.xaml
    /// </summary>
    public partial class PgSuperUserMenu3 : Page
    {

        private List<int> D_ListUpdateDevicePLC_2000 = new List<int>();
        private MyLogger logger = new MyLogger("PG_SUPER_USE_SERVO_SETUP");
        public PgSuperUserMenu3()
        {
            InitializeComponent();
            this.Loaded += PgSuperUserMenu3_Loaded;
            this.Unloaded += PgSuperUserMenu3_Unloaded;
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.btSetting5.Click += BtSetting5_Click;
            this.BtnSave.Click += BtnSave_Click;



            this.TbxDevice1.TouchDown += TBNotCheck_TouchDown;
            this.TbxDevice1.PreviewMouseDown += TBNotCheck_PreviewMouseDown;

            this.TbxDevice2.TouchDown += TBNotCheck_TouchDown;
            this.TbxDevice2.PreviewMouseDown += TBNotCheck_PreviewMouseDown;

            this.TbxDevice3.TouchDown += TB_TouchDown;
            this.TbxDevice3.PreviewMouseDown += TB_PreviewMouseDown;

            this.TbxDevice4.TouchDown += TB_TouchDown;
            this.TbxDevice4.PreviewMouseDown += TB_PreviewMouseDown;

            this.TbxDevice5.TouchDown += TB_TouchDown;
            this.TbxDevice5.PreviewMouseDown += TB_PreviewMouseDown;

            this.TbxDevice19.TouchDown += TBNotCheck_TouchDown;
            this.TbxDevice19.PreviewMouseDown += TBNotCheck_PreviewMouseDown;

            this.TbxDevice20.TouchDown += TBNotCheck_TouchDown;
            this.TbxDevice20.PreviewMouseDown += TBNotCheck_PreviewMouseDown;

            this.TbxDevice21.TouchDown += TB_TouchDown;
            this.TbxDevice21.PreviewMouseDown += TB_PreviewMouseDown;

            this.TbxDevice22.TouchDown += TB_TouchDown;
            this.TbxDevice22.PreviewMouseDown += TB_PreviewMouseDown;

            this.TbxDevice23.TouchDown += TB_TouchDown;
            this.TbxDevice23.PreviewMouseDown += TB_PreviewMouseDown;

        }

       

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("You Want Save Setting?")) return;


            int Ax1_ACC_TIME =  Convert.ToInt32(Math.Round(Convert.ToDouble(TbxDevice1.Text)));
            int Ax1_DEC_TIME = Convert.ToInt32(Math.Round(Convert.ToDouble(TbxDevice2.Text)));

            int Ax2_ACC_TIME = Convert.ToInt32(Math.Round(Convert.ToDouble(TbxDevice19.Text)));
            int Ax2_DEC_TIME = Convert.ToInt32(Math.Round(Convert.ToDouble(TbxDevice20.Text)));

            int Ax1_SPEED_LIMIT_ALL = (int)(Convert.ToDouble(TbxDevice3.Text) * 100);
            int Ax1_SPEED_ORG = (int)(Convert.ToDouble(TbxDevice4.Text) * 100);
            int Ax1_SPEED_JOG = (int)(Convert.ToDouble(TbxDevice5.Text) * 100);

            int Ax2_SPEED_LIMIT_ALL = (int)(Convert.ToDouble(TbxDevice21.Text) * 100);
            int Ax2_SPEED_ORG = (int)(Convert.ToDouble(TbxDevice22.Text) * 100);
            int Ax2_SPEED_JOG = (int)(Convert.ToDouble(TbxDevice23.Text) * 100);



            if (UiManager.PLC.isOpen())
            {
                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX1_ACC_TIME, Ax1_ACC_TIME);
                logger.Create($"PC_Write_AX1_ACC_TIME: {PLCStore.D_AX1_ACC_TIME}  = {Ax1_ACC_TIME}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX1_DCC_TIME, Ax1_DEC_TIME);
                logger.Create($"PC_Write_AX1_DEC_TIME: {PLCStore.D_AX1_DCC_TIME}  = {Ax1_DEC_TIME}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX2_ACC_TIME, Ax2_ACC_TIME);
                logger.Create($"PC_Write_AX2_ACC_TIME: {PLCStore.D_AX2_ACC_TIME}  = {Ax2_ACC_TIME}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX2_DCC_TIME, Ax2_DEC_TIME);
                logger.Create($"PC_Write_AX2_DEC_TIME: {PLCStore.D_AX2_DCC_TIME}  = {Ax2_DEC_TIME}", LogLevel.Information);


                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX1_SPEED_LIMIR_ALL, Ax1_SPEED_LIMIT_ALL);
                logger.Create($"PC_Write_AX1_LIMIT_SPEED_ALL: {PLCStore.D_AX1_SPEED_LIMIR_ALL}  = {Ax1_SPEED_LIMIT_ALL}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX1_JOG_SPEED,Ax1_SPEED_JOG);
                logger.Create($"PC_Write_AX1_JOG_SPPED: {PLCStore.D_AX1_JOG_SPEED}  = {Ax1_SPEED_JOG}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX1_ORG_SPEED, Ax1_SPEED_ORG);
                logger.Create($"PC_Write_AX1_ORG_SPEED: {PLCStore.D_AX1_ORG_SPEED}  = {Ax1_SPEED_ORG}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX2_SPEED_LIMIR_ALL, Ax2_SPEED_LIMIT_ALL);
                logger.Create($"PC_Write_AX2_LIMIT_SPEED_ALL: {PLCStore.D_AX2_SPEED_LIMIR_ALL}  = {Ax2_SPEED_LIMIT_ALL}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX2_JOG_SPEED, Ax2_SPEED_JOG);
                logger.Create($"PC_Write_AX2_JOG_SPPED: {PLCStore.D_AX2_JOG_SPEED}  = {Ax2_SPEED_JOG}", LogLevel.Information);

                UiManager.PLC.WriteDoubleWord(DeviceCode.D, PLCStore.D_AX2_ORG_SPEED, Ax2_SPEED_ORG);
                logger.Create($"PC_Write_AX2_ORG_SPEED: {PLCStore.D_AX2_ORG_SPEED}  = {Ax2_SPEED_ORG}", LogLevel.Information);

                WndMessenger ShowMessenger = new WndMessenger();
                ShowMessenger.MessengerShow("Messenger : Save Data Successfully ");


            }
            else
            {
                WndMessenger ShowMessenger = new WndMessenger();
                ShowMessenger.MessengerShow("Messenger : Save Data Was NOT Successful ");
            }
            this.UpdateOneShotPLC();
        }

        private void PgSuperUserMenu3_Unloaded(object sender, RoutedEventArgs e)
        {
           
        }

        private async void PgSuperUserMenu3_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1);
            this.UpdateOneShotPLC();
        }

        private void TB_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
                TextBox_TextChanged(textbox, new RoutedEventArgs());
            }
        }

        private void TB_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
                TextBox_TextChanged(textbox, new RoutedEventArgs());
            }
        }

        private void TBNotCheck_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
                
            }
        }

        private void TBNotCheck_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
                
            }
        }

        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(textBox.Text) && float.TryParse(textBox.Text, out float number))
            {
                if (!textBox.Text.Contains('.'))
                {
                    // Thêm "0.000" sau dấu phẩy cho số nguyên
                    textBox.Text = $"{textBox.Text}.00";
                }
                else
                {
                    string[] parts = textBox.Text.Split('.');
                    if (parts.Length == 2)
                    {
                        if (parts[1].Length > 2)
                        {
                            // Chỉ lấy ba số sau dấu phẩy
                            textBox.Text = $"{parts[0]}.{parts[1].Substring(0, 2)}";
                        }
                        else if (parts[1].Length < 3)
                        {
                            // Nếu ít hơn ba số sau dấu phẩy, thêm số 0 cho đủ
                            textBox.Text = $"{parts[0]}.{parts[1].PadRight(2, '0')}";
                        }
                    }
                }
            }
            else
            {
               
            }


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

        private void UpdateOneShotPLC()
        {
            if(UiManager.PLC.isOpen())
            {
                UiManager.PLC.ReadMultiDoubleWord(DeviceCode.D, 2000, 900, out D_ListUpdateDevicePLC_2000);

            }
            this.UpdateUIDevice();
        }
        private void UpdateUIDevice()
        {
            if (UiManager.PLC.isOpen())
            {
               Dispatcher.Invoke(() => 
               {
                   if(D_ListUpdateDevicePLC_2000.Count >=1)
                   {
                       this.TbxDevice1.Text = this.D_ListUpdateDevicePLC_2000[2].ToString();
                       this.TbxDevice2.Text = this.D_ListUpdateDevicePLC_2000[42].ToString();

                       this.TbxDevice19.Text = this.D_ListUpdateDevicePLC_2000[4].ToString();
                       this.TbxDevice20.Text = this.D_ListUpdateDevicePLC_2000[44].ToString();

                       this.TbxDevice3.Text = this.FormatNumber(D_ListUpdateDevicePLC_2000[82]);
                       this.TbxDevice4.Text = this.FormatNumber(D_ListUpdateDevicePLC_2000[162]);
                       this.TbxDevice5.Text = this.FormatNumber(D_ListUpdateDevicePLC_2000[242]);

                       this.TbxDevice21.Text = this.FormatNumber(D_ListUpdateDevicePLC_2000[84]);
                       this.TbxDevice22.Text = this.FormatNumber(D_ListUpdateDevicePLC_2000[164]);
                       this.TbxDevice23.Text = this.FormatNumber(D_ListUpdateDevicePLC_2000[244]);
                   }    
               });
            }
        }
        public string FormatNumber(long number)
        {
            bool isNegative = number < 0;
            double dividedNumber = number / 100.0;
            string numberStr = Math.Abs(dividedNumber).ToString("F2"); // Format with 2 decimal places

            string[] parts = numberStr.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1] : "00";

            string formattedIntegerPart = "";
            while (integerPart.Length > 3)
            {
                formattedIntegerPart = "," + integerPart.Substring(integerPart.Length - 3) + formattedIntegerPart;
                integerPart = integerPart.Substring(0, integerPart.Length - 3);
            }
            formattedIntegerPart = integerPart + formattedIntegerPart;

            string formatted = formattedIntegerPart + "." + decimalPart;

            if (isNegative)
            {
                formatted = "-" + formatted;
            }
            return formatted;
        }
    }
}
