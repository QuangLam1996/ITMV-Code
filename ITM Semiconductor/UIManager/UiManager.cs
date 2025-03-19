using ITM_Semiconductor.Folder.PageUI;
using ITM_Semiconductor.Library.PageUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using DTO;
using System.IO;
using System.IO.Ports;
using Mitsubishi;
using static System.Data.Entity.Infrastructure.Design.Executor;
using ITM_Semiconductor;
using System.ComponentModel;
using System.Threading;
using FluentFTP;
using static Mitsubishi.Q_Enthernet;
using System.Net;
using static ITM_Semiconductor.MesComm;
using MvCamCtrl.NET;
using VisionInspection;




namespace ITM_Semiconductor
{
    public enum PAGE_ID
    {
        // Page Main
        PAGE_MAIN = 0,

        // Page Menu
        PAGE_MENU,

        PAGE_TEACHING_MENU,
        PAGE_TEACHING_MENU_MANUAL1,
        PAGE_TEACHING_MENU_MANUAL2,
        PAGE_TEACHING_MENU_ROBOT_JOG,
        PAGE_TEACHING_MENU_JIG_SETUP,

        PAGE_MANUAL_OPERATION,
        PAGE_MANUAL_OPERATION1,
        PAGE_MANUAL_OPERATION2,
        PAGE_MANUAL_OPERATION3,

        PAGE_MECHANICAL_MENU,
        PAGE_MECHANICAL_MENU1,
        PAGE_MECHANICAL_MENU2,
        PAGE_MECHANICAL_MENU3,
        PAGE_MECHANICAL_SETUP_TCP_PLC,
        PAGE_MECHANICAL_SETUP_TCP_SCANNER,

        PAGE_SUPER_USER_MENU,
        PAGE_SUPER_USER_MENU_DELAY_MACHINE,
        PAGE_SUPER_USER_MENU_SETTING_ALARM,
        PAGE_SUPER_USER_MENU_SETTING_SERVO,
        PAGE_SUPER_USER_MENU_SETTING_ROBOT,

        PAGE_MODEL,
        PAGE_STATUS_MENU,

        PAGE_SYSTEM_MENU,
        PAGE_SYSTEM_MENU_SYSTEM_MACHINE,

        PAGE_ASSIGN_MENU,

        // Page I/O
        PAGE_IO,
        PAGE_IO1,

        // Page Alarm 
        PAGE_ALARM,

        PAGE_CAMERASETTING,

    }


    class UiManager
    {
        private static UiManager instance = new UiManager();
        public static UiManager Instance => instance;
        public MainWindow wndMain;
        private static MyLogger logger = new MyLogger("UiManager");
        public static Hashtable pageTable = new Hashtable();
        public static AppSettings appSettings = new AppSettings();
        public static Q_Enthernet PLC = new Q_Enthernet();
        public static ScannerTCP Scanner;
        public static HikCamDevice hikCamera = new HikCamDevice();
        public static HikCam Cam1 = new HikCam();
        public static HikCam Cam2 = new HikCam();
        public string userName { get; set; }

        public static string ip = "";

        // File Image Keyence
        public static FtpClient FTP_KEYENCE;

        // Communicaton Mes
        public static MesComm MES;
        private static Object LockerMES = new object();





        public void Startup()
        {
            logger.Create("Startup:", LogLevel.Information);
            try
            {
                // Load Settings
                LoadAppSetting();

                // Create Database if not existed
                Dba.createDatabaseIfNotExisted();

                // Initialize Page in Project
                initPageTable();



                // Load MainWindow
                LoadMainWindow();


                SwitchPage(PAGE_ID.PAGE_MAIN);

                // Load Excel file for alarms
                AlarmList.LoadAlarm();

                // Load Excel file for status
                StatusMachine.LoadStatus();

                // Set user
                appSettings.UseName = "Operator";

                // Connection to PLC
                ConnectPLC();
                CameraListAcq();





                // // Connection to Scanner
                //ConnectScanner();

                // // Connection To FTP Keyence
                // InitialzeFTP_KEYENCE();
                // connectionFTP_KEYENCE(1000);

                // // Connect MES
                // ConnectionToMES();


            }
            catch (Exception ex)
            {
                logger.Create("Startup error:" + ex.Message, LogLevel.Error);
            }


        }

