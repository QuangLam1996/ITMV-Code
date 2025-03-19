using DTO;
using KeyPad;
using Mitsubishi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
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
    /// Interaction logic for PgSuperUserMenu.xaml
    /// </summary>
    public partial class PgSuperUserMenu : Page
    {
        MyLogger logger = new MyLogger("PG_SuperUseMenu_Time_ON_OFF");
        #region Min Max
        float MinDevice1 = 0;
        float MinDevice2 = 0;
        float MinDevice3 = 0;
        float MinDevice4 = 0;
        float MinDevice5 = 0;
        float MinDevice6 = 0;
        float MinDevice7 = 0;
        float MinDevice8 = 0;
        float MinDevice9 = 0;
        float MinDevice10 = 0;
        float MinDevice11 = 0;
        float MinDevice12 = 0;
        float MinDevice13 = 0;
        float MinDevice14 = 0;
        float MinDevice15 = 0;
        float MinDevice16 = 0;
        float MinDevice17 = 0;
        float MinDevice18 = 0;
        float MinDevice19 = 0;
        float MinDevice20 = 0;
        float MinDevice21 = 0;
        float MinDevice22 = 0;
        float MinDevice23 = 0;
        float MinDevice24 = 0;
        float MinDevice25 = 0;
        float MinDevice26 = 0;
        float MinDevice27 = 0;
        float MinDevice28 = 0;
        float MinDevice29 = 0;
        float MinDevice30 = 0;
        float MinDevice31 = 0;
        float MinDevice32 = 0;
        float MinDevice33 = 0;
        float MinDevice34 = 0;
        float MinDevice35 = 0;
        float MinDevice36 = 0;
        


        float MaxDevice1 = 100;
        float MaxDevice2 = 100;
        float MaxDevice3 = 100;
        float MaxDevice4 = 100;
        float MaxDevice5 = 100;
        float MaxDevice6 = 100;
        float MaxDevice7 = 100;
        float MaxDevice8 = 100;
        float MaxDevice9 = 100;
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
        public PgSuperUserMenu()
        {
            InitializeComponent();
            this.btSetting1.Click += BtSetting1_Click;
            this.btSetting2.Click += BtSetting2_Click;
            this.btSetting3.Click += BtSetting3_Click;
            this.btSetting4.Click += BtSetting4_Click;
            this.btSetting5.Click += BtSetting5_Click;
            this.Loaded += PgSuperUserMenu_Loaded;

            this.btSave.Click += BtSave_Click;


            #region TBX_KEYPAD
            this.tbxTimeOn1.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn1.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn2.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn2.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn3.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn3.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn4.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn4.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn5.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn5.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn6.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn6.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn7.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn7.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn8.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn8.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn9.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn9.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn10.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn10.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn11.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn11.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn12.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn12.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn13.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn13.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn14.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn14.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn15.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn15.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn16.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn16.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn17.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn17.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn18.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn18.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn19.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn19.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn20.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn20.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn21.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn21.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn22.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn22.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn23.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn23.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn24.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn24.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn25.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn25.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn26.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn26.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn27.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn27.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn28.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn28.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn29.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn29.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn30.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn30.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn31.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn31.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn32.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn32.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn33.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn33.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn34.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn34.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn35.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn35.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOn36.TouchDown += Tbx_TouchDown;
            this.tbxTimeOn36.PreviewMouseDown += Tbx_PreviewMouseDown;



            this.tbxTimeOff1.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff1.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff2.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff2.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff3.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff3.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff4.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff4.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff5.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff5.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff6.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff6.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff7.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff7.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff8.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff8.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff9.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff9.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff10.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff10.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff11.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff11.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff12.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff12.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff13.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff13.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff14.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff14.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff15.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff15.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff16.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff16.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff17.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff17.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff18.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff18.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff19.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff19.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff20.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff20.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff21.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff21.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff22.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff22.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff23.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff23.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff24.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff24.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff25.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff25.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff26.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff26.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff27.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff27.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff28.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff28.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff29.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff29.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff30.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff30.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff31.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff31.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff32.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff32.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff33.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff33.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff34.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff34.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff35.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff35.PreviewMouseDown += Tbx_PreviewMouseDown;

            this.tbxTimeOff36.TouchDown += Tbx_TouchDown;
            this.tbxTimeOff36.PreviewMouseDown += Tbx_PreviewMouseDown;
            #endregion
        }

     
        private async void BtSave_Click(object sender, RoutedEventArgs e)
        {
            WndConfirm confirmYesNo = new WndConfirm();
            if (!confirmYesNo.DoComfirmYesNo("You Want Save Setting?")) return;
            await Task.Delay(1);

            int TimeOn1 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn1.Text) * 10);
            int TimeOn2 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn2.Text) * 10);
            int TimeOn3 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn3.Text) * 10);
            int TimeOn4 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn4.Text) * 10);
            int TimeOn5 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn5.Text) * 10);

            int TimeOn6 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn6.Text) * 10);
            int TimeOn7 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn7.Text) * 10);
            int TimeOn8 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn8.Text) * 10);
            int TimeOn9 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn9.Text) * 10);
            int TimeOn10 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn10.Text) * 10);

            int TimeOn11 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn11.Text) * 10);
            int TimeOn12 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn12.Text) * 10);
            int TimeOn13 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn13.Text) * 10);
            int TimeOn14 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn14.Text) * 10);
            int TimeOn15 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn15.Text) * 10);

            int TimeOn16 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn16.Text) * 10);
            int TimeOn17 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn17.Text) * 10);
            int TimeOn18 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn18.Text) * 10);
            int TimeOn19 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn19.Text) * 10);
            int TimeOn20 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn20.Text) * 10);

            int TimeOn21 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn21.Text) * 10);
            int TimeOn22 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn22.Text) * 10);
            int TimeOn23 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn23.Text) * 10);
            int TimeOn24 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn24.Text) * 10);
            int TimeOn25 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn25.Text) * 10);

            int TimeOn26 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn26.Text) * 10);
            int TimeOn27 = Convert.ToInt16(Convert.ToDouble(tbxTimeOn27.Text) * 10);




            int TimeOff1 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff1.Text) * 10);
            int TimeOff2 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff2.Text) * 10);
            int TimeOff3 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff3.Text) * 10);
            int TimeOff4 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff4.Text) * 10);
            int TimeOff5 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff5.Text) * 10);

            int TimeOff6 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff6.Text) * 10);
            int TimeOff7 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff7.Text) * 10);
            int TimeOff8 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff8.Text) * 10);
            int TimeOff9 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff9.Text) * 10);
            int TimeOff10 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff10.Text) * 10);

            int TimeOff11 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff11.Text) * 10);
            int TimeOff12 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff12.Text) * 10);
            int TimeOff13 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff13.Text) * 10);
            int TimeOff14 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff14.Text) * 10);
            int TimeOff15 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff15.Text) * 10);

            int TimeOff16 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff16.Text) * 10);
            int TimeOff17 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff17.Text) * 10);
            int TimeOff18 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff18.Text) * 10);
            int TimeOff19 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff19.Text) * 10);
            int TimeOff20 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff20.Text) * 10);

            int TimeOff21 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff21.Text) * 10);
            int TimeOff22 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff22.Text) * 10);
            int TimeOff23 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff23.Text) * 10);
            int TimeOff24 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff24.Text) * 10);
            int TimeOff25 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff25.Text) * 10);

            int TimeOff26 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff26.Text) * 10);
            int TimeOff27 = Convert.ToInt16(Convert.ToDouble(tbxTimeOff27.Text) * 10);


            if (UiManager.PLC.isOpen())
            {
                #region TimeOn
                UiManager.PLC.WriteWord(DeviceCode.D, 1002, TimeOn1);
                logger.Create($"PC_Write_TimeOn1:{1002} = {TimeOn1}",LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1004, TimeOn2);
                logger.Create($"PC_Write_TimeOn2:{1004} = {TimeOn2}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1006, TimeOn3);
                logger.Create($"PC_Write_TimeOn3:{1006} = {TimeOn3}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1008, TimeOn4);
                logger.Create($"PC_Write_TimeOn4:{1008} = {TimeOn4}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1010, TimeOn5);
                logger.Create($"PC_Write_TimeOn5:{1010} = {TimeOn5}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1012, TimeOn6);
                logger.Create($"PC_Write_TimeOn6:{1012} = {TimeOn6}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1014, TimeOn7);
                logger.Create($"PC_Write_TimeOn7:{1014} = {TimeOn7}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1016, TimeOn8);
                logger.Create($"PC_Write_TimeOn8:{1016} = {TimeOn8}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1018, TimeOn9);
                logger.Create($"PC_Write_TimeOn9:{1018} = {TimeOn9}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1020, TimeOn10);
                logger.Create($"PC_Write_TimeOn10:{1020} = {TimeOn10}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1022, TimeOn11);
                logger.Create($"PC_Write_TimeOn11:{1022} = {TimeOn11}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1024, TimeOn12);
                logger.Create($"PC_Write_TimeOn12:{1024} = {TimeOn12}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1026, TimeOn13);
                logger.Create($"PC_Write_TimeOn13:{1026} = {TimeOn13}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1028, TimeOn14);
                logger.Create($"PC_Write_TimeOn14:{1028} = {TimeOn14}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1030, TimeOn15);
                logger.Create($"PC_Write_TimeOn15:{1030} = {TimeOn15}", LogLevel.Information);





                UiManager.PLC.WriteWord(DeviceCode.D, 1042, TimeOn16);
                logger.Create($"PC_Write_TimeOn16:{1042} = {TimeOn16}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1044, TimeOn17);
                logger.Create($"PC_Write_TimeOn17:{1044} = {TimeOn17}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1046, TimeOn18);
                logger.Create($"PC_Write_TimeOn18:{1046} = {TimeOn18}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1048, TimeOn19);
                logger.Create($"PC_Write_TimeOn19:{1048} = {TimeOn19}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1050, TimeOn20);
                logger.Create($"PC_Write_TimeOn20:{1050} = {TimeOn20}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1052, TimeOn21);
                logger.Create($"PC_Write_TimeOn21:{1052} = {TimeOn21}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1054, TimeOn22);
                logger.Create($"PC_Write_TimeOn22:{1054} = {TimeOn22}", LogLevel.Information);



                UiManager.PLC.WriteWord(DeviceCode.D, 1062, TimeOn23);
                logger.Create($"PC_Write_TimeOn23:{1062} = {TimeOn23}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1064, TimeOn24);
                logger.Create($"PC_Write_TimeOn24:{1064} = {TimeOn24}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1066, TimeOn25);
                logger.Create($"PC_Write_TimeOn25:{1066} = {TimeOn25}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1068, TimeOn26);
                logger.Create($"PC_Write_TimeOn26:{1068} = {TimeOn26}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1032, TimeOn27);
                logger.Create($"PC_Write_TimeOn27:{1032} = {TimeOn27}", LogLevel.Information);
                #endregion




                #region TimeOFF
                UiManager.PLC.WriteWord(DeviceCode.D, 1003, TimeOff1);
                logger.Create($"PC_Write_TimeOn1:{1003} = {TimeOff1}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1005, TimeOff2);
                logger.Create($"PC_Write_TimeOn2:{1005} = {TimeOff2}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1007, TimeOff3);
                logger.Create($"PC_Write_TimeOn3:{1007} = {TimeOff3}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1009, TimeOff4);
                logger.Create($"PC_Write_TimeOn4:{1009} = {TimeOff4}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1011, TimeOff5);
                logger.Create($"PC_Write_TimeOn5:{1009} = {TimeOff5}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1013, TimeOff6);
                logger.Create($"PC_Write_TimeOn6:{1013} = {TimeOff6}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1015, TimeOff7);
                logger.Create($"PC_Write_TimeOn7:{1015} = {TimeOff7}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1017, TimeOff8);
                logger.Create($"PC_Write_TimeOn8:{1017} = {TimeOff8}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1019, TimeOff9);
                logger.Create($"PC_Write_TimeOn9:{1019} = {TimeOff9}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1021, TimeOff10);
                logger.Create($"PC_Write_TimeOn10:{1021} = {TimeOff10}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1023, TimeOff11);
                logger.Create($"PC_Write_TimeOn11:{1023} = {TimeOff11}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1025,TimeOff12);
                logger.Create($"PC_Write_TimeOn12:{1025} = {TimeOff12}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1027, TimeOff13);
                logger.Create($"PC_Write_TimeOn13:{1027} = {TimeOff13}", LogLevel.Information);

               
                UiManager.PLC.WriteWord(DeviceCode.D, 1029, TimeOff14);
                logger.Create($"PC_Write_TimeOn14:{1029} = {TimeOff14}", LogLevel.Information);





                UiManager.PLC.WriteWord(DeviceCode.D, 1031, TimeOff15);
                logger.Create($"PC_Write_TimeOn15:{1031} = {TimeOff15}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1043, TimeOff16);
                logger.Create($"PC_Write_TimeOn16:{1043} = {TimeOff16}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1045, TimeOff17);
                logger.Create($"PC_Write_TimeOn1:{1045} = {TimeOff17}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1047, TimeOff18);
                logger.Create($"PC_Write_TimeOn1:{1047} = {TimeOff18}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1049, TimeOff19);
                logger.Create($"PC_Write_TimeOn1:{1049} = {TimeOff19}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1051, TimeOff20);
                logger.Create($"PC_Write_TimeOn1:{1051} = {TimeOff20}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1053, TimeOff21);
                logger.Create($"PC_Write_TimeOn1:{1053} = {TimeOff21}", LogLevel.Information);



                UiManager.PLC.WriteWord(DeviceCode.D, 1055, TimeOff22);
                logger.Create($"PC_Write_TimeOn1:{1055} = {TimeOff22}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1063, TimeOff23);
                logger.Create($"PC_Write_TimeOn1:{1063} = {TimeOff23}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1065, TimeOff24);
                logger.Create($"PC_Write_TimeOn1:{1065} = {TimeOff24}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1067, TimeOff25);
                logger.Create($"PC_Write_TimeOn1:{1067} = {TimeOff25}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1069, TimeOff26);
                logger.Create($"PC_Write_TimeOn1:{1069} = {TimeOff26}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1069, TimeOff26);
                logger.Create($"PC_Write_TimeOn1:{1069} = {TimeOff26}", LogLevel.Information);

                UiManager.PLC.WriteWord(DeviceCode.D, 1033, TimeOff27);
                logger.Create($"PC_Write_TimeOn1:{1033} = {TimeOff27}", LogLevel.Information);
                #endregion

                WndMessenger ShowMessenger = new WndMessenger();
                ShowMessenger.MessengerShow("Messenger : Save Data Successfully ");
            }
            else
            {
                WndMessenger ShowMessenger = new WndMessenger();
                ShowMessenger.MessengerShow("Messenger : Save Data Fail ");
            }
            this.UpdateUI();

        }

        private void Tbx_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
                TextBox_Check(textbox, new RoutedEventArgs());
            }
        }

        private void Tbx_TouchDown(object sender, TouchEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            Keypad keyboardWindow = new Keypad();
            if (keyboardWindow.ShowDialog() == true)
            {
                textbox.Text = keyboardWindow.Result;
                TextBox_Check(textbox, new RoutedEventArgs());
            }
        }

        private async void PgSuperUserMenu_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetLabelMaxContents();
            await Task.Delay(1);
            this.UpdateUI();
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
        #region UPDATE TEXBOX CHANGED
        private void TextBox_Check(object sender, RoutedEventArgs e)
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

                    WndMessenger ShowMessenger = new WndMessenger();
                    ShowMessenger.MessengerShow($"Vui lòng nhập một số lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.");
                    //MessageBox.Show($"Vui lòng nhập một số lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.", "Thông Bao Lỗi");
                    textBox.Text = "";
                    textBox.Text = minCondition.ToString();
                    textBox.Background = Brushes.Black;
                }
                if (number > maxCondition)
                {
                    textBox.Background = Brushes.Red;
                    WndMessenger ShowMessenger = new WndMessenger();
                    ShowMessenger.MessengerShow($"Vui lòng nhập một số  lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.");
                   // MessageBox.Show($"Vui lòng nhập một số  lớn hơn {minCondition} và nhỏ hơn {maxCondition} cho {textBox.Name}.", "Thông Báo Lỗi");
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
                case "tbxTimeOn1":
                    return MinDevice1;
                case "tbxTimeOn2":
                    return MinDevice2;
                case "tbxTimeOn3":
                    return MinDevice3;
                case "tbxTimeOn4":
                    return MinDevice4;
                case "tbxTimeOn5":
                    return MinDevice5;
                case "tbxTimeOn6":
                    return MinDevice6;
                case "tbxTimeOn7":
                    return MinDevice7;
                case "tbxTimeOn8":
                    return MinDevice8;
                case "tbxTimeOn9":
                    return MinDevice9;
                case "tbxTimeOn10":
                    return MinDevice10;
                case "tbxTimeOn11":
                    return MinDevice11;
                case "tbxTimeOn12":
                    return MinDevice12;
                case "tbxTimeOn13":
                    return MinDevice13;
                case "tbxTimeOn14":
                    return MinDevice14;
                case "tbxTimeOn15":
                    return MinDevice15;
                case "tbxTimeOn16":
                    return MinDevice16;
                case "tbxTimeOn17":
                    return MinDevice17;
                case "tbxTimeOn18":
                    return MinDevice18;


                case "tbxTimeOn19":
                    return MinDevice19;
                case "tbxTimeOn20":
                    return MinDevice20;
                case "tbxTimeOn21":
                    return MinDevice21;
                case "tbxTimeOn22":
                    return MinDevice22;
                case "tbxTimeOn23":
                    return MinDevice23;
                case "tbxTimeOn24":
                    return MinDevice24;
                case "tbxTimeOn25":
                    return MinDevice25;
                case "tbxTimeOn26":
                    return MinDevice26;
                case "tbxTimeOn27":
                    return MinDevice27;
                case "tbxTimeOn28":
                    return MinDevice28;
                case "tbxTimeOn29":
                    return MinDevice29;
                case "tbxTimeOn30":
                    return MinDevice30;
                case "tbxTimeOn31":
                    return MinDevice31;
                case "tbxTimeOn32":
                    return MinDevice32;
                case "tbxTimeOn33":
                    return MinDevice33;
                case "tbxTimeOn34":
                    return MinDevice34;
                case "tbxTimeOn35":
                    return MinDevice35;
                case "tbxTimeOn36":
                    return MinDevice36;



                case "tbxTimeOff1":
                    return MinDevice1;
                case "tbxTimeOff2":
                    return MinDevice2;
                case "tbxTimeOff3":
                    return MinDevice3;
                case "tbxTimeOff4":
                    return MinDevice4;
                case "tbxTimeOff5":
                    return MinDevice5;
                case "tbxTimeOff6":
                    return MinDevice6;
                case "tbxTimeOff7":
                    return MinDevice7;
                case "tbxTimeOff8":
                    return MinDevice8;
                case "tbxTimeOff9":
                    return MinDevice9;
                case "tbxTimeOff10":
                    return MinDevice10;
                case "tbxTimeOff11":
                    return MinDevice11;
                case "tbxTimeOff12":
                    return MinDevice12;
                case "tbxTimeOff13":
                    return MinDevice13;
                case "tbxTimeOff14":
                    return MinDevice14;
                case "tbxTimeOff15":
                    return MinDevice15;
                case "tbxTimeOff16":
                    return MinDevice16;
                case "tbxTimeOff17":
                    return MinDevice17;
                case "tbxTimeOff18":
                    return MinDevice18;


                case "tbxTimeOff19":
                    return MinDevice19;
                case "tbxTimeOff20":
                    return MinDevice20;
                case "tbxTimeOff21":
                    return MinDevice21;
                case "tbxTimeOff22":
                    return MinDevice22;
                case "tbxTimeOff23":
                    return MinDevice23;
                case "tbxTimeOff24":
                    return MinDevice24;
                case "tbxTimeOff25":
                    return MinDevice25;
                case "tbxTimeOff26":
                    return MinDevice26;
                case "tbxTimeOff27":
                    return MinDevice27;
                case "tbxTimeOff28":
                    return MinDevice28;
                case "tbxTimeOff29":
                    return MinDevice29;
                case "tbxTimeOff30":
                    return MinDevice30;
                case "tbxTimeOff31":
                    return MinDevice31;
                case "tbxTimeOff32":
                    return MinDevice32;
                case "tbxTimeOff33":
                    return MinDevice33;
                case "tbxTimeOff34":
                    return MinDevice34;
                case "tbxTimeOff35":
                    return MinDevice35;
                case "tbxTimeOff36":
                    return MinDevice36;

                default:
                    return 0;
            }
        }
        private float GetMaxCondition(string textBoxName)
        {

            switch (textBoxName)
            {
                case "tbxTimeOn1":
                    return MaxDevice1;
                case "tbxTimeOn2":
                    return MaxDevice2;
                case "tbxTimeOn3":
                    return MaxDevice3;
                case "tbxTimeOn4":
                    return MaxDevice4;
                case "tbxTimeOn5":
                    return MaxDevice5;
                case "tbxTimeOn6":
                    return MaxDevice6;
                case "tbxTimeOn7":
                    return MaxDevice7;
                case "tbxTimeOn8":
                    return MaxDevice8;
                case "tbxTimeOn9":
                    return MaxDevice9;
                case "tbxTimeOn10":
                    return MaxDevice10;
                case "tbxTimeOn11":
                    return MaxDevice11;
                case "tbxTimeOn12":
                    return MaxDevice12;
                case "tbxTimeOn13":
                    return MaxDevice13;
                case "tbxTimeOn14":
                    return MaxDevice14;
                case "tbxTimeOn15":
                    return MaxDevice15;
                case "tbxTimeOn16":
                    return MaxDevice16;
                case "tbxTimeOn17":
                    return MaxDevice17;
                case "tbxTimeOn18":
                    return MaxDevice18;

                case "tbxTimeOn19":
                    return MaxDevice19;
                case "tbxTimeOn20":
                    return MaxDevice20;
                case "tbxTimeOn21":
                    return MaxDevice21;
                case "tbxTimeOn22":
                    return MaxDevice22;
                case "tbxTimeOn23":
                    return MaxDevice23;
                case "tbxTimeOn24":
                    return MaxDevice24;
                case "tbxTimeOn25":
                    return MaxDevice25;
                case "tbxTimeOn26":
                    return MaxDevice26;
                case "tbxTimeOn27":
                    return MaxDevice27;
                case "tbxTimeOn28":
                    return MaxDevice28;
                case "tbxTimeOn29":
                    return MaxDevice29;
                case "tbxTimeOn30":
                    return MaxDevice30;
                case "tbxTimeOn31":
                    return MaxDevice31;
                case "tbxTimeOn32":
                    return MaxDevice32;
                case "tbxTimeOn33":
                    return MaxDevice33;
                case "tbxTimeOn34":
                    return MaxDevice34;
                case "tbxTimeOn35":
                    return MaxDevice35;
                case "tbxTimeOn36":
                    return MaxDevice36;




                case "tbxTimeOff1":
                    return MaxDevice1;
                case "tbxTimeOff2":
                    return MaxDevice2;
                case "tbxTimeOff3":
                    return MaxDevice3;
                case "tbxTimeOff4":
                    return MaxDevice4;
                case "tbxTimeOff5":
                    return MaxDevice5;
                case "tbxTimeOff6":
                    return MaxDevice6;
                case "tbxTimeOff7":
                    return MaxDevice7;
                case "tbxTimeOff8":
                    return MaxDevice8;
                case "tbxTimeOff9":
                    return MaxDevice9;
                case "tbxTimeOff10":
                    return MaxDevice10;
                case "tbxTimeOff11":
                    return MaxDevice11;
                case "tbxTimeOff12":
                    return MaxDevice12;
                case "tbxTimeOff13":
                    return MaxDevice13;
                case "tbxTimeOff14":
                    return MaxDevice14;
                case "tbxTimeOff15":
                    return MaxDevice15;
                case "tbxTimeOff16":
                    return MaxDevice16;
                case "tbxTimeOff17":
                    return MaxDevice17;
                case "tbxTimeOff18":
                    return MaxDevice18;

                   
                case "tbxTimeOff19":
                    return MaxDevice19;
                case "tbxTimeOff20":
                    return MaxDevice20;
                case "tbxTimeOff21":
                    return MaxDevice21;
                case "tbxTimeOff22":
                    return MaxDevice22;
                case "tbxTimeOff23":
                    return MaxDevice23;
                case "tbxTimeOff24":
                    return MaxDevice24;
                case "tbxTimeOff25":
                    return MaxDevice25;
                case "tbxTimeOff26":
                    return MaxDevice26;
                case "tbxTimeOff27":
                    return MaxDevice27;
                case "tbxTimeOff28":
                    return MaxDevice28;
                case "tbxTimeOff29":
                    return MaxDevice29;
                case "tbxTimeOff30":
                    return MaxDevice30;
                case "tbxTimeOff31":
                    return MaxDevice31;
                case "tbxTimeOff32":
                    return MaxDevice32;
                case "tbxDevice033":
                    return MaxDevice33;
                case "tbxDevice034":
                    return MaxDevice34;
                case "tbxDevice035":
                    return MaxDevice35;
                case "tbxDevice036":
                    return MaxDevice36;
                default:
                    return 50;
            }
        }
        private void SetLabelMaxContents()
        {
            // Regular labels
            for (int i = 1; i <= 36; i++)
            {
                SetLabelContent("lbMax" + i, "MaxDevice" + i);
            }

            // Labels with leading zero

            for (int i = 1; i <= 36; i++)
            {
                SetLabelContent("lbMin" + i, "MinDevice" + i);
            }

        }
        private void SetLabelContent(string labelName, string fieldName)
        {
            var label = this.FindName(labelName) as Label;
            var fieldInfo = this.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null)
            {
                var fieldValue = fieldInfo.GetValue(this);
                if (label != null && fieldValue != null)
                {
                    label.Content = fieldValue.ToString();
                }
                else if (label == null)
                {
                    WndMessenger ShowMessenger = new WndMessenger();
                    ShowMessenger.MessengerShow($"Label with name '{labelName}' not found.");
                   // MessageBox.Show($"Label with name '{labelName}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    WndMessenger ShowMessenger = new WndMessenger();
                    ShowMessenger.MessengerShow($"Field '{fieldName}' is null.");
                    //MessageBox.Show($"Field '{fieldName}' is null.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {

                WndMessenger ShowMessenger = new WndMessenger();
                ShowMessenger.MessengerShow($"Field '{fieldName}' not found.");
               // MessageBox.Show($"Field '{fieldName}' not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateUI()
        {
            await Task.Run(() =>
            {
                if (UiManager.PLC.isOpen() == true)
                {
                    int ReadTimeOn1;
                    int ReadTimeOn2;
                    int ReadTimeOn3;
                    int ReadTimeOn4;
                    int ReadTimeOn5;
                    int ReadTimeOn6;
                    int ReadTimeOn7;
                    int ReadTimeOn8;
                    int ReadTimeOn9;
                    int ReadTimeOn10;
                    int ReadTimeOn11;
                    int ReadTimeOn12;
                    int ReadTimeOn13;
                    int ReadTimeOn14;
                    int ReadTimeOn15;
                    int ReadTimeOn16;
                    int ReadTimeOn17;
                    int ReadTimeOn18;
                    int ReadTimeOn19;
                    int ReadTimeOn20;
                    int ReadTimeOn21;
                    int ReadTimeOn22;
                    int ReadTimeOn23;
                    int ReadTimeOn24;
                    int ReadTimeOn25;
                    int ReadTimeOn26;
                    int ReadTimeOn27;
                    //int ReadTimeOn28;
                    //int ReadTimeOn29;
                    //int ReadTimeOn30;
                    //int ReadTimeOn31;
                    //int ReadTimeOn32;
                    //int ReadTimeOn33;
                    //int ReadTimeOn34;
                    //int ReadTimeOn35;
                    //int ReadTimeOn36;

                    int ReadTimeOff1;
                    int ReadTimeOff2;
                    int ReadTimeOff3;
                    int ReadTimeOff4;
                    int ReadTimeOff5;
                    int ReadTimeOff6;
                    int ReadTimeOff7;
                    int ReadTimeOff8;
                    int ReadTimeOff9;
                    int ReadTimeOff10;
                    int ReadTimeOff11;
                    int ReadTimeOff12;
                    int ReadTimeOff13;
                    int ReadTimeOff14;
                    int ReadTimeOff15;
                    int ReadTimeOff16;
                    int ReadTimeOff17;
                    int ReadTimeOff18;
                    int ReadTimeOff19;
                    int ReadTimeOff20;
                    int ReadTimeOff21;
                    int ReadTimeOff22;
                    int ReadTimeOff23;
                    int ReadTimeOff24;
                    int ReadTimeOff25;
                    int ReadTimeOff26;
                    int ReadTimeOff27;
                    //int ReadTimeOff28;
                    //int ReadTimeOff29;
                    //int ReadTimeOff30;
                    //int ReadTimeOff31;
                    //int ReadTimeOff32;
                    //int ReadTimeOff33;
                    //int ReadTimeOff34;
                    //int ReadTimeOff35;
                    //int ReadTimeOff36;

                    UiManager.PLC.ReadWord(DeviceCode.D, 1002, out ReadTimeOn1);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1004, out ReadTimeOn2);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1006, out ReadTimeOn3);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1008, out ReadTimeOn4);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1010, out ReadTimeOn5);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1012, out ReadTimeOn6);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1014, out ReadTimeOn7);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1016, out ReadTimeOn8);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1018, out ReadTimeOn9);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1020, out ReadTimeOn10);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1022, out ReadTimeOn11);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1024, out ReadTimeOn12);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1026, out ReadTimeOn13);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1028, out ReadTimeOn14);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1030, out ReadTimeOn15);

                    UiManager.PLC.ReadWord(DeviceCode.D, 1042, out ReadTimeOn16);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1044, out ReadTimeOn17);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1046, out ReadTimeOn18);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1048, out ReadTimeOn19);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1050, out ReadTimeOn20);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1052, out ReadTimeOn21);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1054, out ReadTimeOn22);

                    UiManager.PLC.ReadWord(DeviceCode.D, 1062, out ReadTimeOn23);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1064, out ReadTimeOn24);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1066, out ReadTimeOn25);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1068, out ReadTimeOn26);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1032, out ReadTimeOn27);


                    UiManager.PLC.ReadWord(DeviceCode.D, 1003, out ReadTimeOff1);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1005, out ReadTimeOff2);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1007, out ReadTimeOff3);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1009, out ReadTimeOff4);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1011, out ReadTimeOff5);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1013, out ReadTimeOff6);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1015, out ReadTimeOff7);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1017, out ReadTimeOff8);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1019, out ReadTimeOff9);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1021, out ReadTimeOff10);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1023, out ReadTimeOff11);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1025, out ReadTimeOff12);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1027, out ReadTimeOff13);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1029, out ReadTimeOff14);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1031, out ReadTimeOff15);

                    UiManager.PLC.ReadWord(DeviceCode.D, 1043, out ReadTimeOff16);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1045, out ReadTimeOff17);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1047, out ReadTimeOff18);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1049, out ReadTimeOff19);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1051, out ReadTimeOff20);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1053, out ReadTimeOff21);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1055, out ReadTimeOff22);

                    UiManager.PLC.ReadWord(DeviceCode.D, 1063, out ReadTimeOff23);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1065, out ReadTimeOff24);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1067, out ReadTimeOff25);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1069, out ReadTimeOff26);
                    UiManager.PLC.ReadWord(DeviceCode.D, 1033, out ReadTimeOff27);

                    Dispatcher.BeginInvoke(new Action(() =>
                    {
                        this.tbxTimeOn1.Text = "Spare";
                        this.tbxTimeOn2.Text = "Spare";
                        this.tbxTimeOn3.Text = "Spare";
                        this.tbxTimeOn4.Text = "Spare";
                        this.tbxTimeOn5.Text = "Spare";
                        this.tbxTimeOn6.Text = "Spare";
                        this.tbxTimeOn7.Text = "Spare";
                        this.tbxTimeOn8.Text = "Spare";
                        this.tbxTimeOn9.Text = "Spare";
                        this.tbxTimeOn10.Text = "Spare";
                        this.tbxTimeOn11.Text = "Spare";
                        this.tbxTimeOn12.Text = "Spare";
                        this.tbxTimeOn13.Text = "Spare";
                        this.tbxTimeOn14.Text = "Spare";
                        this.tbxTimeOn15.Text = "Spare";
                        this.tbxTimeOn16.Text = "Spare";
                        this.tbxTimeOn17.Text = "Spare";
                        this.tbxTimeOn18.Text = "Spare";
                        this.tbxTimeOn19.Text = "Spare";
                        this.tbxTimeOn20.Text = "Spare";
                        this.tbxTimeOn21.Text = "Spare";
                        this.tbxTimeOn22.Text = "Spare";
                        this.tbxTimeOn23.Text = "Spare";
                        this.tbxTimeOn24.Text = "Spare";
                        this.tbxTimeOn25.Text = "Spare";
                        this.tbxTimeOn26.Text = "Spare";
                        this.tbxTimeOn27.Text = "Spare";
                        this.tbxTimeOn28.Text = "Spare";
                        this.tbxTimeOn29.Text = "Spare";
                        this.tbxTimeOn30.Text = "Spare";
                        this.tbxTimeOn31.Text = "Spare";
                        this.tbxTimeOn32.Text = "Spare";
                        this.tbxTimeOn33.Text = "Spare";
                        this.tbxTimeOn34.Text = "Spare";
                        this.tbxTimeOn35.Text = "Spare";
                        this.tbxTimeOn36.Text = "Spare";

                        this.tbxTimeOff1.Text = "Spare";
                        this.tbxTimeOff2.Text = "Spare";
                        this.tbxTimeOff3.Text = "Spare";
                        this.tbxTimeOff4.Text = "Spare";
                        this.tbxTimeOff5.Text = "Spare";
                        this.tbxTimeOff6.Text = "Spare";
                        this.tbxTimeOff7.Text = "Spare";
                        this.tbxTimeOff8.Text = "Spare";
                        this.tbxTimeOff9.Text = "Spare";
                        this.tbxTimeOff10.Text = "Spare";
                        this.tbxTimeOff11.Text = "Spare";
                        this.tbxTimeOff12.Text = "Spare";
                        this.tbxTimeOff13.Text = "Spare";
                        this.tbxTimeOff14.Text = "Spare";
                        this.tbxTimeOff15.Text = "Spare";
                        this.tbxTimeOff16.Text = "Spare";
                        this.tbxTimeOff17.Text = "Spare";
                        this.tbxTimeOff18.Text = "Spare";
                        this.tbxTimeOff19.Text = "Spare";
                        this.tbxTimeOff20.Text = "Spare";
                        this.tbxTimeOff21.Text = "Spare";
                        this.tbxTimeOff22.Text = "Spare";
                        this.tbxTimeOff23.Text = "Spare";
                        this.tbxTimeOff24.Text = "Spare";
                        this.tbxTimeOff25.Text = "Spare";
                        this.tbxTimeOff26.Text = "Spare";
                        this.tbxTimeOff27.Text = "Spare";
                        this.tbxTimeOff28.Text = "Spare";
                        this.tbxTimeOff29.Text = "Spare";
                        this.tbxTimeOff30.Text = "Spare";
                        this.tbxTimeOff31.Text = "Spare";
                        this.tbxTimeOff32.Text = "Spare";
                        this.tbxTimeOff33.Text = "Spare";
                        this.tbxTimeOff34.Text = "Spare";
                        this.tbxTimeOff35.Text = "Spare";
                        this.tbxTimeOff36.Text = "Spare";

                        this.tbxTimeOn1.Text = (ReadTimeOn1 / 10f).ToString("F1");
                        this.tbxTimeOn2.Text = (ReadTimeOn2 / 10f).ToString("F1");
                        this.tbxTimeOn3.Text = (ReadTimeOn3 / 10f).ToString("F1");
                        this.tbxTimeOn4.Text = (ReadTimeOn4 / 10f).ToString("F1");
                        this.tbxTimeOn5.Text = (ReadTimeOn5 / 10f).ToString("F1");

                        this.tbxTimeOn6.Text = (ReadTimeOn6 / 10f).ToString("F1");
                        this.tbxTimeOn7.Text = (ReadTimeOn7 / 10f).ToString("F1");
                        this.tbxTimeOn8.Text = (ReadTimeOn8 / 10f).ToString("F1");
                        this.tbxTimeOn9.Text = (ReadTimeOn9 / 10f).ToString("F1");
                        this.tbxTimeOn10.Text = (ReadTimeOn10 / 10f).ToString("F1");

                        this.tbxTimeOn11.Text = (ReadTimeOn11 / 10f).ToString("F1");
                        this.tbxTimeOn12.Text = (ReadTimeOn12 / 10f).ToString("F1");
                        this.tbxTimeOn13.Text = (ReadTimeOn13 / 10f).ToString("F1");
                        this.tbxTimeOn14.Text = (ReadTimeOn14 / 10f).ToString("F1");
                        this.tbxTimeOn15.Text = (ReadTimeOn15 / 10f).ToString("F1");

                        this.tbxTimeOn16.Text = (ReadTimeOn16 / 10f).ToString("F1");
                        this.tbxTimeOn17.Text = (ReadTimeOn17 / 10f).ToString("F1");
                        this.tbxTimeOn18.Text = (ReadTimeOn18 / 10f).ToString("F1");
                        this.tbxTimeOn19.Text = (ReadTimeOn19 / 10f).ToString("F1");
                        this.tbxTimeOn20.Text = (ReadTimeOn20 / 10f).ToString("F1");

                        this.tbxTimeOn21.Text = (ReadTimeOn21 / 10f).ToString("F1");
                        this.tbxTimeOn22.Text = (ReadTimeOn22 / 10f).ToString("F1");
                        this.tbxTimeOn23.Text = (ReadTimeOn23 / 10f).ToString("F1");
                        this.tbxTimeOn24.Text = (ReadTimeOn24 / 10f).ToString("F1");
                        this.tbxTimeOn25.Text = (ReadTimeOn25 / 10f).ToString("F1");

                        this.tbxTimeOn26.Text = (ReadTimeOn26 / 10f).ToString("F1");
                        this.tbxTimeOn27.Text = (ReadTimeOn27 / 10f).ToString("F1");




                        this.tbxTimeOff1.Text = (ReadTimeOff1 / 10f).ToString("F1");
                        this.tbxTimeOff2.Text = (ReadTimeOff2 / 10f).ToString("F1");
                        this.tbxTimeOff3.Text = (ReadTimeOff3 / 10f).ToString("F1");
                        this.tbxTimeOff4.Text = (ReadTimeOff4 / 10f).ToString("F1");
                        this.tbxTimeOff5.Text = (ReadTimeOff5 / 10f).ToString("F1");

                        this.tbxTimeOff6.Text = (ReadTimeOff6 / 10f).ToString("F1");
                        this.tbxTimeOff7.Text = (ReadTimeOff7 / 10f).ToString("F1");
                        this.tbxTimeOff8.Text = (ReadTimeOff8 / 10f).ToString("F1");
                        this.tbxTimeOff9.Text = (ReadTimeOff9 / 10f).ToString("F1");
                        this.tbxTimeOff10.Text = (ReadTimeOff10 / 10f).ToString("F1");

                        this.tbxTimeOff11.Text = (ReadTimeOff11 / 10f).ToString("F1");
                        this.tbxTimeOff12.Text = (ReadTimeOff12 / 10f).ToString("F1");
                        this.tbxTimeOff13.Text = (ReadTimeOff13 / 10f).ToString("F1");
                        this.tbxTimeOff14.Text = (ReadTimeOff14 / 10f).ToString("F1");
                        this.tbxTimeOff15.Text = (ReadTimeOff15 / 10f).ToString("F1");

                        this.tbxTimeOff16.Text = (ReadTimeOff16 / 10f).ToString("F1");
                        this.tbxTimeOff17.Text = (ReadTimeOff17 / 10f).ToString("F1");
                        this.tbxTimeOff18.Text = (ReadTimeOff18 / 10f).ToString("F1");
                        this.tbxTimeOff19.Text = (ReadTimeOff19 / 10f).ToString("F1");
                        this.tbxTimeOff20.Text = (ReadTimeOff20 / 10f).ToString("F1");

                        this.tbxTimeOff21.Text = (ReadTimeOff21 / 10f).ToString("F1");
                        this.tbxTimeOff22.Text = (ReadTimeOff22 / 10f).ToString("F1");
                        this.tbxTimeOff23.Text = (ReadTimeOff23 / 10f).ToString("F1");
                        this.tbxTimeOff24.Text = (ReadTimeOff24 / 10f).ToString("F1");
                        this.tbxTimeOff25.Text = (ReadTimeOff25 / 10f).ToString("F1");

                        this.tbxTimeOff26.Text = (ReadTimeOff26 / 10f).ToString("F1");
                        this.tbxTimeOff27.Text = (ReadTimeOff27 / 10f).ToString("F1");

                    }));
                }

            });
            if (!UiManager.PLC.isOpen())
            {
                for (int i = 1; i <= 36; i++)
                {
                    string textBoxName = "tbxTimeOn" + i;
                    TextBox textBox = this.FindName(textBoxName) as TextBox;
                    if (textBox != null)
                    {
                        textBox.Text = "Error";
                    }
                }
                for (int i = 1; i <= 36; i++)
                {
                    string textBoxName = "tbxTimeOff" + i;
                    TextBox textBox = this.FindName(textBoxName) as TextBox;
                    if (textBox != null)
                    {
                        textBox.Text = "Error";
                    }
                }
                
            }


        }
        #endregion
    }
}
