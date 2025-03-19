using DTO;
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
    /// Interaction logic for PgStatusMenu.xaml
    /// </summary>
    public partial class PgStatusMenu : Page
    {
        private static MyLogger logger = new MyLogger("PgStatusLog");

        private const int ALARM_PAGE_SIZE = 100;
        private int alarmCurrerntPage = 0;
        private int alarmTotalPage = 0;

        private const int LOG_PAGE_SIZE = 100;
        private int logCurrentPage = 0;
        private int logTotalPage = 0;
        public PgStatusMenu()
        {
            InitializeComponent();
            // Common
            this.Loaded += this.PgStatusLog_Loaded;
            this.Unloaded += this.PgStatusLog_Unloaded;

            // Action Button
            this.btAlarmFirst.Click += this.BtAlarmFirst_Click;
            this.btAlarmPrePage.Click += this.BtAlarmPrePage_Click;
            this.btAlarmPrevious.Click += this.BtAlarmPrevious_Click;
            this.btAlarmCurrent.Click += this.BtAlarmCurrent_Click;
            this.btAlarmNext.Click += this.BtAlarmNext_Click;
            this.btAlarmNextPage.Click += this.BtAlarmNextPage_Click;
            this.btAlarmLast.Click += this.BtAlarmLast_Click;

            this.btLogPrevious.Click += this.BtLogPrevious_Click;
            this.btLogNext.Click += this.BtLogNext_Click;
            this.btLogToday.Click += this.BtLogToday_Click;
            this.btLogPrePage.Click += this.BtLogPrePage_Click;
            this.btLogNextPage.Click += BtLogNextPage_Click;
            this.dtLogDate.SelectedDateChanged += DtLogDate_SelectedDateChanged;

            // Change UI
           
        }
       
        private void PgStatusLog_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_LOADED);

                this.alarmTotalPage = this.getTotalPageCount();
                this.alarmCurrerntPage = 0;
                this.loadEvents();

                this.logCurrentPage = 0;
                this.dtLogDate.SelectedDate = DateTime.Today;
                loadLogs();
            }
            catch (Exception ex)
            {
               
                logger.Create("Page Status Log Loaded Error: " + ex.Message, LogLevel.Error);
            }
        }
        private int getTotalPageCount()
        {
            var evCnt = DbRead.CountEvents();
            return (evCnt + ALARM_PAGE_SIZE - 1) / ALARM_PAGE_SIZE;
        }
        private void loadEvents()
        {
            this.btAlarmCurrent.Content = String.Format("{0}/{1}", alarmCurrerntPage + 1, alarmTotalPage);

            var events = DbRead.GetEvents(alarmCurrerntPage, ALARM_PAGE_SIZE);
            dgridAlarms.ItemsSource = events;

            dgridAlarms.Focus();
            dgridAlarms.SelectedIndex = 0;
        }
        private void loadLogs()
        {
            var dt = this.dtLogDate.SelectedDate.Value;
            var logCnt = DbRead.CountUserLogs(dt);
            logTotalPage = (logCnt + LOG_PAGE_SIZE - 1) / LOG_PAGE_SIZE;
            var userLogs = DbRead.GetUserLogs(dt, logCurrentPage, LOG_PAGE_SIZE);
            dgridLogs.ItemsSource = userLogs;

            this.btLogCurrent.Content = String.Format("{0}/{1}", logCurrentPage + 1, logTotalPage);

            dgridLogs.Focus();
            dgridLogs.SelectedIndex = 0;
        }
        private void PgStatusLog_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_UNLOAED);
            }
            catch (Exception ex)
            {
                
                logger.Create("Page Status Log Unloaded Error: " + ex.Message, LogLevel.Error);
            }
        }
        private void DtLogDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_CHANGE_DATE);
                logCurrentPage = 0;
                loadLogs();
            }
            catch (Exception ex)
            {
               
                logger.Create("DtDate_SelectedDateChanged error:" + ex.Message, LogLevel.Error);
            }
        }
        private void BtLogNextPage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
               UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_NEXT_PAGE);
                if (logCurrentPage < logTotalPage - 1)
                {
                    logCurrentPage++;
                }
                loadLogs();
            }
            catch (Exception ex)
            {
               
                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtLogPrePage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_PRE_PAGE);
                if (logCurrentPage > 0)
                {
                    logCurrentPage--;
                }
                loadLogs();
            }
            catch (Exception ex)
            {
               
                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtLogToday_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_TODAY);
                var dt = this.dtLogDate.SelectedDate.Value;
                this.dtLogDate.SelectedDate = DateTime.Today;
            }
            catch (Exception ex)
            {
               
                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtLogNext_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
               UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_EVENT_LOG);
                var dt = this.dtLogDate.SelectedDate.Value;
                this.dtLogDate.SelectedDate = dt.AddDays(1);
            }
            catch (Exception ex)
            {
               
                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
        }
        private void BtLogPrevious_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var btName = button.Name;
            try
            {
                UserManager.createUserLog(UserActions.PAGE_STATUS_LOG_BUTTON_EVENT_PREVIOUS);
                var dt = this.dtLogDate.SelectedDate.Value;
                this.dtLogDate.SelectedDate = dt.AddDays(-1);
            }
            catch (Exception ex)
            {
                
                logger.Create(("Action Button {0} Error: ", btName) + ex.Message, LogLevel.Error);
            }
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