        private void LoadMainWindow()
        {

            wndMain = new MainWindow();
            // Create Main window:
            wndMain.frmMainContent.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            wndMain.Show();

        }
        private static void initPageTable()
        {

            pageTable.Add(PAGE_ID.PAGE_MAIN, new PgMain());


            pageTable.Add(PAGE_ID.PAGE_MENU, new PgMenu());

            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION, new PgManualOperation());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION1, new PgManualOperation1());

            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU, new PgMechanicalMenu());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU1, new PgMechanicalMenu1());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU2, new PgMechanicalMenu2());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU3, new PgMechanicalMenu3());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_SETUP_TCP_PLC, new PgMechanicalMenu4());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_SETUP_TCP_SCANNER, new PgMechanicalMenu5());
            pageTable.Add(PAGE_ID.PAGE_MODEL, new PgModel());

            pageTable.Add(PAGE_ID.PAGE_STATUS_MENU, new PgStatusMenu());

            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU, new PgSuperUserMenu());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_DELAY_MACHINE, new PgSuperUserMenu1());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_ALARM, new PgSuperUserMenu2());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_SERVO, new PgSuperUserMenu3());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_ROBOT, new PgSuperUserMenu4());
            pageTable.Add(PAGE_ID.PAGE_SYSTEM_MENU, new PgSystemMenu());
            pageTable.Add(PAGE_ID.PAGE_SYSTEM_MENU_SYSTEM_MACHINE, new PgSystemMenu01());

            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU, new PgTeachingMenu());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_MANUAL1, new PgTeachingMenu01());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_MANUAL2, new PgTeachingMenu02());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU_JIG_SETUP, new PgTeachingMenu03());


            pageTable.Add(PAGE_ID.PAGE_ASSIGN_MENU, new PgAssignMenu());

            pageTable.Add(PAGE_ID.PAGE_IO, new PgIO());
            pageTable.Add(PAGE_ID.PAGE_IO1, new PgIO1());

            pageTable.Add(PAGE_ID.PAGE_ALARM, new PgAlarm());

            pageTable.Add(PAGE_ID.PAGE_CAMERASETTING, new PgCamera());

        }
        public void SwitchPage(PAGE_ID pgID)     // ham de thay dd
        {
            if (pageTable.ContainsKey(pgID))
            {
                var pg = (System.Windows.Controls.Page)pageTable[pgID];
                wndMain.UpdateMainContent(pg);
                wndMain.btMenu.ClearValue(Button.BackgroundProperty);
                wndMain.btMain.ClearValue(Button.BackgroundProperty);
                wndMain.btIO.ClearValue(Button.BackgroundProperty);
                wndMain.btLastJam.ClearValue(Button.BackgroundProperty);

                if (pgID == PAGE_ID.PAGE_MAIN)
                {
                    wndMain.btMain.Background = Brushes.LightGreen;
                }
                if ((pgID >= PAGE_ID.PAGE_MENU) & (pgID <= PAGE_ID.PAGE_ASSIGN_MENU))
                {
                    wndMain.btMenu.Background = Brushes.LightGreen;
                }
                if ((pgID >= PAGE_ID.PAGE_IO) & (pgID <= PAGE_ID.PAGE_IO1))
                {
                    wndMain.btIO.Background = Brushes.LightGreen;
                }
                if (pgID == PAGE_ID.PAGE_ALARM)
                {
                    wndMain.btLastJam.Background = Brushes.LightGreen;
                }

            }
        }
        public static void SaveAppSetting()            ///  LUU THONG SO SETTING
        {
            try
            {
                if (appSettings == null)
                {
                    appSettings = new AppSettings();
                }
                string str = appSettings.TOJSON();
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppSettings.SETTING_FILE_NAME);   // duong dan den file exe de mo ung dung
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(path))
                {
                    streamWriter.WriteLine(str);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
            }
            catch (Exception ex)
            {
                logger.Create("SaveAppSetting" + ex.Message, LogLevel.Error);
            }

        }
        public static void LoadAppSetting()           // LOAD DU LIEU SETTING
        {

            String filePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), AppSettings.SETTING_FILE_NAME);
            if (File.Exists(filePath))
            {
                using (StreamReader file = File.OpenText(filePath))
                {
                    appSettings = AppSettings.FromJSON(file.ReadToEnd());
                }
            }
            else
            {
                appSettings = new AppSettings();
            }
        }

        #region Connect PLC
        public static void ConnectPLC()
        {
            PLC = new Q_Enthernet(UiManager.appSettings.Setting_PLCTcp.Ip1, UiManager.appSettings.Setting_PLCTcp.Port1);
            try
            {
                PLC.Start();
                // PLC.ConnectWithTimeOut(3000);
            }
            catch (Exception ex)
            {
                logger.Create($"ConnectPLCAsync: {ex.Message}", LogLevel.Error);

            }
        }
        public static void DisconnectPLC()
        {
            if (UiManager.PLC != null || UiManager.PLC.isOpen() == true)
            {
                UiManager.PLC.Disconnect();
            }
        }
        #endregion

        #region CAMERA CONTROL

        public static void CameraListAcq()
        {
            hikCamera.DeviceListAcq();
            ConectCamera1();
            ConectCamera2();
        }
        public static void ConectCamera1()
        {
            MyCamera.MV_CC_DEVICE_INFO_LIST m_pDeviceList = hikCamera.m_pDeviceList;

            MyCamera.MV_CC_DEVICE_INFO device1 = UiManager.appSettings.connection.camera1.device;
            int ret1 = Cam1.Open(device1, HikCam.AquisMode.AcquisitionMode);
            Cam1.SetExposeTime((int)UiManager.appSettings.connection.camera1.ExposeTime);
            Thread.Sleep(2);
            if (ret1 == MyCamera.MV_OK)
            {
                //return true;
            }
            else
            {
                //return false;
            }

        }
        public static void ConectCamera2()
        {
            MyCamera.MV_CC_DEVICE_INFO device2 = UiManager.appSettings.connection.camera2.device;
            int ret2 = Cam2.Open(device2, HikCam.AquisMode.AcquisitionMode);
            Cam2.SetExposeTime((int)UiManager.appSettings.connection.camera2.ExposeTime);
            Thread.Sleep(2);
            if (ret2 == MyCamera.MV_OK)
            {
                //return true;
            }
            else
            {
                //return false;
            }


        }

        public static int Camera1Close()
        {
            int ret = MyCamera.MV_OK;
            if (Cam1 != null)
            {
                ret = Cam1.Close();
            }
            return ret;
        }
        public static int Camera2Close()
        {
            int ret = MyCamera.MV_OK;
            if (Cam1 != null)
            {
                ret = Cam2.Close();
            }
            return ret;
        }
        public static int Camera1Dispose()
        {
            int ret = MyCamera.MV_OK;
            if (Cam1 != null)
            {
                ret = Cam1.DisPose();
            }
            return ret;
        }
        public static int Camera2Dispose()
        {
            int ret = MyCamera.MV_OK;
            if (Cam1 != null)
            {
                ret = Cam2.DisPose();
            }
            return ret;
        }

        #endregion

        #region Connect Scanner
        public static void ConnectScanner()
        {
            Scanner = new ScannerTCP(UiManager.appSettings.SettingScannerTCP.IpAdress1, UiManager.appSettings.SettingScannerTCP.Port1);
            try
            {
                Scanner.Start();
            }
            catch (Exception ex)
            {
                logger.Create($"ConnectScannerAsync: {ex.Message}", LogLevel.Error);
            }
        }
        public static void DisconnectScanner()
        {
            if (Scanner != null)
            {
                Scanner.Stop();
            }
        }
        #endregion

        #region FTP Keyence
        public static void InitialzeFTP_KEYENCE()
        {

            FTP_KEYENCE = new FtpClient(appSettings.FTPClientSettings.Host, appSettings.FTPClientSettings.UserID, appSettings.FTPClientSettings.PassWord, appSettings.FTPClientSettings.Port);
        }
        public static bool connectionFTP_KEYENCE(int timeOut)
        {
            bool ResultConnect = false;
            try
            {
                Action action = delegate
                {
                    FTP_KEYENCE.AutoConnect();
                };
                IAsyncResult asyncResult = action.BeginInvoke(null, null);
                if (asyncResult.AsyncWaitHandle.WaitOne(timeOut))
                {
                    return ResultConnect;
                }
            }
            catch (Exception ex)
            {
                logger.Create(string.Format("Connection To FTP Error: {0}", ex.Message), LogLevel.Error);

            }
            return false;
        }


        public static void StopConnectFTP_KEYENCE()
        {
            if (FTP_KEYENCE != null)
            {
                FTP_KEYENCE.Disconnect();
                Thread.Sleep(200);
            }
        }
        public static bool IsConnectionFTP_Keyence()
        {
            if (FTP_KEYENCE != null)
            {
                return FTP_KEYENCE.IsConnected;
            }
            return false;
        }
        #endregion

        #region Connect MES
        public static void ConnectionToMES()
        {
            try
            {
                MES = null;
                InitialMES();
            }
            catch (Exception ex)
            {
                logger.Create(String.Format("Connection To MES Error: " + ex.Message), LogLevel.Error);
            }
        }
        private static void InitialMES()
        {
            try
            {
                if (MES != null) return;
                string IP = appSettings.MesSettings1.localIp;
                int port = appSettings.MesSettings1.localPort;
                MES = new MesComm(IP, port);
                MES.Start();
            }
            catch (Exception ex)
            {
                logger.Create(String.Format("Initialze Com MES Error: " + ex.Message), LogLevel.Error);
            }
        }
        public static Boolean IsConnectionMESProtocol()
        {
            lock (LockerMES)
            {
                if (MES != null) { return MES.IsConnected; }
                else { return false; }
            }
        }
        public static void StopConnectionMES()
        {
            if (MES != null) { MES.Stop(); }
        }
        //private static void MESChangeConnection(EndPoint remoteEP, Boolean isConnected)
        //{
        //    if (ChangeConnectionMES_MC != null)
        //    {
        //        ChangeConnectionMES_MC(remoteEP, isConnected);
        //    }
        //}
        //private static void LogMESReciverMESProtocol(String log)
        //{
        //    if (LogCallbackMESProtocolChanged != null)
        //    {
        //        LogCallbackMESProtocolChanged(log);
        //    }
        //}
        #endregion
    }


}
