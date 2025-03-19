using DTO;
using KeyPad;
using Mitsubishi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations.Model;
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
    /// Interaction logic for PgSuperUserMenu1.xaml
    /// </summary>
    public partial class PgSuperUserMenu1 : Page
    {
        MyLogger logger = new MyLogger("PagePgSuperUserMenu1");
        private const int DEVICE1 = 200;  
        private const int DEVICE2 = 202;
        private const int DEVICE3 = 204;
        private const int DEVICE4 = 206;
        private const int DEVICE5 = 208;

        #region khai bao bien so sanh
        float MinDevive01 = 10;
        float MinDevive02 = 10;
        float MinDevive03 = 10;
        float MinDevive04 = 10;
        float MinDevive05 = 0;
        float MinDevive06 = 0;
        float MinDevive07 = 0;
        float MinDevive08 = 0;
        float MinDevive09 = 0;
        float MinDevive10 = 0;
        float MinDevive11 = 0;
        float MinDevive12 = 0;
        float MinDevive13 = 0;
        float MinDevive14 = 0;
        float MinDevive15 = 0;
        float MinDevive16 = 0;
        float MinDevive17 = 0;
        float MinDevive18 = 0;
        float MinDevive19 = 0;
        float MinDevive20 = 0;
        float MinDevive21 = 0;
        float MinDevive22 = 0;
        float MinDevive23 = 0;
        float MinDevive24 = 0;
        float MinDevive25 = 0;
        float MinDevive26 = 0;
        float MinDevive27 = 0;
        float MinDevive28 = 0;
        float MinDevive29 = 0;
        float MinDevive30 = 0;
        float MinDevive31 = 0;
        float MinDevive32 = 0;
        float MinDevive33 = 0;
        float MinDevive34 = 0;
        float MinDevive35 = 0;
        float MinDevive36 = 0;

        float MaxDevice01 = 100;
        float MaxDevice02 = 100;
        float MaxDevice03 = 100;
        float MaxDevice04 = 100;
        float MaxDevice05 = 100;
        float MaxDevice06 = 100;
        float MaxDevice07 = 100;
        float MaxDevice08 = 100;
        float MaxDevice09 = 100;
        float MaxDevice10 = 100;
        float MaxDevice11 = 100;
        float MaxDevice12 = 100;
        float MaxDevice13 = 100;
        float MaxDevice14 = 100;
        float MaxDevice15 = 100;
        float MaxDevice16 = 100;
        float MaxDevice17 = 100;
        float MaxDevice18 = 100;
        float MaxDevice19 = 100;
        float MaxDevice20 = 100;
        float MaxDevice21 = 100;
        float MaxDevice22 = 100;
        float MaxDevice23 = 100;
        float MaxDevice24 = 100;
        float MaxDevice25 = 100;
        float MaxDevice26 = 100;
        float MaxDevice27 = 100;
        float MaxDevice28 = 100;
        float MaxDevice29 = 100;
        float MaxDevice30 = 100;
        float MaxDevice31 = 100;
        float MaxDevice32 = 100;
        float MaxDevice33 = 100;
        float MaxDevice34 = 100;
        float MaxDevice35 = 100;
        float MaxDevice36 = 100;
        #endregion

        private Q_Enthernet PLC;
        public PgSuperUserMenu1()
        {
            InitializeComponent();
            this.Loaded += PgSuperUserMenu1_Loaded;
            this.Unloaded += PgSuperUserMenu1_Unloaded;
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.BtnSave.Click += BtnSave_Click;
            this.BtnUndo.Click += BtnUndo_Click;

            #region Event TouchDown
            //this.TbxDevice01.PreviewMouseDown += TbxDevice01_PreviewMouseDown;
            this.TbxDevice1.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice2.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice3.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice4.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice5.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice6.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice7.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice8.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice9.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice10.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice11.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice12.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice13.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice14.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice15.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice16.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice17.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice18.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice19.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice20.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice21.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice22.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice23.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice24.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice25.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice26.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice27.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice28.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice29.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice30.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice31.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice32.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice33.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice34.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice35.TouchDown += TbxDevice_TouchDown;
            this.TbxDevice36.TouchDown += TbxDevice_TouchDown;
            #endregion

        }

       

        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm comfirmYesNo = new WndConfirm();
            if (!comfirmYesNo.DoComfirmYesNo("Clear parameters to default")) return;
            this.TbxDevice1.Text = MinDevive01.ToString();
            this.TbxDevice2.Text = MinDevive02.ToString();
            this.TbxDevice3.Text = MinDevive03.ToString();
            this.TbxDevice4.Text = MinDevive04.ToString();
            this.TbxDevice5.Text = MinDevive05.ToString();
            this.TbxDevice6.Text = MinDevive06.ToString();
            this.TbxDevice7.Text = MinDevive07.ToString();
            this.TbxDevice8.Text = MinDevive08.ToString();
            this.TbxDevice9.Text = MinDevive09.ToString();
            this.TbxDevice10.Text = MinDevive10.ToString();
            this.TbxDevice11.Text = MinDevive11.ToString();
            this.TbxDevice12.Text = MinDevive12.ToString();
            this.TbxDevice13.Text = MinDevive13.ToString();
            this.TbxDevice14.Text = MinDevive14.ToString();
            this.TbxDevice15.Text = MinDevive15.ToString();
            this.TbxDevice16.Text = MinDevive16.ToString();
            this.TbxDevice17.Text = MinDevive17.ToString();
            this.TbxDevice18.Text = MinDevive18.ToString();
            this.TbxDevice19.Text = MinDevive19.ToString();
            this.TbxDevice20.Text = MinDevive20.ToString();
            this.TbxDevice21.Text = MinDevive21.ToString();
            this.TbxDevice22.Text = MinDevive22.ToString();
            this.TbxDevice23.Text = MinDevive23.ToString();
            this.TbxDevice24.Text = MinDevive24.ToString();
            this.TbxDevice25.Text = MinDevive25.ToString();
            this.TbxDevice26.Text = MinDevive26.ToString();
            this.TbxDevice27.Text = MinDevive27.ToString();
            this.TbxDevice28.Text = MinDevive28.ToString();
            this.TbxDevice29.Text = MinDevive29.ToString();
            this.TbxDevice30.Text = MinDevive30.ToString();
            this.TbxDevice31.Text = MinDevive31.ToString();
            this.TbxDevice32.Text = MinDevive32.ToString();
            this.TbxDevice33.Text = MinDevive33.ToString();
            this.TbxDevice34.Text = MinDevive34.ToString();
            this.TbxDevice35.Text = MinDevive35.ToString();
            this.TbxDevice36.Text = MinDevive36.ToString();
        }

        private void PgSuperUserMenu1_Unloaded(object sender, RoutedEventArgs e)
        {
            this.PLCStop();
        }
        private async void PgSuperUserMenu1_Loaded(object sender, RoutedEventArgs e)
        {
            this.Uploadtextbox();
            await PLCConnect();
            this.UpdateUI();

        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WndConfirm comfirmYesNo = new WndConfirm();
                if (!comfirmYesNo.DoComfirmYesNo("You Want Save Setting?")) return;
                if (PLC.isOpen())
                {
                    //TextBox[] textBoxes = { TbxDevice1, TbxDevice2, TbxDevice3 , TbxDevice4 , TbxDevice5 };
                    //for (int i = 0; i < textBoxes.Length; i++)
                    //{
                    //    if (float.TryParse(textBoxes[i].Text, out float outDevice))
                    //    {
                    //        float Device = outDevice * 10;
                    //        int writeDevice = (int)(Device);
                    //        PLC.WriteWord(DeviceCode.D, DEVICE1 + i, writeDevice);
                    //    }
                    //}
                    if (float.TryParse(TbxDevice1.Text, out float OutDevice01))
                    {
                        float floatValue01 = OutDevice01 * 10;
                        int WriteDevice01 = (int)(floatValue01);
                        PLC.WriteWord(DeviceCode.D, DEVICE1, WriteDevice01);
                    }

                    if (float.TryParse(TbxDevice2.Text, out float OutDevice02))
                    {
                        float floatValue02 = OutDevice02 * 10;
                        int WriteDevice02 = (int)(floatValue02);
                        PLC.WriteWord(DeviceCode.D, DEVICE2, WriteDevice02);
                    }

                    if (float.TryParse(TbxDevice3.Text, out float OutDevice03))
                    {
                        float floatValue03 = OutDevice03 * 10;
                        int WriteDevice03 = (int)(floatValue03);
                        PLC.WriteWord(DeviceCode.D, DEVICE3, WriteDevice03);
                    }

                    if (float.TryParse(TbxDevice4.Text, out float OutDevice04))
                    {
                        float floatValue04 = OutDevice04 * 10;
                        int WriteDevice04 = (int)(floatValue04);
                        PLC.WriteWord(DeviceCode.D, DEVICE4, WriteDevice04);
                    }
                    if (float.TryParse(TbxDevice5.Text, out float OutDevice05))
                    {
                        float floatValue05 = OutDevice05 * 10;
                        int WriteDevice05 = (int)(floatValue05);
                        PLC.WriteWord(DeviceCode.D, DEVICE5, WriteDevice05);
                    }

                    this.UpdateUI();
                }
                else
                {
                    MessageBox.Show("Lưu không thành công. vui lòng kiếm tra lại ", "Thông báo Save dữ liệu vào PLC");
                }
            }
            catch (Exception ex)
            {
                logger.Create($"BtnSave_Click: {ex.Message}", LogLevel.Error);
            }
           
        }

        private void PLCStop()
        {
           
            if (PLC != null && PLC.isOpen() == true)
            {
                PLC.Disconnect();
            }
        }
        private async void UpdateUI()
        {
            await Task.Run(() =>
            {
                if (PLC.isOpen() == true)
                {
                    int ReadDevice01, ReadDevice02, ReadDevice03, ReadDevice04, ReadDevice05;
                    PLC.ReadWord(DeviceCode.D, DEVICE1, out ReadDevice01);
                    PLC.ReadWord(DeviceCode.D, DEVICE2, out ReadDevice02);
                    PLC.ReadWord(DeviceCode.D, DEVICE3, out ReadDevice03);
                    PLC.ReadWord(DeviceCode.D, DEVICE4, out ReadDevice04);
                    PLC.ReadWord(DeviceCode.D, DEVICE5, out ReadDevice05);

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.TbxDevice1.Text = "Spare";
                        this.TbxDevice2.Text = "Spare";
                        this.TbxDevice3.Text = "Spare";
                        this.TbxDevice4.Text = "Spare";
                        this.TbxDevice5.Text = "Spare";
                        this.TbxDevice6.Text = "Spare";
                        this.TbxDevice7.Text = "Spare";
                        this.TbxDevice8.Text = "Spare";
                        this.TbxDevice9.Text = "Spare";
                        this.TbxDevice10.Text = "Spare";
                        this.TbxDevice11.Text = "Spare";
                        this.TbxDevice12.Text = "Spare";
                        this.TbxDevice13.Text = "Spare";
                        this.TbxDevice14.Text = "Spare";
                        this.TbxDevice15.Text = "Spare";
                        this.TbxDevice16.Text = "Spare";
                        this.TbxDevice17.Text = "Spare";
                        this.TbxDevice18.Text = "Spare";
                        this.TbxDevice19.Text = "Spare";
                        this.TbxDevice20.Text = "Spare";
                        this.TbxDevice21.Text = "Spare";
                        this.TbxDevice22.Text = "Spare";
                        this.TbxDevice23.Text = "Spare";
                        this.TbxDevice24.Text = "Spare";
                        this.TbxDevice25.Text = "Spare";
                        this.TbxDevice26.Text = "Spare";
                        this.TbxDevice27.Text = "Spare";
                        this.TbxDevice28.Text = "Spare";
                        this.TbxDevice29.Text = "Spare";
                        this.TbxDevice30.Text = "Spare";
                        this.TbxDevice31.Text = "Spare";
                        this.TbxDevice32.Text = "Spare";
                        this.TbxDevice33.Text = "Spare";
                        this.TbxDevice34.Text = "Spare";
                        this.TbxDevice35.Text = "Spare";
                        this.TbxDevice36.Text = "Spare";

                        this.TbxDevice1.Text = (ReadDevice01 / 10f).ToString("F1");
                        this.TbxDevice2.Text = (ReadDevice02 / 10f).ToString("F1");
                        this.TbxDevice3.Text = (ReadDevice03 / 10f).ToString("F1");
                        this.TbxDevice4.Text = (ReadDevice04 / 10f).ToString("F1");
                        this.TbxDevice5.Text = (ReadDevice05 / 10f).ToString("F1");
                    }));
                }

            });
            if (PLC.isOpen() != true)
            {
                for (int i = 1; i <= 36; i++)
                {
                    string textBoxName = "TbxDevice" + i;
                    TextBox textBox = this.FindName(textBoxName) as TextBox;
                    if (textBox != null)
                    {
                        textBox.Text = "Error";
                    }
                }
                //MessageBox.Show("Không thể đọc được dữ liệu từ PLC . Vui lòng kiểm tra kết nối ", "Thông báo kết nối thất bại");
            }


        }
        public async Task PLCConnect()
        {
            PLC = new Q_Enthernet(UiManager.appSettings.Setting_PLCTcp.Ip1, UiManager.appSettings.Setting_PLCTcp.Port1);
            var kqTask = Task.Run(() => PLC.Connect());
            var kq = await kqTask;
            if (kq)
            {
                
            }
        }
        private void BtSetting2_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU1);
            UiManager.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU1);
        }

        private void BtSetting1_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU);
            UiManager.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU);
        }
        private void BtSetting3_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_SUPER_USER_MENU2);
            UiManager.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU2);
        }
        #region Event TouchDown
        private void TbxDevice01_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
            // Nhập keypad xong quay lại check điều kiện
            TextBox_TextChanged(textbox, new RoutedEventArgs());
        }

        private void TbxDevice_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
                textbox.Text = keyboardWindow.Result;
            // Nhập keypad xong quay lại check điều kiện
            TextBox_TextChanged(textbox, new RoutedEventArgs());
        }
        #endregion
        #region sosanh
        private void Uploadtextbox()
        {
            this.LbMin01.Content = this.MinDevive01;
            this.LbMin02.Content = this.MinDevive02;
            this.LbMin03.Content = this.MinDevive03;
            this.LbMin04.Content = this.MinDevive04;
            this.LbMin05.Content = this.MinDevive05;
            this.LbMin06.Content = this.MinDevive06;
            this.LbMin07.Content = this.MinDevive07;
            this.LbMin08.Content = this.MinDevive08;
            this.LbMin09.Content = this.MinDevive09;
            this.LbMin10.Content = this.MinDevive10;
            this.LbMin11.Content = this.MinDevive11;
            this.LbMin12.Content = this.MinDevive12;
            this.LbMin13.Content = this.MinDevive13;
            this.LbMin14.Content = this.MinDevive14;
            this.LbMin15.Content = this.MinDevive15;
            this.LbMin16.Content = this.MinDevive16;
            this.LbMin17.Content = this.MinDevive17;
            this.LbMin18.Content = this.MinDevive18;
            this.LbMin19.Content = this.MinDevive19;
            this.LbMin20.Content = this.MinDevive20;
            this.LbMin21.Content = this.MinDevive21;
            this.LbMin22.Content = this.MinDevive22;
            this.LbMin23.Content = this.MinDevive23;
            this.LbMin24.Content = this.MinDevive24;
            this.LbMin25.Content = this.MinDevive25;
            this.LbMin26.Content = this.MinDevive26;
            this.LbMin27.Content = this.MinDevive27;
            this.LbMin28.Content = this.MinDevive28;
            this.LbMin29.Content = this.MinDevive29;
            this.LbMin30.Content = this.MinDevive30;
            this.LbMin31.Content = this.MinDevive31;
            this.LbMin32.Content = this.MinDevive32;
            this.LbMin33.Content = this.MinDevive33;
            this.LbMin34.Content = this.MinDevive34;
            this.LbMin35.Content = this.MinDevive35;
            this.LbMin36.Content = this.MinDevive36;

            this.LbMax01.Content = this.MaxDevice01;
            this.LbMax02.Content = this.MaxDevice02;
            this.LbMax03.Content = this.MaxDevice03;
            this.LbMax04.Content = this.MaxDevice04;
            this.LbMax05.Content = this.MaxDevice05;
            this.LbMax06.Content = this.MaxDevice06;
            this.LbMax07.Content = this.MaxDevice07;
            this.LbMax08.Content = this.MaxDevice08;
            this.LbMax09.Content = this.MaxDevice09;
            this.LbMax10.Content = this.MaxDevice10;
            this.LbMax11.Content = this.MaxDevice11;
            this.LbMax12.Content = this.MaxDevice12;
            this.LbMax13.Content = this.MaxDevice13;
            this.LbMax14.Content = this.MaxDevice14;
            this.LbMax15.Content = this.MaxDevice15;
            this.LbMax16.Content = this.MaxDevice16;
            this.LbMax17.Content = this.MaxDevice17;
            this.LbMax18.Content = this.MaxDevice18;
            this.LbMax19.Content = this.MaxDevice19;
            this.LbMax20.Content = this.MaxDevice20;
            this.LbMax21.Content = this.MaxDevice21;
            this.LbMax22.Content = this.MaxDevice22;
            this.LbMax23.Content = this.MaxDevice23;
            this.LbMax24.Content = this.MaxDevice24;
            this.LbMax25.Content = this.MaxDevice25;
            this.LbMax26.Content = this.MaxDevice26;
            this.LbMax27.Content = this.MaxDevice27;
            this.LbMax28.Content = this.MaxDevice28;
            this.LbMax29.Content = this.MaxDevice29;
            this.LbMax30.Content = this.MaxDevice30;
            this.LbMax31.Content = this.MaxDevice31;
            this.LbMax32.Content = this.MaxDevice32;
            this.LbMax33.Content = this.MaxDevice33;
            this.LbMax34.Content = this.MaxDevice34;
            this.LbMax35.Content = this.MaxDevice35;
            this.LbMax36.Content = this.MaxDevice36;

        }
        private void TextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (!string.IsNullOrWhiteSpace(textBox.Text) && float.TryParse(textBox.Text, out float number))
            {
                // Lấy điều kiện cụ thể cho TextBox hiện tại (ví dụ: dựa trên tên của TextBox)
                float minCondition = GetMinCondition(textBox.Name);
                float maxCondition = GetMaxCondition(textBox.Name);

                if (number >= minCondition && number <= maxCondition)
                {

                    textBox.Background = Brushes.Black; // Trở lại màu nền mặc định
                }
                if (number < minCondition)
                {
                    textBox.Background = Brushes.Red;
                    MessageBox.Show($"Vui lòng nhập một số lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.", "Thông Bao Lỗi");
                    textBox.Text = "";
                    textBox.Text = minCondition.ToString();
                    textBox.Background = Brushes.Black;
                }
                if (number > maxCondition)
                {
                    textBox.Background = Brushes.Red;
                    MessageBox.Show($"Vui lòng nhập một số  lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.", "Thông Báo Lỗi");
                    textBox.Text = "";
                    textBox.Text += maxCondition.ToString();
                    textBox.Background = Brushes.Black;
                }
            }
            else
            {

                textBox.Background = Brushes.Red;
                textBox.Text = "0";
                textBox.Background = Brushes.Black;
            }
            if (textBox.Text.Contains('.'))
            {
                string[] parts = textBox.Text.Split('.');
                if (parts.Length == 2 && parts[1].Length > 1)
                {
                    textBox.Text = $"{parts[0]}.{parts[1][0]}"; // Chỉ lấy một số sau dấu phẩy
                }
            }



        }
        private float GetMinCondition(string textBoxName)
        {
            switch (textBoxName)
            {
                case "TbxDevice1":
                    return MinDevive01;
                case "TbxDevice2":
                    return MinDevive02;
                case "TbxDevice3":
                    return MinDevive03;
                case "TbxDevice4":
                    return MinDevive04;
                case "TbxDevice5":
                    return MinDevive05;
                case "TbxDevice6":
                    return MinDevive06;
                case "TbxDevice7":
                    return MinDevive07;
                case "TbxDevice8":
                    return MinDevive08;
                case "TbxDevice9":
                    return MinDevive09;
                case "TbxDevice10":
                    return MinDevive10;
                case "TbxDevice11":
                    return MinDevive11;
                case "TbxDevice12":
                    return MinDevive12;
                case "TbxDevice13":
                    return MinDevive13;
                case "TbxDevice14":
                    return MinDevive14;
                case "TbxDevice15":
                    return MinDevive15;
                case "TbxDevice16":
                    return MinDevive16;
                case "TbxDevice17":
                    return MinDevive17;
                case "TbxDevice18":
                    return MinDevive18;
                case "TbxDevice19":
                    return MinDevive19;
                case "TbxDevice20":
                    return MinDevive20;
                case "TbxDevice21":
                    return MinDevive21;
                case "TbxDevice22":
                    return MinDevive22;
                case "TbxDevice23":
                    return MinDevive23;
                case "TbxDevice24":
                    return MinDevive24;
                case "TbxDevice25":
                    return MinDevive25;
                case "TbxDevice26":
                    return MinDevive26;
                case "TbxDevice27":
                    return MinDevive27;
                case "TbxDevice28":
                    return MinDevive28;
                case "TbxDevice29":
                    return MinDevive30;
                case "TbxDevice31":
                    return MinDevive31;
                case "TbxDevice32":
                    return MinDevive32;
                case "TbxDevice33":
                    return MinDevive34;
                case "TbxDevice35":
                    return MinDevive35;
                case "TbxDevice36":
                    return MinDevive36;
               
                default:
                    return 0;
            }
        }

        
        private float GetMaxCondition(string textBoxName)
        {
           
            switch (textBoxName)
            {
                case "TbxDevice1":
                    return MaxDevice01;
                case "TbxDevice2":
                    return MaxDevice02;
                case "TbxDevice3":
                    return MaxDevice03;
                case "TbxDevice4":
                    return MaxDevice04;
                case "TbxDevice5":
                    return MaxDevice05;
                case "TbxDevice6":
                    return MaxDevice06;
                case "TbxDevice7":
                    return MaxDevice07;
                case "TbxDevice8":
                    return MaxDevice08;
                case "TbxDevice9":
                    return MaxDevice09;
                case "TbxDevice10":
                    return MaxDevice10;
                case "TbxDevice11":
                    return MaxDevice12;
                case "TbxDevice13":
                    return MaxDevice13;
                case "TbxDevice14":
                    return MaxDevice14;
                case "TbxDevice15":
                    return MaxDevice15;
                case "TbxDevice16":
                    return MaxDevice16;
                case "TbxDevice17":
                    return MaxDevice17;
                case "TbxDevice18":
                    return MaxDevice18;
                case "TbxDevice19":
                    return MaxDevice19;
                case "TbxDevice20":
                    return MaxDevice20;
                case "TbxDevice21":
                    return MaxDevice21;
                case "TbxDevice22":
                    return MaxDevice22;
                case "TbxDevice23":
                    return MaxDevice23;
                case "TbxDevice24":
                    return MaxDevice24;
                case "TbxDevice25":
                    return MaxDevice25;
                case "TbxDevice26":
                    return MaxDevice26;
                case "TbxDevice27":
                    return MaxDevice27;
                case "TbxDevice28":
                    return MaxDevice28;
                case "TbxDevice29":
                    return MaxDevice29;
                case "TbxDevice30":
                    return MaxDevice30;
                case "TbxDevice31":
                    return MaxDevice31;
                case "TbxDevice32":
                    return MaxDevice32;
                case "TbxDevice33":
                    return MaxDevice33;
                case "TbxDevice34":
                    return MaxDevice34;
                case "TbxDevice35":
                    return MaxDevice35;
                case "TbxDevice36":
                    return MaxDevice36;
                default:
                    return 50;
            }
        }
        #endregion
    }
}
