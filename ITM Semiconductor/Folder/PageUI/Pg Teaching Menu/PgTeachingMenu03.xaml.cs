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
    public partial class PgTeachingMenu03 : Page
    {
        private int D200 = 0;
        private int D230 = 0;
        private int D202 = 0;
        private int D204 = 0;
        private int D210 = 0;
        private int D212 = 0;
        private int D220 = 0;
        private int D222 = 0;

        //Test Git Hub
        private int D223 = 0;
        private int D224 = 0;

        //Test Git Hub
        private int D225 = 0;
        private int D226 = 0;
        //Test Git Hub
        private int D227 = 0;


        public PgTeachingMenu03()
        {
            InitializeComponent();
            this.Loaded += PgTeachingMenu03_Loaded;
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;

            this.btSave.Click += BtSave_Click;

            this.tbxToolOnCH1.TouchDown += Tbx_TouchDown;
            this.tbxPaletJig.TouchDown += Tbx_TouchDown;

            this.tbxTrayColumnPallet.TouchDown += Tbx_TouchDown;
            this.tbxTrayRowPallet.TouchDown += Tbx_TouchDown;

            this.tbxJIGColumnPallet.TouchDown += Tbx_TouchDown;
            this.tbxJIGRowPallet.TouchDown += Tbx_TouchDown;

            this.tbxTrayColumn.TouchDown += Tbx_TouchDown;
            this.tbxTrayRow.TouchDown += Tbx_TouchDown;
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
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 200, out D200);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 230, out D230);

            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 202, out D202);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 204, out D204);

            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 210, out D210);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 212, out D212);

            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 220, out D220);
            UiManager.PLC.ReadDoubleWord(Mitsubishi.DeviceCode.D, 222, out D222);

            tbxToolOnCH1.Text = D200.ToString();
            tbxPaletJig.Text = D230.ToString();

            tbxTrayColumn.Text = D202.ToString();
            tbxTrayRow.Text = D204.ToString();

            tbxTrayColumnPallet.Text = D210.ToString();
            tbxTrayRowPallet.Text = D212.ToString();

            tbxJIGColumnPallet.Text = D220.ToString();
            tbxJIGRowPallet.Text = D220.ToString();
        }
        private void BtSave_Click(object sender, RoutedEventArgs e)
        {

            if (!UiManager.PLC.isOpen())
                return;
            UserManager.createUserLog(UserActions.LOTIN_BUTTON_OK);
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("Do You Want Save Setting?"))
                return;
            D200 = Convert.ToInt16(this.tbxToolOnCH1.Text);
            D230 = Convert.ToInt16(this.tbxPaletJig.Text);

            D202 = Convert.ToInt16(this.tbxTrayColumn.Text);
            D204 = Convert.ToInt16(this.tbxTrayRow.Text);

            D210 = Convert.ToInt16(this.tbxTrayColumnPallet.Text);
            D212 = Convert.ToInt16(this.tbxTrayRowPallet.Text);

            D220 = Convert.ToInt16(this.tbxJIGColumnPallet.Text);
            D222 = Convert.ToInt16(this.tbxJIGRowPallet.Text);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 200, D200);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 230, D230);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 202, D202);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 204, D204);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 210, D210);
            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 212, D212);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 220, D220);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 222, D222);

            UiManager.PLC.WriteDoubleWord(Mitsubishi.DeviceCode.D, 222, D222);


            UiManager.PLC.WriteBit(Mitsubishi.DeviceCode.L, 04, true);

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
