using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.IO;
using System.Reflection.Emit;
using DTO;

namespace ITM_Semiconductor.Folder.PageUI
{
    /// <summary>
    /// Interaction logic for PgAlarm.xaml
    /// </summary>
    public partial class PgAlarm : Page
    {
        private const String dbFileName = "SIP_AutoLoading.db";
        private MyLogger logger = new MyLogger("PgAlarm");


        private const int ALARM_PAGE_SIZE = 100;
        private int alarmCurrerntPage = 0;
        private int alarmTotalPage = 0;


        public PgAlarm()
        {
            InitializeComponent();
            this.Loaded += PgAlarm_Loaded;
            this.btAlarmFirst.Click += this.BtAlarmFirst_Click;
            this.btAlarmPrePage.Click += this.BtAlarmPrePage_Click;
            this.btAlarmPrevious.Click += this.BtAlarmPrevious_Click;
            this.btAlarmCurrent.Click += this.BtAlarmCurrent_Click;
            this.btAlarmNext.Click += this.BtAlarmNext_Click; 
            this.btAlarmNextPage.Click += this.BtAlarmNextPage_Click;
            this.btAlarmLast.Click += this.BtAlarmLast_Click;

        }

        private void PgAlarm_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_LOADED);

                this.alarmTotalPage = this.getTotalPageCount();
                this.alarmCurrerntPage = 0;
                this.loadEvents();

             
            }
            catch (Exception ex)
            {

                logger.Create("Page Status Log Loaded Error: " + ex.Message, LogLevel.Error);
            }
        }
        private int getTotalPageCount()
        {
            var evCnt = DbRead.CountAlarm();
            return (evCnt + ALARM_PAGE_SIZE - 1) / ALARM_PAGE_SIZE;
        }
        private void loadEvents()
        {
            this.btAlarmCurrent.Content = String.Format("{0}/{1}", alarmCurrerntPage + 1, alarmTotalPage);

            var events = DbRead.GetAlarm(alarmCurrerntPage, ALARM_PAGE_SIZE);
            dgridAlarms.ItemsSource = events;

            dgridAlarms.Focus();
            dgridAlarms.SelectedIndex = 0;
        }

        private void BtAlarmLast_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_ALARM_LAST);
                alarmTotalPage = getTotalPageCount();
                if (alarmTotalPage > 0)
                {
                    alarmCurrerntPage = alarmTotalPage - 1;
                }
                loadEvents();
            }
            catch (Exception ex)
            {

                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtAlarmNextPage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_ALARM_NEXT_PAGE);
                alarmTotalPage = getTotalPageCount();
                if (alarmCurrerntPage < alarmTotalPage - 1)
                {
                    alarmCurrerntPage++;
                }
                loadEvents();
            }
            catch (Exception ex)
            {

                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtAlarmNext_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_ALARM_NEXT);
                dgridAlarms.Focus();
                int nextIndex = dgridAlarms.SelectedIndex + 1;
                if (nextIndex < dgridAlarms.Items.Count)
                {
                    dgridAlarms.SelectedIndex = nextIndex;
                }
            }
            catch (Exception ex)
            {

                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtAlarmCurrent_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_ALARM_CURRENT);
            }
            catch (Exception ex)
            {
                logger.Create("Action Button {0} Error: " + ex.Message, LogLevel.Error);
            }
        }
        private void BtAlarmPrevious_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_ALARM_PREVIOUS);
                this.dgridAlarms.Focus();
                int nextIndex = dgridAlarms.SelectedIndex;
                if (nextIndex > 0)
                {
                    dgridAlarms.SelectedIndex = nextIndex - 1;
                }
            }
            catch (Exception ex)
            {

                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtAlarmPrePage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_ALARM_PRE_PAGE);
                this.alarmTotalPage = getTotalPageCount();
                if (this.alarmCurrerntPage > 0)
                {
                    this.alarmCurrerntPage--;
                }
                this.loadEvents();
            }
            catch (Exception ex)
            {

                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtAlarmFirst_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_ALARM_FIRST);
                this.alarmTotalPage = getTotalPageCount();
                this.alarmCurrerntPage = 0;
                this.loadEvents();
            }
            catch (Exception ex)
            {

                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }




    }
}
