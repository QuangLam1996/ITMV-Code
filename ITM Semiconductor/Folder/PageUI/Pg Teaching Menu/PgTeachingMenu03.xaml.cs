using ITM_Semiconductor.Properties;
using KeyPad;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
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
    /// Interaction logic for PgTeachingMenu03.xaml
    /// </summary>
    /// DO QUANG LAM
    public partial class PgTeachingMenu03 : Page
    {
        private int ToolOnCH1 = 0;

        private int OnePaletJigCH1 = 0;

        private int PickJigCH1 = 0;


        private int RowTrayCH1 = 0;

        private int ColumTrayCH1 = 0;

        private int PaletCH1 = 0;

        private int NumberPalletJig = 0;
        private int NumberrowCreatPickPalletJig = 0;
        private int NumbercolumnCreatPickPalletJig = 0;

        private int NumberrowCreatPlacePalletTray = 0;
        private int NumbercolumnCreatPlacePalletTray = 0;

        private int HaveToolFrame = 0;

        private bool OnOffFrame = false;



        public PgTeachingMenu03()
        {
            InitializeComponent();
            this.Loaded += PgTeachingMenu03_Loaded;
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;

            this.btSave.Click += BtSave_Click;

            this.btnOffFrame.Click += BtnOffFrame_Click;
            this.btnOnFrame.Click += BtnOnFrame_Click;

            this.tbxColumTrayCH1.TouchDown += Tbx_TouchDown;
            this.tbxNumbercolumnCreatPlacePalletTray.TouchDown += Tbx_TouchDown;
            this.tbxNumberPalletJig.TouchDown += Tbx_TouchDown;
            this.tbxNumberrowCreatPickPallet.TouchDown += Tbx_TouchDown;
            this.tbxNumberColumnCreatPlacePallet.TouchDown += Tbx_TouchDown;
            this.tbxNumberrowCreatPlacePalletTray.TouchDown += Tbx_TouchDown;
            this.tbxPaletJigCH1.TouchDown += Tbx_TouchDown;
            this.tbxPaletRowCH1.TouchDown += Tbx_TouchDown;
            this.tbxPickJigCH1.TouchDown += Tbx_TouchDown;
            this.tbxRowTrayCH1.TouchDown += Tbx_TouchDown;
            this.tbxToolOnCH1.TouchDown += Tbx_TouchDown;



        }

        private void BtnOnFrame_Click(object sender, RoutedEventArgs e)
        {
            this.OnOffFrame = true;
            this.btnOnFrame.Background = Brushes.LightGreen;
            this.btnOffFrame.Background = Brushes.White;
        }

        private void BtnOffFrame_Click(object sender, RoutedEventArgs e)
        {
            this.OnOffFrame = false;
            this.btnOnFrame.Background = Brushes.White;
            this.btnOffFrame.Background = Brushes.LightGreen;
        }

        private void Tbx_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;

            }
        }

        private void PgTeachingMenu03_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (!UiManager.PLC.isOpen())
                return;
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7700, out ToolOnCH1);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7702, out OnePaletJigCH1);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7704, out PickJigCH1);

            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7706, out RowTrayCH1);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7708, out ColumTrayCH1);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7710, out PaletCH1);

            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7712, out NumberPalletJig);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7714, out NumberrowCreatPickPalletJig);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7716, out NumbercolumnCreatPickPalletJig);

            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7718, out NumberrowCreatPlacePalletTray);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7720, out NumbercolumnCreatPlacePalletTray);

            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 7722, out HaveToolFrame);



            tbxToolOnCH1.Text = ToolOnCH1.ToString();
            tbxPaletJigCH1.Text = OnePaletJigCH1.ToString();
            tbxPickJigCH1.Text = PickJigCH1.ToString();


            tbxRowTrayCH1.Text = RowTrayCH1.ToString();
            tbxColumTrayCH1.Text = ColumTrayCH1.ToString();
            tbxPaletRowCH1.Text = PaletCH1.ToString();

            tbxNumberPalletJig.Text = NumberPalletJig.ToString();
            tbxNumberrowCreatPickPallet.Text = NumberrowCreatPickPalletJig.ToString();
            tbxNumberColumnCreatPlacePallet.Text = NumbercolumnCreatPickPalletJig.ToString();

            tbxNumberrowCreatPlacePalletTray.Text = NumberrowCreatPlacePalletTray.ToString();
            tbxNumbercolumnCreatPlacePalletTray.Text = NumbercolumnCreatPlacePalletTray.ToString();

            if (HaveToolFrame == 0)
            {
                this.btnOnFrame.Background = Brushes.White;
                this.btnOffFrame.Background = Brushes.LightGreen;
            }
            else if (HaveToolFrame == 1)
            {
                this.btnOffFrame.Background = Brushes.White;
                this.btnOnFrame.Background = Brushes.LightGreen;
            }

        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {

            if (!UiManager.PLC.isOpen())
                return;
            UserManager.createUserLog(UserActions.LOTIN_BUTTON_OK);
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("Do You Want Save Setting?"))
                return;
            ToolOnCH1 = Convert.ToInt16(this.tbxToolOnCH1.Text);
            OnePaletJigCH1 = Convert.ToInt16(this.tbxPaletJigCH1.Text);
            PickJigCH1 = Convert.ToInt16(this.tbxPickJigCH1.Text);


            RowTrayCH1 = Convert.ToInt16(this.tbxRowTrayCH1.Text);
            ColumTrayCH1 = Convert.ToInt16(this.tbxColumTrayCH1.Text);
            PaletCH1 = Convert.ToInt16(this.tbxPaletRowCH1.Text);

            NumberPalletJig = Convert.ToInt16(this.tbxNumberPalletJig.Text);
            NumberrowCreatPickPalletJig = Convert.ToInt16(this.tbxNumberrowCreatPickPallet.Text);
            NumbercolumnCreatPickPalletJig = Convert.ToInt16(this.tbxNumberColumnCreatPlacePallet.Text);

            NumberrowCreatPlacePalletTray = Convert.ToInt16(this.tbxNumberrowCreatPlacePalletTray.Text);
            NumbercolumnCreatPlacePalletTray = Convert.ToInt16(this.tbxNumbercolumnCreatPlacePalletTray.Text);

            if (OnOffFrame)
            {
                HaveToolFrame = 1;
            }
            else
            {
                HaveToolFrame = 0;
            }




            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7700, ToolOnCH1);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7702, OnePaletJigCH1);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7704, PickJigCH1);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7706, RowTrayCH1);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7708, ColumTrayCH1);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7710, PaletCH1);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7712, NumberPalletJig);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7714, NumberrowCreatPickPalletJig);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7716, NumbercolumnCreatPickPalletJig);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7718, NumberrowCreatPlacePalletTray);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7720, NumbercolumnCreatPlacePalletTray);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 7722, HaveToolFrame);


            UpdateUI();

            WndMessenger ShowMessenger = new WndMessenger();
            ShowMessenger.MessengerShow("Messenger : Save Data Successfully ");

        }

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
            UserManager.createUserLog(UserActions.PAGE_TEACHING_MENU);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU);

        }
    }
}
