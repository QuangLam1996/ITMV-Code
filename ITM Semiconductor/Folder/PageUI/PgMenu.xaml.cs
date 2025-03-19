using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ITM_Semiconductor
{
    /// <summary>
    /// Interaction logic for PgMenu.xaml
    /// </summary>
    public partial class PgMenu : Page
    {
        private System.Timers.Timer clock = new System.Timers.Timer(1000);
        public PgMenu()
        {
            InitializeComponent();
            this.Loaded += PgMenu_Loaded;
            this.btLogin.Click += BtLogin_Click;
            this.btLogout.Click += BtLogout_Click;
            //this.btAssignMenu.Click += BtAssignMenu_Click;
            this.btManual.Click += BtManual_Click;
            this.btMechanical.Click += BtMechanical_Click;
            this.btModel.Click += BtModel_Click;
            this.btStatus.Click += BtStatus_Click;
            this.btSuperUser.Click += BtSuperUser_Click;

            this.btSystem.Click += BtSystem_Click;
            this.btTeaching.Click += BtTeaching_Click;
            this.btImageSetting.Click += BtImageSetting_Click;

        }

        private void BtImageSetting_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_CAMERA_SETTING);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_CAMERASETTING);
        }

        private void BtTeaching_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MENU_BUTTON_TEACHING);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_TEACHING_MENU);
        }

        private void BtSystem_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MENU_BUTTON_SYSTEM);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SYSTEM_MENU);
        }

        private void BtSuperUser_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MENU_BUTTON_SUPERUSER);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_SUPER_USER_MENU_SETTING_ALARM);
        }

        private void BtStatus_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MENU_BUTTON_STATUS);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_STATUS_MENU);
        }

        private void BtModel_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MENU_BUTTON_MODEL);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MODEL);
        }

        private void BtMechanical_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.PAGE_MECHANICAL_SETUP_TCP_PLC);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MECHANICAL_SETUP_TCP_PLC);
        }

        private void BtManual_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MENU_BUTTON_MANUAL);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_MANUAL_OPERATION);
        }

        private void BtAssignMenu_Click(object sender, RoutedEventArgs e)
        {
            UserManager.createUserLog(UserActions.MENU_BUTTON_ASIGN);
            UiManager.Instance.SwitchPage(PAGE_ID.PAGE_ASSIGN_MENU);
        }

        private void BtLogout_Click(object sender, RoutedEventArgs e)
        {
            UiManager.appSettings.UseName = UiManager.appSettings.Operator;
            UserManager.LogOut();
            updateUI();
        }

        private async void PgMenu_Loaded(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1);
            Dispatcher.Invoke(() =>
            {
                updateUI();
                clock.AutoReset = true;
                clock.Elapsed += this.Clock_Elapsed;
                clock.Start();
            });
            await Task.Delay(1);


        }
        private void Clock_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() => {
                    this.lblCurrentTime.Content = DateTime.Now.ToString("HH:mm:ss yyyy-MM-dd");
                });
            }
            catch { }

        }
        private void updateUI()
        {
            if (UserManager.IsLogOn() == 1)
            {
                myImage.Source = new BitmapImage(new Uri("Icon/Manager2.png", UriKind.RelativeOrAbsolute));
                this.lblMode.Content = "Manager";


                this.btTeaching.IsEnabled = true;
                this.btMechanical.IsEnabled = true;
                this.btManual.IsEnabled = true;
                this.btStatus.IsEnabled = true;
                this.btModel.IsEnabled = true;
                this.btSuperUser.IsEnabled = false;
                this.btSystem.IsEnabled = false;
                //this.btAssignMenu.IsEnabled = false;
            }
            if (UserManager.IsLogOn() == 2)
            {
                myImage.Source = new BitmapImage(new Uri("Icon/Autotem2.png", UriKind.RelativeOrAbsolute));
                this.lblMode.Content = "AutoTeams";

                this.btTeaching.IsEnabled = true;
                this.btMechanical.IsEnabled = true;
                this.btManual.IsEnabled = true;
                this.btStatus.IsEnabled = true;
                this.btModel.IsEnabled = true;
                this.btSuperUser.IsEnabled = true;
                this.btSystem.IsEnabled = true;
                //this.btAssignMenu.IsEnabled = true;
            }
            if (UserManager.IsLogOn() == 0)
            {
                myImage.Source = new BitmapImage(new Uri("Icon/Operator.png", UriKind.RelativeOrAbsolute));
                this.lblMode.Content = "Operator";




                //this.btTeaching.IsEnabled = true;
                //this.btMechanical.IsEnabled = true;
                //this.btLogout.IsEnabled = true;
                //this.btManual.IsEnabled = true;
                //this.btStatus.IsEnabled = true;
                //this.btModel.IsEnabled = true;
                //this.btSuperUser.IsEnabled = true;
                //this.btSystem.IsEnabled = true;
                //this.btAssignMenu.IsEnabled = true;

                this.btTeaching.IsEnabled = false;
                this.btMechanical.IsEnabled = false;
                this.btManual.IsEnabled = true;
                this.btStatus.IsEnabled = true;
                this.btModel.IsEnabled = true;
                this.btSuperUser.IsEnabled = false;
                this.btSystem.IsEnabled = false;
                //this.btAssignMenu.IsEnabled = false;


            }
            if (UserManager.IsLogOn() == 3)
            {
                myImage.Source = new BitmapImage(new Uri("Icon/Operator.png", UriKind.RelativeOrAbsolute));
                this.lblMode.Content = "Operator";

                this.btTeaching.IsEnabled = false;
                this.btMechanical.IsEnabled = false;
                this.btManual.IsEnabled = true;
                this.btStatus.IsEnabled = true;
                this.btModel.IsEnabled = true;
                this.btSuperUser.IsEnabled = false;
                this.btSystem.IsEnabled = false;
                //this.btAssignMenu.IsEnabled = false;
            }
        }
        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            //new WndLogon().DoLogOn(Window.GetWindow(this));
            WndLogin wndLogin = new WndLogin();
            wndLogin.ShowDialog();
            updateUI();
        }
    }
}
