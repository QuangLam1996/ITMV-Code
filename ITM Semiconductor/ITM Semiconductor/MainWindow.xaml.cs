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
using System.IO;
using System.Reflection;
using System.ComponentModel;



namespace ITM_Semiconductor
{
  

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
        public const String AppVersionNumber = "1.2.23";
        public const String AppVersionTime = "14-Jun-2021";
        private System.Timers.Timer clock = new System.Timers.Timer(1000);
     
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
            this.btPower.Click += BtPower_Click;
            this.btMenu.Click += BtMenu_Click;
            this.btMain.Click += BtMain_Click;
            this.btIO.Click += BtIO_Click;
            this.btLastJam.Click += BtLastJam_Click;

       

        }

        private void BtLastJam_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.WND_ALARM);
            UiManager.SwitchPage(PAGE_ID.PAGE_ALARM);
        }

        private void BtIO_Click(object sender, RoutedEventArgs e)
        {
           UserManager.createUserLog(UserActions.WND_IO);
           UiManager.SwitchPage(PAGE_ID.PAGE_IO);
        }

        private void BtMain_Click(object sender, RoutedEventArgs e)
        {
           UserManager.createUserLog(UserActions.WND_MAIN);
           UiManager.SwitchPage(PAGE_ID.PAGE_MAIN);
        }

        private void BtMenu_Click(object sender, RoutedEventArgs e)
        {
           UserManager.createUserLog(UserActions.WND_MENU);
           UiManager.SwitchPage(PAGE_ID.PAGE_MENU);
        }

        private void BtPower_Click(object sender, RoutedEventArgs e)
        {
          UserManager.createUserLog(UserActions.WND_CLOSE);
          App.Current.Shutdown();
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.clock.Stop();
            ///
          
        }
        

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            clock.AutoReset = true;
            clock.Elapsed += this.Clock_Elapsed;
            clock.Start();
            DateTime buildDate = GetBuildDate();
            // Hiển thị ngày giờ build
            string ver = string.Format( buildDate.ToString("G"));
            TblVersion.Content = $"Update : {ver}";


        }
        private DateTime GetBuildDate()
        {
            string filePath = Assembly.GetExecutingAssembly().Location;
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.LastWriteTime;
        }
        private void Clock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try {
                this.Dispatcher.Invoke(() => {

                    this.lblCurrentDate.Content = String.Format("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
                   
                });
            } 
            catch { }
            
        }
        public void UpdateMainContent(object obj)
        {
            this.frmMainContent.Navigate(obj);
        }


     

       
    }
   


}
