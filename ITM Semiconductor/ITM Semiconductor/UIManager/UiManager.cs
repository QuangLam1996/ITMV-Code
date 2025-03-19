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



namespace ITM_Semiconductor
{
    public enum PAGE_ID
    {
        PAGE_MAIN = 0,
        PAGE_MENU,

        PAGE_IO,
        PAGE_IO1,

        PAGE_ALARM,
        PAGE_ASSIGN_MENU,

        PAGE_MANUAL_OPERATION,
        PAGE_MANUAL_OPERATION1,
        PAGE_MANUAL_OPERATION2,
        PAGE_MANUAL_OPERATION3,

        PAGE_MECHANICAL_MENU,
        PAGE_MECHANICAL_MENU1,
        PAGE_MECHANICAL_MENU2,
        PAGE_MECHANICAL_MENU3,
        PAGE_MECHANICAL_MENU4,
        PAGE_MECHANICAL_MENU5,

        PAGE_MODEL,
        PAGE_STATUS_MENU,

        PAGE_SUPER_USER_MENU,
        PAGE_SUPER_USER_MENU1,
        PAGE_SUPER_USER_MENU2,

        PAGE_SYSTEM_MENU,
        PAGE_TEACHING_MENU,

       
    }
    class UiManager
    {
       
        private static MainWindow wndMain;
        private static MyLogger logger = new MyLogger("UiManager");
        public static Hashtable pageTable = new Hashtable();
        public static AppSettings appSettings = new AppSettings();
        public string userName { get; set; }

        public static string ip = "";


        public static void Startup()
        {
            logger.Create( "Startup:", LogLevel.Information);
            try
            {
               
                initPageTable();
               
                LoadAppSetting();
                wndMain = new MainWindow();
                // Create Main window:
                wndMain.frmMainContent.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
                wndMain.Show();
                SwitchPage(PAGE_ID.PAGE_MAIN);

                //WndLogin wndLogin = new WndLogin();
                //wndLogin.ShowDialog();

                AlarmList.LoadAlarm();

                AlarmSQLDba.AlarmCreateDatabase();
                Dba.createDatabaseIfNotExisted();
                appSettings.UseName = "Operator";
            }
           catch (Exception ex)
           {
               logger.Create("Startup error:" + ex.Message,LogLevel.Error);
           }


        }


        private static void initPageTable()
        {
            
            pageTable.Add(PAGE_ID.PAGE_MAIN, new PgMain());
            pageTable.Add(PAGE_ID.PAGE_MENU, new PgMenu());

            pageTable.Add(PAGE_ID.PAGE_IO, new PgIO());
            pageTable.Add(PAGE_ID.PAGE_IO1, new PgIO1());

            pageTable.Add(PAGE_ID.PAGE_ALARM, new PgAlarm());
            pageTable.Add(PAGE_ID.PAGE_ASSIGN_MENU, new PgAssignMenu());

            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION,  new PgManualOperation());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION1, new PgManualOperation1());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION2, new PgManualOperation2());
            pageTable.Add(PAGE_ID.PAGE_MANUAL_OPERATION3, new PgManualOperation3());

            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU,  new PgMechanicalMenu());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU1, new PgMechanicalMenu1());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU2, new PgMechanicalMenu2());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU3, new PgMechanicalMenu3());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU4, new PgMechanicalMenu4());
            pageTable.Add(PAGE_ID.PAGE_MECHANICAL_MENU5, new PgMechanicalMenu5());

            pageTable.Add(PAGE_ID.PAGE_MODEL,new PgModel());
            pageTable.Add(PAGE_ID.PAGE_STATUS_MENU , new PgStatusMenu());

            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU, new PgSuperUserMenu());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU1, new PgSuperUserMenu1());
            pageTable.Add(PAGE_ID.PAGE_SUPER_USER_MENU2, new PgSuperUserMenu2());

            pageTable.Add(PAGE_ID.PAGE_SYSTEM_MENU, new PgSystemMenu());
            pageTable.Add(PAGE_ID.PAGE_TEACHING_MENU, new PgTeachingMenu());
           
          
        }
        public static void SwitchPage(PAGE_ID pgID)     // ham de thay dd
        {
            if (pageTable.ContainsKey(pgID))
            {
                var pg = (System.Windows.Controls.Page)pageTable[pgID];
                wndMain.UpdateMainContent(pg);
                wndMain.btMenu.ClearValue(Button.BackgroundProperty);
                wndMain.btMain.ClearValue(Button.BackgroundProperty);
                wndMain.btIO.ClearValue(Button.BackgroundProperty);
                wndMain.btLastJam.ClearValue(Button.BackgroundProperty);
                switch (pgID)
                {
                    case PAGE_ID.PAGE_MAIN:
                        wndMain.btMain.Background = Brushes.LightGreen; break;
                    case PAGE_ID.PAGE_MENU:
                        wndMain.btMenu.Background = Brushes.LightGreen; break;
                    case PAGE_ID.PAGE_IO:
                        wndMain.btIO.Background = Brushes.LightGreen; break;
                    case PAGE_ID.PAGE_ALARM:
                        wndMain.btLastJam.Background = Brushes.LightGreen; break;

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
                logger.Create("SaveAppSetting" + ex.Message,LogLevel.Error);
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
    }
       
}
